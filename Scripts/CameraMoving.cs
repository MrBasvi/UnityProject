using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ����� CameraMoving, ����������� MonoBehaviour, ��������� ��������� ������ � ����.
public class CameraMoving : MonoBehaviour
{
    // ������� ��� ������������ ��������� ������.
    public KeyCode toggleKey = KeyCode.Q;

    // ����, �����������, ������� �� ��������� ������.
    public bool isActive = true;

    // ����������� ����������� �������� ������.
    public float dumping = 1.5f;

    // �������� ������ ������������ ������.
    public Vector2 offset = new Vector2(2f, 2f);

    // ����, �����������, ��������� �� ����� �����.
    public bool isLeft;

    // ��������� ������.
    private Transform player;

    // ��������� ��������� ������� ������ �� ��� X.
    private int lastX;

    // ������� �������� ������.
    [SerializeField] float leftLimit;
    [SerializeField] float rightLimit;
    [SerializeField] float upLimit;
    [SerializeField] float downLimit;

    // ����� Start ���������� ��� ������������� �������.
    private void Start()
    {
        // ������������� ���������� �������� ��� �������� �� ��� X.
        offset = new Vector2(Mathf.Abs(offset.x), offset.y);

        // ������� ������ � ������������� ��������� ������� ������.
        FindPlayer(isLeft);
    }

    // ����� ��� ������ ������ � ��������� ��������� ������� ������.
    public void FindPlayer(bool playerIsLeft)
    {
        // ������� ������ �� ����.
        player = GameObject.FindGameObjectWithTag("Player").transform;

        // ���������� ��������� ��������� ������� ������ �� ��� X.
        lastX = Mathf.RoundToInt(player.position.x);

        // ������������� ��������� ������� ������ � ����������� �� ����������� ������.
        if (playerIsLeft)
        {
            transform.position = new Vector3(player.position.x - offset.x, player.position.y + offset.y, transform.position.z);
        }
        else
        {
            transform.position = new Vector3(player.position.x + offset.x, player.position.y + offset.y, transform.position.z);
        }
    }

    // ����� Update ���������� ������ ����.
    private void Update()
    {
        // ���� ��������� ������ �������.
        if (isActive)
        {
            // ���� ����� ������.
            if (player)
            {
                // ���������� ������� ������� ������ �� ��� X.
                int currentX = Mathf.RoundToInt(player.position.x);

                // ���������� ����������� �������� ������.
                if (currentX > lastX)
                {
                    isLeft = false;
                }
                else if (currentX < lastX)
                {
                    isLeft = true;
                }

                // ��������� ��������� ��������� ������� ������ �� ��� X.
                lastX = Mathf.RoundToInt(player.position.x);

                // ���������� ������� ������� ������ � ����������� �� ����������� ������.
                Vector3 target;
                if (isLeft)
                {
                    target = new Vector3(player.position.x - offset.x, player.position.y + offset.y, transform.position.z);
                }
                else
                {
                    target = new Vector3(player.position.x + offset.x, player.position.y + offset.y, transform.position.z);
                }

                // ������ ���������� ������ � ������� ������� � ����������� �� ��������� ������.
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

            // ������������ ������� ������ � �������� �������� ������.
            transform.position = new Vector3(Mathf.Clamp(transform.position.x, leftLimit, rightLimit), Mathf.Clamp(transform.position.y, downLimit, upLimit), transform.position.z);

            // ����������� ��������� ������ ��� ������� �������� �������.
            if (Input.GetKeyDown(toggleKey))
            {
                ToggleBehavior();
            }
        }
    }

    // ����� ��� ������������ ��������� ������.
    public void ToggleBehavior()
    {
        // ������������ ������� ��������� ������.
        isActive = false;
        enabled = false;

        // ������� � ���������� ������ ������ ��� ���������� �������.
        CameraMovingTest secondBehavior = GetComponent<CameraMovingTest>();
        if (secondBehavior != null)
        {
            secondBehavior.enabled = true;
            secondBehavior.StartBehavior();
        }
    }
}
