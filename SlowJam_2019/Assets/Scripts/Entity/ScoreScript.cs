using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreScript : MonoBehaviour
{
    private Rigidbody rigidBody;
    private int startScore;
    private int endScore;
    Collider m_Collider;

    int GetScore(Vector3 v)
    {
        return (int)(v[0] * v[1] * v[2] * 1000);
    }

    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        InvokeRepeating("LevelDone", 1f, 1f);
        startScore = GetScore(transform.localScale);
    }

    public void LevelDone()
    {
        endScore = GetScore(transform.localScale);
    }
}