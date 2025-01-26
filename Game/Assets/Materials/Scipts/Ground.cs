using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Класс Ground, наследующий MonoBehaviour, управляет взаимодействием с землей в игре.
public class Ground : MonoBehaviour
{
    // Метод Start вызывается перед первым обновлением кадра.
    void Start()
    {
        // В данном случае метод пуст, но он может быть использован для инициализации.
    }

    // Метод Update вызывается каждый кадр.
    void Update()
    {
        // В данном случае метод пуст, но он может быть использован для обновления состояния.
    }

    // Метод OnCollisionStay2D вызывается, когда объект остается в контакте с другим объектом.
    void OnCollisionStay2D(Collision2D collision)
    {
        // Если игрок не мертв.
        if (PhisKey.IsDead != true)
        {
            // Если объект, с которым происходит контакт, является игроком.
            if (collision.gameObject == Hero.Instance.gameObject)
            {
                // Если игрок не находится на земле и не находится на мобах.
                if (Hero.Instance.IsGrounded == false && Hero.Instance.IsGroundedOnMobs == false)
                {
                    // Устанавливаем флаг Crutch в true(Crutch дословно переводится как костыль. У меня тогда стул немножко возгорел от прекрасного движка unity и его взаимодействия с объектами).
                    Hero.Instance.Crutch = true;
                }

                // Если игрок находится на земле или на мобах.
                if (Hero.Instance.IsGrounded == true || Hero.Instance.IsGroundedOnMobs == true)
                {
                    // Устанавливаем флаг Crutch в false.
                    Hero.Instance.Crutch = false;
                }
            }
        }
    }
}
