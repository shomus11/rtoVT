using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class proto_movement : MonoBehaviour
{

    Rigidbody2D rigidbody;
    Vector2 movement;
    float spd;
    public PlayerData playerData;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        spd = playerData.moveSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        movement = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        rigidbody.velocity = movement.normalized * spd;

        if (playerData.healthPoint < 0f)
            Debug.Log("Game over");
    }
}
