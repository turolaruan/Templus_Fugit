using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


public class StoreItem : MonoBehaviour
{
    public int preco;

    private void Start()
    {
        // Verifica se a cena atual é "Store"
        if (SceneManager.GetActiveScene().name != "Store")
        {
            // Tenta encontrar o filho chamado "PrecoTexto" e desativá-lo
            Transform precoTexto = transform.Find("PrecoTexto");
            if (precoTexto != null)
            {
                precoTexto.gameObject.SetActive(false);
            }
        }
    }

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
