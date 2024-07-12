using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrierLogic : MonoBehaviour
{

    private float speed = 2f;
    private Vector3 direction = Vector3.right;

    private void FixedUpdate()
    {
        transform.Translate((direction * speed) * Time.deltaTime);
    }
    

    void OnCollisionEnter(Collision collision)
    {
        
        if (collision.gameObject.tag == "Wall")
        {

            direction = -direction;
        }
    }


}
