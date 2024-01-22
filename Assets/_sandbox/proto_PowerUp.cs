using UnityEngine;

public class proto_PowerUp : MonoBehaviour
{
    public PowerUp powerUpType;
    public float powerUpsValue;

    public float moveSpeed;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * moveSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<proto_movement>(out proto_movement player))
        {
            player.UpgradePowers(powerUpType, powerUpsValue);
            this.gameObject.SetActive(false);
        }

    }
}
