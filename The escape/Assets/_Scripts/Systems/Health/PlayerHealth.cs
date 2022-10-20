using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public Image bar;
    private float fill, curHealth;
    public float maxHealth = 100f, damage = 20f;
    float last_damage_time;


    // Start is called before the first frame update
    void Start()
    {
        curHealth = maxHealth;
        fill = 1f;
        last_damage_time = Time.time;
    }

    public void PlayerDeath()
    {
        Debug.Log("Game Over");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    // Update is called once per frame
    void Update()
    {
       if (curHealth<= 0f)
        {
            PlayerDeath();
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        EnemyCombat enemy = collision.gameObject.GetComponent<EnemyCombat>();
        if (enemy!=null && Time.time > (last_damage_time + 2))
        {
            curHealth -= enemy.damage;
            fill = curHealth / maxHealth;
            bar.fillAmount = fill;
            last_damage_time = Time.time;
        }
    }
}
