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

    public int waveMobCount;
    private float mobSpawnOffset = 0.3f;

    public Transform exampleMob;


    void Start()
    {
        timeToNextWave = timeBetweenWaves;
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

        StartCoroutine(MobSpawner(exampleMob));
    }


    IEnumerator MobSpawner(Transform mob)
    {
        for (int i = 0; i < waveMobCount; i++)
        {
            SpawnMob(mob);
            yield return new WaitForSeconds(mobSpawnOffset);
        }
    }


    void SpawnMob(Transform mob)
    {
        Instantiate(mob, this.transform.position, this.transform.rotation);
    }
}
