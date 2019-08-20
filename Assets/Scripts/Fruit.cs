﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : MonoBehaviour
{

    private Rigidbody2D fruit;
    private Level level;
    private Barricade[] barricades;
    float speed = 4.00f;

    
    void Start()
    {
        fruit = GetComponent<Rigidbody2D>();
        level = FindObjectOfType<Level>();
        barricades = FindObjectsOfType<Barricade>();
    }

    
    void Update()
    {
        fruit.velocity = new Vector2(0, -speed);

        if (fruit.position.y <= -1.0)
        {
            Destroy(gameObject);
        }
    }

    private void RestoreBarricades()
    {
        foreach (Barricade barricade in barricades)
        {
            barricade.Restore();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag == "Pacman")
        {
            switch (gameObject.tag)
            {
                case "Cherry":
                    Debug.Log("CHERRY");
                    level.UpdateScoreCherry();
                    break;
                case "Strawberry":
                    Debug.Log("STRAWBERRY");
                    RestoreBarricades();
                    break;
                case "Peach":
                    Debug.Log("PEACH");
                    break;
                case "Apple":
                    Debug.Log("APPLE");
                    break;
                default:
                    break;
            }

            Destroy(gameObject);
            collision.gameObject.GetComponent<Pacman>().EatFruit();
            level.UpdateScoreFruit();
        }
        
        
    }
}
