using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ����� Spikes, ����������� MonoBehaviour, ��������� ���������� ����� � ����.
public class Spikes : MonoBehaviour
{
    // ��������� SpriteRenderer ��� ���������� ��������.
    public SpriteRenderer sprite;

    // ������ �� ���������� BoxCollider2D.
    public BoxCollider2D boxCollider1;
    public BoxCollider2D boxCollider2;
    public BoxCollider2D boxCollider3;
    public BoxCollider2D boxCollider4;

    // ����, �����������, ��������� �� ������ � ��������� ���.
    private bool IsSleep;

    // ��������� Animator ��� ���������� ����������.
    private Animator anim;

    // ��� ���������� �������.
    private string lastSprite;

    // ������� ������.
    Sprite currentSprite;

    // ����� Start ���������� ����� ������ ����������� �����.
    void Start()
    {
        // �������� ���������� Animator � SpriteRenderer.
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();

        // ��������� ��� BoxCollider2D.
        boxCollider1.enabled = false;
        boxCollider2.enabled = false;
        boxCollider3.enabled = false;
        boxCollider4.enabled = false;

        // ������������� ��������� ��������.
        IsSleep = false;
        currentSprite = sprite.sprite;
        lastSprite = currentSprite.name;
    }

    // ����� Update ���������� ������ ����.
    void Update()
    {
        // ���� ������� ������ ���������.
        if (currentSprite.name != lastSprite)
        {
            lastSprite = currentSprite.name;
        }

        // ��������� ������� ������.
        currentSprite = sprite.sprite;

        // ���� ������ �� ��������� � ��������� ���.
        if (IsSleep == false)
        {
            // ������������� ���������� �������� ��������.
            anim.speed = 1;

            // �������� ��������� ����������� � ����������� �� �������� �������.
            ChangeCollider(currentSprite, lastSprite);
        }

        // ���� ������ ��������� � ��������� ���.
        if (IsSleep == true)
        {
            // ������������� ��������.
            anim.speed = 0;
        }
    }

    // �������� ��� �������� ������� � ��������� ���.
    private IEnumerator Sleep()
    {
        yield return new WaitForSeconds(2f);
        IsSleep = false;
    }

    // ����� ��� ��������� ��������� ����������� � ����������� �� �������� �������.
    private void ChangeCollider(Sprite currentSprite, string lastSprite)
    {
        // � ����������� �� ����� �������� ������� �������� ��� ��������� ��������������� ����������.
        if (currentSprite.name == "Spikes_0" && lastSprite == "Spikes_7")
        {
            boxCollider1.enabled = false;
            IsSleep = true;
            StartCoroutine(Sleep());
        }
        if (currentSprite.name == "Spikes_1")
        {
            boxCollider1.enabled = true;
        }
        if (currentSprite.name == "Spikes_2")
        {
            boxCollider1.enabled = false;
            boxCollider2.enabled = true;
        }
        if (currentSprite.name == "Spikes_3")
        {
            boxCollider2.enabled = false;
            boxCollider3.enabled = true;
        }
        if (currentSprite.name == "Spikes_4")
        {
            boxCollider3.enabled = false;
            boxCollider4.enabled = true;
        }
        if (currentSprite.name == "Spikes_5")
        {
            boxCollider4.enabled = false;
            boxCollider3.enabled = true;
        }
        if (currentSprite.name == "Spikes_6")
        {
            boxCollider3.enabled = false;
            boxCollider2.enabled = true;
        }
        if (currentSprite.name == "Spikes_7")
        {
            boxCollider2.enabled = false;
            boxCollider1.enabled = true;
        }
    }

    // ����� ���������� ��� ������������ � ������ ��������.
    private void OnCollisionStay2D(Collision2D collision)
    {
        // ���� ������������ ��������� � ������� � ����� �� ��������.
        if (collision.gameObject == Hero.Instance.gameObject && Hero.Instance.isInvicible == false)
        {
            // ����� �������� ����.
            Hero.Instance.GetDamage();
        }
    }
}
