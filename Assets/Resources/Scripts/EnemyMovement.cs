﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    //setup enemy
    Rigidbody m_Rigidbody;
    public float m_Speed;
    public float m_Turn;

    //pull public variable from BBox script within BoundingBox Object
    GameObject boundingBox;
    bool canMove;

    //Setup Player info
    Vector3 playerLocation;
    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("PlayerCube");

        m_Speed = Random.Range(4.0f, 5.5f);
        m_Turn = Random.Range(1.0f, 8.0f);

        //Fetch the Rigidbody component you attach from your GameObject
        m_Rigidbody = GetComponent<Rigidbody>();
        //m_Speed = 4.0f;

        //pulling variable from script connected to boundingBox game object
        boundingBox = GameObject.Find("BoundingBox");
        canMove = boundingBox.GetComponent<BBox>().playerInBox;
    }

    // Update is called once per frame
    void Update()
    {
        playerLocation = player.transform.position;
        //check if player is within box or not
        canMove = boundingBox.GetComponent<BBox>().playerInBox;

        if (canMove)
        {
            //rotate enemy towards player
            var rotation = Quaternion.LookRotation(playerLocation - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * m_Turn);

            //check if player is infront of enemy if so move towards player
            float angel = Vector3.Angle(transform.forward, playerLocation - transform.position);
            if (Mathf.Abs(angel) < 60)
            {
                transform.position += transform.forward * m_Speed * Time.deltaTime;
            }
            
        }

    }
}