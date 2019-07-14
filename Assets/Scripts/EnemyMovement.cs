using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{

    Rigidbody m_Rigidbody;
    float m_Speed;
    //float m_Turn;
    bool canMove = false;
    Vector3 playerLocation;
    public GameObject player;
    GameObject boundingBox; 

    // Start is called before the first frame update
    void Start()
    {
        //Fetch the Rigidbody component you attach from your GameObject
        m_Rigidbody = GetComponent<Rigidbody>();
        //Set the speed of the GameObject
        m_Speed = 4.0f;
        //m_Turn = 100.0f;

        boundingBox = GameObject.Find("BoundingBox");
        canMove = boundingBox.GetComponent<BBox>().playerInBox;

    }

    // Update is called once per frame
    void Update()
    {
        playerLocation = player.transform.position;
        transform.rotation = Quaternion.Euler(0, transform.eulerAngles.y, 0);
        canMove = boundingBox.GetComponent<BBox>().playerInBox;

        Debug.Log(canMove);
        if (canMove)
        {

            transform.LookAt(player.transform);
            transform.position += transform.forward * m_Speed * Time.deltaTime;
           
        }

    }
}
