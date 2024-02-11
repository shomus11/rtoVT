using UnityEngine;

public class ExplosionBehavior : MonoBehaviour
{

    public float audioLength;
    Animator anim;
    AudioSource audioSource;
    Vector3 startScale;
    // Start is called before the first frame update
    void Start()
    {
    }

    private void Awake()
    {
        startScale = gameObject.transform.localScale;
    }
    public void initExplosion()
    {
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        audioLength = audioSource.clip.length + 0.05f;
        audioSource.PlayOneShot(audioSource.clip);
    }
    // Update is called once per frame
    void Update()
    {
        audioLength -= Time.deltaTime;
        if (audioLength <= 0)
        {
            gameObject.SetActive(false);
            gameObject.transform.localScale = startScale;
        }
    }
}
