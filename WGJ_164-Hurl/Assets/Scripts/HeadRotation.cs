using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadRotation : MonoBehaviour
{
    [SerializeField] float rotationSpeed = 0;
    [SerializeField] Vector2 rotationRange = Vector2.zero;
    public float rotationDirection = 0;

    private void Start()
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
        float currentRotation = transform.eulerAngles.z;
        currentRotation = Mathf.Clamp
            (currentRotation + rotationDirection * Time.fixedDeltaTime * rotationSpeed, 
            rotationRange.x, rotationRange.y);
        transform.eulerAngles = new Vector3(0, 0, currentRotation);
    }

    void Deactivate(bool b) => enabled = false; //Remove control when the battle is over.
}
