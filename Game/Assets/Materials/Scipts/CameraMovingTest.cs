using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovingTest : MonoBehaviour
{
    public KeyCode toggleKey = KeyCode.Q; // Клавиша для переключения
    private bool isActive = false; //Изначально скрипт офнут
    public float moveSpeed = 5f; // Скорость движения камеры
    public Vector2 minBounds; // Минимальные границы движения
    public Vector2 maxBounds; // Максимальные границы движения
    public bool IsRunning = false; //ПроверОЧКА на ускорение камеры

    void Update()
    {
        if (isActive)
        {
            IsRunning = Input.GetKey(KeyCode.LeftShift); 
            // Получаем ввод с клавиатуры
            float moveHorizontal = Input.GetAxis("CameraHorizontal");
            float moveVertical = Input.GetAxis("CameraVertical");
            if (IsRunning == true)
            {
                Vector3 movement = new Vector3(moveHorizontal, moveVertical, 0) * moveSpeed * 2 * Time.deltaTime;
                Vector3 newPosition = transform.position + movement;
                newPosition.x = Mathf.Clamp(newPosition.x, minBounds.x, maxBounds.x); //Движение камеры по х
                newPosition.y = Mathf.Clamp(newPosition.y, minBounds.y, maxBounds.y); //Движение камеры по y

                transform.position = newPosition; // Присваиваем новое значение камере
            }
            if (IsRunning == false)
            {
                Vector3 movement = new Vector3(moveHorizontal, moveVertical, 0) * moveSpeed * Time.deltaTime;
                Vector3 newPosition = transform.position + movement;
                newPosition.x = Mathf.Clamp(newPosition.x, minBounds.x, maxBounds.x);
                newPosition.y = Mathf.Clamp(newPosition.y, minBounds.y, maxBounds.y);

                transform.position = newPosition;
            }
      
            if (Input.GetKeyDown(toggleKey))
            {
                ToggleBehavior();
            }
        }
    }
    public void StartBehavior()
    {
        isActive = true;
    }

    public void ToggleBehavior()
    {
        isActive = false;
        enabled = false; // Деактивируем этот скрипт

        // Находим и активируем первый скрипт
        CameraMoving firstBehavior = GetComponent<CameraMoving>();
        if (firstBehavior != null)
        {
            firstBehavior.enabled = true;
            firstBehavior.isActive = true;
        }
    }
}
