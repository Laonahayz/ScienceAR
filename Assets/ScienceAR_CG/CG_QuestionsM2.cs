using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.SceneManagement;
using System;

[System.Serializable]
public class CG_QuestionInfo
{
    public string QuestionText;
    public string[] AnswersText;
    public string ExplanationText;
    public int CorrectAnswerIndex;
    public Sprite ExplanationSprite;
    public int StepAssociated; //問題對應的實驗步驟
}
public class CG_QuestionsM2 : MonoBehaviour
{
    [Header("說明")]
    public TMP_Text QuestionTitle_Explain;
    public TMP_Text AnsExplain;
    public TMP_Text CorrectAns;
    public Image Explain_IMG;

    [Header("問答")]
    public TMP_Text Question;
    public TMP_Text[] Ans;
    public Button[] AnswerButtons;

    [Header("用來切換問題區&說明區")]
    public GameObject Questions;
    public GameObject Explain;
    public GameObject GoToExplain_BG;
    public GameObject NextBTN;
    public GameObject ChooseBTN;

    [Header("重試問答")]
    public TMP_Text re_Question;
    public TMP_Text re_Explain_Question;
    public TMP_Text[] re_Ans;
    public Button[] re_AnswerButtons;
    public TMP_Text re_AnsExplain;
    public TMP_Text re_CorrectAns;
    public Image re_Explain_IMG;
    public GameObject re_NextBTN;
    public GameObject retryQuestion_Interface;
    public GameObject re_Ques_BG;
    public GameObject re_Explain_BG;

    [Header("重試實驗判定")]
    public static bool ReStartExperiment;

    [Header("問題資訊")]
    public int QuestionNum = 0;
    private int currentErrorIndex = 0;  //存在CG_PersistenceManager的錯題索引數字
    private int retryCurrentIndex = 0;  // 紀錄當前重試錯題的索引
    private int currentStep = 0;        //存在CG_PersistenceManager的錯題索引數字
    public static bool CanCloseBackToExplain = false;

    //行為紀錄數值
    private int correctAnswerCount, wrongAnswerCount;
    private int Re_correctAnswerCount, Re_wrongAnswerCount;
    private DateTime singleExplanationStartTime;  // 單題詳解開始時間
    private float totalExplanationReadTime = 0f;  // 總詳解閱讀時間（秒）

