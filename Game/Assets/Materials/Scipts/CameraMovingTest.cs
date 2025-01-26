using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovingTest : MonoBehaviour
{
    public KeyCode toggleKey = KeyCode.Q; // ������� ��� ������������
    private bool isActive = false; //���������� ������ �����
    public float moveSpeed = 5f; // �������� �������� ������
    public Vector2 minBounds; // ����������� ������� ��������
    public Vector2 maxBounds; // ������������ ������� ��������
    public bool IsRunning = false; //���������� �� ��������� ������

    void Update()
    {
        if (isActive)
        {
            IsRunning = Input.GetKey(KeyCode.LeftShift); 
            // �������� ���� � ����������
            float moveHorizontal = Input.GetAxis("CameraHorizontal");
            float moveVertical = Input.GetAxis("CameraVertical");
            if (IsRunning == true)
            {
                Vector3 movement = new Vector3(moveHorizontal, moveVertical, 0) * moveSpeed * 2 * Time.deltaTime;
                Vector3 newPosition = transform.position + movement;
                newPosition.x = Mathf.Clamp(newPosition.x, minBounds.x, maxBounds.x); //�������� ������ �� �
                newPosition.y = Mathf.Clamp(newPosition.y, minBounds.y, maxBounds.y); //�������� ������ �� y

                transform.position = newPosition; // ����������� ����� �������� ������
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
        enabled = false; // ������������ ���� ������

        // ������� � ���������� ������ ������
        CameraMoving firstBehavior = GetComponent<CameraMoving>();
        if (firstBehavior != null)
        {
            firstBehavior.enabled = true;
            firstBehavior.isActive = true;
        }
    }
}
