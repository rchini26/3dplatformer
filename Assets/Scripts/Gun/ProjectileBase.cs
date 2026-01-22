using UnityEngine;

public class ProjectileBase : MonoBehaviour
{
    public float timeToDestroy = 1.5f;
    public int damageAmount = 1;
    public float projectileSpeed = 50f;
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
        
    }
}
