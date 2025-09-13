using UnityEngine;

public class Player : Character
{
    public override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        Rb = GetComponent<Rigidbody>();
    }

    public override void Move()
    {
        // Si se mantiene Fire2, no moverse
        if (Input.GetButton("Fire2"))
        {
            // Opcional: desacelerar suavemente mientras se mantiene Fire2
            Rb.linearVelocity = Vector3.Lerp(Rb.linearVelocity, Vector3.zero, deceleration * Time.deltaTime);
            return;
        }

        float moveX = Input.GetAxisRaw("Horizontal");
        float moveZ = Input.GetAxisRaw("Vertical");

        Vector3 inputDir = new Vector3(moveX, 0, moveZ);

        if (inputDir.sqrMagnitude > 0.01f)
        {
            inputDir.Normalize();

            float angle = Mathf.Atan2(inputDir.z, inputDir.x) * Mathf.Rad2Deg;
            float snappedAngle = Mathf.Round(angle / 45f) * 45f;
            float rad = snappedAngle * Mathf.Deg2Rad;

            Vector3 moveDir = new Vector3(Mathf.Cos(rad), 0, Mathf.Sin(rad));
            Vector3 targetVelocity = moveDir * moveSpeed;

            float lerpFactor = (inputDir.magnitude > 0) ? acceleration : deceleration;
            Rb.linearVelocity = Vector3.Lerp(Rb.linearVelocity, targetVelocity, lerpFactor * Time.deltaTime);
        }
        else
        {
            Rb.linearVelocity = Vector3.Lerp(Rb.linearVelocity, Vector3.zero, deceleration * Time.deltaTime);
        }
    }

    public override void LookAt()
    {
        float lookX = Input.GetAxis("RightStickX");
        float lookZ = Input.GetAxis("RightStickZ");

        Vector3 lookDir = new Vector3(lookX, 0, lookZ);

        if (lookDir.sqrMagnitude > 0.01f)
        {
            float angle = Mathf.Atan2(lookDir.z, lookDir.x) * Mathf.Rad2Deg;
            float snappedAngle = Mathf.Round(angle / 45f) * 45f;
            float rad = snappedAngle * Mathf.Deg2Rad;
            Vector3 snappedDir = new Vector3(Mathf.Cos(rad), 0, Mathf.Sin(rad));
            Quaternion targetRotation = Quaternion.LookRotation(snappedDir, Vector3.up);
            transform.rotation = targetRotation;
        }
    }

}