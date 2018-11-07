using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour {

    public Transform enemyPrefab;
    public Transform enemyPrefab2;
    public Transform spawnPoint;

    public float timeBetweenWaves = 5f;
    private float countdown = 2f;
    private int waveNumber = 0;


	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		if(countdown <= 0f)
        {
            StartCoroutine(SpawnWave());
            countdown = timeBetweenWaves;
        }

        countdown -= Time.deltaTime;
	}

    IEnumerator SpawnWave()
    {
        waveNumber++;

        for (int i = 0; i < waveNumber; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(0.2f);
            SpawnEnemy2();
            yield return new WaitForSeconds(0.25f);
        }

    }

    void SpawnEnemy()
    {
        Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
    }

    void SpawnEnemy2()
    {
        Instantiate(enemyPrefab2, spawnPoint.position, spawnPoint.rotation*Quaternion.Euler(90,0,0));
    }
}
