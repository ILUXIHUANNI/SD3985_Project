using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageZone : MonoBehaviour
{
    // Start is called before the first frame update
    public int damageAmount = 1;
    float invincibleTimer;
    float timeInvincible = 1.0f;
    bool isInvincible = false;
    private SpriteRenderer rbSprite;

    public AudioClip hurt;

    private void Start()
    {
        rbSprite = GameObject.FindGameObjectWithTag("Player").GetComponent<SpriteRenderer>();
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            RubyController player = collision.GetComponent<RubyController>();
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

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isInvincible = false;
            rbSprite.color = Color.white;
        }
    }
}
