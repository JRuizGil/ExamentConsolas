using UnityEngine;

/// <summary>
/// Clase base para enemigos. Se puede heredar para crear diferentes tipos de enemigos.
/// </summary>
[RequireComponent(typeof(Rigidbody))]
public class Enemy : Character
{
    [Header("Enemy Settings")]
    public float speed = 5f; // Velocidad de movimiento

    protected Transform player;

    public override void Awake()
    {
        base.Awake(); // Inicializa currentHealth desde Character
        Rb = GetComponent<Rigidbody>();

        // Evitar rotaciones extrañas y movimiento vertical
        Rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        Rb.useGravity = false;

        FindPlayer();
    }

    public override void OnEnable()
    {
        base.OnEnable(); // Reinicia health
        FindPlayer();

        if (Rb != null)
            Rb.WakeUp();
    }

    public override void FixedUpdate()
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
    /// Movimiento hacia el jugador usando Rigidbody.velocity
    /// </summary>
    protected virtual void MoveTowardsPlayer()
    {
        if (player == null || Rb == null) return;

        Vector3 direction = (player.position - transform.position);
        direction.y = 0f;
        direction.Normalize();

        Rb.linearVelocity = direction * speed; // ✅ velocity correcto
    }

    /// <summary>
    /// Rotación suave hacia el jugador
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
    /// Recibe daño
    /// </summary>
    public override void TakeDamage(int amount)
    {
        base.TakeDamage(amount); // Reduce currentHealth desde Character
        if (IsDead())
        {
            Die();
        }
    }

    /// <summary>
    /// Desactiva el enemigo (vuelve a la pool)
    /// </summary>
    protected virtual void Die()
    {
        if (Rb != null) Rb.linearVelocity = Vector3.zero;
        gameObject.SetActive(false);
    }
}
