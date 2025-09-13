using System;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public Transform firePoint;
    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }
    
    private void Shoot()
    {
        PlayerBulletPool.Instance.GetBullet(firePoint.position, firePoint.rotation);
    }
}