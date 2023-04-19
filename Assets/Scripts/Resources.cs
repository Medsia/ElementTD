using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameResources : MonoBehaviour
{
    public int gold, income, lives;
    public Text goldText;
    public Text livesText;
    public GameObject deathPanel;
    public Button speedBtn;

    public void Start()
    {
        deathPanel.SetActive(false);
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
        if (lives <= 0)
        {
            deathPanel.SetActive(true);
        }
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ChangeGameSpeed()
    {
        if (Time.timeScale == 0.5)
        {
            Time.timeScale = 1;
        }
        else if (Time.timeScale == 1)
        {
            Time.timeScale = 2;
        }
        else if (Time.timeScale == 2)
        {
            Time.timeScale = 0.5f;
        }
        speedBtn.GetComponentInChildren<TextMeshProUGUI>().text = Time.timeScale + "x";
    }
}
