using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class StartScene : MonoBehaviour
{
    public TMP_InputField InputNum, PWD_InputField;
    private string correctPassword = "871121";
    public GameObject HintText;
    public GameObject BG, Input;
    public GameObject Correct_msg, Wrong_msg;
    // Start is called before the first frame update
    void Start()
    {
        if (SceneManager.GetActiveScene().name == "End")
        {
            BehaviorLogger.Instance.ShareLogFile();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void CheckPassword()
    {
        string inputPassword = PWD_InputField.text;

        if (inputPassword == correctPassword)
        {
            Correct_msg.SetActive(true);
            Wrong_msg.SetActive(false);
            PlayerPrefs.DeleteAll();
        }
        else
        {
            Correct_msg.SetActive(false);
            Wrong_msg.SetActive(true);
        }
    }
    public void GoToInput_OnClick()
    {
        BG.SetActive(false);
        Input.SetActive(true);
    }
    public void GoToExperiment_OnClick()
    {
        string inputText = InputNum.text; // 取得輸入文字

        if (string.IsNullOrEmpty(inputText)) // 檢查是否為空
        {
            HintText.SetActive(true);
        }
        else
        {
            SceneManager.LoadScene("Demo");
        }
    }

    public void GoToURL()
    {
        Application.OpenURL("https://forms.gle/ttYwiCAmv5mTE4Ph7");
    }
}
