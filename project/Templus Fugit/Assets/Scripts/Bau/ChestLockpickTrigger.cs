using UnityEngine;
using UnityEngine.SceneManagement;

public class ChestLockpickTrigger : MonoBehaviour, IInteractable
{
    [Header("Identificador único do baú (para persistência)")]
    public string chestID;

    [Header("Minigame de Lockpick")]
    public GameObject lockpickUI;
    public LockpickDifficulty difficulty = LockpickDifficulty.Medium;
    private LockpickMinigame minigame;

    [Header("Recompensa em Moedas")]
    public int coinReward = 5;

    // [Header("Recompensa")]
    // public GameObject rewardPrefab;
    // public Transform rewardSpawnPoint;

    private bool opened = false;

    void Start()
    {
        // Pega a referência ao minigame
        if (lockpickUI != null)
            minigame = lockpickUI.GetComponent<LockpickMinigame>();

        // Se já estiver aberto nesta run, desative o baú
        if (!string.IsNullOrEmpty(chestID) &&
            GameManager.Instance.openedChests.Contains(chestID))
        {
            opened = true;
            gameObject.SetActive(false);
        }
    }

    // Chamado pelo PlayerController
    public void Interact()
    {
        TryOpen();
    }

    private void TryOpen()
    {
        if (opened || minigame == null) 
            return;

        // Abre a UI e inicia o minigame, passando a dificuldade e o callback
        lockpickUI.SetActive(true);
        minigame.StartMinigame(difficulty, UnlockChest);
    }

    // Callback executado quando o minigame devolve sucesso
    private void UnlockChest()
    {
        opened = true;

        // Marca como aberto no GameManager
        if (!string.IsNullOrEmpty(chestID))
            GameManager.Instance.openedChests.Add(chestID);

        // Instancia a recompensa
        // if (rewardPrefab != null && rewardSpawnPoint != null)
        //     Instantiate(
        //         rewardPrefab,
        //         rewardSpawnPoint.position,
        //         rewardSpawnPoint.rotation
        //     );
        var player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            var pc = player.GetComponent<PlayerController>();
            if (pc != null)
                pc.AddCoins(coinReward);
        }

        // Aqui você pode disparar uma animação (opcional)
        // animator.SetTrigger("Open");

        // Desativa o baú (uma run só, sem reinício de cena)
        gameObject.SetActive(false);
    }
}
