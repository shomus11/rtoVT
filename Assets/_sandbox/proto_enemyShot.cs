using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class proto_enemyShot : MonoBehaviour
{
    public GameObject projectileObj;
    public float fireRate;
    public Vector2 angle;
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
            Instantiate(projectileObj, transform.position, Quaternion.identity);

            // GameObject projectile = proto_objPool.Instance.GetPooledObj();
            // if (projectile != null)
            // {
            //     projectile.transform.position = transform.position;
            //     angle = transform.localRotation * Vector2.down;
            //     projectile.GetComponent<proto_projectile>().direction = angle.normalized;
            //     projectile.SetActive(true);
            // }

            yield return new WaitForSeconds(fireRate);
        }
    }
}
