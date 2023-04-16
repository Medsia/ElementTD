using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public float Radius, FireDelay, Damage;
    public Transform BulletPrefab;
    public LayerMask EnemyLayer;
    public int cost;

    private float timeToFire;
    private Transform gun, enemy, firePoint;
    // Start is called before the first frame update
    void Start()
    {
        timeToFire = FireDelay;
        gun = transform.GetChild(0);
        firePoint = gun.GetChild(0);
    }

    // Update is called once per frame
    void Update()
    {
        if (timeToFire > 0)
            timeToFire -= Time.deltaTime;
        else if (enemy)
            Fire();

        if(enemy)
        {
            Vector3 lookAt = enemy.position;
            lookAt.y = transform.position.y;
            gun.rotation = Quaternion.LookRotation(gun.position - lookAt);

            if (Vector3.Distance(gun.position, enemy.position) > Radius)
                enemy = null;
        }
        else
        {
            FindEnemy();
        }
    }   

    void Fire()
    {
        Transform bullet = Instantiate(BulletPrefab, firePoint.position, Quaternion.identity);
        bullet.LookAt(enemy);
        bullet.GetComponent<Bullet>().Damage = Damage;

        timeToFire = FireDelay;
    }

    void FindEnemy()
    {
        Collider[] colls = Physics.OverlapSphere(transform.position, Radius, EnemyLayer);

        if(colls.Length > 0)
        {
            enemy = colls[0].transform;
        }
    }
}
