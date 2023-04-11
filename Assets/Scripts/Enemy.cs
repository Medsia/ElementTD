using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    private float rotationSpeed = 5f;

    public float maxHP;
    [SerializeField]
    private float currentHP;
    public float teleportHPLossPercent;

    public Transform[] waypoints;
    private Transform currentPoint;

    [SerializeField]
    private int index;

    private Vector3 startDirection;
    private Vector3 direction;

    private GameResources res;


    void Start()
    {
        index = 1;

        waypoints = Waypoints.points;
        currentPoint = waypoints[index];

        startDirection = waypoints[index].position - transform.position;

        currentHP = maxHP;

        res = FindObjectOfType<GameResources>();
    }


    void Respawn()
    {
        index = 1;
        currentPoint = waypoints[index];

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

        direction = waypoints[index].position - transform.position;
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, direction, rotationSpeed * Time.deltaTime, 0);
        transform.rotation = Quaternion.LookRotation(newDirection);

        transform.position = Vector3.MoveTowards(transform.position, currentPoint.position, speed * Time.deltaTime);

        if (transform.position == currentPoint.position)
        {
            index++;
            if(index >= waypoints.Length)
            {
                Respawn();
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
            currentHP -= other.GetComponent<Bullet>().Damage;
        }
    }

}
