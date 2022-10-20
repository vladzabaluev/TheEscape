using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace _Scripts.Systems.Health
{
    public class PlayerHealth : MonoBehaviour
    {

        public Image bar;
        public float fill;
        float last_damage_time;


        // Start is called before the first frame update
        void Start()
        {
            fill = 1f;
            last_damage_time = Time.time;
        }

        // Update is called once per frame
        void Update()
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
}
