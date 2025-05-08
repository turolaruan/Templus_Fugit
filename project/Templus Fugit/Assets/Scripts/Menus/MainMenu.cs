using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GUISkin layout; // Fonte do placar
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnGUI() {
        GUI.skin = layout;
    }

    public void PlayGame() {
        // GameManager.Instance.ResetRunData(); // Reseta os dados do jogo antes de iniciar
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Instructions() {
        // Carrega a cena de instruções
        SceneManager.LoadScene("Instructions");
    }

    public void Volume() {
        // Carrega a cena de volume
        SceneManager.LoadScene("VolumeGame");
    }

    public void QuitGame() {
        // Fecha o jogo
        Application.Quit();
    }
}
