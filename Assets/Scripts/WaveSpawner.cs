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

    private int startMobCount = 0;
    public int mobCountIncrement;
    private int currentWaveMobCount = 0;
    private float mobSpawnOffset = 0.5f;

    private List<Transform> mobsToSpawn = new List<Transform>();

    public Transform exampleMob;


    void Start()
    {
        timeToNextWave = timeBetweenWaves;
        currentWaveMobCount = startMobCount;

        StartCoroutine(MobSpawner());
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
        currentWaveMobCount += mobCountIncrement;

        for (int i = 0; i < currentWaveMobCount; i++)
        {
            mobsToSpawn.Add(exampleMob);
        }
    }


    IEnumerator MobSpawner()
    {
        while (true)
        {
            if(mobsToSpawn.Count > 0)
            {
                SpawnMob(mobsToSpawn.ElementAt(0));
                mobsToSpawn.RemoveAt(0);
            }

            yield return new WaitForSeconds(mobSpawnOffset);
        }
    }


    void SpawnMob(Transform mob)
    {
        Instantiate(mob, this.transform.position, this.transform.rotation);
    }
}
