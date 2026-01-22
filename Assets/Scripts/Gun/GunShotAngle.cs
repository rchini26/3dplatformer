using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunShotAngle : GunShotLimit
{
    public int amountPerShot = 4;
    public float angle = 15f;

    public override void Shoot()
    {
        int multiplier = 0;
            
        for (int i = 0; i < amountPerShot; i++)
        {
            if (i % 2 == 0)
            {
                multiplier++;
            }
            
            
            var projectile = Instantiate(projectilePrefab);

            projectile.transform.position = positionToShoot.position;
            projectile.transform.rotation = positionToShoot.rotation * Quaternion.Euler(0, i%2 == 0 ? angle : -angle, 0);
            projectile.projectileSpeed = speed;
            projectile.transform.parent = null;
        }
    }
}
