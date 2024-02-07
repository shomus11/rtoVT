using DG.Tweening;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    [SerializeField] private Animator anim;
    public float scoreMultiplier = 1;
    private Rigidbody2D rb;
    private Vector2 startPos;
    private Vector2 endPos;
    private EnemyShot_Controller enemyShot;
    public float enemyHealthPoint = 3f;
    private bool isCurrentlyAttacking = false;
    Sequence npcSequence;
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
        if (GameManager.instance.gameState == GameStates.Gameplay)
        {
            Vector2 currentPos = transform.position;

            if (enemyHealthPoint < 0f)
            {
                // proto_enemySpawn.Instance.enemies.Remove(this);
                GameManager.instance.AddKillAndScore(scoreMultiplier);
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
    }

    public void MoveNPC(Vector3 startPos, Vector3 endPos, string animation1, string animation2, string animation3)
    {

        if (gameObject.activeSelf)
        {
            npcSequence = DOTween.Sequence();
            float animationDuration = 0.25f;
            float totalAnimationDuration = 0;
            npcSequence.Insert(totalAnimationDuration, transform.DOLocalMove(startPos, animationDuration * 8).From(endPos));
            npcSequence.InsertCallback(totalAnimationDuration, () => PlayNPCAnimation(animation1));
            totalAnimationDuration += animationDuration * 8;
            npcSequence.Insert(totalAnimationDuration, transform.DOLocalMove(endPos, animationDuration * 8).From(startPos));
            npcSequence.InsertCallback(totalAnimationDuration, () => PlayNPCAnimation(animation2));
            totalAnimationDuration += animationDuration * 8;
            npcSequence.InsertCallback(totalAnimationDuration, () => PlayNPCAnimation(animation3));
        }
    }

    public void Paused()
    {
        npcSequence.Pause();
    }

    public void Resume()
    {
        npcSequence.Play();
    }


    public void PlayNPCAnimation(string animationName)
    {
        anim.Play(animationName);
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<ProjectileBehavior>(out ProjectileBehavior projectile))
        {
            enemyHealthPoint -= projectile.dmg;
        }
    }
}
