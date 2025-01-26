using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Класс Sounds, наследующий MonoBehaviour, управляет воспроизведением звуков в игре.
public class Sounds : MonoBehaviour
{
    // Массив звуковых клипов.
    public AudioClip[] sounds;

    // Свойство для получения компонента AudioSource.
    private AudioSource audioSrc => GetComponent<AudioSource>();

    // Метод для воспроизведения звука.
    public void PlaySound(AudioClip clip, bool destroyed = false, float volume = 1f)
    {
        // Если звук должен быть воспроизведен как уничтоженный объект.
        if (destroyed)
        {
            // Воспроизводит звук в указанной позиции с заданной громкостью.
            AudioSource.PlayClipAtPoint(clip, transform.position, volume);
        }
        else
        {
            // Воспроизводит звук через компонент AudioSource с заданной громкостью.
            audioSrc.PlayOneShot(clip, volume);
        }
    }
}
