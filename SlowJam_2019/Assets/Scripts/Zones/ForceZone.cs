using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceZone : Zone
{ 
    public override void ManageZone()
    {
        Collider[] hitObjects = ReturnHitObjects(zoneSettings);

        List<Rigidbody> hitRigidbodies = ReturnHitRigidbodies(hitObjects);

        for (int i = 0; i < hitRigidbodies.Count; i++)
        {
            hitRigidbodies[i].AddForce(zoneSettings.zoneForceDirection * zoneSettings.zoneForceStrength, ForceMode.Impulse);
        }
    }

    private List<Rigidbody> ReturnHitRigidbodies(Collider[] hitObjects)
    {
        List<Rigidbody> newHitRigidbodies = new List<Rigidbody>();

        for (int i = 0; i < hitObjects.Length; i++)
        {
            Rigidbody newRigidbody = hitObjects[i].GetComponent<Rigidbody>();

            if (newRigidbody != null)
            {
                newHitRigidbodies.Add(newRigidbody);
            }
        }

        return newHitRigidbodies;
    }
} 