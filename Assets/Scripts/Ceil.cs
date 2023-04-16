using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ceil : MonoBehaviour
{
    public Transform TowerPrefab;
    public Material CanMaterial, CantMaterial, MainMaterial;
    public bool CanBuild;

    private Renderer _renderer;
    private GameResources res;
    private Tower tower;

    // Start is called before the first frame update
    void Start()
    {
        _renderer = GetComponent<Renderer>();
        res = FindObjectOfType<GameResources>();
        tower = TowerPrefab.GetComponent<Tower>();
    }

   private void OnMouseUp()
    {
        if(CanBuild && res.gold >= tower.cost)
        {
            Instantiate(TowerPrefab, transform.position, Quaternion.identity);
            CanBuild = false;
            res.Build(tower.cost);
        }
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
}

