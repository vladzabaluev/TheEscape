using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        CharacterStats charStats = collision.gameObject.GetComponent<CharacterStats>();
        if(charStats!=null)
        {
            charStats.TakeDamage(charStats.maxHealth);
        }    
    }
}
