using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody2D rigidbody;
    [SerializeField] private float speed;
    [SerializeField] private float time = 3.0f;
    public int damageAmount = 1;
    float invincibleTimer;
    float timeInvincible = 1.0f;
    bool isInvincible = false;
    private bool vertical;
    private float timer;
    int direction = 1;
    Animator animator;
    private SpriteRenderer rbSprite;

    public bool broken = true;

    public float timeSW = 0;

    public ParticleSystem smokeEffect;

    public AudioClip hurt;
    public AudioClip fix;
    private AudioSource audioSource;
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        timer = time;
        animator = GetComponent<Animator>();
        rbSprite = GameObject.FindGameObjectWithTag("Player").GetComponent<SpriteRenderer>();

        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (timeSW > 0)
        {
            timeSW -= Time.deltaTime;
            if (timeSW <= 1)
            {
                broken = true;
                rigidbody.simulated = true;
                animator.SetTrigger("Fixed");
                timeSW = 0;
                smokeEffect.Stop();
                audioSource.Play();
                PlaySound(fix);
            }
        }
        timer -= Time.deltaTime;
        if (timer < 0)
        { 
            direction = -direction;
            timer = time;
            int temp = Random.Range(0, 2);
            if (temp == 0)
                vertical = false;
            else
                vertical = true;
        }
        if (!broken)
        {
            return;
        }
        
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 position = rigidbody.position;

        if (vertical)
        {
            position.y +=  speed * Time.deltaTime * direction;
            animator.SetFloat("Move X", 0);
            animator.SetFloat("Move Y", direction);
        }
        else
        {
            position.x +=  speed * Time.deltaTime * direction;
            animator.SetFloat("Move X", direction);
            animator.SetFloat("Move Y", 0);
        }

        rigidbody.MovePosition(position);
        if (!broken)
        {
            return;
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            RubyController player = collision.collider.GetComponent<RubyController>();
            if (isInvincible)
            {
                invincibleTimer -= Time.deltaTime;
                if (invincibleTimer < 0)
                {
                    isInvincible = false;
                }
            }
            else
            {
                //this.gameObject.SetActive(false);
                player.ChangeHP(-damageAmount);
                player.PlaySound(hurt);
                rbSprite.color = Color.red;
                invincibleTimer = timeInvincible;
                isInvincible = true;
            }

        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            isInvincible = false;
            rbSprite.color = Color.white;
        }
    }

    public void Fix()
    {
        broken = false;
        rigidbody.simulated = false;
        animator.SetTrigger("Fixed");
        timeSW = 5;
        smokeEffect.Play();
        StopSound();
    }

    public void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }

    public void StopSound()
    {
        audioSource.Stop();
    }
}
