using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ����� Parallax, ����������� MonoBehaviour, ��������� �������� ���������� � ����.
public class Parallax : MonoBehaviour
{
    // ����, �� ������� ������� ������ � �����������.
    [SerializeField] Transform followingTarget;

    // ���� ������� ���������� (�� 0 �� 1).
    [SerializeField, Range(0f, 1f)] float parallaxStrenght = 0.1f;

    // ���� ��� ���������� ������������� ����������.
    [SerializeField] bool disableVerticalParallax;

    // ���������� ������� ����.
    Vector3 targetPreviosPosition;

    // ����� Start ���������� ��� ������������� �������.
    void Start()
    {
        // ���� ���� �� �����������, ���������� �������� ������.
        if (!followingTarget)
        {
            followingTarget = Camera.main.transform;
        }

        // ��������� ��������� ������� ����.
        targetPreviosPosition = followingTarget.position;
    }

    // ����� Update ���������� ������ ����.
    private void Update()
    {
        // ��������� ��������� ������� ����.
        var delta = followingTarget.position - targetPreviosPosition;

        // ���� ������������ ��������� ��������, �������� ��������� �� ��� Y.
        if (disableVerticalParallax)
        {
            delta.y = 0;
        }

        // ��������� ���������� ������� ����.
        targetPreviosPosition = followingTarget.position;

        // ��������� ��������� ������� � ������� � �����������, ���������� �� ���� ����������.
        transform.position += delta * parallaxStrenght;
    }
}
