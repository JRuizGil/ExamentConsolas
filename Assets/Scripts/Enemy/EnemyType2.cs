using UnityEngine;

public class EnemyType2 : Enemy
{
    public int damageToPlayer = 15;
    public float pushForce = 4f;

    private void Awake()
    {
        base.Awake();
        speed = 5f;
        maxHealth = 50;
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