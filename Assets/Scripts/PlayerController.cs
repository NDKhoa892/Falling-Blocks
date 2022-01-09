using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController: MonoBehaviour {

    public float speed = 7;
    public event System.Action OnPlayerDeath;

    float screenHalfWidthInWorldUnit;

    // Start is called before the first frame update
    void Start() {
        float playerHalfWidth = transform.localScale.x / 2f;
        screenHalfWidthInWorldUnit = Camera.main.aspect * Camera.main.orthographicSize + playerHalfWidth;
    }

    // Update is called once per frame
    void Update() {
        float inputX = Input.GetAxisRaw("Horizontal");
        float velocity = inputX * speed;

        transform.Translate(Vector2.right * velocity * Time.deltaTime);

        if (transform.position.x < -screenHalfWidthInWorldUnit)
            transform.position = new Vector2(screenHalfWidthInWorldUnit, transform.position.y);
        else if (transform.position.x > screenHalfWidthInWorldUnit)
            transform.position = new Vector2(-screenHalfWidthInWorldUnit, transform.position.y);
    }

    private void OnTriggerEnter2D(Collider2D triggerCollider) {
        if (triggerCollider.tag == "Falling Block") {
            if (OnPlayerDeath != null) {
                OnPlayerDeath();
            }

            Destroy(gameObject);
        }
    }
}
