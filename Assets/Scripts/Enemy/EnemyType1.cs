using UnityEngine;

public class EnemyType1 : Enemy
{
    public int damageToPlayer = 10;   // daño al Player
    public float pushForce = 5f;      // fuerza de empuje

    public override void Awake()
    {
        base.Awake();
        speed = 7f;
        maxHealth = 30;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Player player = collision.gameObject.GetComponent<Player>();
            if (player != null)
            {
                player.TakeDamage(damageToPlayer);

                // Empujar usando el método público
                Vector3 pushDir = (collision.transform.position - transform.position).normalized;
                player.ApplyPush(pushDir * pushForce);
            }
        }
    }

}