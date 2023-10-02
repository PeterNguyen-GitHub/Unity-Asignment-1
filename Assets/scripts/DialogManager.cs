using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

//Manages dialog so it can be shown on screen
public class DialogManager : MonoBehaviour
{
    [SerializeField] GameObject dialogBox;
    [SerializeField] Text dialogText;

    [SerializeField] int lettersPerSecond;

    public event Action OnShowDialog;
    public event Action OnHideDialog;
    public static DialogManager Instance { get; private set;}
    private void Awake() {
        Instance = this; 
    }

    Dialog dialog;
    int currentLine = 0; 
    bool isTyping; 


    public IEnumerator ShowDialog(Dialog dialog)
    {
        yield return new WaitForEndOfFrame();
        OnShowDialog?.Invoke();

        this.dialog = dialog;
        dialogBox.SetActive(true);
        StartCoroutine(TypeDialog(dialog.Lines[0]));
    }

    public void HandleUpdate()
    {
        if(Input.GetKeyDown(KeyCode.Z) && !isTyping)
        {
            ++currentLine;
            if(currentLine < dialog.Lines.Count)//start showing next lines
            {
                StartCoroutine(TypeDialog(dialog.Lines[currentLine]));//show line

            }else{
                dialogBox.SetActive(false); //turn off dialog box
                currentLine = 0; 
                OnHideDialog?.Invoke(); //then hide it
            }
        }
    }

    //pokemon/finalfantasy text animation
    public IEnumerator TypeDialog(string line)
    {
        isTyping = true; 
        dialogText.text = "";
        //this adds each letter one by one for the animation
        foreach(var letter in line.ToCharArray())
        {
            dialogText.text += letter; 
            yield return new WaitForSeconds(1f / lettersPerSecond);
        }
        isTyping = false; 
    }
}
