using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletPool : MonoBehaviour
{
    public static PlayerBulletPool Instance;

    public Bullet bulletPrefab;
    public int poolSize = 20;

    private Queue<Bullet> bullets = new Queue<Bullet>();

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        // Crear pool inicial
        for (int i = 0; i < poolSize; i++)
        {
            AddBulletToPool();
        }
    }

    private void AddBulletToPool()
    {
        Bullet b = Instantiate(bulletPrefab);
        b.gameObject.SetActive(false);
        bullets.Enqueue(b);
    }

    public Bullet GetBullet(Vector3 position, Quaternion rotation)
    {
        // Si la cola está vacía o todas las balas están activas, crear una nueva
        while (bullets.Count > 0)
        {
            Bullet bullet = bullets.Dequeue();
            if (bullet != null)
            {
                bullet.transform.position = position;
                bullet.transform.rotation = rotation;
                bullet.gameObject.SetActive(true);
                return bullet;
            }
        }

        // Crear nueva bala si no hay disponibles
        AddBulletToPool();
        return GetBullet(position, rotation);
    }

    public void ReturnBullet(Bullet bullet)
    {
        if (bullet == null) return;
        bullet.gameObject.SetActive(false);
        bullets.Enqueue(bullet);
    }
}