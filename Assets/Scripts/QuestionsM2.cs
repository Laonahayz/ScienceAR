using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.SceneManagement;
using System;

[System.Serializable]
public class QuestionInfo
{
    public string QuestionText;
    public string[] AnswersText;
    public string ExplanationText;
    public int CorrectAnswerIndex;
    public Sprite ExplanationSprite;
    public int StepAssociated; //問題對應的實驗步驟
}
public class QuestionsM2 : MonoBehaviour
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
    public GameObject GotoExperiment_BG;
    public GameObject GotoNextExperiment;
    public GameObject NextBTN;
    public GameObject Quesion_Interface;
    public GameObject retryQuestion_Interface;
    public GameObject GoToReQuesion;
    public GameObject re_Ques_BG;
    public GameObject GoToExplain;
    public GameObject BeakerHintAnim;
    [Header("重試問答")]
    public TMP_Text re_Question;
    public TMP_Text[] re_Ans;
    public Button[] re_AnswerButtons;
    public GameObject re_NextBTN;

    [Header("問題資訊")]
    public int QuestionNum = 0;
    private int currentErrorIndex = 0;  //存在PersistenceManager的錯題索引數字
    private int currentStep = 0;        //存在PersistenceManager的錯題索引數字
    private int nextStepAssociated = 1;
    private int retryCurrentIndex = 0;  // 紀錄當前重試錯題的索引
    public static bool CanCloseBackToExplain = false;
    public static bool isbacktostep;

    //行為紀錄數值
    private int correctAnswerCount, wrongAnswerCount;
    private int Re_correctAnswerCount, Re_wrongAnswerCount;
    private int totalStepsToReturn;
    private DateTime singleExplanationStartTime;  // 單題詳解開始時間
    private float totalExplanationReadTime = 0f;  // 總詳解閱讀時間（秒）
    [SerializeField]
    public List<QuestionInfo> QuestionsList = new List<QuestionInfo>()
    {
        new QuestionInfo
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
        new QuestionInfo
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
        new QuestionInfo
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
        new QuestionInfo
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
        new QuestionInfo
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
        new QuestionInfo
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
        new QuestionInfo
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
        new QuestionInfo
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
        new QuestionInfo
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
        new QuestionInfo
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
        new QuestionInfo
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
        currentStep = PlayerPrefs.GetInt("CurrentStep", 0);
        Debug.Log("currentStep" + currentStep);
        Debug.Log("nextStepAssociated" + nextStepAssociated);
        GotoNextExperiment.SetActive(false);
        if (PlayerPrefs.GetInt("ReviewMistakes", 0) == 1)   //如果是從解釋頁面跳回來就顯示這邊
        {
            int stepToReturn = PlayerPrefs.GetInt("CurrentStep", 0);
            currentErrorIndex = PlayerPrefs.GetInt("CurrentErrorIndex", 0);
            Hint_Manager.Currentstep = stepToReturn;
            Stirring.step1_times = 0;
            Stirring.step2_times = 0;
            Stirring.step3_times = 0;
            Stirring.Step1_Stirring = false;
            Stirring.Step2_Stirring = false;
            Stirring.Step3_Stirring = false;
        }
        else
        {
            DisplayCurrentQuestion();
        }
    }

    void Update()
    {
        isbacktostep = PlayerPrefs.GetInt("isbacktostep", 0) == 1;
        int currentErrorIndex = PlayerPrefs.GetInt("CurrentErrorIndex", 0);
        if (isbacktostep && currentErrorIndex + 1 < PersistenceManager.Instance.IncorrectSteps.Count)
        {
            var errorDetail = PersistenceManager.Instance.IncorrectSteps[currentErrorIndex + 1];
            nextStepAssociated = errorDetail.StepAssociated;
            isbacktostep = false;
            //Debug.Log(nextStepAssociated);
        }
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
        if(isCorrect)
        {
            correctAnswerCount++;
            Debug.Log("正確次數 = " + correctAnswerCount);
        }
        if (!isCorrect)
        {
            wrongAnswerCount++;
            Debug.Log("錯誤次數 = " + wrongAnswerCount);
            var detail = new IncorrectStepDetail
            {
                QuestionNum = QuestionNum,
                StepAssociated = currentQuestion.StepAssociated
            };

            // 檢查是否已經記錄過相同的錯誤
            if (!PersistenceManager.Instance.IncorrectSteps.Any(x => x.QuestionNum == QuestionNum && x.StepAssociated == currentQuestion.StepAssociated))
            {
                PersistenceManager.Instance.IncorrectSteps.Add(detail);
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
            GotoExperiment_BG.SetActive(true);
        }
        for (int i = 0; i < AnswerButtons.Length; i++)
        {
            var buttonImage = AnswerButtons[i].GetComponent<Image>();
            buttonImage.color = Color.white;
        }
        NextBTN.SetActive(false);
    }
    
    public void BacktoStepBTN_OnClick()        //套在BacktoStep的BTN上
    {
        Debug.Log("返回步驟");
        
        if (PersistenceManager.Instance.IncorrectSteps.Count > 0)
        {
            Hint_Manager.Currentstep = currentStep;

            PlayerPrefs.SetInt("CurrentStep", currentStep);
            PlayerPrefs.SetInt("CurrentErrorIndex", currentErrorIndex);
            PlayerPrefs.SetInt("ReviewMistakes", 1); // 1 表示 true
            PlayerPrefs.Save();

            GotoExperiment_BG.SetActive(false);
            Questions.SetActive(false);
            Explain.SetActive(false);
            CanCloseBackToExplain = false;

            Debug.Log("返回到步驟" + currentStep);
            // 重新載入場景
            ReloadScene();
        }
    }
    public void Explain_NextBTN_OnClick()        //套在Explain_NextBTN的BTN上
    {
        // 計算此題詳解時間
        float singleExplanationTime = (float)(DateTime.Now - singleExplanationStartTime).TotalSeconds;
        // 累加到總詳解閱讀時間
        totalExplanationReadTime += singleExplanationTime;
        // 記錄此題詳解時間
        BehaviorLogger.Instance.LogBehavior("此題詳解閱讀時間為：" + singleExplanationTime + " 秒");
        // 重設單題詳解的開始時間為當前時間
        singleExplanationStartTime = DateTime.Now;
        if (currentErrorIndex + 1 < PersistenceManager.Instance.IncorrectSteps.Count)
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
            BehaviorLogger.Instance.LogBehavior("總詳解閱讀時間為：" + totalExplanationReadTime + " 秒");
            GoToReQuesion.SetActive(true);
        }
    }
    private void CalculateTotalStepsToReturn()  //計算需返回幾個步驟並記錄
    {
        // 使用 HashSet 來儲存獨特的步驟
        HashSet<int> uniqueSteps = new HashSet<int>();

        foreach (var error in PersistenceManager.Instance.IncorrectSteps)
        {
            uniqueSteps.Add(error.StepAssociated); // 加入錯誤對應的步驟
        }

        // 將步驟數量賦值給 totalStepsToReturn
        totalStepsToReturn = uniqueSteps.Count;

        BehaviorLogger.Instance.LogBehavior("學生需要返回步驟，且步驟總數：" + totalStepsToReturn);
    }
    public void GotoExperimentBTN_OnClick(int errorIndex)        //從問答跳到重試實驗
    {
        PlayerPrefs.SetInt("isbacktostep", 1);
        //Debug.Log(nextStepAssociated);
        if (errorIndex >= 0 && errorIndex < PersistenceManager.Instance.IncorrectSteps.Count)
        {
            var errorDetail = PersistenceManager.Instance.IncorrectSteps[errorIndex];
            currentStep = errorDetail.StepAssociated;
            currentErrorIndex = errorIndex;
        }
        if (PersistenceManager.Instance.IncorrectSteps.Count > 0)
        {
            Hint_Manager.Currentstep = currentStep;

            PlayerPrefs.SetInt("CurrentStep", currentStep);
            PlayerPrefs.SetInt("CurrentErrorIndex", currentErrorIndex);
            PlayerPrefs.SetInt("ReviewMistakes", 1); // 1 表示 true
            PlayerPrefs.Save();

            GotoExperiment_BG.SetActive(false);
            Questions.SetActive(false);
            Explain.SetActive(false);
            CanCloseBackToExplain = false;

            Debug.Log("返回到步驟" + currentStep);
            Debug.Log("currentStep" + currentStep);
            Debug.Log("nextStepAssociated" + nextStepAssociated);
            BehaviorLogger.Instance.LogBehavior("回答正確次數：" + correctAnswerCount + " 次");
            BehaviorLogger.Instance.LogBehavior("回答錯誤次數：" + wrongAnswerCount + " 次，需閱讀" + wrongAnswerCount + " 次詳解");
            BehaviorLogger.Instance.LogBehavior("回答正確率：" + (float)correctAnswerCount / (correctAnswerCount + wrongAnswerCount) * 100 + " %");
            CalculateTotalStepsToReturn();
            // 重新載入場景
            ReloadScene();
        }
        else
        {
            BehaviorLogger.Instance.LogBehavior("學生不需閱讀詳解。");
            BehaviorLogger.Instance.LogBehavior("學生不需返回步驟。");
            SceneManager.LoadScene("End");
        }
    }
    public void ReturnToExperiment_OnClick()
    {
        PlayerPrefs.SetInt("isbacktostep", 1);
        foreach (var step in PersistenceManager.Instance.IncorrectSteps)
        {
            Debug.Log($"QuestionNum: {step.QuestionNum}, StepAssociated: {step.StepAssociated}");
        }

        while (currentErrorIndex + 1 < PersistenceManager.Instance.IncorrectSteps.Count)
        {
            //currentErrorIndex++;

            if (nextStepAssociated != currentStep)
            {
                currentErrorIndex++;
                var errorDetail = PersistenceManager.Instance.IncorrectSteps[currentErrorIndex];
                currentStep = errorDetail.StepAssociated;
            }
            else if(nextStepAssociated == currentStep)
            {
                currentErrorIndex += 2;
                var errorDetail = PersistenceManager.Instance.IncorrectSteps[currentErrorIndex];
                currentStep = errorDetail.StepAssociated;
            }

            Hint_Manager.Currentstep = currentStep;

            PlayerPrefs.SetInt("CurrentStep", currentStep);
            PlayerPrefs.SetInt("CurrentErrorIndex", currentErrorIndex);
            PlayerPrefs.SetInt("ReviewMistakes", 1); // 1 表示 true
            PlayerPrefs.Save();

            GotoExperiment_BG.SetActive(false);
            Questions.SetActive(false);
            Explain.SetActive(false);
            CanCloseBackToExplain = false;

            Debug.Log("currentStep" + currentStep);
            Debug.Log("nextStepAssociated" + nextStepAssociated);
            Debug.Log("currentErrorIndex" + currentErrorIndex);
            

            if (currentErrorIndex > PersistenceManager.Instance.IncorrectSteps.Count - 1)
            {
                BeakerHintAnim.SetActive(false);
                GoToExplain.SetActive(true);
                GotoNextExperiment.SetActive(false);
                CanCloseBackToExplain = true;
                Debug.Log("所有錯誤步驟已完成");
                return; // 退出迴圈和方法
            }

            ReloadScene();
            return; // 返回場景後中止迴圈
        }

        if (currentErrorIndex >= PersistenceManager.Instance.IncorrectSteps.Count - 1)
        {
            GoToExplain.SetActive(true);
            GotoNextExperiment.SetActive(false);
            CanCloseBackToExplain = true;
            Debug.Log("所有錯誤步驟已完成");
        }
    }
    public void GoToExplainBTN_OnClick()
    {
        Quesion_Interface.SetActive(true);
        Questions.SetActive(false);
        Explain.SetActive(true);
        singleExplanationStartTime = DateTime.Now;
        foreach (var error in PersistenceManager.Instance.IncorrectSteps)
        {
            Debug.Log($"題目編號: {error.QuestionNum}, 步驟: {error.StepAssociated}");
        }
        //在這邊取得第一個錯題的資訊
        currentErrorIndex = 0;
        ShowErrorQuestion(currentErrorIndex);
    }
    private void ShowErrorQuestion(int errorIndex)  //解釋頁面對應資料
    {
        if (errorIndex >= 0 && errorIndex < PersistenceManager.Instance.IncorrectSteps.Count)
        {
            var errorDetail = PersistenceManager.Instance.IncorrectSteps[errorIndex];
            var questionInfo = QuestionsList[errorDetail.QuestionNum];

            QuestionTitle_Explain.text = questionInfo.QuestionText;
            AnsExplain.text = questionInfo.ExplanationText;
            Explain_IMG.sprite = questionInfo.ExplanationSprite;
            CorrectAns.text = questionInfo.AnswersText[questionInfo.CorrectAnswerIndex];

            //currentStep = errorDetail.StepAssociated;

            if (IsCurrentStepCompleted(currentStep))
            {
                NextBTN.SetActive(false);
            }
            else
            {
                NextBTN.SetActive(true);
            }
        }
    }
    private bool IsCurrentStepCompleted(int currentStep)
    {
        var stepErrorsIndexes = PersistenceManager.Instance.IncorrectSteps
        .Select((detail, index) => new { detail, index })
        .Where(x => x.detail.StepAssociated == currentStep)
        .Select(x => x.index)
        .ToList();

        foreach (var index in stepErrorsIndexes)
        {
            if (index > currentErrorIndex)
            {
                return false;
            }
        }
        return true;
    }
    public void ReloadScene()
    {
        // 獲取當前活動場景的名稱
        string sceneName = SceneManager.GetActiveScene().name;
        // 重新加載當前場景
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
        GotoNextExperiment.SetActive(false);
        ShowRetryQuestion(retryCurrentIndex);
    }

    private void ShowRetryQuestion(int index)
    {
        if (index < PersistenceManager.Instance.IncorrectSteps.Count)
        {
            var errorDetail = PersistenceManager.Instance.IncorrectSteps[index];
            var questionInfo = QuestionsList[errorDetail.QuestionNum];

            re_Question.text = questionInfo.QuestionText;
            for (int i = 0; i < re_Ans.Length; i++)
            {
                re_Ans[i].text = questionInfo.AnswersText[i];
                int answerIndex = i;
                re_AnswerButtons[i].onClick.RemoveAllListeners();
                re_AnswerButtons[i].onClick.AddListener(() => OnRetryAnswerSelected(answerIndex));
            }
        }
        else
        {
            retryQuestion_Interface.SetActive(false);
            //SceneManager.LoadScene("End");
        }
    }

    public void OnRetryAnswerSelected(int selectedAnswerIndex)
    {
        var errorDetail = PersistenceManager.Instance.IncorrectSteps[retryCurrentIndex];
        var questionInfo = QuestionsList[errorDetail.QuestionNum];
        bool isCorrect = questionInfo.CorrectAnswerIndex == selectedAnswerIndex;
        if (isCorrect)
        {
            Re_correctAnswerCount++;
            Debug.Log("重試問答正確次數 = " + Re_correctAnswerCount);
        }
        if(!isCorrect)
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
                if (!isCorrect)
                {
                    buttonImage.color = new Color(0.4188679f, 1, 0.4730556f, 0.9f);
                }
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
        re_NextBTN.SetActive(true);
    }

    public void ProceedToNextRetryQuestion()
    {
        foreach (var button in re_AnswerButtons)
        {
            button.interactable = true;
        }
        if (retryCurrentIndex >= PersistenceManager.Instance.IncorrectSteps.Count -1)
        {
            BehaviorLogger.Instance.LogBehavior("重試回答正確次數：" + Re_correctAnswerCount + " 次");
            BehaviorLogger.Instance.LogBehavior("重試回答錯誤次數：" + Re_wrongAnswerCount + " 次");
            BehaviorLogger.Instance.LogBehavior("重試回答正確率：" + (float)Re_correctAnswerCount / (Re_correctAnswerCount + Re_wrongAnswerCount) * 100 + " %");
            SceneManager.LoadScene("End");
            //re_Ques_BG.SetActive(false);
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
