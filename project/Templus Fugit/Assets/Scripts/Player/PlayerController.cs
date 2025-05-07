using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Teclas de Movimento e Interação")]
    public KeyCode moveUpKey = KeyCode.W;
    public KeyCode moveDownKey = KeyCode.S;
    public KeyCode moveLeftKey = KeyCode.A;
    public KeyCode moveRightKey = KeyCode.D;
    public KeyCode interactKey = KeyCode.E;

    [Header("Movimentação e Interação")]
    public float moveSpeed = 5f;
    public float interactionRange = 2f;
    public LayerMask interactableLayer;

    [Header("Limites do Cenário")]
    public float boundXEsquerda = -7.458932f;
    public float boundXDireita = 7.458932f;
    public float boundYBaixo = -2.997569f;
    public float boundYCima = 35.39562f;

    private Rigidbody2D rb2d;
    private Animator animator;
    private Vector2 movement;
    private bool canMove = true;

    [Header("Animações")]
    public RuntimeAnimatorController andarCima;
    public RuntimeAnimatorController andarBaixo;
    public RuntimeAnimatorController andarEsquerda;
    public RuntimeAnimatorController andarDireita;
    public RuntimeAnimatorController paradoCostas;
    public RuntimeAnimatorController paradoFrente;
    public RuntimeAnimatorController paradoEsquerda;
    public RuntimeAnimatorController paradoDireita;

    [Header("Parâmetros de Combate")]
    private bool canBeHit = true; // Para evitar que de dano enquanto está em cooldown
    public float hitCooldown = 2f; // Tempo de cooldown para receber dano

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        if (!canMove) return;

        HandleMovement();
        HandleInteraction();
    }

    void HandleMovement()
    {
        Vector2 movement = Vector2.zero;

        if (Input.GetKey(moveUpKey))
        {
            movement.y += 1;
            animator.runtimeAnimatorController = andarCima;
        }
        else if (Input.GetKey(moveDownKey))
        {
            movement.y -= 1;
            animator.runtimeAnimatorController = andarBaixo;
        }
        else if (Input.GetKey(moveLeftKey))
        {
            movement.x -= 1;
            animator.runtimeAnimatorController = andarEsquerda;
        }
        else if (Input.GetKey(moveRightKey))
        {
            movement.x += 1;
            animator.runtimeAnimatorController = andarDireita;
        }
        else
        {
            if (animator.runtimeAnimatorController == andarCima)
                animator.runtimeAnimatorController = paradoCostas;
            else if (animator.runtimeAnimatorController == andarBaixo)
                animator.runtimeAnimatorController = paradoFrente;
            else if (animator.runtimeAnimatorController == andarEsquerda)
                animator.runtimeAnimatorController = paradoEsquerda;
            else if (animator.runtimeAnimatorController == andarDireita)
                animator.runtimeAnimatorController = paradoDireita;
        }

        movement = movement.normalized * moveSpeed * Time.deltaTime;
        Vector2 newPosition = rb2d.position + movement;

        newPosition.x = Mathf.Clamp(newPosition.x, boundXEsquerda, boundXDireita);
        newPosition.y = Mathf.Clamp(newPosition.y, boundYBaixo, boundYCima);

        rb2d.MovePosition(newPosition);
    }

    void HandleInteraction()
    {
        if (Input.GetKeyDown(interactKey))
        {
            Collider2D[] interactables = Physics2D.OverlapCircleAll(transform.position, interactionRange, interactableLayer);
            foreach (Collider2D interactable in interactables)
            {
                IInteractable interactableObject = interactable.GetComponent<IInteractable>();
                if (interactableObject != null)
                {
                    interactableObject.Interact();
                    return;
                }
            }
        }
    }


    public void SetCanMove(bool value)
    {
        canMove = value;
    }

    public void TakeDamage(int amount)
    {
        if (!canBeHit)
            return;

        // Reduz as vidas no GameManager
        GameManager.Instance.LoseLife(amount);

        // Inicia o cooldown para evitar dano consecutivo
        StartCoroutine(HitCooldownCoroutine());
    }

    private IEnumerator HitCooldownCoroutine()
    {
        yield return new WaitForSeconds(hitCooldown);
        canBeHit = true;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, interactionRange);
    }
}
