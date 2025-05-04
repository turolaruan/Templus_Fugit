using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PuzzleSequenceManager : MonoBehaviour
{
    public List<KeyCode> sequence;                // Sequência correta de teclas
    private int currentIndex = 0;

    public TextMeshPro sequenceDisplay3D;         // Texto 3D no chão
    public float penaltyTime = 5f;                // Tempo perdido ao errar

    public GameManager gameManager;               // Referência ao sistema de tempo
    private bool puzzleCompleted = false;

    public PlayerController playerController;         // Referência ao script de movimento do jogador

    void Start()
    {
        UpdateSequenceText();

        // Travar o movimento do jogador ao iniciar o puzzle
        if (playerController != null)
        {
            playerController.SetCanMove(false); // Impede o movimento
        }
    }

    void Update()
    {
        if (puzzleCompleted) return;

        HandleInput();
    }

    void HandleInput()
    {
        if (Input.anyKeyDown)
        {
            if (Input.GetKeyDown(sequence[currentIndex]))
            {
                StartCoroutine(FlashText(Color.green));
                currentIndex++;

                if (currentIndex >= sequence.Count)
                {
                    PuzzleSolved();
                }
            }
            else
            {
                // Penalidade
                GameManager.Instance.ReduceTime(penaltyTime); // Chama o método diretamente no GameManager
                StartCoroutine(FlashText(Color.red));
                currentIndex = 0;
            }
        }
    }

    void UpdateSequenceText()
    {
        string display = "";
        foreach (KeyCode key in sequence)
        {
            display += key.ToString() + " ";
        }

        if (sequenceDisplay3D != null)
        {
            sequenceDisplay3D.text = display;
        }
    }

    IEnumerator FlashText(Color flashColor)
    {
        if (sequenceDisplay3D != null)
        {
            Color originalColor = sequenceDisplay3D.color;
            sequenceDisplay3D.color = flashColor;
            yield return new WaitForSeconds(0.2f);
            sequenceDisplay3D.color = originalColor;
        }
    }

    void PuzzleSolved()
    {
        puzzleCompleted = true;

        // Reativar o movimento do jogador ao resolver o puzzle
        if (playerController != null)
        {
            playerController.SetCanMove(true); // Permite o movimento
        }

        // Abrir portas (destruir objetos com tag "Porta")
        GameObject[] portas = GameObject.FindGameObjectsWithTag("Door");
        foreach (GameObject porta in portas)
        {
            Destroy(porta);
        }

        // Destruir o objeto atual (o que contém o script)
        Destroy(gameObject);
    }
}
