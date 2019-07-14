﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bounds : MonoBehaviour
{
    //Make sure to assign this in the Inspector window
    public Transform m_NewTransform;
    public bool playerInBox = false;
    Collider m_Collider;
    Vector3 m_Point;

    // Start is called before the first frame update
    void Start()
    {
        //Fetch the Collider from the GameObject this script is attached to
        m_Collider = GetComponent<Collider>();
        //Assign the point to be that of the Transform you assign in the Inspector window
        m_Point = m_NewTransform.position;
    }

    // Update is called once per frame
    void Update()
    {
        m_Point = m_NewTransform.position;
        //If the first GameObject's Bounds contains the Transform's position, output a message in the console
        if (m_Collider.bounds.Contains(m_Point))
        {
            Debug.Log("Bounds contain the point : " + m_Point);
            playerInBox = true;
        }
        else
        {
            playerInBox = false;
        }
    }
}