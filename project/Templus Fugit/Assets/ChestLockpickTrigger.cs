using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestLockpickTrigger : MonoBehaviour, IInteractable
{
    [Header("ID Único do Baú")]
    public string chestID;

    [Header("Minigame")]
    public GameObject lockpickUI; // UI do minigame
    private LockpickMinigame minigame;

    // [Header("Recompensa")]
    // public GameObject rewardPrefab;
    // public Transform rewardSpawnPoint;

    private bool opened = false;

    void Start()
    {
        if (lockpickUI != null)
            minigame = lockpickUI.GetComponent<LockpickMinigame>();

        // Verifica se esse baú já foi aberto nesta run
        if (GameManager.Instance.openedChests.Contains(chestID))
        {
            opened = true;
            gameObject.SetActive(false); // esconde o baú, ou apenas bloqueia interação
        }
    }

    public void TryOpen()
    {
        if (opened || minigame == null) return;

        lockpickUI.SetActive(true);
        minigame.StartMinigame(onSuccess: UnlockChest);
    }

    private void UnlockChest()
    {
        opened = true;
        GameManager.Instance.openedChests.Add(chestID); // marca como aberto

        Debug.Log("Baú aberto com sucesso!");

        // if (rewardPrefab != null && rewardSpawnPoint != null)
        // {
        //     Instantiate(rewardPrefab, rewardSpawnPoint.position, Quaternion.identity);
        // }

        gameObject.SetActive(false); // ou troca sprite, anima, etc.
    }

    public void Interact()
    {
        TryOpen();
    }
}