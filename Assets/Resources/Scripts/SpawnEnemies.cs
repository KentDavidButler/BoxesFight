using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemies : MonoBehaviour
{

    GameObject spawnableArea;
    GameObject enemyObject;

    // Start is called before the first frame update
    public void Start()
    {

        enemyObject = Resources.Load("GameObjects/EnemyCube") as GameObject;
        spawnableArea = GameObject.Find("BoundingBox");
        Vector3 rndPosWithin;

        for (int i = 0; i < 2 ; i++)
        {
            rndPosWithin = new Vector3(Random.Range(-10f, 9f), 0.5f, Random.Range(0f, 12f));
            //rndPosWithin = transform.TransformPoint(rndPosWithin * .5f);
            Instantiate(enemyObject, rndPosWithin, transform.rotation);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Spawn(int enemyCnt)
    {

        enemyObject = Resources.Load("GameObjects/EnemyCube") as GameObject;
        spawnableArea = GameObject.Find("BoundingBox");
        Vector3 rndPosWithin;

        for (int i = 0; i < enemyCnt; i++)
        {
            rndPosWithin = new Vector3(Random.Range(-10f, 9f), 0.5f, Random.Range(0f, 12f));
            //rndPosWithin = transform.TransformPoint(rndPosWithin * .5f);
            Instantiate(enemyObject, rndPosWithin, transform.rotation);
        }
    }
}
