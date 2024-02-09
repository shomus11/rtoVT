using System.Collections.Generic;
using UnityEngine;

public class HommingBehavior : MonoBehaviour
{
    public float moveSpeed;
    public float dmg;
    public float dmgAmp = 0;

    [SerializeField] List<EnemyBehavior> enemyAround;
    [SerializeField] float radius = 1;
    [SerializeField] bool canExplosion = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.gameState == GameStates.Gameplay)
        {
            if (!canExplosion)
            {
                transform.Translate(Vector2.up * moveSpeed * Time.deltaTime);
            }
            if (transform.position.y > 2.5f)
            {
                canExplosion = true;
            }

            if (canExplosion)
            {
                ExplosionAround();
            }
        }
        //    ExplosionDamage(gameObject.transform.position., radius);
    }
    void ExplosionAround()
    {
        for (int i = 0; i < enemyAround.Count; i++)
        {
            if (enemyAround[i])
                enemyAround[i].DamagedHommingMissile();
        }
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.TryGetComponent<EnemyBehavior>(out EnemyBehavior enemy))
        {
            enemyAround.Add(enemy);
        }
    }


}
