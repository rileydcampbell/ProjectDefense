using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawnController : MonoBehaviour
{

    public enum SpawnState { Spawning, Waiting, ReadyToStart };

    public static WaveSpawnController spawnController;

    [System.Serializable]
    public class Waves
    {
        public string name;
        public Transform enemy;
        public int count;
        public float rate;
        public Transform enemy2;
        public int count2;
        public float rate2;
    }

    public Transform spawnPoint;
    public Waves[] waves;
    private int nextWave = 0;

    public GameObject waveTextController;
    public GameObject waveMessageController;

    private float searchCountdown = 1f;

    private SpawnState state = SpawnState.Waiting;

    private void Start()
    {
        spawnController = this;
    }

    private void Update()
    {
        if (state == SpawnState.ReadyToStart)
        {
            if (state != SpawnState.Spawning)
            {
                StartCoroutine(SpawnWave(waves[nextWave]));
            }
        }
    }

    IEnumerator SpawnWave(Waves _wave)
    {
        state = SpawnState.Spawning;
        waveTextController.GetComponent<WaveScript>().UpdateWaveState(true);
        for (int i = 0; i < _wave.count; i++)
        {
            SpawnEnemy(_wave.enemy);
            yield return new WaitForSeconds(1f / _wave.rate);
        }

        for (int i = 0; i < _wave.count2; i++)
        {
            SpawnEnemy(_wave.enemy2);
            yield return new WaitForSeconds(1f / _wave.rate2);
        }

        WaveCompleted();

        waveTextController.GetComponent<WaveScript>().UpdateWaveState(false);
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
        state = SpawnState.Waiting;

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

    public void StartWave()
    {
        if(state == SpawnState.Waiting)
        {
            state = SpawnState.ReadyToStart;
        }
        
    }

    public bool IsWaveActive()
    {
        if(state == SpawnState.Spawning)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

}
