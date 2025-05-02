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
            else if (currentScene == "Cena2" && previousScene == "Cena5") // transicao da Cena5 para Cena2
            {
                thePlayer.transform.position = new Vector3(-0.08580431f, 14.966f, 0f);
            }
            else if (currentScene == "Cena5" && previousScene == "Cena2") // transicao da Cena2 para Cena5
            {
                thePlayer.transform.position = new Vector3(0.02f, -2.710258f, 0f);
            }
            else if (currentScene == "Cena5" && previousScene == "Cena10") // transicao da Cena10 para Cena5
            {
                thePlayer.transform.position = new Vector3(-6.265987f, -0.5999068f, 0f);
            }
            else if (currentScene == "Cena10" && previousScene == "Cena5") // transicao da Cena5 para Cena10
            {
                thePlayer.transform.position = new Vector3(6.02f, -0.66f, 0f);
            }
            else if (currentScene == "Cena5" && previousScene == "Cena9") // transicao da Cena9 para Cena5
            {
                thePlayer.transform.position = new Vector3(6.159083f, -0.532959f, 0f);
            }
            else if (currentScene == "Cena9" && previousScene == "Cena5") // transicao da Cena5 para Cena9
            {
                thePlayer.transform.position = new Vector3(-6.272023f, -0.66f, 0f);
            }
            else if (currentScene == "Cena5" && previousScene == "Cena6") // transicao da Cena6 para Cena5
            {
                thePlayer.transform.position = new Vector3(0.02f, 1.730677f, 0f);
            }
            else if (currentScene == "Cena6" && previousScene == "Cena5") // transicao da Cena5 para Cena6
            {
                thePlayer.transform.position = new Vector3(0.02f, -2.7f, 0f);
            }
            else if (currentScene == "Cena6" && previousScene == "Cena15") // transicao da Cena15 para Cena6
            {
                thePlayer.transform.position = new Vector3(0.02f, 1.730677f, 0f);
            }
            else if (currentScene == "Cena15" && previousScene == "Cena6") // transicao da Cena6 para Cena15
            {
                thePlayer.transform.position = new Vector3(0.02f, -2.7f, 0f);
            }
            else if (currentScene == "Cena10" && previousScene == "Cena11") // transicao da Cena11 para Cena10
            {
                thePlayer.transform.position = new Vector3(0.02f, 1.730677f, 0f);
            }
            else if (currentScene == "Cena11" && previousScene == "Cena10") // transicao da Cena10 para Cena11
            {
                thePlayer.transform.position = new Vector3(0.02f, -2.7f, 0f);
            }
            else if (currentScene == "Cena9" && previousScene == "Cena12") // transicao da Cena12 para Cena9
            {
                thePlayer.transform.position = new Vector3(0.02f, 1.730677f, 0f);
            }
            else if (currentScene == "Cena12" && previousScene == "Cena9") // transicao da Cena9 para Cena12
            {
                thePlayer.transform.position = new Vector3(0.02f, -2.7f, 0f);
            }
            else if (currentScene == "Cena11" && previousScene == "Cena13") // transicao da Cena13 para Cena11
            {
                thePlayer.transform.position = new Vector3(0.02f, 1.730677f, 0f);
            }
            else if (currentScene == "Cena13" && previousScene == "Cena11") // transicao da Cena11 para Cena13
            {
                thePlayer.transform.position = new Vector3(0.02f, -2.7f, 0f);
            }
            else if (currentScene == "Cena12" && previousScene == "Cena14") // transicao da Cena14 para Cena12
            {
                thePlayer.transform.position = new Vector3(0.02f, 1.730677f, 0f);
            }
            else if (currentScene == "Cena14" && previousScene == "Cena12") // transicao da Cena12 para Cena14
            {
                thePlayer.transform.position = new Vector3(0.02f, -2.7f, 0f);
            }
            else if (currentScene == "Cena13" && previousScene == "Cena15") // transicao da Cena15 para Cena13
            {
                thePlayer.transform.position = new Vector3(6.105287f, -0.4260389f, 0f);
            }
            else if (currentScene == "Cena15" && previousScene == "Cena13") // transicao da Cena13 para Cena15
            {
                thePlayer.transform.position = new Vector3(-5.948434f, -0.519862f, 0f);
            }
            else if (currentScene == "Cena15" && previousScene == "Cena14") // transicao da Cena14 para Cena15
            {
                thePlayer.transform.position = new Vector3(5.942899f, -0.4849414f, 0f);
            }
            else if (currentScene == "Cena14" && previousScene == "Cena15") // transicao da Cena15 para Cena14
            {
                thePlayer.transform.position = new Vector3(-6.119143f, -0.6036257f, 0f);
            }
            else if (currentScene == "Cena16" && previousScene == "Cena15") // transicao da Cena15 para Cena16
            {
                thePlayer.transform.position = new Vector3(0.02f, -2.7f, 0f);
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
            if (playerPosition.x >= -0.7820935f && playerPosition.x <= 0.8307027f && Mathf.Approximately(playerPosition.y, -3.100172f))
            {
                SavePlayerPosition();
                SceneManager.LoadScene("Cena2");
            }
            else if (playerPosition.x >= -0.7181748f && playerPosition.x <= 0.7148368f && Mathf.Approximately(playerPosition.y, 2.066446f))
            {
                SavePlayerPosition();
                SceneManager.LoadScene("Cena6");
            }
            else if (Mathf.Approximately(playerPosition.x, 6.514736f) && playerPosition.y >= -1.51467f && playerPosition.y <= 0.5432134f)
            {
                SavePlayerPosition();
                SceneManager.LoadScene("Cena9");
            }
            else if (Mathf.Approximately(playerPosition.x, -6.772685f) && playerPosition.y >= -1.605973f && playerPosition.y <= 0.5947051f)
            {
                SavePlayerPosition();
                SceneManager.LoadScene("Cena10");
            }
        }

        else if (SceneManager.GetActiveScene().name == "Cena6")
        {
            if (playerPosition.x >= -0.7820935f && playerPosition.x <= 0.8307027f && Mathf.Approximately(playerPosition.y, -3.11787f))
            {
                SavePlayerPosition();
                SceneManager.LoadScene("Cena5");
            }
            else if (playerPosition.x >= -0.7684372f && playerPosition.x <= 0.760188f && Mathf.Approximately(playerPosition.y, 2.016829f))
            {
                SavePlayerPosition();
                SceneManager.LoadScene("Cena15");
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
            if (Mathf.Approximately(playerPosition.x, -6.678032f) && playerPosition.y >= -1.55167f && playerPosition.y <= 0.5063582f)
            {
                SavePlayerPosition();
                SceneManager.LoadScene("Cena5");
            }
            else if (playerPosition.x >= -0.7181748f && playerPosition.x <= 0.7148368f && Mathf.Approximately(playerPosition.y, 2.003242f))
            {
                SavePlayerPosition();
                SceneManager.LoadScene("Cena12");
            }
        }
        else if (SceneManager.GetActiveScene().name == "Cena10")
        {
            if (Mathf.Approximately(playerPosition.x, 6.734772f) && playerPosition.y >= -1.521577f && playerPosition.y <= 0.5304125f)
            {
                SavePlayerPosition();
                SceneManager.LoadScene("Cena5");
            }
            else if (playerPosition.x >= -0.7181748f && playerPosition.x <= 0.7148368f && Mathf.Approximately(playerPosition.y, 2.003242f))
            {
                SavePlayerPosition();
                SceneManager.LoadScene("Cena11");
            }
        }
        else if (SceneManager.GetActiveScene().name == "Cena11")
        {
            if (playerPosition.x >= -0.7820935f && playerPosition.x <= 0.8307027f && Mathf.Approximately(playerPosition.y, -3.11787f))
            {
                SavePlayerPosition();
                SceneManager.LoadScene("Cena10");
            }
            else if (playerPosition.x >= -0.7181748f && playerPosition.x <= 0.7148368f && Mathf.Approximately(playerPosition.y, 2.003242f))
            {
                SavePlayerPosition();
                SceneManager.LoadScene("Cena13");
            }
        }
        else if (SceneManager.GetActiveScene().name == "Cena12")
        {
            if (playerPosition.x >= -0.7820935f && playerPosition.x <= 0.8307027f && Mathf.Approximately(playerPosition.y, -3.11787f))
            {
                SavePlayerPosition();
                SceneManager.LoadScene("Cena9");
            }
            else if (playerPosition.x >= -0.7181748f && playerPosition.x <= 0.7148368f && Mathf.Approximately(playerPosition.y, 2.003242f))
            {
                SavePlayerPosition();
                SceneManager.LoadScene("Cena14");
            }
        }
        else if (SceneManager.GetActiveScene().name == "Cena13")
        {
            if (playerPosition.x >= -0.7820935f && playerPosition.x <= 0.8307027f && Mathf.Approximately(playerPosition.y, -3.11787f))
            {
                SavePlayerPosition();
                SceneManager.LoadScene("Cena11");
            }
            else if (Mathf.Approximately(playerPosition.x, 6.600544f) && playerPosition.y >= -1.586784f && playerPosition.y <= 0.5661189f)
            {
                SavePlayerPosition();
                SceneManager.LoadScene("Cena15");
            }
        }
        else if (SceneManager.GetActiveScene().name == "Cena14")
        {
            if (playerPosition.x >= -0.7820935f && playerPosition.x <= 0.8307027f && Mathf.Approximately(playerPosition.y, -3.11787f))
            {
                SavePlayerPosition();
                SceneManager.LoadScene("Cena12");
            }
            else if (Mathf.Approximately(playerPosition.x, -6.746841f) && playerPosition.y >= -1.55167f && playerPosition.y <= 0.5063582f)
            {
                SavePlayerPosition();
                SceneManager.LoadScene("Cena15");
            }
        }
        else if (SceneManager.GetActiveScene().name == "Cena15")
        {
            if (playerPosition.x >= -0.7820935f && playerPosition.x <= 0.8307027f && Mathf.Approximately(playerPosition.y, -3.100172f))
            {
                SavePlayerPosition();
                SceneManager.LoadScene("Cena6");
            }
            else if (Mathf.Approximately(playerPosition.x, -6.678032f) && playerPosition.y >= -1.55167f && playerPosition.y <= 0.5063582f)
            {
                SavePlayerPosition();
                SceneManager.LoadScene("Cena13");
            }
            else if (Mathf.Approximately(playerPosition.x, 6.600544f) && playerPosition.y >= -1.568303f && playerPosition.y <= 0.5683065f)
            {
                SavePlayerPosition();
                SceneManager.LoadScene("Cena14");
            }
            else if (playerPosition.x >= -0.7296513f && playerPosition.x <= 0.7507334f && Mathf.Approximately(playerPosition.y, 2.066446f))
            {
                SavePlayerPosition();
                SceneManager.LoadScene("Cena16_Boss");
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
