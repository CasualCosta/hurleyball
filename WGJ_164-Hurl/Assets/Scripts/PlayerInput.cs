using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [Header("Reference Variables")]
    [SerializeField] UnitMovement unitMovement = null;
    [SerializeField] HeadRotation headRotation = null;
    [SerializeField] Shooter shooter = null;
    [SerializeField] Animator animator = null;
    [Tooltip("The button the player presses to shoot")]
    [SerializeField] KeyCode shootingKey = KeyCode.Space;


    public bool isPlayerOne;

    private bool canAct = false;
    // Start is called before the first frame update
    void Start()
    {
        if (unitMovement == null)
            unitMovement = GetComponent<UnitMovement>();
        if (headRotation == null)
            headRotation = GetComponent<HeadRotation>();
        BlackPanel.OnFightBegin += ActivateInput;
    }

    private void OnDisable()
    {
        BlackPanel.OnFightBegin -= ActivateInput;
    }

    void ActivateInput() => canAct = true;
    // Update is called once per frame
    void Update()
    {
        if (!canAct)
            return;
        if (isPlayerOne) {
            unitMovement.moveDirection = Input.GetAxisRaw("P1_Horizontal");
            headRotation.rotationDirection = Input.GetAxisRaw("P1_Vertical");
        }
        else {
            unitMovement.moveDirection = Input.GetAxisRaw("P2_Horizontal");
            headRotation.rotationDirection = -Input.GetAxisRaw("P2_Vertical");
        }
        animator.SetFloat("Speed",Mathf.Abs(unitMovement.moveDirection));
        shooter.isShooting = Input.GetKey(shootingKey);
    }
}
