using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class BossGFX : MonoBehaviour
{
    public AIPath aiPath; // Referência ao componente AIPath do A* Pathfinding
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(aiPath.desiredVelocity.x >= 0.01f)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f); // Vira para a direita
        }
        else if(aiPath.desiredVelocity.x <= -0.01f)
        {
            transform.localScale = new Vector3(1f, 1f, 1f); // Vira para a esquerda
        }
        else if(aiPath.desiredVelocity.y >= 0.01f)
        {
            transform.localScale = new Vector3(1f, 1f, 1f); // Vira para cima
        }
        else if(aiPath.desiredVelocity.y <= -0.01f)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f); // Vira para baixo
        }
    }
}
