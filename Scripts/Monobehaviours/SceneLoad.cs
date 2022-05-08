using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoad : MonoBehaviour
{
    /*
     *  M�todo que passa parametro inteiro para LoadScene referente
     *  � pagina (cena) desejada
     */
    public void LoadOnClick()
    {
        SceneManager.LoadScene(2);    // metodo do Unity para carregar a cena
    }
}
