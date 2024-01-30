using System.Collections;
using UnityEngine;

public class EnemyShot_Controller : MonoBehaviour
{
    public GameObject projectileObj;
    public float fireRate;
    public Vector2 angle;
    // Start is called before the first frame update


    void Start()
    {

    }

    public void StartAttacking(bool isAttacking)
    {
        if (isAttacking)
        {
            fireRate = 1f;
            StartCoroutine(Shooting());
        }
        else
        {
            StopCoroutine(Shooting());
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    private IEnumerator Shooting()
    {
        while (true)
        {
            if (GameManager.instance.gameState == GameStates.Gameplay)
            {
                GameObject projectile = ObjectPooler.sharedInstance.GetPooledObject(ObjectPooler.sharedInstance.EnemyProjectilePooledList);
                if (projectile == null)
                {
                    ObjectPooler.sharedInstance.InitSpawnObject(
                        ObjectPooler.sharedInstance.EnemyProjectilePrefabs,
                        ObjectPooler.sharedInstance.EnemyProjectilePooledList,
                        ObjectPooler.sharedInstance.EnemyProjectileAmountToPool,
                        ObjectPooler.sharedInstance.EnemyProjectileContainer
                        );
                    projectile = ObjectPooler.sharedInstance.GetPooledObject(ObjectPooler.sharedInstance.EnemyProjectilePooledList);
                }
                if (projectile != null)
                {
                    projectile.transform.position = transform.position;
                    projectile.transform.rotation = Quaternion.EulerAngles(transform.localRotation * Vector2.down);
                    projectile.SetActive(true);
                }
            }
            yield return new WaitForSeconds(fireRate);
        }
    }
}
