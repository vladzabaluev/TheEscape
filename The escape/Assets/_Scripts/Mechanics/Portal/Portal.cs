using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    private static bool isTeleported = false;
    public bool IsActivePortal = true;

    //Делаем эту системы с переменной чуть более гибкой
    [SerializeField] private float minDistanceToTeleportAgain = 1f;

    //Таким образом не любой объект может стать тем, что мы
    //называем порталом, а только тот, на котором висит этот скрипт(просто лишний раз обезопасиваешь себя от ошибок)
    public Portal anotherPortal;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Нам нужно проверять активен ли другой портал, а не тот, в который мы входим
        if (anotherPortal.IsActivePortal)
        {
            if (!isTeleported)
            {
                collision.gameObject.transform.position = anotherPortal.gameObject.transform.position;
                isTeleported = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (isTeleported)
        {
            if (Vector2.Distance(collision.gameObject.transform.position,
                anotherPortal.gameObject.transform.position) >= minDistanceToTeleportAgain)
            {
                isTeleported = false;
            }
        }
    }
}