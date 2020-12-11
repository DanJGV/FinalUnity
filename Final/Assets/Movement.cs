using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Movement : MonoBehaviour
{
    // Start is called before the first frame update

    public List<Transform> BodyParts = new List<Transform>();
    public float minDistance = 0.25f;
    public float speed = 1;
    public float rotSpeed = 50;
    public int beginSize;
    public GameObject body;
    private float distance;
    public float timeFromLastRetry;
    private Transform currentBodyPart;
    private Transform prevBodyPart;
    public Text currentScore;
    public Text score;
    public GameObject deathScreen;
    public bool isAlive;

    void Start()
    {
        StartLevel();
    }

    public void StartLevel()
    {
        timeFromLastRetry = Time.time;
        deathScreen.SetActive(false);

        for(int i = BodyParts.Count - 1; i > 1; i--)
        {
            Destroy(BodyParts[i].gameObject);
            BodyParts.Remove(BodyParts[i]);
        }

        BodyParts[0].position = new Vector3(0, 0f, 0);
        BodyParts[0].rotation = Quaternion.identity;

        currentScore.gameObject.SetActive(true);
        currentScore.text = "Score: 0";

        isAlive = true;

        for (int i = 0; i < beginSize - 1; i++)
        {
            AddBodyPart();
        }

        BodyParts[0].position = new Vector3(2, 0f, 0);
    }
    // Update is called once per frame
    void Update()
    {
        if (isAlive)
        {
            Move();
        }

        if (Input.GetKey(KeyCode.Q))
        {
            AddBodyPart();
        }


    }

    public void Move()
    {
        float currentSpeed = speed;

        if(Input.GetKey(KeyCode.W))
        {
           
            currentSpeed *= 2;
        }

        BodyParts[0].Translate(BodyParts[0].forward * currentSpeed * Time.smoothDeltaTime, Space.World);

        if(Input.GetAxis("Horizontal") != 0)
        {
            BodyParts[0].Rotate(Vector3.up * rotSpeed * Time.deltaTime * Input.GetAxis("Horizontal"));
        }

        for (int i = 1; i < BodyParts.Count; i++)
        {
            currentBodyPart = BodyParts[i];
            prevBodyPart = BodyParts[i - 1];

            distance = Vector3.Distance(prevBodyPart.position, currentBodyPart.position);

            Vector3 newPos = prevBodyPart.position;
            newPos.y = BodyParts[0].position.y;

            float T = Time.deltaTime * distance / minDistance * currentSpeed;

            if( T > 0.5f)
            {
                T = 0.5f;
            }
            currentBodyPart.position = Vector3.Slerp(currentBodyPart.position, newPos, T);
            currentBodyPart.rotation = Quaternion.Slerp(currentBodyPart.rotation, prevBodyPart.rotation, T);

        }
    }
    public void AddBodyPart()
    {
        Transform newPart = (Instantiate(body, BodyParts[BodyParts.Count - 1].position, BodyParts[BodyParts.Count - 1].rotation) as GameObject).transform;
        newPart.SetParent(transform);
        BodyParts.Add(newPart);

        currentScore.text = "Score: " + (BodyParts.Count - beginSize).ToString();
    
    }

    public void Death()
    {
        isAlive = false;

        //score.text = "Final Score: " + (BodyParts.Count - beginSize).ToString();

        deathScreen.gameObject.SetActive(true);
    }

   public void QuitGame()
    {
        Application.Quit();
    }
}
