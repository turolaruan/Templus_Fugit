using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance; // Singleton instance
    public static int PlayerScore1 = 0; // Pontuação do player 1
    public GUISkin layout;              // Fonte do contador de tempo
    public static int lifes = 3;        // Vidas do jogador
    public static GameObject thePlayer; // Referência ao objeto jogador

    public float gameTime = 300f;        // Tempo total do jogo em segundos
    private float currentTime;          // Tempo restante

    // Dicionário para armazenar a última posição do jogador em cada cena
    private Dictionary<string, Vector3?> savedPositions = new Dictionary<string, Vector3?>();

    // Variável para armazenar a cena anterior
    private string previousScene = null;

    private bool isInteracting = false; // Verifica se o jogador está interagindo com algo

    private readonly Dictionary<(string from, string to), Vector3> sceneTransitions = new Dictionary<(string, string), Vector3>
    {
        { ("Cena3", "Cena2"), new Vector3(-6.042691f, 8.385761f, 0f) },
        { ("Cena2", "Cena3"), new Vector3(6.16f, -1.6f, 0f) },
        { ("Cena4", "Cena2"), new Vector3(5.78788f, 8.521598f, 0f) },
        { ("Cena2", "Cena4"), new Vector3(-6.16f, -1.03f, 0f) },
        { ("Cena5", "Cena2"), new Vector3(-0.08580431f, 14.966f, 0f) },
        { ("Cena2", "Cena5"), new Vector3(0.02f, -2.710258f, 0f) },
        { ("Cena10", "Cena5"), new Vector3(-6.265987f, -0.5999068f, 0f) },
        { ("Cena5", "Cena10"), new Vector3(6.02f, -0.66f, 0f) },
        { ("Cena9", "Cena5"), new Vector3(6.159083f, -0.532959f, 0f) },
        { ("Cena5", "Cena9"), new Vector3(-6.272023f, -0.66f, 0f) },
        { ("Cena6", "Cena5"), new Vector3(0.02f, 1.730677f, 0f) },
        { ("Cena5", "Cena6"), new Vector3(0.02f, -2.7f, 0f) },
        { ("Cena15", "Cena6"), new Vector3(0.02f, 1.730677f, 0f) },
        { ("Cena6", "Cena15"), new Vector3(0.02f, -2.7f, 0f) },
        { ("Cena11", "Cena10"), new Vector3(0.02f, 1.730677f, 0f) },
        { ("Cena10", "Cena11"), new Vector3(0.02f, -2.7f, 0f) },
        { ("Cena12", "Cena9"), new Vector3(0.02f, 1.730677f, 0f) },
        { ("Cena9", "Cena12"), new Vector3(0.02f, -2.7f, 0f) },
        { ("Cena13", "Cena11"), new Vector3(0.02f, 1.730677f, 0f) },
        { ("Cena11", "Cena13"), new Vector3(0.02f, -2.7f, 0f) },
        { ("Cena14", "Cena12"), new Vector3(0.02f, 1.730677f, 0f) },
        { ("Cena12", "Cena14"), new Vector3(0.02f, -2.7f, 0f) },
        { ("Cena15", "Cena13"), new Vector3(6.105287f, -0.4260389f, 0f) },
        { ("Cena13", "Cena15"), new Vector3(-5.948434f, -0.519862f, 0f) },
        { ("Cena14", "Cena15"), new Vector3(5.942899f, -0.4849414f, 0f) },
        { ("Cena15", "Cena14"), new Vector3(-6.119143f, -0.6036257f, 0f) },
        { ("Cena15", "Cena16"), new Vector3(0.02f, -2.7f, 0f) }
    };

    private readonly List<string> sceneOrder = new List<string>
    {
        "Cena1", "Cena2", "Cena3", "Cena4", "Cena5", "Cena6", "Cena7", "Cena8",
        "Cena9", "Cena10", "Cena11", "Cena12", "Cena13", "Cena14", "Cena15", "Cena16_Boss"
    };

    public HashSet<string> openedChests = new HashSet<string>();

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

    void OnEnable() => SceneManager.sceneLoaded += OnSceneLoaded; // Registra o evento de carregamento de cena
    void OnDisable() => SceneManager.sceneLoaded -= OnSceneLoaded; // Remove o evento de carregamento de cena

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
        if (currentTime <= 0) currentTime = gameTime;
        thePlayer = GameObject.FindGameObjectWithTag("Player");
        DrawLifes();
        PositionPlayerOnSceneLoad();
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
        Debug.Log($"Cena atual: {currentScene}, Cena anterior: {previousScene}");

        if (savedPositions.ContainsKey(currentScene) && savedPositions[currentScene].HasValue)
        {
            if (sceneTransitions.TryGetValue((previousScene, currentScene), out Vector3 targetPosition))
            {
                thePlayer.transform.position = targetPosition;
            }
            else
            {
                thePlayer.transform.position = savedPositions[currentScene].Value;
            }
        }
        else
        {
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
        // Impede a transição de cena enquanto o jogador está interagindo
        if (isInteracting) return;

        if (thePlayer == null)
        {
            thePlayer = GameObject.FindGameObjectWithTag("Player");
            if (thePlayer == null)
            {
                return; // ainda não está disponível, evita NullReference
            }
        }

        DrawLifes();

        // Verifica se a cena atual é "Cena2" ou posterior antes de atualizar o tempo
        if (SceneManager.GetActiveScene().name == "Cena2" || IsSceneAfter("Cena2"))
        {
            // Atualiza o tempo restante
            currentTime -= Time.deltaTime;

            // Reinicia o jogo se o tempo chegar a 0
            if (currentTime <= 0)
            {
                RestartGame();
            }
        }

        CheckSceneTransitions(SceneManager.GetActiveScene().name, thePlayer.transform.position);
    }

    private void CheckSceneTransitions(string scene, Vector2 pos)
    {
        var transitions = new Dictionary<string, (Vector2 min, Vector2 max, string targetScene)[]>
        {
            { "Cena1", new[] { (new Vector2(-1.909972f, 34.06695f), new Vector2(1.910001f, 34.06695f), "Cena2") } },
            { "Cena2", new[] { (new Vector2(-0.9350259f, 15.53006f), new Vector2(0.935003f, 15.53006f), "Cena5"),
                               (new Vector2(-6.335011f, 8.068845f), new Vector2(-6.335011f, 8.935001f), "Cena3"),
                               (new Vector2(6.308114f, 8.064985f), new Vector2(6.308114f, 8.935001f), "Cena4") } },
            { "Cena3", new[] { (new Vector2(6.652393f, -1.935015f), new Vector2(6.652393f, -1.208376f), "Cena2") } },
            { "Cena4", new[] { (new Vector2(-6.752596f, -1.477549f), new Vector2(-6.752596f, -0.5053926f), "Cena2") } },
            { "Cena5", new[] { (new Vector2(-0.7820935f, -3.702741f), new Vector2(0.8307027f, -3.702741f), "Cena2"),
                               (new Vector2(-0.7181748f, 2.066446f), new Vector2(0.7148368f, 2.066446f), "Cena6"),
                               (new Vector2(6.514736f, -1.51467f), new Vector2(6.514736f, 0.5432134f), "Cena9"),
                               (new Vector2(-6.772685f, -1.605973f), new Vector2(-6.772685f, 0.5947051f), "Cena10") } },
            { "Cena6", new[] { (new Vector2(-0.7820935f, -3.11787f), new Vector2(0.8307027f, -3.11787f), "Cena5"),
                               (new Vector2(-0.7684372f, 2.016829f), new Vector2(0.760188f, 2.016829f), "Cena15") } },
            { "Cena8", new[] { (new Vector2(-0.7060629f, -3.107711f), new Vector2(0.7072755f, -3.107711f), "Cena5") } },
            { "Cena9", new[] { (new Vector2(-6.678032f, -1.55167f), new Vector2(-6.678032f, 0.5063582f), "Cena5"),
                               (new Vector2(-0.7181748f, 2.003242f), new Vector2(0.7148368f, 2.003242f), "Cena12") } },
            { "Cena10", new[] { (new Vector2(6.734772f, -1.521577f), new Vector2(6.734772f, 0.5304125f), "Cena5"),
                                (new Vector2(-0.7181748f, 2.003242f), new Vector2(0.7148368f, 2.003242f), "Cena11") } },
            { "Cena11", new[] { (new Vector2(-0.7820935f, -3.11787f), new Vector2(0.8307027f, -3.11787f), "Cena10"),
                                (new Vector2(-0.7181748f, 2.003242f), new Vector2(0.7148368f, 2.003242f), "Cena13") } },
            { "Cena12", new[] { (new Vector2(-0.7820935f, -3.11787f), new Vector2(0.8307027f, -3.11787f), "Cena9"),
                                (new Vector2(-0.7181748f, 2.003242f), new Vector2(0.7148368f, 2.003242f), "Cena14") } },
            { "Cena13", new[] { (new Vector2(-0.7820935f, -3.11787f), new Vector2(0.8307027f, -3.11787f), "Cena11"),
                                (new Vector2(6.600544f, -1.586784f), new Vector2(6.600544f, 0.5661189f), "Cena15") } },
            { "Cena14", new[] { (new Vector2(-0.7820935f, -3.11787f), new Vector2(0.8307027f, -3.11787f), "Cena12"),
                                (new Vector2(-6.746841f, -1.55167f), new Vector2(-6.746841f, 0.5063582f), "Cena15") } },
            { "Cena15", new[] { (new Vector2(-0.7820935f, -3.100172f), new Vector2(0.8307027f, -3.100172f), "Cena6"),
                                (new Vector2(-6.678032f, -1.55167f), new Vector2(-6.678032f, 0.5063582f), "Cena13"),
                                (new Vector2(6.600544f, -1.568303f), new Vector2(6.600544f, 0.5683065f), "Cena14"),
                                (new Vector2(-0.7296513f, 2.066446f), new Vector2(0.7507334f, 2.066446f), "Cena16_Boss") } }
        };

        if (transitions.ContainsKey(scene))
        {
            foreach (var (min, max, targetScene) in transitions[scene])
            {
                if (pos.x >= min.x && pos.x <= max.x && pos.y >= min.y && pos.y <= max.y)
                {
                    // Verifica se a cena de destino é diferente da cena atual
                    if (SceneManager.GetActiveScene().name != targetScene)
                    {
                        Debug.Log($"Transição de cena: {scene} -> {targetScene}");

                        // Salva a posição do jogador antes de trocar de cena
                        SavePlayerPosition();
                        SceneManager.LoadScene(targetScene);
                        break;
                    }
                }
            }
        }
    }

    public void LoseLife(int amount)
    {
        lifes -= amount; // Reduz a quantidade de vidas com base no dano
        Debug.Log("Vidas restantes: " + lifes);

        DrawLifes(); // Atualiza a exibição dos corações na tela

        if (lifes > 0)
        {
            SavePlayerPosition(); // Salva a posição do jogador
        }
        else
        {
            RestartGame(); // Reinicia o jogo se as vidas acabarem
        }
    }

    public static void DrawLifes()
    {
        for (int i = 0; i < 3; i++)
        {
            GameObject heart = GameObject.Find($"Heart{i}");
            if (heart != null)
            {
                // Exibe apenas os corações correspondentes às vidas restantes
                heart.GetComponent<SpriteRenderer>().enabled = i < lifes;
            }
        }
    }

    // Gerência da pontuação e fluxo do jogo
    void OnGUI()
    {
        // Verifica se a cena atual é "Cena1"
        if (SceneManager.GetActiveScene().name == "Cena1")
        {
            return; // Não exibe o timer na Cena1
        }

        // Define a cor do texto no GUISkin
        layout.label.normal.textColor = Color.white; // Altere para a cor desejada

        GUI.skin = layout;

        // Exibe o tempo restante no formato "0s"
        string timeText = Mathf.FloorToInt(currentTime).ToString() + "s";

        // Define a posição no canto superior direito
        float labelWidth = 100f; // Largura do texto
        float labelHeight = 50f; // Altura do texto
        float xPosition = Screen.width - labelWidth - 10f; // 10px de margem da borda direita
        float yPosition = 10f; // 10px de margem do topo

        GUI.Label(new Rect(xPosition, yPosition, labelWidth, labelHeight), timeText);
    }

    // public void GameOver()
    // {
    //     lifes = 3; // Reset lifes when game is over
    //     PlayerScore1 = 0; // Reset score when game is over
    //     savedPositions.Clear(); // Limpa todas as posições salvas
    //     SceneManager.LoadScene("GameOver");
    // }

    public void RestartGame()
    {
        lifes = 3; // Reset vidas ao reiniciar o jogo
        currentTime = gameTime; // Reinicia o tempo
        // savedPositions.Clear(); // Limpa todas as posições salvas
        SceneManager.LoadScene("Cena2"); // Reinicia na cena inicial
    }

    private bool IsSceneAfter(string sceneName) => sceneOrder.IndexOf(SceneManager.GetActiveScene().name) > sceneOrder.IndexOf(sceneName);

    public void ReduceTime(float penalty)
    {
        currentTime -= penalty; // Subtrai o tempo de penalidade
        if (currentTime < 0)
        {
            currentTime = 0; // Garante que o tempo não fique negativo
            RestartGame(); // Reinicia o jogo se o tempo acabar
        }
    }

    public void SetInteracting(bool interacting)
    {
        isInteracting = interacting;
        Debug.Log($"isInteracting: {isInteracting}");
    }

    // Chame isso quando o jogo iniciar uma nova run
    public void ResetRunData()
    {
        openedChests.Clear(); // Limpa os baús abertos
    }
}
