using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ceil : MonoBehaviour
{
    public static Transform TowerPrefab;
    public Material CanMaterial, CantMaterial, MainMaterial;
    public bool CanBuild;
    public static event Action TowerToBuildChanged;

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
        if (TowerPrefab)
        {
            if (CanBuild)
            {
                _renderer.material = CanMaterial;
            }
            else
                _renderer.material = CantMaterial;
        }
    }

    private void OnMouseExit()
    {
        _renderer.material = MainMaterial;
    }

    private void BuildTower()
    {
        if (TowerPrefab && CanBuild && res.gold >= tower.cost)
        {
            Instantiate(TowerPrefab, transform.position, Quaternion.identity);
            TowerPrefab = null;
            CanBuild = false;
            res.Build(tower.cost);
            _renderer.material = MainMaterial;
            TowerToBuildChanged?.Invoke();
        }
    }

    public static void ChangeTowerToBuild(Transform towerPrefab)
    {
        TowerPrefab = towerPrefab;
        tower = TowerPrefab.GetComponent<Tower>();
        TowerToBuildChanged?.Invoke();
    }
}

