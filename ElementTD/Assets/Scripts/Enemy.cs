using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float Speed, RotationSpeed, HP;
    public Transform[] Points;

    private Transform currentPoint;
    private int index;
    private Vector3 direction;
    private GameResources res;
    // Start is called before the first frame update
    void Start()
    {
        index = 0;
        currentPoint = Points[index];
        res = FindObjectOfType<GameResources>();


    }

    // Update is called once per frame
    void Update()
    {
        direction = Points[index].position - transform.position;
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, direction, RotationSpeed * Time.deltaTime, 0);
        transform.rotation = Quaternion.LookRotation(newDirection);

        transform.position = Vector3.MoveTowards(transform.position, currentPoint.position, Speed * Time.deltaTime);

        if (transform.position == currentPoint.position)
        {
            index++;
            if(index >= Points.Length)
            {
                Destroy(gameObject);
                res.LostLive();
            }
            else
            {
                currentPoint = Points[index];
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
