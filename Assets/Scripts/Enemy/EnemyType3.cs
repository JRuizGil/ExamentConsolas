using UnityEngine;

public class EnemyType3 : Enemy
{
    public int damageToPlayer = 20;
    public float pushForce = 3f;

    private void Awake()
    {
        base.Awake();
        speed = 2f;
        maxHealth = 100;
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