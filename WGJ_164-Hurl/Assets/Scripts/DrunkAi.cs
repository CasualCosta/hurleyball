using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrunkAi : MonoBehaviour
{
    public Rigidbody2D bullet;
    public LayerMask layerMask;

    [SerializeField] Animator animator = null;

    [HideInInspector]
    private UnitMovement movement;
    [HideInInspector]
    private HeadRotation rotation;
    [HideInInspector]
    private Shooter shooter;

    private bool canAct;

    public float MaxShootInterval;
    public Vector2 boundary;
    public float MAX_MoveTime;
    public float MIN_MoveTime;

    [SerializeField] float reduceZeroMove = 0.3f;

    private float shootTime;
    private float moveTime;
    private int moveDir;


    // Start is called before the first frame update
    void Start()
    {
        GetComponent<PlayerInput>().enabled = CrossSceneData.isOpponentPlayer;
        enabled = !CrossSceneData.isOpponentPlayer;


        movement = GetComponent<UnitMovement>();
        rotation = GetComponentInChildren<HeadRotation>();
        shooter = GetComponentInChildren<Shooter>();

        moveDir = Random.Range(-1, 1);
        moveTime = Random.Range(MIN_MoveTime, MAX_MoveTime);
        shootTime = Random.Range(shooter.fireRate, MaxShootInterval);

        BlackPanel.OnFightBegin += ActivateInput;
    }

    private void OnDisable()
    {
        BlackPanel.OnFightBegin -= ActivateInput;
    }

    // Update is called once per frame
    void Update()
    {
        if (!canAct)
            return;

        AiMovement();
    }

    private float landDistance;

    void AiMovement()
    {
        float marginError = 0.9f;

        float velocity = (shooter.bulletForce / bullet.mass) * Time.fixedDeltaTime;
        float gravity = Physics2D.gravity.y * bullet.gravityScale * marginError;
        float angle = rotation.transform.eulerAngles.z * Mathf.Deg2Rad;

        landDistance = Mathf.Pow(velocity, 2) * Mathf.Sin(2 * angle) / gravity;
        Debug.DrawLine(transform.position + transform.up, new Vector2(transform.position.x - landDistance, transform.position.y + 1));

        RaycastHit2D hit = Physics2D.Raycast(transform.position + transform.up, transform.TransformDirection(-Vector2.right), 30f, layerMask);

        if (Mathf.Abs(hit.distance - landDistance) < 1.5f) {
            rotation.rotationDirection = 0;
        }
        else {
            float distance = landDistance - hit.distance;
            float direction = Mathf.Clamp(distance, -1f, 1f);

            rotation.rotationDirection = -direction;
        }

        PointlessMovement();
        PointlessShooting();
    }

    void PointlessMovement()
    {
        if (moveTime <= 0) {
            moveDir = Random.Range(-1, 1);
            moveTime = Random.Range(MIN_MoveTime, MAX_MoveTime);
            
            if (moveDir == 0) 
                moveTime *= reduceZeroMove;
        }

        if (transform.position.x < boundary.x) {
            moveDir = 1;
            moveTime = Random.Range(MIN_MoveTime, MAX_MoveTime);

            if (moveDir == 0)
                moveTime *= reduceZeroMove;
        }
        else if (transform.position.x > boundary.y) {
            moveDir = -1;
            moveTime = Random.Range(MIN_MoveTime, MAX_MoveTime);

            if (moveDir == 0)
                moveTime *= reduceZeroMove;
        }

        moveTime -= Time.deltaTime;
        movement.moveDirection = moveDir;
        animator.SetFloat("Speed",Mathf.Abs(movement.moveDirection));
    }

    void PointlessShooting()
    {
        if (shootTime <= 0) {
            shootTime = Random.Range(shooter.fireRate, MaxShootInterval);
            shooter.Shoot();
        }

        shootTime -= Time.deltaTime;
    }

    void ActivateInput() => canAct = true;
}
