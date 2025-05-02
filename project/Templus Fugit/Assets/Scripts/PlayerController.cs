using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public KeyCode moveUpKey = KeyCode.W; // Move o player pra cima
    public KeyCode moveDownKey = KeyCode.S; // Move o player pra baixo
    public KeyCode moveLeftKey = KeyCode.A; // Move o player pra esquerda
    public KeyCode moveRightKey = KeyCode.D; // Move o player pra direita
    public float moveSpeed = 5f; // Velocidade de movimento do player
    public float boundXEsquerda = -7.458932f; // Define os limites em X
    public float boundXDireita = 7.458932f; // Define os limites em X
    public float boundYBaixo = -2.997569f; // Define os limites em Y
    public float boundYCima = 35.39562f; // Define os limites em Y

    private Rigidbody2D rb2d; // Define o corpo rígido 2D que representa o player
    private Animator animator; // Define o componente Animator
    private Vector2 movement; // Define o vetor de movimento do player

    public RuntimeAnimatorController andarCima; // Define o animator controller para quando pressionar a tecla W
    public RuntimeAnimatorController andarBaixo; // Define o animator controller para quando pressionar a tecla S
    public RuntimeAnimatorController andarEsquerda; // Define o animator controller para quando pressionar a tecla A
    public RuntimeAnimatorController andarDireita; // Define o animator controller para quando pressionar a tecla D
    public RuntimeAnimatorController paradoCostas; // Define o animator controller para quando soltar a tecla W
    public RuntimeAnimatorController paradoFrente; // Define o animator controller para quando soltar a tecla S
    public RuntimeAnimatorController paradoEsquerda; // Define o animator controller para quando soltar a tecla A
    public RuntimeAnimatorController paradoDireita; // Define o animator controller para quando soltar a tecla D

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>(); // Inicializa o player
        animator = GetComponentInChildren<Animator>(); // Inicializa o animator
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 movement = Vector2.zero;

        if (Input.GetKey(moveUpKey))
        {
            movement.y += 1;
            animator.runtimeAnimatorController = andarCima; // Troca para a animação de andar para cima
        }
        else if (Input.GetKey(moveDownKey))
        {
            movement.y -= 1;
            animator.runtimeAnimatorController = andarBaixo; // Troca para a animação de andar para baixo
        }
        else if (Input.GetKey(moveLeftKey))
        {
            movement.x -= 1;
            animator.runtimeAnimatorController = andarEsquerda; // Troca para a animação de andar para a esquerda
        }
        else if (Input.GetKey(moveRightKey))
        {
            movement.x += 1;
            animator.runtimeAnimatorController = andarDireita; // Troca para a animação de andar para a direita
        }
        else
        {
            // Define animações de parado com base na última direção
            if (animator.runtimeAnimatorController == andarCima)
            {
                animator.runtimeAnimatorController = paradoCostas;
            }
            else if (animator.runtimeAnimatorController == andarBaixo)
            {
                animator.runtimeAnimatorController = paradoFrente;
            }
            else if (animator.runtimeAnimatorController == andarEsquerda)
            {
                animator.runtimeAnimatorController = paradoEsquerda;
            }
            else if (animator.runtimeAnimatorController == andarDireita)
            {
                animator.runtimeAnimatorController = paradoDireita;
            }
        }

        movement = movement.normalized * moveSpeed * Time.deltaTime;
        Vector2 newPosition = rb2d.position + movement;

        // Limita a posição do player dentro dos bounds
        newPosition.x = Mathf.Clamp(newPosition.x, boundXEsquerda, boundXDireita);
        newPosition.y = Mathf.Clamp(newPosition.y, boundYBaixo, boundYCima);

        rb2d.MovePosition(newPosition);
    }
}
