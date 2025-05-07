using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] 
    private Transform target; // O alvo que a câmera deve seguir
    [SerializeField] 
    private float smoothSpeed = 0.125f; // A velocidade de suavização do movimento da câmera
    private Vector3 offset; // O deslocamento da câmera em relação ao alvo
    [Header("Camera Bounds")]
    public Vector3 minCameraBounds; // Limite mínimo da câmera
    public Vector3 maxCameraBounds; // Limite máximo da câmera

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        Vector3 desiredPosition = target.position + offset; // A posição desejada da câmera
        var localPosition = transform.localPosition; // A posição local da câmera
        Vector3 smoothedPosition = Vector3.Lerp(localPosition, desiredPosition, smoothSpeed); // A posição suavizada da câmera

        // Atualiza a posição local da câmera, preservando o Z atual
        localPosition = new Vector3(
            Mathf.Clamp(smoothedPosition.x, minCameraBounds.x, maxCameraBounds.x), // Limita a posição X da câmera
            Mathf.Clamp(smoothedPosition.y, minCameraBounds.y, maxCameraBounds.y), // Limita a posição Y da câmera
            localPosition.z // Mantém o Z atual da câmera
        );

        transform.localPosition = localPosition; // Atualiza a posição da câmera
    }

    public void SetTarget(Transform targetToSet)
    {
        target = targetToSet; // Define o alvo da câmera
    }
}
