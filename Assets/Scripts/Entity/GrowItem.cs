using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowItem : MonoBehaviour
{
    private Rigidbody rigidBody;
    public GameObject iceBall;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    public void OnTriggerEnter(Collider collider)
    {
        print("t it");
        if (collider.gameObject.CompareTag("Entity"))
        {
            print("got it");
            // Call grow function below
            collider.gameObject.GetComponent<EntityController>().GrowFromItem();
        }
            
        Destroy(gameObject);
    }

}