using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Health inherits from IDamageable in case we decide we need more damageable things
public class UnitHealth : MonoBehaviour, IDamageable 
{
    [Tooltip("Health values. X is current and Y is maximum")]
    [SerializeField] Vector2 health = Vector2.one;
    [SerializeField] bool isPlayerOne = true;

    public static event Action<Vector2, bool> OnHealthChange; //event to update health
    public static event Action<bool> OnDeath; //bool is "isPlayerOne"
    // Start is called before the first frame update
    void Start()
    {
        OnDeath += Deactivate;
        OnHealthChange?.Invoke(health, isPlayerOne);
    }

    private void OnDisable()
    {
        OnDeath -= Deactivate;
    }

    public void ChangeHealth(float value)
    {
        health.x = Mathf.Clamp(health.x + value, 0, health.y);
        OnHealthChange?.Invoke(health, isPlayerOne);
        if (health.x == 0)
            Die();
    }

    void Die()
    {
        OnDeath?.Invoke(isPlayerOne);
        Destroy(gameObject);
    }

    void Deactivate(bool b) => enabled = false; //Remove control when the battle is over.
}
