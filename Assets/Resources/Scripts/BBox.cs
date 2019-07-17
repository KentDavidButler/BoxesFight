using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BBox : MonoBehaviour
{
    public bool playerInBox = false;

    Transform m_NewTransform;
    Collider m_Collider;
    Vector3 m_Point;

    // Start is called before the first frame update
    void Start()
    {
        if (GameObject.Find("PlayerCube(Clone)") == null)
        {
            m_NewTransform = new GameObject().transform;
            //Assign the point to be that of the Transform you assign in the Inspector window
            m_Point = m_NewTransform.position;
        }
        else
        {
            m_NewTransform = GameObject.Find("PlayerCube(Clone)").transform;
            //Assign the point to be that of the Transform you assign in the Inspector window
            m_Point = m_NewTransform.position;
        }
        //Fetch the Collider from the GameObject this script is attached to
        m_Collider = GetComponent<Collider>();

    }

    // Update is called once per frame
    void Update()
    {
        //Fetch the Collider from the GameObject this script is attached to
        m_Collider = GetComponent<Collider>();

        try
        {
            m_Point = m_NewTransform.position;
            m_NewTransform = GameObject.Find("PlayerCube(Clone)").transform;
        }
        catch (System.Exception)
        {
            //exception thrown when player is deleted and have to reset it
            m_NewTransform = GameObject.Find("PlayerCube(Clone)").transform;
        }

        //If the first GameObject's Bounds contains the Transform's position, output a message in the console
        if (m_Collider.bounds.Contains(m_Point))
        {
            playerInBox = true;
        }
        else
        {
            playerInBox = false;
        }

    }
}
