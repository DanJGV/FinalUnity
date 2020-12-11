using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Head : MonoBehaviour
{
    public Movement movement;
    public SpawnObject spawn;
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Food")
        {
            movement.AddBodyPart();
            Destroy(collision.gameObject);

            spawn.SpawnFood();
        }

        else
        {
            if(collision.transform != movement.BodyParts[1] && movement.isAlive)
            {
                if(Time.time - movement.timeFromLastRetry > 5)
                {
                    movement.Death();
                }
            }
        }
    }
}
