using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LevelSelector : MonoBehaviour
{
    //������ �������� ����, ������ ����������� -_-
    public void Select(int NumberOnBuild)
    {
        SceneManager.LoadScene(NumberOnBuild);
    }
}
