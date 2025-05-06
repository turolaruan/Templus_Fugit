using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SellerDialogue : MonoBehaviour, IInteractable
{
    public GameObject dialogueBox; // Referência ao objeto do diálogo
    private bool isDialogueActive = false; // Verifica se o diálogo está ativo
    private bool isPlayerNearby = false; // Verifica se o jogador está próximo
    private Rigidbody2D rb2d; // Define o corpo rígido 2D que representa o player

    // Start é chamado antes do primeiro frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>(); // Inicializa o player
        if (dialogueBox != null)
        {
            dialogueBox.SetActive(false); // Garante que a caixa de diálogo esteja desativada no início
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && isPlayerNearby)
        {
            Interact();
        }
    }

    public void Interact()
    {
        if (!isDialogueActive)
        {
            ShowDialogue();
        }
        else
        {
            CloseDialogue();
        }
    }

    private void ShowDialogue()
    {
        if (dialogueBox != null)
        {
            dialogueBox.SetActive(true); // Ativa a caixa de diálogo
        }
        isDialogueActive = true;

        // Certifique-se de que o texto do TMP não seja alterado
        var textComponent = dialogueBox.GetComponentInChildren<TMPro.TextMeshProUGUI>();
        if (textComponent != null)
        {
            textComponent.enabled = true; // Garante que o texto seja exibido
        }
    }

    private void CloseDialogue()
    {
        if (dialogueBox != null)
        {
            dialogueBox.SetActive(false); // Desativa a caixa de diálogo
        }
        isDialogueActive = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerNearby = true; // Detecta que o jogador está próximo
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerNearby = false; // Detecta que o jogador saiu da área
        }
    }
}
