using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float maxHP;
    public float teleportHPLossPercent;
    public float speed;


    private float currentHP;

    private float rotationSpeed = 10f;

    private Transform[] waypoints;
    private int currentWaypointIndex;
    private Transform currentWaypoint;

    private Vector3 startDirection;
    private Vector3 direction;

    private GameResources resources;


    void Start()
    {
        currentWaypointIndex = 1;
        waypoints = Waypoints.points;
        currentWaypoint = waypoints[currentWaypointIndex];

        startDirection = waypoints[currentWaypointIndex].position - transform.position;

        currentHP = maxHP;

        resources = FindObjectOfType<GameResources>();
    }


    void Respawn()
    {
        currentWaypointIndex = 1;
        currentWaypoint = waypoints[currentWaypointIndex];

        var hpLoss = maxHP * teleportHPLossPercent / 100f;
        currentHP -= hpLoss;

        transform.position = waypoints[0].transform.position;
        transform.rotation = Quaternion.LookRotation(startDirection);
    }


    void Update()
    {
        if (currentHP <= 0)
        {
            Destroy(gameObject);
        }

        direction = waypoints[currentWaypointIndex].position - transform.position;
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, direction, rotationSpeed * Time.deltaTime, 0);
        transform.rotation = Quaternion.LookRotation(newDirection);

        transform.position = Vector3.MoveTowards(transform.position, currentWaypoint.position, speed * Time.deltaTime);

        if (transform.position == currentWaypoint.position)
        {
            currentWaypointIndex++;
            if(currentWaypointIndex >= waypoints.Length)
            {
                Respawn();
                resources.LostLive();
            }
            else
            {
                currentWaypoint = waypoints[currentWaypointIndex];
            }
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Bullet")
        {
            currentHP -= other.GetComponent<Bullet>().Damage;
            Destroy(other.gameObject);
        }
    }

}
