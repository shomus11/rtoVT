using DG.Tweening;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    [SerializeField] private Animator anim;
    private Rigidbody2D rb;
    private Vector2 startPos;
    private Vector2 endPos;
    private EnemyShot_Controller enemyShot;
    public float enemyHealthPoint = 3f;
    private bool isCurrentlyAttacking = false;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        enemyShot = GetComponent<EnemyShot_Controller>();
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
    }

    public void PlayNPCAnimation(string animationName)
    {
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
