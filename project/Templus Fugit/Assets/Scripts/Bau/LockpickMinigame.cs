using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum LockpickDifficulty { Easy, Medium, Hard }

public class LockpickMinigame : MonoBehaviour
{
    [Header("Referências da UI")]
    [Tooltip("Arraste aqui o TextMeshProUGUI que exibe o prompt (ex: PRESSIONE: A)")]
    public TextMeshProUGUI keyPrompt;
    public RectTransform oscillatingBar;
    public RectTransform hitZone;

    [Header("Configuração")]
    public float barSpeed = 300f;

    private string[] sequence;
    private int currentIndex;
    private bool isMovingRight;
    private bool isPlaying;
    private System.Action onSuccessCallback;

    void Update()
    {
        if (!isPlaying) return;

        // 1) Oscila a barra
        float move = barSpeed * Time.deltaTime;
        Vector2 pos = oscillatingBar.anchoredPosition;
        pos.x += isMovingRight ? move : -move;
        if (pos.x > 150) isMovingRight = false;
        if (pos.x < -150) isMovingRight = true;
        oscillatingBar.anchoredPosition = pos;

        // 2) Captura tecla
        if (Input.anyKeyDown)
        {
            string key = Input.inputString.ToUpper();
            if (key.Length == 1 && key == sequence[currentIndex] && IsInZone(pos.x))
            {
                currentIndex++;
                if (currentIndex >= sequence.Length)
                    Success();
                else
                    keyPrompt.text = $"PRESSIONE: {sequence[currentIndex]}";
            }
            else
            {
                Fail();
            }
        }
    }

    /// <summary>
    /// Inicia o minigame com dificuldade e callback.
    /// </summary>
    public void StartMinigame(LockpickDifficulty difficulty, System.Action onSuccess = null)
    {
        // 0) trava o player
        var player = FindObjectOfType<PlayerController>();
        if (player != null) player.SetCanMove(false);

        onSuccessCallback = () =>
        {
            if (player != null) player.SetCanMove(true);
            onSuccess?.Invoke();
        };

        currentIndex = 0;
        isPlaying = true;
        isMovingRight = true;
        oscillatingBar.anchoredPosition = new Vector2(-150, 0);

        // 1) define tamanho e zona
        int length;
        float zoneWidth;
        switch (difficulty)
        {
            case LockpickDifficulty.Easy:
                length = 3; zoneWidth = 80; break;
            case LockpickDifficulty.Medium:
                length = Random.Range(4, 6); zoneWidth = 50; break;
            default: // Hard
                length = Random.Range(6, 8); zoneWidth = 30; break;
        }

        // 2) gera sequência A–Z
        sequence = GenerateRandomSequence(length);

        // 3) ajusta o hitZone
        hitZone.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, zoneWidth);
        float halfRange = 150f;
        float minX = -halfRange + zoneWidth * .5f;
        float maxX =  halfRange - zoneWidth * .5f;
        float randomX = Random.Range(minX, maxX);
        hitZone.anchoredPosition = new Vector2(randomX, hitZone.anchoredPosition.y);

        // 4) exibe primeira letra
        keyPrompt.text = $"PRESSIONE: {sequence[0]}";
    }

    private string[] GenerateRandomSequence(int length)
    {
        const string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        var generated = new string[length];
        for (int i = 0; i < length; i++)
            generated[i] = alphabet[Random.Range(0, alphabet.Length)].ToString();
        return generated;
    }

    private bool IsInZone(float barX)
    {
        float center = hitZone.anchoredPosition.x;
        float half = hitZone.sizeDelta.x * .5f;
        return barX >= center - half && barX <= center + half;
    }

    private void Fail()
    {
        currentIndex = 0;
        keyPrompt.text = $"PRESSIONE: {sequence[0]}";
        Debug.Log("Errou! Sequência reiniciada.");
    }

    private void Success()
    {
        isPlaying = false;
        keyPrompt.text = "SUCESSO!";
        Debug.Log("Sequência concluída!");
        gameObject.SetActive(false);

        var player = FindObjectOfType<PlayerController>();
        if (player != null) player.SetCanMove(true);

        onSuccessCallback?.Invoke();
    }
}