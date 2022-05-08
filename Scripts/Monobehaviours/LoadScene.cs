using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    /*
     *  Método que passa parametro inteiro para LoadScene referente
     *  á pagina (cena) desejada
     */
    public void LoadOnClick(int cena)
    {
        SceneManager.LoadScene(cena);    // metodo do Unity para carregar a cena
    }
}

