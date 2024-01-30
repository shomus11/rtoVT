using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

[System.Serializable]
public enum PowerUp
{
    fireRate,
    damageAmplifier,
    moreBullet
}

public class PlayerController : MonoBehaviour
{

    Rigidbody2D rb;
    Vector2 movement;
    float spd;
    float hp;
    public PlayerData playerData;
    public TextMeshProUGUI debugTxt;
    private Animator anim;

    public List<PlayerShot_Controller> gunList;

    // Start is called before the first frame update
    void Start()
    {
        PlayerShot_Controller[] proto_Shoots = gameObject.GetComponentsInChildren<PlayerShot_Controller>();
        gunList = proto_Shoots.ToList();


        rb = GetComponent<Rigidbody2D>();
        spd = playerData.moveSpeed;
        hp = playerData.healthPoint;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.gameState == GameStates.Gameplay)
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
    }


    public void UpgradePowers(PowerUp powerUp, float value = 0)
    {
        switch (powerUp)
        {
            case PowerUp.fireRate:
                UpgradeFireRate(value);
                break;

            case PowerUp.damageAmplifier:
                UpgradeDamage(value);
                break;

            case PowerUp.moreBullet:

                break;

            default:
                break;
        }
    }

    public void UpgradeFireRate(float value)
    {
        for (int i = 0; i < gunList.Count; i++)
        {
            gunList[i].fireRate -= value;
        }
    }
    public void UpgradeDamage(float value)
    {
        for (int i = 0; i < gunList.Count; i++)
        {
            gunList[i].damageAmplified += value;
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<EnemyProjectile>(out EnemyProjectile enemy))
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