    [SerializeField]
    public List<CG_QuestionInfo> QuestionsList = new List<CG_QuestionInfo>()
    {
        new CG_QuestionInfo
        {
            QuestionText = "1. 混合椰子油和酒精後，溶液的物理狀態變化為何？",
            AnswersText = new string[] {
                "會產生新的化學物質，使得溶液變成有色且產生氣泡。",
                "當椰子油和酒精混合時，屬於物理變化，並不會有新物質產生，因此只會呈現透明無色的溶液狀態。",
                "溶液會產生咖啡色的沉澱物。",
                "溶液變得完全不透明，看起來像是乳白色的混合物。"
            },
            ExplanationText = "解釋：當椰子油和酒精混合時，屬於物理變化，並不會有新物質產生，因此只會呈現透明無色的溶液狀態。",
            CorrectAnswerIndex = 1,
            StepAssociated = 1,
        },
        new CG_QuestionInfo
        {
            QuestionText = "2. 為什麼製作肥皂要加入酒精？",
            AnswersText = new string[] {
                "加入酒精的目的是為了提高肥皂的香氣，使得肥皂在使用時有更好的香味體驗。",
                "酒精的加入是為了殺菌，確保肥皂製作過程中不會有細菌滋生，保持肥皂的衛生。",
                "加入酒精是為了調節肥皂的顏色，使最終產品能有更鮮豔的色彩表現。",
                "酒精可以作為溶劑，幫助椰子油中的脂肪酸與氫氧化鈉更好混合，加快皂化反應的速率。"
            },
            ExplanationText = "解釋：酒精可以作為溶劑，幫助椰子油中的脂肪酸與氫氧化鈉更好混合，加快皂化反應的速率。",
            CorrectAnswerIndex = 3,
            StepAssociated = 1
        },
        new CG_QuestionInfo
        {
            QuestionText = "3. 為什麼製作肥皂要添加氫氧化鈉溶液？",
            AnswersText = new string[] {
                "因為氫氧化鈉是一種鹼，而皂化反應是一種油脂與鹼反應生成脂肪酸鈉（肥皂）和甘油的過程。",
                "添加氫氧化鈉是為了直接增加溶液的溫度，從而加速所有化學反應的速率。",
                "添加氫氧化鈉是為了使溶液酸化，從而促進皂化反應。",
                "添加氫氧化鈉主要作用可以保持整個實驗溶液的穩定性。"
            },
            ExplanationText = "解釋：因為氫氧化鈉與椰子油中的脂肪酸發生化學反應，生成肥皂和甘油，這個過程稱為皂化反應。",
            CorrectAnswerIndex = 0,
            StepAssociated = 2
        },
        new CG_QuestionInfo
        {
            QuestionText = "4. 下列何者敘述符合皂化反應的過程？",
            AnswersText = new string[] {
                "氫氧化鈉在高溫下會變黏，從而使整個混合物的黏稠度增加，產生肥皂。",
                "油脂或脂肪在高溫下自行分解，產生肥皂。",
                "氫氧化鈉與椰子油中的脂肪酸發生化學反應，生成肥皂和甘油。",
                "酒精在加熱過程中會與氫氧化鈉反應，產生肥皂。"
            },
            ExplanationText = "解釋：氫氧化鈉是一種鹼，而皂化反應是一種酯類與鹼透過化學反應生成脂肪酸鹽（肥皂）和甘油的過程。",
            CorrectAnswerIndex = 2,
            StepAssociated = 2
        },
        new CG_QuestionInfo
        {
            QuestionText = "5. 加入飽和鹽水後溶液的變化？",
            AnswersText = new string[] {
                "加入飽和食鹽水後，肥皂會從溶液中分離出來，形成浮在溶液表面的白色固體。",
                "加入飽和食鹽水後會使溶液的pH值發生劇變，從而使溶液變得極酸。",
                "加入飽和食鹽水後可以使得肥皂的顏色發生變化。",
                "加入飽和食鹽水後會立即引發劇烈的化學反應，釋放大量熱能和氣體，從而導致溶液沸騰。"
            },
            ExplanationText = "解釋：肥皂會從溶液中被分離出來，變成浮在溶液表面的白色固體。",
            CorrectAnswerIndex = 0,
            StepAssociated = 3
        },
        new CG_QuestionInfo
        {
            QuestionText = "6. 溶液表面浮出肥皂，其化學原理是什麼？",
            AnswersText = new string[] {
                "利用了「乳化作用」原理，幫助油脂與水混合，形成穩定的乳液。",
                "利用了「滲透作用」原理，使溶液中的水分移向飽和鹽水，減少溶液中的水分含量，分離出肥皂。",
                "利用了「鹽析作用」原理，肥皂難溶於飽和食鹽水之特性，使肥皂與甘油分離浮於水面。",
                "利用了「沉澱作用」原理，透過清除溶液中的雜質，分離出肥皂。"
            },
            ExplanationText = "解釋：加入飽和食鹽水後透過「鹽析作用」的原理，肥皂會因為其難溶於飽和食鹽水的特性從溶液中被分離出來，浮在溶液表面。",
            CorrectAnswerIndex = 2,
            StepAssociated = 3
        },
        new CG_QuestionInfo
        {
            QuestionText = "7. 為什麼肥皂會浮在溶液上層？",
            AnswersText = new string[] {
                "因為肥皂在溶液中形成了氣泡，這些氣泡使肥皂浮到了溶液的上層。",
                "因為肥皂分子在加入鹽水後發生了輕質化反應，變得更輕。",
                "因為溶液加熱後會使得肥皂膨脹，與熱氣球上升原理相同。",
                "因為肥皂的密度小於飽和食鹽水的密度。"
            },
            ExplanationText = "解釋：肥皂上浮的現象，主要是因為肥皂的密度小於水的密度，導致肥皂與甘油分開並浮到水面上。",
            CorrectAnswerIndex = 3,
            StepAssociated = 4
        },
        new CG_QuestionInfo
        {
            QuestionText = "8. 肥皂碰到石蕊試紙會呈現什麼顏色？",
            AnswersText = new string[] {
                "會呈現藍色。",
                "會呈現紅色。",
                "會呈現紫色。",
                "不會有顏色變化。"
            },
            ExplanationText = "解釋：肥皂是鹼性，因此沾上紅色石蕊試紙會變成藍色，而沾上藍色石蕊試紙則不會有顏色變化。",
            CorrectAnswerIndex = 0,
            StepAssociated = 5
        },
        new CG_QuestionInfo
        {
            QuestionText = "9. 基於肥皂碰觸石蕊試紙後呈現的顏色變化，推論肥皂的酸鹼性為何？",
            AnswersText = new string[] {
                "肥皂是鹼性。",
                "肥皂是中性。",
                "無法透過石蕊試紙的顏色變化去推斷肥皂的酸鹼性。",
                "肥皂是酸性。"
            },
            ExplanationText = "解釋：肥皂是鹼性，因此沾上紅色石蕊試紙會變成藍色，而沾上藍色石蕊試紙則不會有顏色變化。",
            CorrectAnswerIndex = 0,
            StepAssociated = 5
        },
        new CG_QuestionInfo
        {
            QuestionText = "10. 沙拉油和蒸餾水混合後的結果為何？",
            AnswersText = new string[] {
                "溶液會變成深藍色。",
                "溶液中的油和水不會混合，形成兩層。",
                "溶液會立即凝固。",
                "溶液會和蒸餾水會完全混合，變成透明溶液。"
            },
            ExplanationText = "解釋：溶液中的油和水不會混合，形成兩層。",
            CorrectAnswerIndex = 1,
            StepAssociated = 6
        },
        new CG_QuestionInfo
        {
            QuestionText = "11. 肥皂如何改變油和水的混合性？",
            AnswersText = new string[] {
                "加入肥皂後，油會被直接被肥皂進行化學分解反應。",
                "加入肥皂後，會立即引發一個強烈的化學反應，使得油被轉化為一種與水可以自由混合的物質。",
                "加入肥皂後，肥皂會吸收所有的油，讓溶液看起來被混合了，但實際上是油被肥皂吸收了。",
                "加入肥皂後，油和水混合物會開始混合，形成乳濁液，這是乳化作用。"
            },
            ExplanationText = "解釋：加入肥皂後，油和水混合物會開始混合，形成乳濁液，這是乳化作用。",
            CorrectAnswerIndex = 3,
            StepAssociated = 7
        },
    };

