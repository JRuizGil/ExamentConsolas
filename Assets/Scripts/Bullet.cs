using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    public int damage = 10;
    private float lifeTime = 3f;
    private float timer;
    private Vector3 direction;

    private void OnEnable()
    {
        timer = 0f;
    }
    private void Update()
    {
        transform.position += transform.forward * (speed * Time.deltaTime);

        timer += Time.deltaTime;
        if (timer >= lifeTime)
        {
            PlayerBulletPool.Instance.ReturnBullet(this);
        }
    }
    public void SetDirection(Vector3 dir)
    {
        direction = dir;
    }
    public virtual void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Enemy")) return;

        var target = other.GetComponent<Character>();
        if (target != null)
        {
            target.TakeDamage(damage);
        }
        PlayerBulletPool.Instance.ReturnBullet(this);
    }
}