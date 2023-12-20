using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool CanMove { get {return canMove;} set { canMove = value;} }
    public bool CanShoot { get {return canShoot;} set { canShoot = value;} }

    public PlayerData playerData;
    private ShootController shootController;
    [SerializeField] private float healthPoint;
    [SerializeField] private bool canShoot;
    [SerializeField] private bool canMove;
    [SerializeField] private float moveSpeed;
    private float horizontalMove;
    private float verticalMove;
    private Vector2 movement;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        shootController = GetComponent<ShootController>();
        moveSpeed = playerData.moveSpeed;
        healthPoint = playerData.healthPoint;
    }

    private void Update()
    {
        GetInput();
        if (canShoot)
            StartCoroutine(shootController.Shoot());
    }

    private void FixedUpdate() {
        Move();
    }

    private void GetInput()
    {
        horizontalMove = Input.GetAxis("Horizontal");
        verticalMove = Input.GetAxis("Vertical");
        movement = new Vector2(horizontalMove, verticalMove);
    }

    private void Move()
    {
        if (!canMove)
            return;
        rb.velocity = movement.normalized * moveSpeed;
    }
}
