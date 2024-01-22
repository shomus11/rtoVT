using UnityEngine;

public class proto_projectile : MonoBehaviour
{


    public float moveSpeed;
    public float dmg;
    public float dmgAmp = 0;
    public Vector2 direction;
    public bool isPowered;
    // Start is called before the first frame update 
    void Start()
    {
        // direction = Vector2.up;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.up * moveSpeed * Time.deltaTime);

        if (isPowered)
            dmg = 10f + dmgAmp;

        if (transform.position.y > 7f)
            this.gameObject.SetActive(false);
        // Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<proto_enemy>(out proto_enemy enemy))
        {
            this.gameObject.SetActive(false);
        }
    }
}
