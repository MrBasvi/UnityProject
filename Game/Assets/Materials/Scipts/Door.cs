using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// ����� Door, ����������� MonoBehaviour, ��������� ���������� ����� � ����.
public class Door : MonoBehaviour
{
    // ��������� Animator ��� ���������� ���������� �����.
    private Animator anim;

    // ����� Awake ���������� ��� ������������� �������.
    void Awake()
    {
        // �������� ��������� Animator.
        anim = GetComponent<Animator>();
    }

    // �������� ��� ��������� � ��������� ��������� �����.
    private States state
    {
        get { return (States)anim.GetInteger("state"); }
        set { anim.SetInteger("state", (int)value); }
    }

    // ����� OnTriggerEnter2D ���������� ��� ������������ � ������ ��������.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // ���� ������������ ��������� � ��������, ���������� ����� "Player", � ����� �������.
        if (collision.CompareTag("Player") && state == States.Open)
        {
            // ��������� �� ��������� �������.
            NextLevel();
        }
    }

    // ����� Update ���������� ������ ����.
    void Update()
    {
        // ���� ���������� ��������� ����� ����� ���������� �����.
        if (CoinCollect.Instance.money == ChildCounter.Instance.childCount)
        {
            // ��������� �����.
            state = States.Open;
        }
    }

    // ����� ��� �������� �� ��������� �������.
    public void NextLevel()
    {
        // �������� ������� ������ �����.
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        // ��������� ��������� ����� (������� ����� + 1).
        SceneManager.LoadScene(currentSceneIndex + 1);
    }

    // ������������ ��� ��������� �����.
    public enum States
    {
        Close,
        Open
    }
}
