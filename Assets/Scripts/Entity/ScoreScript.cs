using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreScript : MonoBehaviour
{
    private float startScore;
    private float endScore;

    int GetScore(Vector3 v)
    {
        return (v[0] * v[1] * v[2] * 1000);
    }

    void Start()
    {
        startScore = GetScore(transform.localScale);
    }

    public void LevelDone()
    {
        endScore = GetScore(transform.localScale);
    }
}