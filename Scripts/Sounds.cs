using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ����� Sounds, ����������� MonoBehaviour, ��������� ���������������� ������ � ����.
public class Sounds : MonoBehaviour
{
    // ������ �������� ������.
    public AudioClip[] sounds;

    // �������� ��� ��������� ���������� AudioSource.
    private AudioSource audioSrc => GetComponent<AudioSource>();

    // ����� ��� ��������������� �����.
    public void PlaySound(AudioClip clip, bool destroyed = false, float volume = 1f)
    {
        // ���� ���� ������ ���� ������������� ��� ������������ ������.
        if (destroyed)
        {
            // ������������� ���� � ��������� ������� � �������� ����������.
            AudioSource.PlayClipAtPoint(clip, transform.position, volume);
        }
        else
        {
            // ������������� ���� ����� ��������� AudioSource � �������� ����������.
            audioSrc.PlayOneShot(clip, volume);
        }
    }
}
