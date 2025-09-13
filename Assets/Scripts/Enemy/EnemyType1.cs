using UnityEngine;

public class EnemyType1 : Enemy
{
    protected override void Awake()
    {
        base.Awake();
        speed = 7f;
        maxHealth = 30;
    }
}

