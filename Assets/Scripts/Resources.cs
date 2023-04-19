using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameResources : MonoBehaviour
{
    public int gold, income, lives;
    public Text goldText;
    public Text livesText;

    public void Start()
    {
        goldText.text = gold.ToString();
        livesText.text = lives.ToString();
    }
    public void Build(int towerCost)
    {
        gold -= towerCost;
        goldText.text = gold.ToString();
    }

    public void Gain()
    {
        gold += income;
        goldText.text = gold.ToString();
    }

    public void LostLive()
    {
        lives--;
        livesText.text = lives.ToString();
    }
}
