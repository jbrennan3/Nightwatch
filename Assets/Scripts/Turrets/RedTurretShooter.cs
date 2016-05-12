﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RedTurretShooter : MonoBehaviour {


    private LineRenderer lr;
    private Queue<GameObject> enemyQueue;

    private float timer;

    void Start()
    {
        lr = GetComponent<LineRenderer>();
        enemyQueue = new Queue<GameObject>();

        lr.SetVertexCount(2);
        lr.enabled = false;
    }
	
	void Update () {

        if(enemyQueue.Count != 0)
        {
            if(enemyQueue.Peek() == null)
            {
                enemyQueue.Dequeue();
            }
            if (enemyQueue.Count != 0)
            {
                ShootLightning();
            }
        }
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            enemyQueue.Enqueue(other.gameObject);
        }

    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            enemyQueue.Dequeue();
            other.GetComponent<EnemyHealth>().isZapped = false;
        }
    }

    void ShootLightning()
    {
        lr.enabled = true;
        GameObject enemy = enemyQueue.Peek();
        if (enemy != null)
        {
            enemy.GetComponent<EnemyHealth>().isZapped = true;

            lr.SetPosition(0, this.transform.position);
            lr.SetPosition(1, enemy.transform.position);
        }
        else
        {
            lr.enabled = false;
        }

        
    }
}