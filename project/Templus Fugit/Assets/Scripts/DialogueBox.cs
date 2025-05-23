using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueBox : MonoBehaviour
{

    public TextMeshProUGUI textComponent;
    public string[] lines;
    public float textSpeed;
    private int index;

    // Start is called before the first frame update
    void Start()
    {
        textComponent.text = string.Empty;
        StartDialog();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)){ // fazer o texto trocar para a proxima linha automaticamente depois que aparecer todo o texto e esperar 2 segundos,sem ter que apertar Space
            if(textComponent.text == lines[index]){
                NextLine();
            }else{
                StopAllCoroutines();
                textComponent.text = lines[index];
            }
        }
    }

    public void StartDialog(){
        index = 0;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine(){
        foreach(char c in lines [index].ToCharArray()){
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    void NextLine(){
        if (index < lines.Length -1 ){
            index++;
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());
        }else{
            gameObject.SetActive(false);
        }
    }
}
