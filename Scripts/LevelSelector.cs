using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LevelSelector : MonoBehaviour
{
    //просто загрузка сцен, ничего интересного -_-
    public void Select(int NumberOnBuild)
    {
        SceneManager.LoadScene(NumberOnBuild);
    }
}
