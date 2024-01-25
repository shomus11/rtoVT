using System.Collections;
using UnityEngine;

public class PlayerShot_Controller : MonoBehaviour
{
    public GameObject projectileObj;
    public float fireRate;
    public Vector2 angle;
    public float damageAmplified;

    // Start is called before the first frame update
    void Start()
    {
        fireRate = 1f;
        StartCoroutine(Shooting());
    }

    // Update is called once per frame
    void Update()
    {

    }

    private IEnumerator Shooting()
    {
        while (true)
        {
            GameObject projectile = ObjectPooler.sharedInstance.GetPooledObject(ObjectPooler.sharedInstance.projectilePooledList);
            if (projectile == null)
            {
                ObjectPooler.sharedInstance.InitSpawnObject(
                    ObjectPooler.sharedInstance.projectilePrefabs,
                    ObjectPooler.sharedInstance.projectilePooledList,
                    ObjectPooler.sharedInstance.projectileAmountToPool
                    );
                projectile = ObjectPooler.sharedInstance.GetPooledObject(ObjectPooler.sharedInstance.projectilePooledList);
            }
            if (projectile != null)
            {
                projectile.transform.position = transform.position;
                angle = transform.localRotation * Vector2.up;
                projectile.transform.rotation = Quaternion.EulerAngles(angle);
                projectile.GetComponent<proto_projectile>().direction = angle.normalized;
                projectile.GetComponent<proto_projectile>().dmgAmp = damageAmplified;
                projectile.SetActive(true);
            }

            yield return new WaitForSeconds(fireRate);
        }
    }
}
