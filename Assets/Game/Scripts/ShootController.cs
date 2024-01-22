using System.Collections;
using UnityEngine;

public class ShootController : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float fireRate;

    public IEnumerator Shoot()
    {
        while (true)
        {
            GameObject projectile = ObjectPooler.sharedInstance.GetPooledObject(ObjectPooler.sharedInstance.projectilePooledList);
            if (projectile != null)
            {
                ObjectPooler.sharedInstance.InitSpawnObject(
                    ObjectPooler.sharedInstance.projectilePrefabs,
                    ObjectPooler.sharedInstance.projectilePooledList,
                    ObjectPooler.sharedInstance.projectileAmountToPool
                    );

            }
            yield return null;
        }
    }
}
