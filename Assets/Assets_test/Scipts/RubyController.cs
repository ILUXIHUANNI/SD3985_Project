using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubyController : MonoBehaviour
{
    public float speed;
    private Rigidbody2D rb;
    private float horizontal;
    private float vertical;
    [SerializeField] private int HP = 100;
    public int currentHP { get => HP; }
    public int MaxHP = 100;
    Animator animator;
    Vector2 lookDirection = new Vector2(1, 0);

    public GameObject bullet;

    private AudioSource audioSource;
    public AudioClip throwCog;

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        animator = this.GetComponent<Animator>();

        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        Vector2 move = new Vector2(horizontal, vertical);

        if (!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f))
        {
            lookDirection.Set(move.x, move.y);
            lookDirection.Normalize();
        }

        animator.SetFloat("Look X", lookDirection.x);
        animator.SetFloat("Look Y", lookDirection.y);
        animator.SetFloat("Speed", move.magnitude);

        if (Input.GetKeyDown(KeyCode.C))
        {
            Launch();
        }

        /*if (Input.GetKeyDown(KeyCode.X))
        {
            RaycastHit2D hit = Physics2D.Raycast(rb.position + Vector2.up * 0.2f, lookDirection, 1.5f, LayerMask.GetMask("NPC"));
            if (hit.collider != null)
            {
                NonPlayerCharacter character = hit.collider.GetComponent<NonPlayerCharacter>();
                if (character != null)
                {
                    character.DisplayDialog();
                }
            }
        }*/
    }

    private void FixedUpdate()
    {
        Vector2 positionToMove = new Vector2(horizontal, vertical) * speed * Time.deltaTime;
        Vector2 newPos = (Vector2)transform.position + positionToMove;
        rb.MovePosition(newPos);
    }

    public void ChangeHP(int value)
    {
        HP += value;
        HP = Mathf.Clamp(HP, 0, MaxHP); //1¡Gobject, 2:min 3:max
        UIHealthBar.instance.SetValue(currentHP / (float)MaxHP);
    }

    void Launch()
    {
        GameObject projectileObject = Instantiate(bullet, rb.position + Vector2.up * 0.5f, Quaternion.identity);
        print("a " + projectileObject.name);
        Projectile projectile = projectileObject.GetComponent<Projectile>();
        projectile.Launch(lookDirection, 300);

        animator.SetTrigger("Launch");
        PlaySound(throwCog);
    }

    public void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }
}