    void Start()
    {
        DisplayCurrentQuestion();
    }

    public void DisplayCurrentQuestion()
    {
        if (QuestionNum < QuestionsList.Count)
        {
            var questionInfo = QuestionsList[QuestionNum];
            Question.text = questionInfo.QuestionText;
            for (int i = 0; i < Ans.Length; i++)
            {
                Ans[i].text = questionInfo.AnswersText[i];
            }
            AnsExplain.text = questionInfo.ExplanationText;
            Explain_IMG.sprite = questionInfo.ExplanationSprite;  // 正確設置圖片
        }
    }


    public void OnAnswerSelected(int answerIndex)
    {
        var currentQuestion = QuestionsList[QuestionNum];
        bool isCorrect = currentQuestion.CorrectAnswerIndex == answerIndex; //辨別選擇答案是否正確
        if (isCorrect)
        {
            correctAnswerCount++;
            Debug.Log("正確次數 = " + correctAnswerCount);
        }
        if (!isCorrect)
        {
            wrongAnswerCount++;
            var detail = new CG_IncorrectStepDetail
            {
                QuestionNum = QuestionNum,
                StepAssociated = currentQuestion.StepAssociated
            };

            // 檢查是否已經記錄過相同的錯誤
            if (!CG_PersistenceManager.Instance.IncorrectSteps.Any(x => x.QuestionNum == QuestionNum && x.StepAssociated == currentQuestion.StepAssociated))
            {
                CG_PersistenceManager.Instance.IncorrectSteps.Add(detail);
            }
        }
        foreach (var button in AnswerButtons)
        {
            button.interactable = false;
        }
        //按鈕顏色
        for (int i = 0; i < AnswerButtons.Length; i++)
        {
            var buttonImage = AnswerButtons[i].GetComponent<Image>();
            if (i == answerIndex)
            {
                buttonImage.color = isCorrect ? new Color(0.4188679f, 1, 0.4730556f, 0.9f) : new Color(1, 0.4784514f, 0.4196079f, 0.9f);
            }
            else if (i == currentQuestion.CorrectAnswerIndex)
            {
                if (!isCorrect) buttonImage.color = new Color(0.4188679f, 1, 0.4730556f, 0.9f);
            }
            else
            {
                buttonImage.color = Color.white;
            }
        }
        NextBTN.SetActive(true);
        
    }
    
