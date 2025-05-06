using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossController : MonoBehaviour
{
    public Transform target; // Alvo para o inimigo seguir
    private NavMeshAgent agent;
    private Animator animator; // Componente Animator para controlar as animações
    private Rigidbody2D rb2d; // Define o corpo rígido 2D que representa o player

    // Parâmetros do Animator
    public RuntimeAnimatorController Attack;
    public RuntimeAnimatorController Death;
    public RuntimeAnimatorController Idle;
    public RuntimeAnimatorController Run;
    public RuntimeAnimatorController Take_hit;

    public float attackRange = 1.0f; // Distância para iniciar o ataque
    public float detectionRange = 10.0f; // Distância para detectar o alvo

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        animator = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float distanceToTarget = Vector2.Distance(transform.position, target.position);

        if (distanceToTarget > detectionRange)
        {
            // Alvo está fora do alcance, animação Idle
            animator.runtimeAnimatorController = Idle;
            agent.isStopped = true;
        }
        else if (distanceToTarget > attackRange)
        {
            // Alvo está dentro do alcance de detecção, mas fora do alcance de ataque
            animator.runtimeAnimatorController = Run;
            agent.isStopped = false;
            agent.SetDestination(target.position);

            // Ajusta a direção do inimigo
            Vector3 direction = target.position - transform.position;
            if (direction.x > 0)
            {
                transform.eulerAngles = new Vector3(0, 0, 0); // Virado para a direita
            }
            else if (direction.x < 0)
            {
                transform.eulerAngles = new Vector3(0, 180, 0); // Virado para a esquerda
            }
        }
        else
        {
            // Alvo está dentro do alcance de ataque
            animator.runtimeAnimatorController = Attack;
            agent.isStopped = true;

            // Ajusta a direção do inimigo
            Vector3 direction = target.position - transform.position;
            if (direction.x > 0)
            {
                transform.eulerAngles = new Vector3(0, 0, 0); // Virado para a direita
            }
            else if (direction.x < 0)
            {
                transform.eulerAngles = new Vector3(0, 180, 0); // Virado para a esquerda
            }
        }
    }

    public void TakeDamage()
    {
        // Lógica para quando o inimigo tomar dano
        animator.runtimeAnimatorController = Take_hit;
        // Adicione lógica para reduzir a vida do inimigo
    }

    public void Die()
    {
        // Lógica para quando o inimigo morrer
        animator.runtimeAnimatorController = Death;
        agent.isStopped = true;
        // Adicione lógica para destruir o inimigo ou desativá-lo
    }
}
