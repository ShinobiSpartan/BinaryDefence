using UnityEngine;

public class Bullet : MonoBehaviour
{

    private Transform target;
    public float bulletSpeed = 5.0f;

    public GameObject impactEffects;

    /// <summary>
    /// finding the target
    /// </summary>
    /// <param name="_target"></param>
    public void Seek(Transform _target)
    {
        target = _target;
    }

    // Update is called once per frame
    void Update()
    {   
        //checking to see if the target is null
        if(target == null)
        {
            Destroy(gameObject);
            return;
        }
        //getting the direction and distance 
        Vector3 direction = target.position - transform.position;
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

        GameObject effects = (GameObject)Instantiate(impactEffects, transform.position, transform.rotation);
        Destroy(effects, 2f);

        Destroy(target);

        Destroy(gameObject);
    }
}
