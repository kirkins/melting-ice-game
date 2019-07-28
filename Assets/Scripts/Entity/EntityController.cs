using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityController : MonoBehaviour
{
    [Header("Entity Size Settings")] 
    public EntitySizeSettings sizeSettings;

    public float jumpStrength = 5;
    public float jumpBoostStrength = 15;
    public float boostStrength = 50;

    Renderer rend;

    [Space(10)] 
    private CameraFollow cameraFollow;

    private MeshFilter entityMesh;

    private Rigidbody rigidBody;

    private bool grounded = true;

    private void Awake()
    {
        InitializeEntity();
    }

    private void InitializeEntity()
    {
        cameraFollow = Camera.main.GetComponent<CameraFollow>();

        entityMesh = GetComponent<MeshFilter>();

        sizeSettings.entityBaseScale = transform.localScale;
    }

    public virtual void SpecialShrink(Vector3 hitPoint)
    {
        Mesh myMesh = entityMesh.mesh;

        Vector3[] modifiedVertices = myMesh.vertices;
   
        for (int i = 0; i < modifiedVertices.Length; i++)
        {
            Vector3 worldMeshPoint = transform.TransformPoint(modifiedVertices[i]);
            
            Vector3 interceptPoint = worldMeshPoint - hitPoint;
            
            float distanceFromPoint = Vector3.Distance(hitPoint, worldMeshPoint);

            if (distanceFromPoint < 1f)
            {
                modifiedVertices[i] += interceptPoint.normalized * Time.deltaTime;
            }
        }

        myMesh.vertices = modifiedVertices;
        myMesh.RecalculateNormals();
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

    public void GrowFromItem()
    {
        Vector3 newSize = new Vector3(4, 4, 4);
        transform.localScale = newSize;
    }

    void Update()
    {
        rigidBody = GetComponent<Rigidbody>();

        if(transform.localScale[0]>0.9)
        {
            transform.localScale -= new Vector3(0.01F, 0.01F, 0.01F);
        } else if (transform.localScale[0] > 0.7)
        {
            transform.localScale -= new Vector3(0.001F, 0.001F, 0.001F);
        }
        else
        {
            transform.localScale -= new Vector3(0.001F, 0.001F, 0.001F);
        }


        if (Input.GetKey("space"))
        {
            Jump();
        }

    }

    void FixedUpdate()
    {
        if (Input.GetKey("left"))
        {
        rigidBody.AddForce(Vector3.left *
            ((Input.GetKey("b")) ?
            boostStrength :
            jumpStrength)
            );
        }

        if (Input.GetKey("right"))
        {
            if (Input.GetKey("b"))
            {
                rigidBody.AddForce(Vector2.right * 50);
            }
            else
            {
                rigidBody.AddForce(Vector2.right * 5);
            }
        }


        if (Input.GetKey("up"))
        {
            Jump();
        }

    }

    void OnCollisionEnter()
    {
        grounded = true;
    }

    void Jump()
    {
        if (grounded)
        { 
            rigidBody.AddForce(Vector3.up * 
                ((Input.GetKey("b")) ? 
                    jumpBoostStrength : 
                    jumpStrength ),
                    ForceMode.Impulse);
            grounded = false;
        }
    }
}



[System.Serializable]
public struct EntitySizeSettings
{
    [Header("Entity Size Attributes")] 
    public Vector3 entityBaseScale;
    
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
