using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Класс CameraMoving, наследующий MonoBehaviour, управляет движением камеры в игре.
public class CameraMoving : MonoBehaviour
{
    // Клавиша для переключения поведения камеры.
    public KeyCode toggleKey = KeyCode.Q;

    // Флаг, указывающий, активно ли поведение камеры.
    public bool isActive = true;

    // Коэффициент сглаживания движения камеры.
    public float dumping = 1.5f;

    // Смещение камеры относительно игрока.
    public Vector2 offset = new Vector2(2f, 2f);

    // Флаг, указывающий, находится ли игрок слева.
    public bool isLeft;

    // Трансформ игрока.
    private Transform player;

    // Последняя известная позиция игрока по оси X.
    private int lastX;

    // Границы движения камеры.
    [SerializeField] float leftLimit;
    [SerializeField] float rightLimit;
    [SerializeField] float upLimit;
    [SerializeField] float downLimit;

    // Метод Start вызывается при инициализации объекта.
    private void Start()
    {
        // Устанавливаем абсолютное значение для смещения по оси X.
        offset = new Vector2(Mathf.Abs(offset.x), offset.y);

        // Находим игрока и устанавливаем начальную позицию камеры.
        FindPlayer(isLeft);
    }

    // Метод для поиска игрока и установки начальной позиции камеры.
    public void FindPlayer(bool playerIsLeft)
    {
        // Находим игрока по тегу.
        player = GameObject.FindGameObjectWithTag("Player").transform;

        // Запоминаем последнюю известную позицию игрока по оси X.
        lastX = Mathf.RoundToInt(player.position.x);

        // Устанавливаем начальную позицию камеры в зависимости от направления игрока.
        if (playerIsLeft)
        {
            transform.position = new Vector3(player.position.x - offset.x, player.position.y + offset.y, transform.position.z);
        }
        else
        {
            transform.position = new Vector3(player.position.x + offset.x, player.position.y + offset.y, transform.position.z);
        }
    }

    // Метод Update вызывается каждый кадр.
    private void Update()
    {
        // Если поведение камеры активно.
        if (isActive)
        {
            // Если игрок найден.
            if (player)
            {
                // Определяем текущую позицию игрока по оси X.
                int currentX = Mathf.RoundToInt(player.position.x);

                // Определяем направление движения игрока.
                if (currentX > lastX)
                {
                    isLeft = false;
                }
                else if (currentX < lastX)
                {
                    isLeft = true;
                }

                // Обновляем последнюю известную позицию игрока по оси X.
                lastX = Mathf.RoundToInt(player.position.x);

                // Определяем целевую позицию камеры в зависимости от направления игрока.
                Vector3 target;
                if (isLeft)
                {
                    target = new Vector3(player.position.x - offset.x, player.position.y + offset.y, transform.position.z);
                }
                else
                {
                    target = new Vector3(player.position.x + offset.x, player.position.y + offset.y, transform.position.z);
                }

                // Плавно перемещаем камеру к целевой позиции в зависимости от состояния игрока.
                if (Hero.Instance.IsRunning == true)
                {
                    Vector3 currentPosition = Vector3.Lerp(transform.position, target, dumping * 2f * Time.deltaTime);
                    transform.position = currentPosition;
                }
                if (Hero.Instance.IsRunning == false)
                {
                    Vector3 currentPosition = Vector3.Lerp(transform.position, target, dumping * Time.deltaTime);
                    transform.position = currentPosition;
                }
            }

            // Ограничиваем позицию камеры в пределах заданных границ.
            transform.position = new Vector3(Mathf.Clamp(transform.position.x, leftLimit, rightLimit), Mathf.Clamp(transform.position.y, downLimit, upLimit), transform.position.z);

            // Переключаем поведение камеры при нажатии заданной клавиши.
            if (Input.GetKeyDown(toggleKey))
            {
                ToggleBehavior();
            }
        }
    }

    // Метод для переключения поведения камеры.
    public void ToggleBehavior()
    {
        // Деактивируем текущее поведение камеры.
        isActive = false;
        enabled = false;

        // Находим и активируем второй скрипт для управления камерой.
        CameraMovingTest secondBehavior = GetComponent<CameraMovingTest>();
        if (secondBehavior != null)
        {
            secondBehavior.enabled = true;
            secondBehavior.StartBehavior();
        }
    }
}
