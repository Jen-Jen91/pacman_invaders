﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pacman : MonoBehaviour, ITouchWalls
{
    float speed = 10.00f;

    public GameObject pill;
    public Transform pillSpawn;
    public float fireRate;
    private float nextFire;

    [SerializeField] Sprite[] pacmanSprites;
    float animationTimout;
    bool touchingLeftWall = false;
    bool touchingRightWall = false;

    void Update()
    {
        var deltaX = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        if (BlockedByWall(deltaX)) deltaX = 0;
        transform.Translate(deltaX, 0, 0);

        if (Input.GetKeyDown(KeyCode.Space) && Time.time > nextFire)
        {
            GetComponent<SpriteRenderer>().sprite = pacmanSprites[1];
            nextFire = Time.time + fireRate;
            Instantiate(pill, pillSpawn.position, pillSpawn.rotation);
            animationTimout = Time.time + 0.15f;
        }

        if (Time.time > animationTimout)
        {
            GetComponent<SpriteRenderer>().sprite = pacmanSprites[0];
        }
    }

    private bool BlockedByWall(float deltaX)
    {
        var blockedByLeftWall = MovingLeft(deltaX) && touchingLeftWall;
        var blockedByRightWall = MovingRight(deltaX) && touchingRightWall;
        return blockedByLeftWall || blockedByRightWall;
    }

    private bool MovingLeft(float deltaX)
    {
        return deltaX < 0;
    }

    private bool MovingRight(float deltaX)
    {
        return deltaX > 0;
    }

    public void EnterWall(float xPos)
    {
        touchingLeftWall = xPos < SceneDimensions.centreX;
        touchingRightWall = xPos > SceneDimensions.centreX;
    }

    public void ExitWall()
    {
        touchingLeftWall = false;
        touchingRightWall = false;
    }
}
