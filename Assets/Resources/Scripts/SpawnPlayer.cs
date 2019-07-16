using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayer : MonoBehaviour
{

    public int playerCnt;
    GameObject playerObject;

    // Start is called before the first frame update
    void Start()
    {
        playerObject = Resources.Load("GameObjects/playerCube") as GameObject;
        Instantiate(playerObject);
    }

    // Update is called once per frame
    void Update()
    {
        if(GameObject.Find("PlayerCube(Clone)") == null)
        {
            Instantiate(playerObject);
        }
    }
}
