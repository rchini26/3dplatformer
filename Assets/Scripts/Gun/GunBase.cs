using System.Collections;
using UnityEngine;

public class GunBase : MonoBehaviour
{
   public ProjectileBase projectilePrefab;
   public Transform positionToShoot;
   public float timeBetweenShots = .05f;
   private Coroutine _currentCoroutine;
   
    protected virtual IEnumerator ShootCoroutine()
    {
        while (true)
        {
            Shoot();
            yield return new WaitForSeconds(timeBetweenShots);
        }
    }

    public void StartShoot()
    {
        CancelShoot();
        _currentCoroutine = StartCoroutine(ShootCoroutine());
    }

    public void CancelShoot()
    {
        if (_currentCoroutine != null) 
            StopCoroutine(_currentCoroutine);
    }
    public void Shoot()
    {
        var projectile = Instantiate(projectilePrefab);
        projectile.transform.position = positionToShoot.position;
        projectile.transform.rotation = positionToShoot.rotation;
    }
}