    public void NextQuestion()  //套在題目的NextBTN上
    {
        QuestionNum++;
        foreach (var button in AnswerButtons)
        {
            button.interactable = true;
        }
        if (QuestionNum < QuestionsList.Count)
        {
            DisplayCurrentQuestion();
        }
        else
        {
            GoToExplain_BG.SetActive(true);
        }
        for (int i = 0; i < AnswerButtons.Length; i++)
        {
            var buttonImage = AnswerButtons[i].GetComponent<Image>();
            buttonImage.color = Color.white;
        }
        NextBTN.SetActive(false);
    }
    public void Explain_NextBTN_OnClick()        //套在Explain_NextBTN的BTN上
    {
        // 計算此題詳解時間
        float singleExplanationTime = (float)(DateTime.Now - singleExplanationStartTime).TotalSeconds;
        // 累加到總詳解閱讀時間
        totalExplanationReadTime += singleExplanationTime;
        // 記錄此題詳解時間
        CG_BehaviorLogger.Instance.LogBehavior("此題詳解閱讀時間為：" + singleExplanationTime + " 秒");
        // 重設單題詳解的開始時間為當前時間
        singleExplanationStartTime = DateTime.Now;
        if (currentErrorIndex + 1 < CG_PersistenceManager.Instance.IncorrectSteps.Count)
        {
            currentErrorIndex++;
            ShowErrorQuestion(currentErrorIndex);
        }
        else
        {
            Debug.Log("Last");
            // 最後一題詳解結束時計算並記錄總詳解閱讀時間
            singleExplanationTime = (float)(DateTime.Now - singleExplanationStartTime).TotalSeconds;
            totalExplanationReadTime += singleExplanationTime; // 累加最後一題的時間
            // 記錄總詳解閱讀時間
            CG_BehaviorLogger.Instance.LogBehavior("總詳解閱讀時間為：" + totalExplanationReadTime + " 秒");
            ChooseBTN.SetActive(true);
        }
    }
    public void GotoExplainBTN_OnClick()        //套在GotoExplainBTN的BTN上
    {
        singleExplanationStartTime = DateTime.Now;
        if (CG_PersistenceManager.Instance.IncorrectSteps.Count <= 0)
        {
            ChooseBTN.SetActive(true);
        }
        CG_BehaviorLogger.Instance.LogBehavior("回答正確次數：" + correctAnswerCount + " 次");
        CG_BehaviorLogger.Instance.LogBehavior("回答錯誤次數：" + wrongAnswerCount + " 次，需閱讀" + wrongAnswerCount + " 次詳解");
        CG_BehaviorLogger.Instance.LogBehavior("回答正確率：" + (float)correctAnswerCount / (correctAnswerCount + wrongAnswerCount) * 100 + " %");
        Questions.SetActive(false);
        GoToExplain_BG.SetActive(false);
        Explain.SetActive(true);
        foreach (var error in CG_PersistenceManager.Instance.IncorrectSteps)
        {
            Debug.Log($"題目編號: {error.QuestionNum}, 步驟: {error.StepAssociated}");
        }
        //在這邊取得第一個錯題的資訊
        currentErrorIndex = 0;
        ShowErrorQuestion(currentErrorIndex);
    }

    public void GoEnd_OnClick()        //套在GotoExplainBTN的BTN上
    {
        SceneManager.LoadScene("CG_End");
        CG_BehaviorLogger.Instance.LogBehavior("選擇結束程式");
    }

    public void GoReWrongQus_OnClick()
    {
        StartRetryQuestions();
        CG_BehaviorLogger.Instance.LogBehavior("選擇重試問答");
    }
    public void GoReExperiment_OnClick()
    {
        ReloadScene();
        CG_BehaviorLogger.Instance.LogBehavior("選擇重試實驗");
    }
    private void ShowErrorQuestion(int errorIndex)  //解釋頁面對應資料
    {
        if (errorIndex >= 0 && errorIndex < CG_PersistenceManager.Instance.IncorrectSteps.Count)
        {
            var errorDetail = CG_PersistenceManager.Instance.IncorrectSteps[errorIndex];
            var questionInfo = QuestionsList[errorDetail.QuestionNum];

            QuestionTitle_Explain.text = questionInfo.QuestionText;
            AnsExplain.text = questionInfo.ExplanationText;
            Explain_IMG.sprite = questionInfo.ExplanationSprite;
            CorrectAns.text = questionInfo.AnswersText[questionInfo.CorrectAnswerIndex];

            currentStep = errorDetail.StepAssociated;
        }
    }
    
