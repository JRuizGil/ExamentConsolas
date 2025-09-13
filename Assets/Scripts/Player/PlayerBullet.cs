using UnityEngine;

public class PlayerBullet : Bullet
{
    private void Awake()
    {
        speed = 25f;
        damage = 20;
    }
    public override void OnTriggerEnter(Collider other)
    {
        // Ignorar colisiones con el jugador mismo
        if (other.CompareTag("Player")) return;

        // Intentar obtener el componente Character del objeto impactado
        var target = other.GetComponent<Character>();

        // Si es un Character y tiene la etiqueta "Enemy", aplica daño
        if (target != null && other.CompareTag("Enemy"))
        {
            target.TakeDamage(damage);
        }

        // Devolver la bala al pool
        PlayerBulletPool.Instance.ReturnBullet(this);
    }

}