using UnityEngine;

/// <summary>
/// Clase base para enemigos. Se puede heredar para crear diferentes tipos de enemigos.
/// </summary>
[RequireComponent(typeof(Rigidbody))]
public class Enemy : MonoBehaviour
{
    [Header("Enemy Settings")]
    public float speed;          // Velocidad de movimiento
    public int maxHealth;         // Vida máxima

    protected int currentHealth;
    protected Rigidbody rb;
    protected Transform player;

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody>();

        // Evitar rotaciones extrañas y movimiento vertical
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        rb.useGravity = false;

        FindPlayer();
    }

    protected virtual void OnEnable()
    {
        currentHealth = maxHealth;
        FindPlayer();
    }

    protected virtual void FixedUpdate()
    {
        MoveTowardsPlayer();
        RotateTowardsPlayer();
    }

    /// <summary>
    /// Encuentra al jugador automáticamente si no está asignado.
    /// </summary>
    protected void FindPlayer()
    {
        if (player == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
                player = playerObj.transform;
        }
    }

    /// <summary>
    /// Movimiento hacia el jugador usando Rigidbody.velocity.
    /// </summary>
    protected virtual void MoveTowardsPlayer()
    {
        if (player == null || rb == null) return;

        Vector3 direction = (player.position - transform.position);
        direction.y = 0f; // Mantener altura constante
        direction.Normalize();

        rb.linearVelocity = direction * speed; // ← Cambiado de linearVelocity a velocity
    }

    /// <summary>
    /// Rotación suave hacia el jugador.
    /// </summary>
    protected virtual void RotateTowardsPlayer()
    {
        if (player == null) return;

        Vector3 lookDir = player.position - transform.position;
        lookDir.y = 0;

        if (lookDir.sqrMagnitude > 0.01f)
        {
            Quaternion targetRot = Quaternion.LookRotation(lookDir);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, Time.deltaTime * 5f);
        }
    }

    /// <summary>
    /// Recibe daño.
    /// </summary>
    public virtual void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
            Die();
    }

    /// <summary>
    /// Desactiva el enemigo (vuelve a la pool).
    /// </summary>
    protected virtual void Die()
    {
        rb.linearVelocity = Vector3.zero; // ← Cambiado también aquí
        gameObject.SetActive(false);
    }
}
