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

    private Rigidbody2D rb2d; // Define o corpo rigido 2D que representa o player
    private Vector2 movement; // Define o vetor de movimento do player

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>(); // Inicializa o player
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 movement = Vector2.zero;

        if (Input.GetKey(moveUpKey))
        {
            movement.y += 1;
        }
        if (Input.GetKey(moveDownKey))
        {
            movement.y -= 1;
        }
        if (Input.GetKey(moveLeftKey))
        {
            movement.x -= 1;
        }
        if (Input.GetKey(moveRightKey))
        {
            movement.x += 1;
        }

        movement = movement.normalized * moveSpeed * Time.deltaTime;
        Vector2 newPosition = rb2d.position + movement;

        // Limita a posição do player dentro dos bounds
        newPosition.x = Mathf.Clamp(newPosition.x, boundXEsquerda, boundXDireita);
        newPosition.y = Mathf.Clamp(newPosition.y, boundYBaixo, boundYCima);

        rb2d.MovePosition(newPosition);
    }
}
