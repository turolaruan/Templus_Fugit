using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemoryFragment : MonoBehaviour
{
    public GameObject dialogueBox; // Referência ao objeto do diálogo
    public Camera mainCamera; // Referência à câmera principal
    private bool isPlayerNearby = false; // Verifica se o jogador está próximo
    private bool isDialogueActive = false; // Verifica se o diálogo está ativo

    // Start is called before the first frame update
    void Start()
    {
        dialogueBox.SetActive(false); // Garante que a caixa de diálogo esteja desativada no início
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.F) && !isDialogueActive)
        {
            ShowDialogue();
        }
        else if (isDialogueActive && Input.GetKeyDown(KeyCode.F))
        {
            CloseDialogue();
        }
    }

    private void ShowDialogue()
    {
        dialogueBox.SetActive(true); // Ativa a caixa de diálogo
        isDialogueActive = true;

        // Certifique-se de que o texto do TMP não seja alterado
        var textComponent = dialogueBox.GetComponentInChildren<TMPro.TextMeshProUGUI>();
        if (textComponent != null)
        {
            textComponent.enabled = true; // Garante que o texto seja exibido
        }

        // Inicia o tremor da câmera
        if (mainCamera != null)
        {
            CameraShake cameraShake = mainCamera.GetComponent<CameraShake>();
            if (cameraShake != null)
            {
                StartCoroutine(cameraShake.Shake(12f, 0.1f)); // Duração de 5s e magnitude de 10
            }
        }
    }

    private void CloseDialogue()
    {
        dialogueBox.SetActive(false); // Desativa a caixa de diálogo
        isDialogueActive = false;
        gameObject.SetActive(false); // Desativa o fragmento de memória
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
