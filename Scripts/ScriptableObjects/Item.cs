using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item")] // Cria o Create Item no diretorio

/*
defini as propriedades dos itens 
 */
public class Item : ScriptableObject
{
    public string NomeObjeto;
    public Sprite sprite;
    public int quantidade;
    public bool empilhavel;
    public enum TipoItem
    {
        MOEDA,
        HEALTH
    }

    public TipoItem tipoItem;
}
