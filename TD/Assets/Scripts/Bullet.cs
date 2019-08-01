using UnityEngine;

public class Bullet : MonoBehaviour
{

    private Transform target;
    public float bulletSpeed = 100.0f;

    public GameObject impactEffects;

    public void Seek(Transform _target)
    {
        target = _target;
    }

    // Update is called once per frame
    void Update()
    {       
        if(target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 direction = target.position - target.position;
        float distanceThisFrame = bulletSpeed * Time.deltaTime;

        if(direction.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }

        transform.Translate(direction.normalized * distanceThisFrame, Space.World);
    }


    void HitTarget()
    {

        GameObject effects = (GameObject)Instantiate(impactEffects, transform.position, transform.rotation);
        Destroy(effects, 2f);

        Destroy(target);

        Destroy(gameObject);
    }
}
