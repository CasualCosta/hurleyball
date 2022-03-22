using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthSlider : MonoBehaviour
{
    [SerializeField] Slider slider = null;
    [SerializeField] bool isPlayerOneSlider = true;
    // Start is called before the first frame update
    void Awake()
    {
        UnitHealth.OnHealthChange += UpdateHealthSlider;
        UnitHealth.OnDeath += Deactivate;
    }

    void OnDisable()
    {
        UnitHealth.OnHealthChange -= UpdateHealthSlider;
        UnitHealth.OnDeath -= Deactivate;
    }

    void UpdateHealthSlider(Vector2 health, bool b)
    {
        if (b != isPlayerOneSlider)
            return;
        slider.maxValue = health.y;
        slider.value = health.x;
    }

    void Deactivate(bool b)
    {
        gameObject.SetActive(false);
    }
}
