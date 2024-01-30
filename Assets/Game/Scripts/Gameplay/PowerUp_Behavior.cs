using UnityEngine;

public class PowerUp_Behavior : MonoBehaviour
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
        if (GameManager.instance.gameState == GameStates.Gameplay)
        {
            transform.Translate(Vector3.down * moveSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<PlayerController>(out PlayerController player))
        {
            player.UpgradePowers(powerUpType, powerUpsValue);
            this.gameObject.SetActive(false);
        }

    }
}
