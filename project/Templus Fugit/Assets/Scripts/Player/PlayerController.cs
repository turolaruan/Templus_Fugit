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

    [Header("Moedas")]
    public int coinCount = 0;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        animator.updateMode = AnimatorUpdateMode.UnscaledTime;
    }

    void Update()
    {
        if (!canMove) return;

        HandleMovement();
        HandleInteraction();

        // usar slot 0…4 via teclas 1–5
        for (int i = 0; i < 5; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1 + i))
            {
                GameManager.Instance.UseInventoryItem(i);
            }
        }
    }

    void HandleMovement()
    {
        Vector2 dir = Vector2.zero;
        if (Input.GetKey(moveUpKey))
        {
            dir.y += 1;
            animator.runtimeAnimatorController = andarCima;
        }
        else if (Input.GetKey(moveDownKey))
        {
            dir.y -= 1;
            animator.runtimeAnimatorController = andarBaixo;
        }
        else if (Input.GetKey(moveLeftKey))
        {
            dir.x -= 1;
            animator.runtimeAnimatorController = andarEsquerda;
        }
        else if (Input.GetKey(moveRightKey))
        {
            dir.x += 1;
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

        float dt = Time.unscaledDeltaTime;
        Vector2 movement = dir.normalized * moveSpeed * dt;

        Vector3 newPos = transform.position + new Vector3(movement.x, movement.y, 0f);

        newPos.x = Mathf.Clamp(newPos.x, boundXEsquerda, boundXDireita);
        newPos.y = Mathf.Clamp(newPos.y, boundYBaixo, boundYCima);

        transform.position = newPos;
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
        if (!canBeHit) return;

        canBeHit = false;                            // bloqueia novos hits
        GameManager.Instance.LoseLife(amount);       // reduz vida e atualiza UI
        StartCoroutine(HitCooldownCoroutine());      // reinicia permissibilidade depois do cooldown
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

    // Dá moedas ao jogador.
    public void AddCoins(int amount = 1)
    {
        GameManager.Instance.AddCoins(amount); // Atualiza o GameManager
        Debug.Log($"Player adicionou {amount} moedas. Total: {GameManager.Instance.coinCount}");
    }

    // Tenta gastar moedas; retorna true se conseguiu.
    public bool SpendCoins(int amount)
    {
        if (GameManager.Instance.coinCount >= amount)
        {
            GameManager.Instance.AddCoins(-amount); // Atualiza o GameManager
            Debug.Log($"Player gastou {amount} moedas. Restam: {GameManager.Instance.coinCount}");
            return true;
        }
        Debug.Log("Moedas insuficientes.");
        return false;
    }

    // Retorna quantas moedas o player tem.
    public int GetCoinCount()
    {
        return GameManager.Instance.coinCount; // Consulta o GameManager
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Se o objeto tiver a tag "Coin"
        if (other.CompareTag("Coin"))
        {
            AddCoins(1);
            Destroy(other.gameObject);
        }
    }
}
