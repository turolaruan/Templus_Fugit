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

    // Start is called before the first frame update
    void Start()
    {
        thePlayer = GameObject.FindGameObjectWithTag("Player"); // Busca a referência do jogador
        DrawLifes();
    }

    public void LoseLife()
    {
        lifes--;
        PlayerScore1 = 0; // Reset score when losing a life
        // Reload the scene, reset coins, reset score, but keep player lives
        if (lifes > 0)
        {
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

    // Update is called once per frame
    void Update()
    {
        DrawLifes();

        if (PlayerScore1 >= 7)
        {
            if (SceneManager.GetActiveScene().buildIndex == 2) // Check if it's the second level
            {
            PlayerScore1 = 0; // Reset score when reaching 7 points
            lifes = 3; // Reset lifes when reaching 7 points
            SceneManager.LoadScene("YouWin"); // Load the victory scene
            }
            else
            {
            PlayerScore1 = 0; // Reset score when reaching 7 points
            lifes = 3; // Reset lifes when reaching 7 points
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); // Load next level
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
        SceneManager.LoadScene("GameOver");
    }

    public void RestartGame()
    {
        lifes = 3; // Reset lifes only when restarting the entire game
        PlayerScore1 = 0;
    }

    public void AddScore()
    {
        PlayerScore1 += 1;
    }
}
