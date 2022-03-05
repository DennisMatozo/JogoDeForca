using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Classe que fará a carga da Cena de acordo com um index
/// PBCJ - Minami
/// </summary>

public class SceneLoader : MonoBehaviour
{
   /*
    *  Método que passa parametro inteiro para LoadScene referente
    *  á pagina (cena) desejada
    */
   public void LoadOnClick(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);    // metodo do Unity para carregar a cena
    }
}
