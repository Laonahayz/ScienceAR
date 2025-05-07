using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChooseClass : MonoBehaviour
{
    public GameObject NotClassmate_BG;
    public void Choose702_OnClick()
    {
        SceneManager.LoadScene("CG_Start");
    }

    public void Choose711_OnClick()
    {
        SceneManager.LoadScene("Start");
    }
    public void NotClassM_BTN_OnClick()
    {
        NotClassmate_BG.SetActive(true);
    }
    public void ChooseDrive_OnClick()
    {
        Application.OpenURL("https://drive.google.com/drive/folders/1LAFkfaLKBQrnVJgn0pwItu-U6UEeDU8D?usp=sharing");
    }
    public void StartBTN_OnClick()
    {
        SceneManager.LoadScene("Start");
    }
}
