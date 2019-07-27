using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityController : MonoBehaviour
{
    [Header("Entity Size Settings")] 
    public EntitySizeSettings sizeSettings;
    
    public virtual void ShrinkEntitySize()
    {
        Debug.Log("Shrinking " + gameObject.name);
        
        Vector3 newSize = transform.localScale -= Vector3.one  * sizeSettings.shrinkMultiplier * Time.deltaTime;

        if (newSize.magnitude < sizeSettings.minSize)
        {
            if (sizeSettings.isKilledAfterMinSize)
            {
                Debug.Log(gameObject.name + " should be dead!");
            }
        }
    }

    public virtual void EnlargeEntitySize()
    {
        Debug.Log("Enlarging " + gameObject.name);
        
        Vector3 newSize = transform.localScale += Vector3.one * sizeSettings.enlargeMultiplier * Time.deltaTime;

        if (newSize.magnitude < sizeSettings.maxSize)
        {
            if (sizeSettings.isKilledAfterMaxSize)
            {
                Debug.Log(gameObject.name + " should be dead!");
            }
        }
    }

    public virtual void ResetEntitySize()
    {
        
    }
}

[System.Serializable]
public struct EntitySizeSettings
{
    [Header("Entity Size Attributes")] 
    public float shrinkMultiplier;
    public float enlargeMultiplier;
    
    [Space(10)]
    public float minSize;
    public float maxSize;

    [Space(10)] 
    public bool isKilledAfterMinSize;
    public bool isKilledAfterMaxSize;
}
