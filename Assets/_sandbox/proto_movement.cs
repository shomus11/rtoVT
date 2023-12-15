using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class proto_movement : MonoBehaviour
{

    Rigidbody2D rigidbody;
    Vector2 movement;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        movement = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        rigidbody.velocity = movement.normalized * 10f;
    }
}
