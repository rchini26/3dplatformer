using UnityEngine;
using System.Collections.Generic;

public class ProjectileBase : MonoBehaviour
{
    public float timeToDestroy = 1.5f;
    public int damageAmount = 1;
    public float projectileSpeed = 50f;
    public List<string> tagsToHit;
    void Awake()
    {
        Destroy(gameObject, timeToDestroy);
    }
    void Update()
    {
        transform.Translate(Vector3.forward * (Time.deltaTime * projectileSpeed));    
    }

    private void OnCollisionEnter(Collision collision)
    {
        foreach (var t in tagsToHit)
        {
            if (collision.transform.CompareTag(t))
            {
                var damageable = collision.gameObject.GetComponent<HealthBase>();
                if (damageable != null) damageable.Damage(damageAmount);
                if (!collision.gameObject.CompareTag("Projectile")) Destroy(gameObject);
            }

            return;
        }
    }
}
