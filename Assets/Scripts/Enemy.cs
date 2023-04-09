using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float Speed;
    public float RotationSpeed;
    public float HP;

    public Transform[] waypoints;
    private Transform currentPoint;

    private int index;

    private Vector3 direction;

    private GameResources res;


    void Start()
    {
        index = 0;

        waypoints = Waypoints.points;
        currentPoint = waypoints[index];

        res = FindObjectOfType<GameResources>();
    }


    void Update()
    {
        direction = waypoints[index].position - transform.position;
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, direction, RotationSpeed * Time.deltaTime, 0);
        transform.rotation = Quaternion.LookRotation(newDirection);

        transform.position = Vector3.MoveTowards(transform.position, currentPoint.position, Speed * Time.deltaTime);

        if (transform.position == currentPoint.position)
        {
            index++;
            if(index >= waypoints.Length)
            {
                Destroy(gameObject);
                res.LostLive();
            }
            else
            {
                currentPoint = waypoints[index];
            }
            
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Bullet")
        {
            HP -= other.GetComponent<Bullet>().Damage;
            Destroy(other.gameObject);

            if (HP <= 0)
            {
                Destroy(gameObject);

            }
        }
    }

}
