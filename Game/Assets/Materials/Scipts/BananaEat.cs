using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BananaEat : Sounds
{
    //���� �������� ������ � ������ �������������, �� ����� ��������, �� �������������, ��� ����� ������������
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Banana"))
        {
            PlaySound(sounds[0]);
            Hero.Instance.PlusHealth();
            Destroy(collision.gameObject);
        }
    }
}
