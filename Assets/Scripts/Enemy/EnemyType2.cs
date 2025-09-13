using UnityEngine;

public class EnemyType2 : Enemy
{
    private void Awake()
    {
        base.Awake();
        speed = 5f;      // más rápido
        maxHealth = 50;   // más resistente
    }
}