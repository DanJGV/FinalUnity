using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TPS_Cam : MonoBehaviour
{
    public float speed = 1;
    public Transform Target;
    public Camera cam;
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Move();
    }

    public void Move()
    {
        Vector3 direction = (Target.position - cam.transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);

        lookRotation.x = transform.rotation.x;
        lookRotation.z = transform.rotation.z;

        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 100);

        transform.position = Vector3.Slerp(transform.position, Target.position, Time.deltaTime * speed);
    }
        
    
}
