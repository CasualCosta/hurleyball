using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitMovement : MonoBehaviour
{
    public Rigidbody2D rb = null;
    [SerializeField] float moveSpeed = 0;
    [HideInInspector] public float moveDirection = 0;
    // Start is called before the first frame update
    void Start()
    {
        UnitHealth.OnDeath += Deactivate;
    }

    private void OnDisable()
    {
        UnitHealth.OnDeath -= Deactivate;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.velocity = new Vector2
            (moveDirection * moveSpeed * Time.fixedDeltaTime, rb.velocity.y);
    }
    void Deactivate(bool b) => enabled = false; //Remove control when the battle is over.
}
