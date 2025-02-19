using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

public class DialogUI : BaseUI
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogText;
    private bool isDialogEnd;
    public override void Init(UIManager uiManager)
    {
        base.Init(uiManager);
        isDialogEnd = true;
    }


    protected override UIState GetUIState()
    {
        return UIState.Dialog;
    }

    public void SetText(string name,string[] dialog)
    {
        if (isDialogEnd)
        {
            isDialogEnd = false;
            nameText.text = name;
            StartCoroutine(DisplayDialog(name,dialog));
        }
        
    }

    private IEnumerator DisplayDialog(string name,string[] sentences)
    {
        foreach (var sentence in sentences)
        {
            dialogText.text = "";
            foreach (char letter in sentence.ToCharArray())
            {
                dialogText.text += letter;
                yield return new WaitForSeconds(0.1f);
            }
        }
        isDialogEnd = true;
        yield return new WaitUntil(() => Input.anyKeyDown);
        nameText.text = "";
        dialogText.text = "";
        SetActive(UIState.Home);

    }
}
