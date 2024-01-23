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
    private bool isCurrentlyAttacking = false;
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
            // proto_enemySpawn.Instance.enemies.Remove(this);
            gameObject.SetActive(false);

        }

        Vector2 direction = (currentPos - startPos).normalized;
        bool shouldAttack = !DOTween.IsTweening(transform);

        if (shouldAttack != isCurrentlyAttacking)
        {
            enemyShot.StartAttacking(shouldAttack);
            isCurrentlyAttacking = shouldAttack;
        }

        // anim.SetFloat("moveX", direction.x);
        // anim.SetFloat("moveY", direction.y);

    }

    public void PlayNPCAnimation(string animationName)
    {
        UnityEngine.Debug.Log(animationName + " animation played");
        anim.Play(animationName);
    }

    public void PlayNPCAnimation(float value)
    {
        UnityEngine.Debug.Log(value + " animation played");
        anim.SetFloat("moveX", value);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<proto_projectile>(out proto_projectile projectile))
        {
            enemyHealthPoint -= projectile.dmg;
        }
    }
}
