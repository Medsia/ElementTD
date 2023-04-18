using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour
{
    private int currentWaveIndex = 0;

    public float timeBetweenWaves;
    private float timeToNextWave;

    private string countdownText = "Time left: ";
    public Text waveCountdownText;

    public int waveEnemyCount;
    private float enemySpawnOffset = 0.3f;

    private GameObject currentWaveEnemy;
    private GameObject nextWaveEnemy;

    private EnemyGenerator _enemyGenerator;


    void Start()
    {
        _enemyGenerator = this.gameObject.GetComponent<EnemyGenerator>();

        timeToNextWave = timeBetweenWaves;

        nextWaveEnemy = _enemyGenerator.GenerateEnemy(currentWaveIndex+1);
    }


    void Update()
    {
        if (timeToNextWave <= 0)
        {
            SpawnWave();
        }

        timeToNextWave -= Time.deltaTime;
        waveCountdownText.text = countdownText + Mathf.Ceil(timeToNextWave).ToString() + " s";
    }


    void SpawnWave()
    {
        currentWaveIndex++;
        timeToNextWave = timeBetweenWaves;
        currentWaveEnemy = nextWaveEnemy;

        StartCoroutine(EnemySpawner(currentWaveEnemy));

        nextWaveEnemy = _enemyGenerator.GenerateEnemy(currentWaveIndex + 1);
    }


    IEnumerator EnemySpawner(GameObject enemy)
    {
        for (int i = 0; i < waveEnemyCount; i++)
        {
            SpawnEnemy(enemy);
            yield return new WaitForSeconds(enemySpawnOffset);
        }

        Destroy(currentWaveEnemy);
    }


    void SpawnEnemy(GameObject enemy)
    {
        var spawnedEnemy = Instantiate(enemy, this.transform.position, this.transform.rotation);
        spawnedEnemy.SetActive(true);
    }
}
