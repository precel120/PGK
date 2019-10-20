﻿using UnityEngine;
using System.Collections;

public class FoeMov : MonoBehaviour
{

    public Transform[] points;
    private float moveSpeed;
    public float speed;
    private int currentPoint;
    private int health;
    public int Health { get { return health; } set { health = value; } }
    // Use this for initialization
    void Start()
    {
        transform.position = points[0].position;
        currentPoint = 0;
        transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
        health = 60;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(Health);
        if (transform.position == points[currentPoint].position)
        {
            transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
            currentPoint++;
        }
        if (currentPoint >= points.Length)
        {
            currentPoint = 0;
        }
        transform.position = Vector2.MoveTowards(transform.position, points[currentPoint].position, moveSpeed = Time.deltaTime*speed);
        if (Health <= 0)
        {
            Destroy(gameObject);
        }
    }
    public void takeDamage(int damage)
    {
        health -= damage;
    }
}