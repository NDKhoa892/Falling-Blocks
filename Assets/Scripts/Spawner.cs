using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

    public GameObject fallingBlockPrefab;
    public Vector2 secondsBetweenSpawnsMinMax;
    float nextSpawnTime;

    public Vector2 spawnSizeMinMax;
    public float spawnAngleMax;

    Vector2 screenHalfSizeWolrdUnits;

    // Start is called before the first frame update
    void Start() {
        screenHalfSizeWolrdUnits = new Vector2(
            Camera.main.aspect * Camera.main.orthographicSize,
            Camera.main.orthographicSize
        );
    }

    // Update is called once per frame
    void Update() {
        if (Time.time > nextSpawnTime) {
            float secondsBetweenSpawns = Mathf.Lerp(
                secondsBetweenSpawnsMinMax.y,
                secondsBetweenSpawnsMinMax.x,
                Difficulty.getDifficultyPercent()
            );
            nextSpawnTime = Time.time + secondsBetweenSpawns;

            float spawnSize = Random.Range(spawnSizeMinMax.x, spawnSizeMinMax.y);
            float spawnAngle = Random.Range(-spawnAngleMax, spawnAngleMax);

            Vector2 spawnPosition = new Vector2(
                Random.Range(-screenHalfSizeWolrdUnits.x, screenHalfSizeWolrdUnits.x),
                screenHalfSizeWolrdUnits.y + spawnSize
            );

            GameObject newBlock = (GameObject)Instantiate(
                fallingBlockPrefab,
                spawnPosition,
                Quaternion.Euler(Vector3.forward * spawnAngle)
            );

            newBlock.transform.localScale = Vector2.one * spawnSize; 
        }
    }
}
