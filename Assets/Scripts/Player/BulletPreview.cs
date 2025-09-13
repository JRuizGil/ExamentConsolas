using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class BulletPreview : MonoBehaviour
{
    public float maxDistance = 50f;   // Distancia máxima del láser
    public LayerMask collisionMask;   // Capas con las que puede chocar

    private LineRenderer lr;

    private void Awake()
    {
        lr = GetComponent<LineRenderer>();
        lr.positionCount = 2;
        lr.startWidth = 0.05f;
        lr.endWidth = 0.05f;

        // Material rojo sencillo
        lr.material = new Material(Shader.Find("Unlit/Color"));
        lr.material.color = Color.red;
    }

    private void Update()
    {
        DrawLaser();
    }

    private void DrawLaser()
    {
        Vector3 startPos = transform.position + Vector3.up * 0.5f; // punto de salida
        Vector3 dir = transform.forward;

        lr.SetPosition(0, startPos);

        if (Physics.Raycast(startPos, dir, out RaycastHit hit, maxDistance, collisionMask))
        {
            // Si choca con algo → línea hasta el impacto
            lr.SetPosition(1, hit.point);
        }
        else
        {
            // Si no choca → línea hasta distancia máxima
            lr.SetPosition(1, startPos + dir * maxDistance);
        }
    }
}