using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyCombat : MonoBehaviour
{
    public float maxHealth=50f, damage=10f;
    public GameObject healthBar;
    private float curHealth, fill, last_damage_time;
    private GameObject bar;
    private Image hpScale;
    // Start is called before the first frame update
    void Start()
    {
        curHealth = maxHealth;
        bar = Instantiate(healthBar, transform.position, transform.rotation);
        bar.transform.localScale = bar.transform.localScale * 0.3f;
        bar.transform.position = new Vector2(transform.position.x, transform.position.y + 1.5f);
        fill = 1f;
        last_damage_time = Time.time;
        Transform _tmp = bar.transform.Find("Bar");
        hpScale = _tmp.GetComponent(typeof(Image)) as Image;

    }

    private void Update()
    {
        if (curHealth <= 0f)
        {
            Destroy(gameObject);
            Destroy(bar);
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if(bar.transform.hasChanged)
        bar.transform.position = new Vector2(transform.position.x, transform.position.y + 1.5f);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        PlayerHealth player = collision.gameObject.GetComponent<PlayerHealth>();
        if (player!= null && Time.time > (last_damage_time + 2))
        {
            curHealth -= player.damage;
            fill = curHealth / maxHealth;
            hpScale.fillAmount = fill;
            last_damage_time = Time.time;
        }
    }
}
