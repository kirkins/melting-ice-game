using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowKeys : MonoBehaviour
{
    private Rigidbody rigidBody;
    bool grounded = true;

    void Update()
    {
        rigidBody = GetComponent<Rigidbody>();

        if (Input.GetKey("left"))
        {
            rigidBody.AddForce(Vector2.left * 5);
        }

        if (Input.GetKey("right"))
        {
            rigidBody.AddForce(Vector2.right * 5);
        }
        if (Input.GetKey("up"))
        {
            Jump();
        }
        if (Input.GetKey("space"))
        {
            gameObject.GetComponent<EntityController>().ShrinkEntitySize();
        }

    }

    void Landed() { grounded = true; }

    void Jump()
    {
        if (grounded)
        {
            rigidBody.velocity = new Vector3(0f, 5, 0f);
            grounded = false;
            Invoke("Landed", 3);
        }
    }


}