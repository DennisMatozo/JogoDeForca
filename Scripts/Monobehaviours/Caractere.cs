using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// abstract indica que a classe n�o pode ser instanciada, e sim herdada
public abstract class Caractere : MonoBehaviour
{
    // public int PontosDano;  // vers�o anterior do valor de "dano"
    
    // public int MaxPontosDano; // vers�o anterior do valor maximo de "dano"
    public float inicioPontosDano;  // valor m�nimo inicial de "sa�de" do Player
    public float MaxPontosDano;     // valor m�ximo permitido de "sa�de" do Player

    public abstract void ResetCaractere();

    /*
     Pinta de vermelho gando recebe dano
     */
    public virtual IEnumerator FlickerCaractere()
    {
        GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(0.1f);
        GetComponent<SpriteRenderer>().color = Color.white;
    }

    public abstract IEnumerator DanoCaractere(int dano, float intervalo);

    /*
     Destroi os caracteres
     */
    public virtual void KillCaractere()
    {
        Destroy(gameObject);
    }
} 
