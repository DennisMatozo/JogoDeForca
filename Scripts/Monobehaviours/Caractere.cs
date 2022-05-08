using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// abstract indica que a classe não pode ser instanciada, e sim herdada
public abstract class Caractere : MonoBehaviour
{
    // public int PontosDano;  // versão anterior do valor de "dano"
    
    // public int MaxPontosDano; // versão anterior do valor maximo de "dano"
    public float inicioPontosDano;  // valor mínimo inicial de "saúde" do Player
    public float MaxPontosDano;     // valor máximo permitido de "saúde" do Player

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
