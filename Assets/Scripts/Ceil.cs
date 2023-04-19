using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ceil : MonoBehaviour
{
    public static Transform TowerPrefab;
    public Material CanMaterial, CantMaterial, MainMaterial;
    public bool CanBuild;

    private Renderer _renderer;
    private GameResources res;
    private static Tower tower;

    // Start is called before the first frame update
    void Start()
    {
        _renderer = GetComponent<Renderer>();
        res = FindObjectOfType<GameResources>();
    }

   private void OnMouseUp()
    {
        BuildTower();
    }

    private void OnMouseOver()
    {
        if(CanBuild)
        {
            _renderer.material = CanMaterial; 
        }
        else
            _renderer.material = CantMaterial;
    }

    private void OnMouseExit()
    {
        _renderer.material = MainMaterial;
    }
    
    private void BuildTower()
    {
        if (TowerPrefab != null && CanBuild && res.gold >= tower.cost)
        {
            Instantiate(TowerPrefab, transform.position, Quaternion.identity);
            CanBuild = false;
            res.Build(tower.cost);
        }
    }

    public static void ChangeTowerToBuild(Transform towerPrefab)
    {
        TowerPrefab = towerPrefab;
        tower = TowerPrefab.GetComponent<Tower>();
    }
}

