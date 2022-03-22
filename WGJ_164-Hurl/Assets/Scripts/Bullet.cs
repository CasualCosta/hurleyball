using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb = null;
    [Tooltip("Damage caused on impact")]
    [SerializeField] float damage = 5f;
    [HideInInspector] public float bulletForce = 0;
    // Start is called before the first frame update
    void Start()
    {
        rb.AddRelativeForce(new Vector2(bulletForce, 0));
        Destroy(gameObject, 5f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IDamageable damageable = collision.GetComponent<IDamageable>();
        if (damageable != null)
        {
            damageable.ChangeHealth(-damage);
        }
        Destroy(gameObject);
    }
}
