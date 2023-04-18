using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    public GameObject enemy;

    private GameObject newEnemy;

    public GameObject[] humans;
    public GameObject[] machines;
    public GameObject[] undead;

    private static List<GameObject[]> enemies = new List<GameObject[]>();

    private System.Random rand = new System.Random();

    public float defaultEnemyHP;
    public float defaultTeleportHPLossPercent;
    public float defaultEnemySpeed;


    void Awake()
    {
        enemies.Add(humans);
        enemies.Add(machines);
        enemies.Add(undead);
    }


    public GameObject GenerateEnemy(int waveIndex)
    {
        newEnemy = Instantiate(enemy);
        newEnemy.SetActive(false);

        SetupScriptValues(waveIndex);
        SelectModel();

        return newEnemy;
    }


    void SetupScriptValues(int waveIndex)
    {
        float maxHp = defaultEnemyHP * waveIndex;
        float teleportHPLossPercent = defaultTeleportHPLossPercent;
        float speed = defaultEnemySpeed;

        newEnemy.GetComponent<Enemy>().maxHP = maxHp;
        newEnemy.GetComponent<Enemy>().teleportHPLossPercent = teleportHPLossPercent;
        newEnemy.GetComponent<Enemy>().speed = speed;
    }


    void SelectModel()
    {
        var enemyModelsOfType = enemies[rand.Next(0, enemies.Count)];
        var selectedModel = enemyModelsOfType[rand.Next(0, enemyModelsOfType.Count())];

        var enemyModel = Instantiate(selectedModel);

        enemyModel.transform.SetParent(newEnemy.transform);
        //enemyModel.transform.localPosition = new Vector3(0, 0.75f, 0);
    }
}
