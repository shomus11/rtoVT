using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class proto_enemy : MonoBehaviour
{

    public float enemyHealthPoint = 3f;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyHealthPoint < 0f)
        {
            Destroy(gameObject);
            
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.TryGetComponent<proto_projectile>(out proto_projectile projectile))
        {
            enemyHealthPoint -= projectile.dmg;
        }
    }
}
