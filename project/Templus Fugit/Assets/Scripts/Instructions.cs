using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Instructions : MonoBehaviour
{
    public GUISkin layout;              // Fonte do placar
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

    public void LoadMainMenu(){
        SceneManager.LoadScene("MainMenu");
    }
}
