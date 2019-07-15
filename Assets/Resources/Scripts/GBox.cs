using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GBox : MonoBehaviour
{
    public bool playerInBox = false;

    Transform player;
    Collider gBoxCollider;
    Vector3 m_Point;
    GameObject[] allPlayerObjs;
    GameObject[] allEnemiesObjs;
    GameObject level;
    private IEnumerator coroutine;
    int enemyCnt;

    void Start()
    {
        enemyCnt = 2;
        coroutine = destroyPlayer(0.5f);
        player = GameObject.Find("PlayerCube(Clone)").transform;
        //Fetch the Collider from the GameObject this script is attached to
        gBoxCollider = GetComponent<Collider>();
        //Assign the point to be that of the Transform you assign in the Inspector window
        level = GameObject.Find("Level");
    }

    // Update is called once per frame
    void Update()
    {
        gBoxCollider = GetComponent<Collider>();
        try
        {
            m_Point = player.position;
        }
        catch (System.Exception)
        {
            //exception thrown when player is deleted and have to reset it
            player = GameObject.Find("PlayerCube(Clone)").transform;

        }

        playerInBox = gBoxCollider.bounds.Contains(m_Point);
        //If the first GameObject's Bounds contains the Transform's position, do something
        if (playerInBox)
        {
            allPlayerObjs = GameObject.FindGameObjectsWithTag("Player");
            allEnemiesObjs = GameObject.FindGameObjectsWithTag("Enemey");
            StartCoroutine(coroutine);
            playerInBox = false;
            gBoxCollider = GetComponent<Collider>();
            enemyCnt++;
            level.GetComponent<SpawnEnemies>().Spawn(enemyCnt);
        }
        

    }

    private IEnumerator destroyPlayer(float waitTime)
    {
        foreach (var playerObj in allPlayerObjs)
        {
            Destroy(playerObj);
            Debug.Log(playerObj.name);
        }
        foreach (var enemyObj in allEnemiesObjs)
        {
            Destroy(enemyObj);
            Debug.Log(enemyObj.name);
        }
        yield return new WaitForSeconds(waitTime);
        coroutine = destroyPlayer(0.5f);
    }
}
