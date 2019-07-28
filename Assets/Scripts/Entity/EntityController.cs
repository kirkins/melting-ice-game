using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityController : MonoBehaviour
{
    [Header("Entity Size Settings")] 
    public EntitySizeSettings sizeSettings;

    [Space(10)] 
    private CameraFollow cameraFollow;

    private MeshCollider entityMeshCollider;
    private MeshFilter entityMesh;
    private Rigidbody myRigidbody;
    
    private void Awake()
    {
        InitializeEntity();
    }

    private void InitializeEntity()
    {
        cameraFollow = Camera.main.GetComponent<CameraFollow>();

        entityMesh = GetComponent<MeshFilter>();

        myRigidbody = GetComponent<Rigidbody>();

        entityMeshCollider = GetComponent<MeshCollider>();

        sizeSettings.entityBaseScale = transform.localScale;
    }

    public virtual void SpecialShrink(Vector3 hitPoint)
    {
        Mesh myMesh = entityMesh.mesh;

        Vector3[] modifiedVertices = myMesh.vertices;

        Vector3 meshCenter = myRigidbody.centerOfMass;
   
        Debug.Log("Hit Object :: " + gameObject.name);
        
        for (int i = 0; i < modifiedVertices.Length; i++)
        {  
            Vector3 worldMeshPoint = transform.TransformPoint(modifiedVertices[i]);
            Vector3 interceptDirection = (meshCenter - worldMeshPoint).normalized;

            float distanceBetweenPoint = Vector3.Distance(hitPoint, worldMeshPoint);

            if (distanceBetweenPoint < sizeSettings.modifyThreshold)
            {
                float distanceRatio = (sizeSettings.modifyThreshold - distanceBetweenPoint) / sizeSettings.modifyThreshold;
                
                modifiedVertices[i] += interceptDirection * distanceRatio * sizeSettings.shrinkMultiplier * Time.deltaTime;
            }
        }

        myMesh.vertices = modifiedVertices;
        
        myMesh.RecalculateNormals();

        entityMeshCollider.sharedMesh = myMesh;
    }
    
    public virtual void ShrinkEntitySize()
    {
        if (sizeSettings.canBeShrinked)
        {
            Vector3 newSize = ReturnNewSize(SIZE_DIRECTION.SHRINK, transform.localScale);
            newSize -= Vector3.one * sizeSettings.shrinkMultiplier * Time.deltaTime;
                                   
            if (newSize.sqrMagnitude < sizeSettings.minSize)
            {
                if (sizeSettings.isKilledAfterMinSize)
                {
                    cameraFollow.RemoveTarget(transform);
                
                    Destroy(gameObject);
                
                    return;
                }
            }
        
            transform.localScale = newSize;
        }     
        
        Debug.Log(GetComponent<Collider>().bounds.size);
    }

    public virtual void EnlargeEntitySize()
    {
        if (sizeSettings.canBeEnlarged)
        {
            Vector3 newSize = ReturnNewSize(SIZE_DIRECTION.ENLARGE, transform.localScale);
                            
            if (newSize.sqrMagnitude > sizeSettings.maxSize)
            {
                if (sizeSettings.isKilledAfterMaxSize)
                {
                    cameraFollow.RemoveTarget(transform);
                
                    Destroy(gameObject);
                
                    return;
                }
            }
        
            transform.localScale = newSize;
        }
    }

    public virtual void ResetEntitySize()
    {
        
    }

    private Vector3 ReturnNewSize(SIZE_DIRECTION sizeDirection, Vector3 baseSize)
    {
        Vector3 newSize = baseSize;
        float sizeMultiplier = 0;

        if (sizeDirection == SIZE_DIRECTION.SHRINK)
        {
            sizeMultiplier = sizeSettings.shrinkMultiplier;

            if (sizeSettings.canXBeModified)
            {
                newSize.x -= 1f * sizeMultiplier * Time.deltaTime;
            }

            if (sizeSettings.canYBeModified)
            {
                newSize.y -= 1f * sizeMultiplier * Time.deltaTime;
            }

            if (sizeSettings.canZBeModified)
            {
                newSize.z -= 1f * sizeMultiplier * Time.deltaTime;
            }
        }
        else
        {
            sizeMultiplier = sizeSettings.enlargeMultiplier;
            
            if (sizeSettings.canXBeModified)
            {
                newSize.x += 1 * sizeMultiplier * Time.deltaTime;
            }

            if (sizeSettings.canYBeModified)
            {
                newSize.y += 1 * sizeMultiplier * Time.deltaTime;
            }

            if (sizeSettings.canZBeModified)
            {
                newSize.z += 1 * sizeMultiplier * Time.deltaTime;
            }
        }

        return newSize;
    }
}

[System.Serializable]
public struct EntitySizeSettings
{
    [Header("Entity Size Attributes")] 
    public Vector3 entityBaseScale;

    [Space(10)] 
    public float modifyThreshold;
    
    public bool canXBeModified;
    public bool canYBeModified;
    public bool canZBeModified;
    
    [Space(10)]
    public float shrinkMultiplier;
    public float enlargeMultiplier;
    
    [Space(10)]
    public float minSize;
    public float maxSize;

    [Space(10)] 
    public bool canBeShrinked;
    public bool canBeEnlarged;
    
    [Space(10)] 
    public bool isKilledAfterMinSize;
    public bool isKilledAfterMaxSize;
}

public enum SIZE_DIRECTION
{
    SHRINK,
    ENLARGE
}
