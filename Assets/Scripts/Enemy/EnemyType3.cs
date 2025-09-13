using UnityEngine;
public class EnemyType3 : Enemy
{
    private void Awake()
    {
        base.Awake();
        speed = 2f;       // lento
        maxHealth = 100;   // mucha salud
    }
}