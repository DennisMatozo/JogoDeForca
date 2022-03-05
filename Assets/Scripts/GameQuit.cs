using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameQuit : MonoBehaviour
{

    public void LoadOnClick(int sceneIndex)
    {
        Application.Quit();    // metodo do Unity para carregar a cena
    }
   
}
