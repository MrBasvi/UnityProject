using UnityEngine;

public class ChildCounter : MonoBehaviour
{
    public int childCount;
    public static ChildCounter Instance { get; set; }
    void Awake()
    {
        Instance = this;
        childCount = CountChildren(transform);
    }

    int CountChildren(Transform parent)
    {
        int count = 0;
        foreach (Transform child in parent)
        {
            count++;
            count += CountChildren(child); // Рекурсивно подсчитываем вложенные дочерние объекты
        }
        return count;
    }
}

//▒▒▒▒▒█▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀█
//▒▒▒▒▒█░▒▒▒▒▒▒▒▓▒▒▓▒▒▒▒▒▒▒░█
//▒▒▒▒▒█░▒▒▓▒▒▒▒▒▒▒▒▒▄▄▒▓▒▒░█░▄▄
//▄▀▀▄▄█░▒▒▒▒▒▒▓▒▒▒▒█░░▀▄▄▄▄▄▀░░█
//█░░░░█░▒▒▒▒▒▒▒▒▒▒▒█░░░░░░░░░░░█
//▒▀▀▄▄█░▒▒▒▒▓▒▒▒▓▒█░░░█▒░░░░█▒░░█
//▒▒▒▒▒█░▒▓▒▒▒▒▓▒▒▒█░░░░░░░▀░░░░░█
//▒▒▒▄▄█░▒▒▒▓▒▒▒▒▒▒▒█░░█▄▄█▄▄█░░█
//▒▒▒█░░░█▄▄▄▄▄▄▄▄▄▄█░█▄▄▄▄▄▄▄▄▄█
//▒▒▒█▄▄█░░█▄▄█░░░░░░█▄▄█░░█▄▄█