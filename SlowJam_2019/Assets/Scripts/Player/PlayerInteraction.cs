using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [Header("Entity Interaction Settings")]
    public EntityInteractionSettings interactionSettings;
    
    public virtual EntityController SearchForEntity()
    {
        Ray checkRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit checkRayInfo = new RaycastHit();

        if (Physics.Raycast(checkRay, out checkRayInfo, interactionSettings.interactionMask))
        {
            return checkRayInfo.transform.GetComponent<EntityController>();
        }

        return null;
    }
}

[System.Serializable]
public struct EntityInteractionSettings
{
    [Header("Entity Interaction Settings")]
    public LayerMask interactionMask;
}
