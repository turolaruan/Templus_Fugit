using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StoreItem : MonoBehaviour
{
    public int preco;

    public void DefinirPreco(int valor)
    {
        preco = valor;

        TextMeshProUGUI texto = GetComponentInChildren<TextMeshProUGUI>();
        if (texto != null)
        {
            texto.text = $"{preco}";
        }
    }
}
