using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameResources : MonoBehaviour
{
    public int Gold, TowerCost, Income, Lives;
  
    public void Build()
    {
        Gold -= TowerCost;
    }

    public void Gain()
    {
        Gold += Income;
    }

    public void LostLive()
    {

    }
}
