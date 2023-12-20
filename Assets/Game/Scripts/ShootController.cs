using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootController : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float fireRate;

    public IEnumerator Shoot()
    {
        while (true)
        {
            GameObject projectile = ObjectPooler.Instance.GetPooledObj();
            if (projectile != null)
            {
                
            }
            yield return null;
        }
    }
}
