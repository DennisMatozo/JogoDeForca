using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : Caractere
{
    public Inventario inventarioPrefab; // referência ao objeto criado do Inventário
    Inventario inventario;
    public HealthBar healthBarPrefab;  // referencia ao objeto prefab criado da HealthBar
    HealthBar healthBar;

    public PontosDano pontosDano;  // tem o valor da "saúde" do objeto

    private void Start()
    {
        inventario = Instantiate(inventarioPrefab);

        pontosDano.valor = inicioPontosDano;
        healthBar = Instantiate(healthBarPrefab);
        healthBar.caractere = this;
    }

    public override IEnumerator DanoCaractere(int dano, float intervalo)
    {
        while (true)
        {
            StartCoroutine(FlickerCaractere());
            pontosDano.valor = pontosDano.valor - dano;
            if(pontosDano.valor <= float.Epsilon)
            {
                KillCaractere();
                SceneManager.LoadScene("GameOver");
                break;
            }
            if(intervalo > float.Epsilon)
            {
                yield return new WaitForSeconds(intervalo);
            }
            else
            {
                break;
            }
        }
    }

    public override void ResetCaractere()
    {
        inventario = Instantiate(inventarioPrefab);
        healthBar = Instantiate(healthBarPrefab);
        healthBar.caractere = this;
        pontosDano.valor = inicioPontosDano;
    }

    public override void KillCaractere()
    {
        base.KillCaractere();
        Destroy(healthBar.gameObject);
        Destroy(inventario.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Coletavel"))
        {
           Item danoObjeto = collision.gameObject.GetComponent<Consumable>().item;
           if(danoObjeto != null)
            {
                bool DeveDesaparecer = false;
                // print("Acertou: " + danoObjeto.NomeObjeto);
                switch (danoObjeto.tipoItem)
                {
                    case Item.TipoItem.MOEDA:
                        // DeveDesaparecer = true; 
                        DeveDesaparecer = inventario.AddItem(danoObjeto);
                        break;
                    case Item.TipoItem.HEALTH:
                        DeveDesaparecer = AjustePontosDano(danoObjeto.quantidade);
                        break;
                    default:
                        break;
                }
                if (DeveDesaparecer)
                {
                    collision.gameObject.SetActive(false);
                }                
             }
        }
        
    }

    public bool AjustePontosDano(int quantidade)
    {
        if (pontosDano.valor < MaxPontosDano)
        {
            pontosDano.valor = pontosDano.valor + quantidade;
            print("Ajustamdo PD por: " + quantidade + ". Novo Valor = " + pontosDano.valor);
            return true;
        }
        else return false;
    }
}
