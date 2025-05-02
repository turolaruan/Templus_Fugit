using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance; // Singleton instance
    public static int PlayerScore1 = 0; // Pontuação do player 1
    public GUISkin layout;              // Fonte do placar
    public static int lifes = 3;        // Vidas do jogador
    public static GameObject thePlayer; // Referência ao objeto jogador

    // Dicionário para armazenar a última posição do jogador em cada cena
    private Dictionary<string, Vector3?> savedPositions = new Dictionary<string, Vector3?>();

    // Variável para armazenar a cena anterior
    private string previousScene = null;

    void Awake()
    {
        // Implement the singleton pattern
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Prevent GameManager from being destroyed on scene reload
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate GameManager instances
        }
    }

    void OnEnable()
    {
        // Registra o evento de carregamento de cena
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        // Remove o registro do evento de carregamento de cena
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // Chamado quando uma nova cena é carregada
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Atualiza a referência ao jogador
        thePlayer = GameObject.FindGameObjectWithTag("Player");

        // Verifica se há uma cena anterior registrada
        if (!string.IsNullOrEmpty(previousScene))
        {
            Debug.Log($"Transição de cena: {previousScene} -> {scene.name}");
        }

        // Posiciona o jogador na cena carregada
        PositionPlayerOnSceneLoad();

        // Atualiza a variável da cena anterior após a lógica de transição
        previousScene = scene.name;
    }

    // Start is called before the first frame update
    void Start()
    {
        thePlayer = GameObject.FindGameObjectWithTag("Player"); // Busca a referência do jogador
        DrawLifes();
        PositionPlayerOnSceneLoad();
    }

    public void LoseLife()
    {
        lifes--;
        PlayerScore1 = 0; // Reset score when losing a life
        // Reload the scene, reset coins, reset score, but keep player lives
        if (lifes > 0)
        {
            SavePlayerPosition(); // Salva a posição antes de recarregar a cena
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        if (lifes == 0)
        {
            GameOver();
        }
    }

    public static void DrawLifes()
    {
        if (lifes == 2)
        {
            GameObject heart0 = GameObject.Find("Heart0");
            heart0.GetComponent<SpriteRenderer>().enabled = false;
        }
        else if (lifes == 1)
        {
            GameObject heart0 = GameObject.Find("Heart0");
            heart0.GetComponent<SpriteRenderer>().enabled = false;
            GameObject heart1 = GameObject.Find("Heart1");
            heart1.GetComponent<SpriteRenderer>().enabled = false;
        }
        else if (lifes == 0)
        {
            GameObject heart0 = GameObject.Find("Heart0");
            heart0.GetComponent<SpriteRenderer>().enabled = false;
            GameObject heart1 = GameObject.Find("Heart1");
            heart1.GetComponent<SpriteRenderer>().enabled = false;
            GameObject heart2 = GameObject.Find("Heart2");
            heart2.GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    // Salva a posição atual do jogador no dicionário
    private void SavePlayerPosition()
    {
        if (thePlayer != null)
        {
            string currentScene = SceneManager.GetActiveScene().name;
            savedPositions[currentScene] = thePlayer.transform.position;
        }
    }

    // Posiciona o jogador ao carregar uma nova cena
    private void PositionPlayerOnSceneLoad()
    {
        string currentScene = SceneManager.GetActiveScene().name;
        string previousScene = this.previousScene;
        Debug.Log($"Cena atual: {currentScene}, Cena anterior: {previousScene}");

        if (savedPositions.ContainsKey(currentScene) && savedPositions[currentScene].HasValue)
        {
            if (currentScene == "Cena2" && previousScene == "Cena3") // transicao da Cena3 para Cena2
            {
                thePlayer.transform.position = new Vector3(-6.042691f, 8.385761f, 0f);
            }
            else if (currentScene == "Cena3" && previousScene == "Cena2") // transicao da Cena2 para Cena3
            {
                thePlayer.transform.position = new Vector3(6.16f, -1.6f, 0f);
            }
            else if (currentScene == "Cena2" && previousScene == "Cena4") // transicao da Cena4 para Cena2
            {
                thePlayer.transform.position = new Vector3(5.78788f, 8.521598f, 0f);
            }
            else if (currentScene == "Cena4" && previousScene == "Cena2") // transicao da Cena2 para Cena4
            {
                thePlayer.transform.position = new Vector3(-6.16f, -1.03f, 0f);
            }
            else if (currentScene == "Cena2" && previousScene == "Cena6") // transicao da Cena6 para Cena2
            {
                thePlayer.transform.position = new Vector3(-0.08580431f, 14.966f, 0f);
            }
            else if (currentScene == "Cena6" && previousScene == "Cena2") // transicao da Cena2 para Cena6
            {
                thePlayer.transform.position = new Vector3(0.02f, -2.839817f, 0f);
            }
            else if (currentScene == "Cena6" && previousScene == "Cena8") 
            {
                thePlayer.transform.position = new Vector3(0.07093602f, 1.779929f, 0f);
            }
            else if (currentScene == "Cena6" && previousScene == "Cena2")
            {
                thePlayer.transform.position = new Vector3(-0.02309176f, -2.92162f, 0f);
            }
            else if (currentScene == "Cena8" && previousScene == "Cena6")
            {
                thePlayer.transform.position = new Vector3(0.02f, -2.822216f, 0f);
            }
            else
            {
                thePlayer.transform.position = savedPositions[currentScene].Value;
            }
        }
        else
        {
            // Define a posição padrão (ponto de spawn)
            GameObject spawnPoint = GameObject.Find("SpawnPoint");
            if (spawnPoint != null)
            {
                thePlayer.transform.position = spawnPoint.transform.position;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (thePlayer == null)
        {
            thePlayer = GameObject.FindGameObjectWithTag("Player");
            if (thePlayer == null)
            {
                return; // ainda não está disponível, evita NullReference
            }
        }

        DrawLifes();

        // Salva a posição do jogador antes de trocar de cena
        Vector2 playerPosition = thePlayer.transform.position;
        if (SceneManager.GetActiveScene().name == "Cena1")
        {
            if (playerPosition.x >= -1.909972f && playerPosition.x <= 1.910001f && Mathf.Approximately(playerPosition.y, 34.06695f))
            {
                SavePlayerPosition();
                SceneManager.LoadScene("Cena2");
            }
        }

        else if (SceneManager.GetActiveScene().name == "Cena2")
        {
            if (playerPosition.x >= -0.9350259f && playerPosition.x <= 0.935003f && Mathf.Approximately(playerPosition.y, 15.4524f))
            {
                SavePlayerPosition();
                SceneManager.LoadScene("Cena5");
            }
            else if (Mathf.Approximately(playerPosition.x, -6.335011f) && playerPosition.y >= 8.068845f && playerPosition.y <= 8.935001f)
            {
                SavePlayerPosition();
                SceneManager.LoadScene("Cena3");
            }
            else if (Mathf.Approximately(playerPosition.x, 6.308114f) && playerPosition.y >= 8.064985f && playerPosition.y <= 8.935001f)
            {
                SavePlayerPosition();
                SceneManager.LoadScene("Cena4");
            }
        }

        else if (SceneManager.GetActiveScene().name == "Cena3")
        {
            if (Mathf.Approximately(playerPosition.x, 6.652393f) && playerPosition.y >= -1.935015f && playerPosition.y <= -1.208376f)
            {
                SavePlayerPosition();
                SceneManager.LoadScene("Cena2");
            }
        }

        else if (SceneManager.GetActiveScene().name == "Cena4")
        {
            if (Mathf.Approximately(playerPosition.x, -6.752596f) && playerPosition.y >= -1.477549f && playerPosition.y <= -0.5053926f)
            {
                SavePlayerPosition();
                SceneManager.LoadScene("Cena2");
            }
        }

        else if (SceneManager.GetActiveScene().name == "Cena5")
        {
            if (playerPosition.x >= -0.7060629f && playerPosition.x <= 0.7072755f && Mathf.Approximately(playerPosition.y, -3.190501f))
            {
                SavePlayerPosition();
                SceneManager.LoadScene("Cena2");
            }
            else if (playerPosition.x >= -0.7181748f && playerPosition.x <= 0.7148368f && Mathf.Approximately(playerPosition.y, 2.134892f))
            {
                SavePlayerPosition();
                SceneManager.LoadScene("Cena8");
            }
            else if (Mathf.Approximately(playerPosition.x, 6.514736f) && playerPosition.y >= -1.51467f && playerPosition.y <= 0.5432134f)
            {
                SavePlayerPosition();
                SceneManager.LoadScene("Cena9");
            }
        }

        else if (SceneManager.GetActiveScene().name == "Cena8")
        {
            if (playerPosition.x >= -0.7060629f && playerPosition.x <= 0.7072755f && Mathf.Approximately(playerPosition.y, -3.107711f))
            {
                SavePlayerPosition();
                SceneManager.LoadScene("Cena5");
            }
        }

        else if (SceneManager.GetActiveScene().name == "Cena9")
        {
            if (Mathf.Approximately(playerPosition.x, -6.678032f) && playerPosition.y >= -1.475696f && playerPosition.y <= 0.5104183f)
            {
                SavePlayerPosition();
                SceneManager.LoadScene("Cena5");
            }
        }
    }

    // Gerência da pontuação e fluxo do jogo
    void OnGUI()
    {
        GUI.skin = layout;
        GUI.Label(new Rect(Screen.width / 2, 2, 200, 200), "" + PlayerScore1);
    }

    public void GameOver()
    {
        lifes = 3; // Reset lifes when game is over
        PlayerScore1 = 0; // Reset score when game is over
        savedPositions.Clear(); // Limpa todas as posições salvas
        SceneManager.LoadScene("GameOver");
    }

    public void RestartGame()
    {
        lifes = 3; // Reset lifes only when restarting the entire game
        PlayerScore1 = 0;
        savedPositions.Clear(); // Limpa todas as posições salvas
    }

    public void AddScore()
    {
        PlayerScore1 += 1;
    }
}
