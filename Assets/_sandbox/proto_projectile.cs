using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class proto_projectile : MonoBehaviour
{

    public float moveSpeed;
    public Vector2 direction;
    // Start is called before the first frame update
    void Start()
    {
        // direction = Vector2.up;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(direction * moveSpeed * Time.deltaTime);

        if (transform.position.y > 7f)
            this.gameObject.SetActive(false);
            // Destroy(gameObject);
    }
}
