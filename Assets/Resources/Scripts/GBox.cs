using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GBox : MonoBehaviour
{
    public float score;
    public float highScore;
    public Text DisplayScore;
    int mulitplyer;

    //player info
    GameObject player;
    Transform player_Transform;
    GameObject[] allPlayerObjs;
    Vector3 m_Point;

    //enemy
    GameObject[] allEnemiesObjs;
    int enemyCnt;

    Collider gBoxCollider;
    bool playerInBox = false;
    GameObject level;

    //set up delay of running functions
    private IEnumerator coroutine_DestroyEverything;
    private IEnumerator coroutine_SpawnEnemies;

    void Start()
    {
        score = 0;
        highScore = PlayerPrefs.GetFloat("High Score");
        mulitplyer = 1;
        enemyCnt = 2;
        player = GameObject.Find("PlayerCube(Clone)");
        coroutine_DestroyEverything = destroyPlayer(0.5f);
        coroutine_SpawnEnemies = CreateEnemies(0.1f);
        SetScoreToScoreBoard();
    }

    // Update is called once per frame
    void Update()
    {
        gBoxCollider = GetComponent<Collider>();
        try
        {
            m_Point = player_Transform.position;
        }
        catch (System.Exception)
        {
            //exception thrown when player is deleted and have to reset it
            player_Transform = GameObject.Find("PlayerCube(Clone)").transform;
            player = GameObject.Find("PlayerCube(Clone)");
        }

        playerInBox = gBoxCollider.bounds.Contains(m_Point);
        //If the first GameObject's Bounds contains the Transform's position, do something
        if (playerInBox)
        {
            allPlayerObjs = GameObject.FindGameObjectsWithTag("Player");
            allEnemiesObjs = GameObject.FindGameObjectsWithTag("Enemey");
            StartCoroutine(coroutine_DestroyEverything);
            playerInBox = false;
            gBoxCollider = GetComponent<Collider>();      
        }

        if (GameObject.FindGameObjectsWithTag("Enemey").Length <= 0)
        {
            StartCoroutine(coroutine_SpawnEnemies);
        }

    }

    private IEnumerator destroyPlayer(float waitTime)
    {
        //calculate Score
        score += player.GetComponent<Player>().GetHealth() * mulitplyer;
        SetScoreToScoreBoard();
        //increase enemies and score multiplyer
        enemyCnt++;
        mulitplyer++;
        foreach (var playerObj in allPlayerObjs)
        {
            Destroy(playerObj);
        }
        foreach (var enemyObj in allEnemiesObjs)
        {
            Destroy(enemyObj);
        }
        yield return new WaitForSeconds(waitTime);
        coroutine_DestroyEverything = destroyPlayer(0.5f);
    }

    private IEnumerator CreateEnemies(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        level = GameObject.Find("Level");
        level.GetComponent<SpawnEnemies>().Spawn(enemyCnt);
        coroutine_SpawnEnemies = CreateEnemies(0.1f);
    }

    void SetScoreToScoreBoard()
    {
        DisplayScore.text = $"HighScore: {highScore.ToString()} {Environment.NewLine}Score: {score.ToString()} ";
    }

    public void MinusScore()
    {
        score = score - 100;
        if (score <= 0)
            score = 0;
        SetScoreToScoreBoard();
    }

    public float GetHighScore()
    {
        return highScore;
    }

    public void SetHighScore()
    {
        if(score >= highScore)
        {
            highScore = score;
        }
    }
}