    public void ReloadScene()
    {
        // 獲取當前活動場景的名稱
        string sceneName = SceneManager.GetActiveScene().name;
        // 重新加載當前場景
        CG_Hint_Manager.Currentstep = 1;
        ReStartExperiment = true;
        SceneManager.LoadScene(sceneName);
    }
    // BTN_Onclick事件
    public void Ans_1_OnClick() { OnAnswerSelected(0); }
    public void Ans_2_OnClick() { OnAnswerSelected(1); }
    public void Ans_3_OnClick() { OnAnswerSelected(2); }
    public void Ans_4_OnClick() { OnAnswerSelected(3); }

    public void StartRetryQuestions()
    {
        retryCurrentIndex = 0;
        retryQuestion_Interface.SetActive(true);
        ChooseBTN.SetActive(false);
        ShowRetryQuestion(retryCurrentIndex);
    }

    private void ShowRetryQuestion(int index)
    {
        if (index < CG_PersistenceManager.Instance.IncorrectSteps.Count)
        {
            var errorDetail = CG_PersistenceManager.Instance.IncorrectSteps[index];
            var questionInfo = QuestionsList[errorDetail.QuestionNum];

            re_Question.text = questionInfo.QuestionText;
            for (int i = 0; i < re_Ans.Length; i++)
            {
                re_Ans[i].text = questionInfo.AnswersText[i];
                int answerIndex = i;
                re_AnswerButtons[i].onClick.RemoveAllListeners();
                re_AnswerButtons[i].onClick.AddListener(() => OnRetryAnswerSelected(answerIndex));
            }

            re_AnsExplain.text = "";
            re_Explain_IMG.sprite = null;
            re_CorrectAns.text = "";
        }
        else
        {
            retryQuestion_Interface.SetActive(false);
            SceneManager.LoadScene("CG_End");
        }
    }

    public void OnRetryAnswerSelected(int selectedAnswerIndex)
    {
        var errorDetail = CG_PersistenceManager.Instance.IncorrectSteps[retryCurrentIndex];
        var questionInfo = QuestionsList[errorDetail.QuestionNum];
        bool isCorrect = questionInfo.CorrectAnswerIndex == selectedAnswerIndex;
        if (isCorrect)
        {
            Re_correctAnswerCount++;
            Debug.Log("重試問答正確次數 = " + Re_correctAnswerCount);
        }
        if (!isCorrect)
        {
            Re_wrongAnswerCount++;
            Debug.Log("重試問答錯誤次數 = " + Re_wrongAnswerCount);
        }
        for (int i = 0; i < re_AnswerButtons.Length; i++)
        {
            var buttonImage = re_AnswerButtons[i].GetComponent<Image>();
            if (i == selectedAnswerIndex)
            {
                buttonImage.color = isCorrect ? new Color(0.4188679f, 1, 0.4730556f, 0.9f) : new Color(1, 0.4784514f, 0.4196079f, 0.9f);
            }
            else if (i == questionInfo.CorrectAnswerIndex)
            {
                if (!isCorrect) buttonImage.color = new Color(0.4188679f, 1, 0.4730556f, 0.9f);
            }
            else
            {
                buttonImage.color = Color.white;
            }
        }
        foreach (var button in re_AnswerButtons)
        {
            button.interactable = false;
        }
        re_AnsExplain.text = questionInfo.ExplanationText;
        re_Explain_IMG.sprite = questionInfo.ExplanationSprite;
        re_CorrectAns.text = questionInfo.AnswersText[questionInfo.CorrectAnswerIndex];

        re_NextBTN.SetActive(true);
    }

    public void ProceedToNextRetryQuestion()
    {
        foreach (var button in re_AnswerButtons)
        {
            button.interactable = true;
        }
        if (retryCurrentIndex >= CG_PersistenceManager.Instance.IncorrectSteps.Count - 1)
        {
            CG_BehaviorLogger.Instance.LogBehavior("重試回答正確次數：" + Re_correctAnswerCount + " 次");
            CG_BehaviorLogger.Instance.LogBehavior("重試回答錯誤次數：" + Re_wrongAnswerCount + " 次");
            CG_BehaviorLogger.Instance.LogBehavior("重試回答正確率：" + (float)Re_correctAnswerCount / (Re_correctAnswerCount + Re_wrongAnswerCount) * 100 + " %");
            SceneManager.LoadScene("CG_End");
        }
        else
        {
            for (int i = 0; i < re_AnswerButtons.Length; i++)
            {
                var buttonImage = re_AnswerButtons[i].GetComponent<Image>();
                buttonImage.color = Color.white;
            }
            retryCurrentIndex++;
            ShowRetryQuestion(retryCurrentIndex);
            re_NextBTN.SetActive(false);

        }
    }
    
}
