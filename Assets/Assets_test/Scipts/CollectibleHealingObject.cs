using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleHealingObject : MonoBehaviour
{
    // Start is called before the first frame update
    public int healingAmount = 10;
    public AudioClip collectedClip;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        { 
            RubyController player = collision.GetComponent<RubyController>();

            if (player.currentHP < player.MaxHP)
            {
                //this.gameObject.SetActive(false);
                player.ChangeHP(healingAmount);
                player.PlaySound(collectedClip);
                Destroy(gameObject);
            }
        }
    }
}
