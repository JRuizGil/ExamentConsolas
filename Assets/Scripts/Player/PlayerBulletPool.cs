using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletPool : Bullet
{
    public static PlayerBulletPool Instance;

    public Bullet bulletPrefab;
    public int poolSize = 20;

    protected Queue<Bullet> Bullets = new Queue<Bullet>();

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        // Crear el pool
        for (int i = 0; i < poolSize; i++)
        {
            Bullet b = Instantiate(bulletPrefab);
            b.gameObject.SetActive(false);
            Bullets.Enqueue(b);
        }
    }

    // Obtener bala del pool
    public Bullet GetBullet(Vector3 position, Quaternion rotation)
    {
        if (Bullets.Count == 0)
        {
            // Si no hay balas disponibles, instanciamos más
            Bullet b = Instantiate(bulletPrefab);
            b.gameObject.SetActive(false);
            Bullets.Enqueue(b);
        }

        Bullet bullet = Bullets.Dequeue();
        bullet.transform.position = position;
        bullet.transform.rotation = rotation;
        bullet.gameObject.SetActive(true);
        return bullet;
    }

    // Devolver bala al pool
    public void ReturnBullet(Bullet bullet)
    {
        bullet.gameObject.SetActive(false);
        Bullets.Enqueue(bullet);
    }
}