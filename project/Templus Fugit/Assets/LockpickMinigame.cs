using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LockpickMinigame : MonoBehaviour
{
    [Header("Referências")]
    public Text keyPrompt;
    public RectTransform oscillatingBar;
    public RectTransform hitZone;

    [Header("Configuração")]
    public float barSpeed = 300f; // Velocidade da barra
    public string[] sequence = { "A", "S", "D" }; // Sequência de teclas

    private int currentIndex = 0;
    private bool isMovingRight = true;
    private bool isPlaying = false;

    private System.Action onSuccessCallback;

    void Start()
    {
        StartMinigame(); // Ativa automaticamente para testes (remova se for usar por evento externo)
    }

    void Update()
    {
        if (!isPlaying) return;

        OscillateBar();

        if (Input.anyKeyDown)
        {
            string keyPressed = Input.inputString.ToUpper();

            if (keyPressed == sequence[currentIndex])
            {
                if (IsBarInsideHitZone())
                {
                    currentIndex++;

                    if (currentIndex >= sequence.Length)
                    {
                        OnSuccess();
                    }
                    else
                    {
                        keyPrompt.text = "PRESSIONE: " + sequence[currentIndex];
                    }
                }
                else
                {
                    OnFail();
                }
            }
            else
            {
                OnFail();
            }
        }
    }

    public void StartMinigame(System.Action onSuccess = null)
    {
        onSuccessCallback = onSuccess;

        currentIndex = 0;
        isPlaying = true;
        isMovingRight = true;
        oscillatingBar.anchoredPosition = new Vector2(-150, 0);
        keyPrompt.text = "PRESSIONE: " + sequence[currentIndex];
    }

    void OscillateBar()
    {
        float move = barSpeed * Time.deltaTime;
        Vector2 pos = oscillatingBar.anchoredPosition;

        pos.x += isMovingRight ? move : -move;

        // Inverte direção
        if (pos.x >= 150) isMovingRight = false;
        if (pos.x <= -150) isMovingRight = true;

        oscillatingBar.anchoredPosition = pos;
    }

    bool IsBarInsideHitZone()
    {
        float barX = oscillatingBar.anchoredPosition.x;
        float hitX = hitZone.anchoredPosition.x;
        float zoneMin = hitX - hitZone.sizeDelta.x / 2f;
        float zoneMax = hitX + hitZone.sizeDelta.x / 2f;

        return barX >= zoneMin && barX <= zoneMax;
    }

    void OnFail()
    {
        Debug.Log("Errou! Reiniciando sequência.");
        currentIndex = 0;
        keyPrompt.text = "PRESSIONE: " + sequence[currentIndex];
    }

    void OnSuccess()
    {
        isPlaying = false;
        keyPrompt.text = "SUCESSO!";
        gameObject.SetActive(false);
        onSuccessCallback?.Invoke();
    }
}
