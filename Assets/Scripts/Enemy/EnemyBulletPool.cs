using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletPool : MonoBehaviour
{
    public static EnemyBulletPool Instance;

    [Header("Prefabs de balas (1,2,3)")]
    public GameObject bulletPrefab1;
    public GameObject bulletPrefab2;
    public GameObject bulletPrefab3;

    [Header("Configuración del pool")]
    public int poolSizePerType = 10;

    private List<Bullet> pool1 = new List<Bullet>();
    private List<Bullet> pool2 = new List<Bullet>();
    private List<Bullet> pool3 = new List<Bullet>();

    void Awake()
    {
        if (Instance == null) Instance = this;

        CreatePool(bulletPrefab1, pool1);
        CreatePool(bulletPrefab2, pool2);
        CreatePool(bulletPrefab3, pool3);
    }

    private void CreatePool(GameObject prefab, List<Bullet> pool)
    {
        for (int i = 0; i < poolSizePerType; i++)
        {
            GameObject obj = Instantiate(prefab, transform);
            obj.SetActive(false);
            pool.Add(obj.GetComponent<Bullet>());
        }
    }

    public Bullet GetBullet(int type)
    {
        List<Bullet> pool = type switch
        {
            1 => pool1,
            2 => pool2,
            3 => pool3,
            _ => pool1
        };

        foreach (Bullet b in pool)
        {
            if (!b.gameObject.activeInHierarchy)
            {
                b.gameObject.SetActive(true);
                return b;
            }
        }

        // Si no hay libres → instanciar uno nuevo y añadirlo
        GameObject prefab = type == 1 ? bulletPrefab1 : type == 2 ? bulletPrefab2 : bulletPrefab3;
        GameObject obj = Instantiate(prefab, transform);
        Bullet newBullet = obj.GetComponent<Bullet>();
        pool.Add(newBullet);
        return newBullet;
    }

    public void ReturnBullet(Bullet bullet)
    {
        bullet.gameObject.SetActive(false);
    }
}