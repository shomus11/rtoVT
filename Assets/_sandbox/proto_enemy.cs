using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using DG.Tweening;

public class proto_enemy : MonoBehaviour
{
    [SerializeField] private Animator anim;
    private Rigidbody2D rb;
    private Vector2 startPos;
    private Vector2 endPos;
    private proto_enemyShot enemyShot;
    public float enemyHealthPoint = 3f;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        enemyShot = GetComponent<proto_enemyShot>();
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 currentPos = transform.position;

        if (enemyHealthPoint < 0f)
        {
            Destroy(gameObject);
        }

        Vector2 direction = (currentPos - startPos).normalized;
        if (!DOTween.IsTweening(transform))
        {
            direction = Vector2.zero;
            return;
        }

        // anim.SetFloat("moveX", direction.x);
        // anim.SetFloat("moveY", direction.y);

    }

    public void PlayNPCAnimation(string animationName)
    {
        UnityEngine.Debug.Log(animationName + " animation played");
        anim.Play(animationName);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<proto_projectile>(out proto_projectile projectile))
        {
            enemyHealthPoint -= projectile.dmg;
        }
    }
}
