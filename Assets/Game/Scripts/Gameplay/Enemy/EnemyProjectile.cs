using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    public float moveSpeed;
    public float dmg;
    public Vector2 direction;
    // Start is called before the first frame update 
    void Start()
    {
        direction = Vector2.down;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(direction * moveSpeed * Time.deltaTime);

        if (transform.position.y < -7f)
            Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<PlayerController>(out PlayerController enemy))
        {
            Destroy(gameObject);
        }
    }
}
