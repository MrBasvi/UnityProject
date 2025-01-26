using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ����� Ground, ����������� MonoBehaviour, ��������� ��������������� � ������ � ����.
public class Ground : MonoBehaviour
{
    // ����� Start ���������� ����� ������ ����������� �����.
    void Start()
    {
        // � ������ ������ ����� ����, �� �� ����� ���� ����������� ��� �������������.
    }

    // ����� Update ���������� ������ ����.
    void Update()
    {
        // � ������ ������ ����� ����, �� �� ����� ���� ����������� ��� ���������� ���������.
    }

    // ����� OnCollisionStay2D ����������, ����� ������ �������� � �������� � ������ ��������.
    void OnCollisionStay2D(Collision2D collision)
    {
        // ���� ����� �� �����.
        if (PhisKey.IsDead != true)
        {
            // ���� ������, � ������� ���������� �������, �������� �������.
            if (collision.gameObject == Hero.Instance.gameObject)
            {
                // ���� ����� �� ��������� �� ����� � �� ��������� �� �����.
                if (Hero.Instance.IsGrounded == false && Hero.Instance.IsGroundedOnMobs == false)
                {
                    // ������������� ���� Crutch � true(Crutch �������� ����������� ��� �������. � ���� ����� ���� �������� �������� �� ����������� ������ unity � ��� �������������� � ���������).
                    Hero.Instance.Crutch = true;
                }

                // ���� ����� ��������� �� ����� ��� �� �����.
                if (Hero.Instance.IsGrounded == true || Hero.Instance.IsGroundedOnMobs == true)
                {
                    // ������������� ���� Crutch � false.
                    Hero.Instance.Crutch = false;
                }
            }
        }
    }
}
