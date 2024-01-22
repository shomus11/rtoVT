using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class proto_movement : MonoBehaviour
{

    Rigidbody2D rb;
    Vector2 movement;
    float spd;
    float hp;
    public PlayerData playerData;
    public TextMeshProUGUI debugTxt;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spd = playerData.moveSpeed;
        hp = playerData.healthPoint;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        movement = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        rb.velocity = movement.normalized * spd;

        if (hp < 0f)
        {
            debugTxt.text = "Kalah";
            Destroy(gameObject);
        }

        PlayerAnimationUpdater();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<proto_enemyProjectile>(out proto_enemyProjectile enemy))
        {
            hp -= enemy.dmg;
        }
    }

    private void PlayerAnimationUpdater()
    {
        anim.SetFloat("MovementValueX", movement.x);
        anim.SetFloat("MovementValueY", movement.y);
    }
}
