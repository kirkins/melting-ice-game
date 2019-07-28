using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    private float startScore;
    private float endScore;

    Vector3 moveDirection = Vector3.left;

    void move()
    {
        Debug.Log(transform.position.y);
        if (transform.position.y >= 10)
        {
            moveDirection = Vector3.right;
        }
        else if(transform.position.y <= -50)
        {
            moveDirection = Vector3.left;
        }

        transform.Translate(moveDirection * Time.deltaTime * 15);
    }

    void Update() { move(); }
}