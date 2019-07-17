using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    float health = 100.0f;
    Rigidbody m_Rigidbody;
    float m_Speed;
    float m_Turn;
    GameObject remains;
    GameObject score;
    private IEnumerator coroutine_Restart;

    // Start is called before the first frame update
    void Start()
    {
        score = GameObject.Find("GoalBox");
        remains = Resources.Load("GameObjects/PlayerRemains") as GameObject;
        //Fetch the Rigidbody component you attach from your GameObject
        m_Rigidbody = GetComponent<Rigidbody>();
        //Set the speed of the GameObject
        m_Speed = 5.0f;
        m_Turn = 100.0f;
        coroutine_Restart = Restart(1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Euler(0, transform.eulerAngles.y, 0);

        if (Input.GetKey(KeyCode.UpArrow))
        {
            //Move the Rigidbody forwards constantly at speed you define (the blue arrow axis in Scene view)
            m_Rigidbody.velocity = transform.forward * m_Speed;
            transform.rotation = Quaternion.Euler(0, transform.eulerAngles.y, 0);
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            //Move the Rigidbody backwards constantly at the speed you define (the blue arrow axis in Scene view)
            m_Rigidbody.velocity = -transform.forward * m_Speed;
            transform.rotation = Quaternion.Euler(0, transform.eulerAngles.y, 0);
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            //Rotate the sprite about the Y axis in the positive direction
            transform.Rotate(new Vector3(0, 1, 0) * Time.deltaTime * m_Turn, Space.World);
            transform.rotation = Quaternion.Euler(0, transform.eulerAngles.y, 0);
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            //Rotate the sprite about the Y axis in the negative direction
            transform.Rotate(new Vector3(0, -1, 0) * Time.deltaTime * m_Turn, Space.World);
            transform.rotation = Quaternion.Euler(0, transform.eulerAngles.y, 0);
        }

        if (Input.GetKey(KeyCode.Escape))
        {
            StartCoroutine(coroutine_Restart);
        }

        if(health <= 0.0f)
        {
            //subtract score on death
            score.GetComponent<GBox>().MinusScore();
            //substitue player for 'dead player'
            Instantiate(remains, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }

    public float GetHealth()
    {
        return health;
    }

    public void Damage(float amount)
    {
        health -= amount;
    }

    private IEnumerator Restart(float waitTime)
    {
        score.GetComponent<GBox>().SetHighScore();
        PlayerPrefs.SetFloat("High Score", score.GetComponent<GBox>().GetHighScore());
        SceneManager.LoadScene("SampleScene");
        yield return new WaitForSeconds(waitTime);
        coroutine_Restart = Restart(1.0f);
    }
}
