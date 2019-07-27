using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinZone : Zone
{
    [Header("Win Zone Attributes")]
    public List<Collider> detectedColliders = new List<Collider>();
    public override void ManageZone()
    {
        Collider[] hitObjects = ReturnHitObjects(zoneSettings);

        for (int i = 0; i < hitObjects.Length; i++)
        {
            if (!detectedColliders.Contains(hitObjects[i]))
            {
                detectedColliders.Add(hitObjects[i]);
                
                ActivateWinZone();
            }
        }
    }

    private void ActivateWinZone()
    {
        
    }
}
