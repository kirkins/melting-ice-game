using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceZone : MonoBehaviour
{
    [Header("Force Zone Settings")] 
    public ForceZoneSettings forceZoneSettings;

    [Space(10)] 
    public bool isZoneActive = true;
    private void FixedUpdate()
    {
        ManageZone();
    }

    private void ManageZone()
    {
        List<Rigidbody> newHitRigidbodies = ReturnHitObjects(forceZoneSettings);

        for (int i = 0; i < newHitRigidbodies.Count; i++)
        {
            newHitRigidbodies[i].AddForce(forceZoneSettings.zoneForceDirection * forceZoneSettings.zoneForceStrength, ForceMode.Impulse);
        }
    }

    private List<Rigidbody> ReturnHitObjects(ForceZoneSettings zoneSettings)
    {
        List<Rigidbody> hitRigidbodies = new List<Rigidbody>();

        if (zoneSettings.zoneShape == FORCEZONE_SHAPE.BOX)
        {
            Collider[] hitObjects = Physics.OverlapBox(transform.position, zoneSettings.zoneSize / 2, Quaternion.Euler(zoneSettings.zoneRotation), zoneSettings.zoneMask);

            for (int i = 0; i < hitObjects.Length; i++)
            {
                Rigidbody hitRigidbody = hitObjects[i].GetComponent<Rigidbody>();

                if (hitRigidbody != null)
                {
                    hitRigidbodies.Add(hitRigidbody);
                }
            }
        }
        else
        {
            Collider[] hitObjects = Physics.OverlapSphere(transform.position, zoneSettings.zoneRadius, zoneSettings.zoneMask);
            
            for (int i = 0; i < hitObjects.Length; i++)
            {
                Rigidbody hitRigidbody = hitObjects[i].GetComponent<Rigidbody>();

                if (hitRigidbody != null)
                {
                    hitRigidbodies.Add(hitRigidbody);
                }
            }
        }

        return hitRigidbodies;
    }

    private void OnDrawGizmos()
    {
        if (forceZoneSettings.zoneShape == FORCEZONE_SHAPE.BOX)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireCube(transform.position, forceZoneSettings.zoneSize);
        }
        else
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, forceZoneSettings.zoneRadius);
        }
        
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, forceZoneSettings.zoneForceDirection);       
    }
}

[System.Serializable]
public struct ForceZoneSettings
{
    [Header("Force Zone Settings")] 
    public FORCEZONE_SHAPE zoneShape;

    [Space(10)] 
    public Vector3 zoneSize;
    public Vector3 zoneRotation;
    
    [Space(10)]
    public float zoneRadius;

    [Space(10)] 
    public Vector3 zoneForceDirection;

    public float zoneForceStrength;

    [Space(10)] 
    public LayerMask zoneMask;
}

public enum FORCEZONE_SHAPE
{
    BOX,
    SPHERE
}
