using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [Header("Entity Interaction Settings")]
    public EntityInteractionSettings interactionSettings;
    
    public virtual HitEntityInfo SearchForEntity()
    {
        Ray checkRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit checkRayInfo = new RaycastHit();

        if (Physics.Raycast(checkRay, out checkRayInfo, interactionSettings.interactionMask))
        {
            return new HitEntityInfo(checkRayInfo.transform.GetComponent<EntityController>(), checkRayInfo.point);
        }

        return new HitEntityInfo();
    }
}

[System.Serializable]
public struct EntityInteractionSettings
{
    [Header("Entity Interaction Settings")]
    public float interactionCheckSize;
    
    [Space(10)]
    public LayerMask interactionMask;
}

[System.Serializable]
public struct HitEntityInfo
{
    public EntityController entityController;
    public Vector3 hitPoint;

    public HitEntityInfo(EntityController newEntityController, Vector3 newHitPoint)
    {
        this.entityController = newEntityController;
        this.hitPoint = newHitPoint;
    }
}
