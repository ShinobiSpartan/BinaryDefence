using UnityEngine;

public class Bullet : MonoBehaviour
{
    #region Variables
    [Header("Speed")]
    //bullet speed 
    public float bulletSpeed = 5.0f;
    //damage values
    public int damage = 2;

    [Header("Bullet Effects")]
    //game object for the impact effects
    public GameObject impactEffects;
    //position for the impact effect offset
    public Vector3 impactEffectsOffset;
    //game object for the targetObject
    private GameObject targetObject;
    //transform for the targetTransform
    private Transform targetTransform;
    #endregion
    /// <summary>
    /// finding the target
    /// </summary>
    /// <param name="_target"></param>
    public void Seek(Transform _target)
    {
        targetTransform = _target;
    }
    /// <summary>
    /// getting impact offset postioning
    /// </summary>
    /// <returns></returns>
    public Vector3 GetImpactEffectsPos()
    {
        return transform.position + impactEffectsOffset;
    }

    // Update is called once per frame
    void Update()
    {   
        //checking to see if the target is null
        if(targetTransform == null)
        {
            //destroys the gameobject
            Destroy(gameObject);
            return;
        }
        //setting the targetObject to the targetTransform gameObject
        targetObject = targetTransform.gameObject;

        //getting the direction and distance 
        Vector3 direction = targetTransform.position - transform.position;
        float distanceThisFrame = bulletSpeed * Time.deltaTime;

        if(direction.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }

        transform.Translate(direction.normalized * distanceThisFrame, Space.World);
    }

    /// <summary>
    /// adding some effects for when the bullets hit and when the target is destroyed
    /// the bullet will also get destroyed when hit with target
    /// </summary>
    void HitTarget()
    {

        GameObject effects = (GameObject)Instantiate(impactEffects, GetImpactEffectsPos(), transform.rotation);
        Destroy(effects, 2f);

        targetObject.GetComponent<ObjectHealth>().TakeDamage(damage);

        Destroy(gameObject);
    }
}
