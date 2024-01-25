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
            Instantiate(projectileObj, transform.position, Quaternion.identity);

            yield return new WaitForSeconds(fireRate);
        }
    }
}
