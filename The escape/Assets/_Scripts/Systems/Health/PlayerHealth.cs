using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public Image bar;
    public float fill;
    private float last_damage_time;
    
    private void Start()
    {
        fill = 1f;
        last_damage_time = Time.time;
    }

    private void Update()
    {
        if (fill<=0f)
        {
            Debug.Log("Game Over");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Obstacle")
        {
            Debug.Log("Game Over");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        else if (collision.gameObject.tag == "Enemy" && Time.time > (last_damage_time + 2))
        {
            fill -= 0.34f;
            bar.fillAmount = fill;
            last_damage_time = Time.time;
        }
    }
}