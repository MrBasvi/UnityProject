using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Класс Parallax, наследующий MonoBehaviour, управляет эффектом параллакса в игре.
public class Parallax : MonoBehaviour
{
    // Цель, за которой следует объект с параллаксом.
    [SerializeField] Transform followingTarget;

    // Сила эффекта параллакса (от 0 до 1).
    [SerializeField, Range(0f, 1f)] float parallaxStrenght = 0.1f;

    // Флаг для отключения вертикального параллакса.
    [SerializeField] bool disableVerticalParallax;

    // Предыдущая позиция цели.
    Vector3 targetPreviosPosition;

    // Метод Start вызывается при инициализации объекта.
    void Start()
    {
        // Если цель не установлена, используем основную камеру.
        if (!followingTarget)
        {
            followingTarget = Camera.main.transform;
        }

        // Сохраняем начальную позицию цели.
        targetPreviosPosition = followingTarget.position;
    }

    // Метод Update вызывается каждый кадр.
    private void Update()
    {
        // Вычисляем изменение позиции цели.
        var delta = followingTarget.position - targetPreviosPosition;

        // Если вертикальный параллакс отключен, обнуляем изменение по оси Y.
        if (disableVerticalParallax)
        {
            delta.y = 0;
        }

        // Обновляем предыдущую позицию цели.
        targetPreviosPosition = followingTarget.position;

        // Применяем изменение позиции к объекту с параллаксом, умноженное на силу параллакса.
        transform.position += delta * parallaxStrenght;
    }
}
