using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class vendedorController : MonoBehaviour
{

    public GameObject[] storePool; //Pool de itens que podem aparecer na loja (colocar os prefabs dos itens no editor)
    public Transform[] itemSlots; // posições no cenário onde os itens vão aparecer

    // Start is called before the first frame update
    void Start()
    {
        List<GameObject> embaralhado = new List<GameObject>(storePool);// Cria uma lista baseada no array storePool para poder embaralhar
        EmbaralharLista(embaralhado);// Embaralha a lista para randomizar os itens

        int itensParaMostrar = Mathf.Min(itemSlots.Length, embaralhado.Count); // Define o número de itens que vamos mostrar (o mínimo entre slots disponíveis e itens embaralhados)

        for (int i = 0; i < itensParaMostrar; i++) // Instancia os itens embaralhados nos slots da loja
        {
            GameObject item = Instantiate(embaralhado[i], itemSlots[i].position, Quaternion.identity);// Cria uma cópia do item no slot correspondente
            item.transform.SetParent(itemSlots[i]); // Define o slot como "pai" do item para organizar a hierarquia na cena (opcional)

            // Atribui um valor aleatório entre 10 e 100, por exemplo
            int precoAleatorio = Random.Range(10, 20);

            StoreItem itemScript = item.GetComponent<StoreItem>();
            if (itemScript != null)
            {
                itemScript.DefinirPreco(precoAleatorio);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(GameManager.PlayerScore1);
    }

    void EmbaralharLista(List<GameObject> lista)    // Função que embaralha a lista usando algoritmo de Fisher-Yates (shuffle)

    {
        for (int i = 0; i < lista.Count; i++)
        {
            int randomIndex = Random.Range(i, lista.Count);  // Escolhe um índice aleatório entre o índice atual e o final da lista

            // Troca o item atual com o item sorteado
            GameObject temp = lista[i];
            lista[i] = lista[randomIndex];
            lista[randomIndex] = temp;
        }
    }
}
