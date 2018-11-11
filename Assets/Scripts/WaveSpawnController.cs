using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawnController : MonoBehaviour
{

    public enum SpawnState { Spawning, Waiting, Counting };

    public static WaveSpawnController spawnController;

    [System.Serializable]
    public class Waves
    {
        public string name;
        public Transform enemy;
        public int count;
        public float rate;
    }

    public Transform spawnPoint;
    public Waves[] waves;
    private int nextWave = 0;

    public float timeBetweenWaves = 5f;
    public float waveCountDown = 0f;

    private float searchCountdown = 1f;

    private SpawnState state = SpawnState.Counting;

    private void Start()
    {
        waveCountDown = timeBetweenWaves;
        spawnController = this;
    }

    private void Update()
    {
        if (state == SpawnState.Waiting)
        {
            if (!EnemyIsAlive())
            {
                WaveCompleted();
            }
            else
            {
                return;
            }
        }

        if (waveCountDown <= 0)
        {
            if (state != SpawnState.Spawning)
            {
                StartCoroutine(SpawnWave(waves[nextWave]));
            }
        }
        else
        {
            waveCountDown -= Time.deltaTime;
        }
    }

    IEnumerator SpawnWave(Waves _wave)
    {
        state = SpawnState.Spawning;

        for (int i = 0; i < _wave.count; i++)
        {
            SpawnEnemy(_wave.enemy);
            yield return new WaitForSeconds(1f / _wave.rate);
        }

        state = SpawnState.Waiting;


        yield break;
    }

    void SpawnEnemy(Transform _enemy)
    {
        Enemy __enemy = _enemy.GetComponent<Enemy>();
        if(__enemy.enemyName == "Fast" || __enemy.enemyName == "Heavy")
        {
            Instantiate(_enemy, spawnPoint.position, spawnPoint.rotation*Quaternion.Euler(90,0,0));
        }
        else
        {
            Instantiate(_enemy, spawnPoint.position, spawnPoint.rotation);
        }
    }

    void WaveCompleted()
    {
        state = SpawnState.Counting;
        waveCountDown = timeBetweenWaves;

        if(nextWave + 1 > waves.Length - 1)
        {
            nextWave = 0;
        }
        else
        {
            nextWave++;
        }
    }

    bool EnemyIsAlive()
    {
        searchCountdown -= Time.deltaTime;
        if(searchCountdown <= 0)
        {
            searchCountdown = 1f;
            if (GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
            {
                return false;
            }
        }
        return true;
    }

    public int GetCurrentWave()
    {
        return (nextWave + 1);
    }

}
