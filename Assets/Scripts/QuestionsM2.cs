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
    public int StepAssociated; //���D����������B�J
}
public class QuestionsM2 : MonoBehaviour
{
    [Header("����")]
    public TMP_Text QuestionTitle_Explain;
    public TMP_Text AnsExplain;
    public TMP_Text CorrectAns;
    public Image Explain_IMG;

    [Header("�ݵ�")]
    public TMP_Text Question;
    public TMP_Text[] Ans;
    public Button[] AnswerButtons;
    [Header("�ΨӤ������D��&������")]
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
    [Header("���հݵ�")]
    public TMP_Text re_Question;
    public TMP_Text[] re_Ans;
    public Button[] re_AnswerButtons;
    public GameObject re_NextBTN;

    [Header("���D��T")]
    public int QuestionNum = 0;
    private int currentErrorIndex = 0;  //�s�bPersistenceManager�����D���޼Ʀr
    private int currentStep = 0;        //�s�bPersistenceManager�����D���޼Ʀr
    private int nextStepAssociated = 1;
    private int retryCurrentIndex = 0;  // ������e���տ��D������
    public static bool CanCloseBackToExplain = false;
    public static bool isbacktostep;

    //�欰�����ƭ�
    private int correctAnswerCount, wrongAnswerCount;
    private int Re_correctAnswerCount, Re_wrongAnswerCount;
    private int totalStepsToReturn;
    private DateTime singleExplanationStartTime;  // ���D�ԸѶ}�l�ɶ�
    private float totalExplanationReadTime = 0f;  // �`�ԸѾ\Ū�ɶ��]��^
    [SerializeField]
    public List<QuestionInfo> QuestionsList = new List<QuestionInfo>()
    {
        new QuestionInfo
        {
            QuestionText = "1. �V�X���l�o�M�s���A���G�����z���A�ܤƬ���H",
            AnswersText = new string[] {
                "�|���ͷs���ƾǪ���A�ϱo���G�ܦ�����B���ͮ�w�C",
                "���l�o�M�s��V�X�ɡA�ݩ󪫲z�ܤơA�ä��|���s���貣�͡A�]���u�|�e�{�z���L�⪺���G���A�C",
                "���G�|���ͩ@�ئ⪺�I�����C",
                "���G�ܱo�������z���A�ݰ_�ӹ��O�ťզ⪺�V�X���C"
            },
            ExplanationText = "�����G���l�o�M�s��V�X�ɡA�ݩ󪫲z�ܤơA�ä��|���s���貣�͡A�]���u�|�e�{�z���L�⪺���G���A�C",
            CorrectAnswerIndex = 1,
            StepAssociated = 1,
        },
        new QuestionInfo
        {
            QuestionText = "2. ������s�@�Ψm�n�[�J�s��H",
            AnswersText = new string[] {
                "�[�J�s�몺�ت��O���F�����Ψm������A�ϱo�Ψm�b�ϥήɦ���n����������C",
                "�s�몺�[�J�O���F���ߡA�T�O�Ψm�s�@�L�{�����|���ӵߴ��͡A�O���Ψm���å͡C",
                "�[�J�s��O���F�ո`�Ψm���C��A�ϳ̲ײ��~�঳���A�v����m��{�C",
                "�s��i�H�@�������A���U���l�o�����תջĻP�B��ƶu��n�V�X�A�[�֨m�Ƥ������t�v�C"
            },
            ExplanationText = "�����G�s��i�H�@�������A���U���l�o�����תջĻP�B��ƶu��n�V�X�A�[�֨m�Ƥ������t�v�C",
            CorrectAnswerIndex = 3,
            StepAssociated = 1
        },
        new QuestionInfo
        {
            QuestionText = "3. ������s�@�Ψm�n�K�[�B��ƶu���G�H",
            AnswersText = new string[] {
                "�]���B��ƶu�O�@���P�A�Өm�Ƥ����O�@�تo�׻P�P�����ͦ��תջĶu�]�Ψm�^�M�̪o���L�{�C",
                "�K�[�B��ƶu�O���F�����W�[���G���ūסA�q�ӥ[�t�Ҧ��ƾǤ������t�v�C",
                "�K�[�B��ƶu�O���F�Ϸ��G�ĤơA�q�ӫP�i�m�Ƥ����C",
                "�K�[�B��ƶu�D�n�@�Υi�H�O����ӹ��緻�G��í�w�ʡC"
            },
            ExplanationText = "�����G�]���B��ƶu�P���l�o�����תջĵo�ͤƾǤ����A�ͦ��Ψm�M�̪o�A�o�ӹL�{�٬��m�Ƥ����C",
            CorrectAnswerIndex = 0,
            StepAssociated = 2
        },
        new QuestionInfo
        {
            QuestionText = "4. �U�C��̱ԭz�ŦX�m�Ƥ������L�{�H",
            AnswersText = new string[] {
                "�B��ƶu�b���ŤU�|���H�A�q�ӨϾ�ӲV�X�����H�Y�׼W�[�A���ͪΨm�C",
                "�o�שίתզb���ŤU�ۦ���ѡA���ͪΨm�C",
                "�B��ƶu�P���l�o�����תջĵo�ͤƾǤ����A�ͦ��Ψm�M�̪o�C",
                "�s��b�[���L�{���|�P�B��ƶu�����A���ͪΨm�C"
            },
            ExplanationText = "�����G�B��ƶu�O�@���P�A�Өm�Ƥ����O�@������P�P�z�L�ƾǤ����ͦ��תջ��Q�]�Ψm�^�M�̪o���L�{�C",
            CorrectAnswerIndex = 2,
            StepAssociated = 2
        },
        new QuestionInfo
        {
            QuestionText = "5. �[�J���M�Q���᷻�G���ܤơH",
            AnswersText = new string[] {
                "�[�J���M���Q����A�Ψm�|�q���G�������X�ӡA�Φ��B�b���G�����զ�T��C",
                "�[�J���M���Q����|�Ϸ��G��pH�ȵo�ͼ@�ܡA�q�ӨϷ��G�ܱo���ġC",
                "�[�J���M���Q����i�H�ϱo�Ψm���C��o���ܤơC",
                "�[�J���M���Q����|�ߧY�޵o�@�P���ƾǤ����A����j�q����M����A�q�ӾɭP���G�m�ˡC"
            },
            ExplanationText = "�����G�Ψm�|�q���G���Q�����X�ӡA�ܦ��B�b���G�����զ�T��C",
            CorrectAnswerIndex = 0,
            StepAssociated = 3
        },
        new QuestionInfo
        {
            QuestionText = "6. ���G���B�X�Ψm�A��ƾǭ�z�O����H",
            AnswersText = new string[] {
                "�Q�ΤF�u�ŤƧ@�Ρv��z�A���U�o�׻P���V�X�A�Φ�í�w���ŲG�C",
                "�Q�ΤF�u���z�@�Ρv��z�A�Ϸ��G�����������V���M�Q���A��ַ��G���������t�q�A�����X�Ψm�C",
                "�Q�ΤF�u�Q�R�@�Ρv��z�A�Ψm�����󹡩M���Q�����S�ʡA�ϪΨm�P�̪o�����B������C",
                "�Q�ΤF�u�I���@�Ρv��z�A�z�L�M�����G��������A�����X�Ψm�C"
            },
            ExplanationText = "�����G�[�J���M���Q����z�L�u�Q�R�@�Ρv����z�A�Ψm�|�]���������󹡩M���Q�����S�ʱq���G���Q�����X�ӡA�B�b���G���C",
            CorrectAnswerIndex = 2,
            StepAssociated = 3
        },
        new QuestionInfo
        {
            QuestionText = "7. ������Ψm�|�B�b���G�W�h�H",
            AnswersText = new string[] {
                "�]���Ψm�b���G���Φ��F��w�A�o�Ǯ�w�ϪΨm�B��F���G���W�h�C",
                "�]���Ψm���l�b�[�J�Q����o�ͤF����Ƥ����A�ܱo�󻴡C",
                "�]�����G�[����|�ϱo�Ψm���ȡA�P����y�W�ɭ�z�ۦP�C",
                "�]���Ψm���K�פp�󹡩M���Q�����K�סC"
            },
            ExplanationText = "�����G�Ψm�W�B���{�H�A�D�n�O�]���Ψm���K�פp������K�סA�ɭP�Ψm�P�̪o���}�ïB������W�C",
            CorrectAnswerIndex = 3,
            StepAssociated = 4
        },
        new QuestionInfo
        {
            QuestionText = "8. �Ψm�I��ۿ��կȷ|�e�{�����C��H",
            AnswersText = new string[] {
                "�|�e�{�Ŧ�C",
                "�|�e�{����C",
                "�|�e�{����C",
                "���|���C���ܤơC"
            },
            ExplanationText = "�����G�Ψm�O�P�ʡA�]���g�W����ۿ��կȷ|�ܦ��Ŧ�A�Ӫg�W�Ŧ�ۿ��կȫh���|���C���ܤơC",
            CorrectAnswerIndex = 0,
            StepAssociated = 5
        },
        new QuestionInfo
        {
            QuestionText = "9. ���Ψm�IĲ�ۿ��կȫ�e�{���C���ܤơA���תΨm�����P�ʬ���H",
            AnswersText = new string[] {
                "�Ψm�O�P�ʡC",
                "�Ψm�O���ʡC",
                "�L�k�z�L�ۿ��կȪ��C���ܤƥh���_�Ψm�����P�ʡC",
                "�Ψm�O�ĩʡC"
            },
            ExplanationText = "�����G�Ψm�O�P�ʡA�]���g�W����ۿ��կȷ|�ܦ��Ŧ�A�Ӫg�W�Ŧ�ۿ��կȫh���|���C���ܤơC",
            CorrectAnswerIndex = 0,
            StepAssociated = 5
        },
        new QuestionInfo
        {
            QuestionText = "10. �F�Ԫo�M�]�H���V�X�᪺���G����H",
            AnswersText = new string[] {
                "���G�|�ܦ��`�Ŧ�C",
                "���G�����o�M�����|�V�X�A�Φ���h�C",
                "���G�|�ߧY���T�C",
                "���G�|�M�]�H���|�����V�X�A�ܦ��z�����G�C"
            },
            ExplanationText = "�����G���G�����o�M�����|�V�X�A�Φ���h�C",
            CorrectAnswerIndex = 1,
            StepAssociated = 6
        },
        new QuestionInfo
        {
            QuestionText = "11. �Ψm�p����ܪo�M�����V�X�ʡH",
            AnswersText = new string[] {
                "�[�J�Ψm��A�o�|�Q�����Q�Ψm�i��ƾǤ��Ѥ����C",
                "�[�J�Ψm��A�|�ߧY�޵o�@�ӱj�P���ƾǤ����A�ϱo�o�Q��Ƭ��@�ػP���i�H�ۥѲV�X������C",
                "�[�J�Ψm��A�Ψm�|�l���Ҧ����o�A�����G�ݰ_�ӳQ�V�X�F�A����ڤW�O�o�Q�Ψm�l���F�C",
                "�[�J�Ψm��A�o�M���V�X���|�}�l�V�X�A�Φ��ſB�G�A�o�O�ŤƧ@�ΡC"
            },
            ExplanationText = "�����G�[�J�Ψm��A�o�M���V�X���|�}�l�V�X�A�Φ��ſB�G�A�o�O�ŤƧ@�ΡC",
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
        if (PlayerPrefs.GetInt("ReviewMistakes", 0) == 1)   //�p�G�O�q�����������^�ӴN��ܳo��
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
            Explain_IMG.sprite = questionInfo.ExplanationSprite;  // ���T�]�m�Ϥ�
        }
    }


    public void OnAnswerSelected(int answerIndex)
    {
        var currentQuestion = QuestionsList[QuestionNum];
        bool isCorrect = currentQuestion.CorrectAnswerIndex == answerIndex; //��O��ܵ��׬O�_���T
        if(isCorrect)
        {
            correctAnswerCount++;
            Debug.Log("���T���� = " + correctAnswerCount);
        }
        if (!isCorrect)
        {
            wrongAnswerCount++;
            Debug.Log("���~���� = " + wrongAnswerCount);
            var detail = new IncorrectStepDetail
            {
                QuestionNum = QuestionNum,
                StepAssociated = currentQuestion.StepAssociated
            };

            // �ˬd�O�_�w�g�O���L�ۦP�����~
            if (!PersistenceManager.Instance.IncorrectSteps.Any(x => x.QuestionNum == QuestionNum && x.StepAssociated == currentQuestion.StepAssociated))
            {
                PersistenceManager.Instance.IncorrectSteps.Add(detail);
            }
        }
        foreach (var button in AnswerButtons)
        {
            button.interactable = false;
        }
        //���s�C��
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
    
    public void NextQuestion()  //�M�b�D�ت�NextBTN�W
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
    
    public void BacktoStepBTN_OnClick()        //�M�bBacktoStep��BTN�W
    {
        Debug.Log("��^�B�J");
        
        if (PersistenceManager.Instance.IncorrectSteps.Count > 0)
        {
            Hint_Manager.Currentstep = currentStep;

            PlayerPrefs.SetInt("CurrentStep", currentStep);
            PlayerPrefs.SetInt("CurrentErrorIndex", currentErrorIndex);
            PlayerPrefs.SetInt("ReviewMistakes", 1); // 1 ��� true
            PlayerPrefs.Save();

            GotoExperiment_BG.SetActive(false);
            Questions.SetActive(false);
            Explain.SetActive(false);
            CanCloseBackToExplain = false;

            Debug.Log("��^��B�J" + currentStep);
            // ���s���J����
            ReloadScene();
        }
    }
    public void Explain_NextBTN_OnClick()        //�M�bExplain_NextBTN��BTN�W
    {
        // �p�⦹�D�ԸѮɶ�
        float singleExplanationTime = (float)(DateTime.Now - singleExplanationStartTime).TotalSeconds;
        // �֥[���`�ԸѾ\Ū�ɶ�
        totalExplanationReadTime += singleExplanationTime;
        // �O�����D�ԸѮɶ�
        BehaviorLogger.Instance.LogBehavior("���D�ԸѾ\Ū�ɶ����G" + singleExplanationTime + " ��");
        // ���]���D�ԸѪ��}�l�ɶ�����e�ɶ�
        singleExplanationStartTime = DateTime.Now;
        if (currentErrorIndex + 1 < PersistenceManager.Instance.IncorrectSteps.Count)
        {
            currentErrorIndex++;
            ShowErrorQuestion(currentErrorIndex);
        }
        else
        {
            Debug.Log("Last");
            // �̫�@�D�Ըѵ����ɭp��ðO���`�ԸѾ\Ū�ɶ�
            singleExplanationTime = (float)(DateTime.Now - singleExplanationStartTime).TotalSeconds;
            totalExplanationReadTime += singleExplanationTime; // �֥[�̫�@�D���ɶ�
            // �O���`�ԸѾ\Ū�ɶ�
            BehaviorLogger.Instance.LogBehavior("�`�ԸѾ\Ū�ɶ����G" + totalExplanationReadTime + " ��");
            GoToReQuesion.SetActive(true);
        }
    }
    private void CalculateTotalStepsToReturn()  //�p��ݪ�^�X�ӨB�J�ðO��
    {
        // �ϥ� HashSet ���x�s�W�S���B�J
        HashSet<int> uniqueSteps = new HashSet<int>();

        foreach (var error in PersistenceManager.Instance.IncorrectSteps)
        {
            uniqueSteps.Add(error.StepAssociated); // �[�J���~�������B�J
        }

        // �N�B�J�ƶq��ȵ� totalStepsToReturn
        totalStepsToReturn = uniqueSteps.Count;

        BehaviorLogger.Instance.LogBehavior("�ǥͻݭn��^�B�J�A�B�B�J�`�ơG" + totalStepsToReturn);
    }
    public void GotoExperimentBTN_OnClick(int errorIndex)        //�q�ݵ����쭫�չ���
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
            PlayerPrefs.SetInt("ReviewMistakes", 1); // 1 ��� true
            PlayerPrefs.Save();

            GotoExperiment_BG.SetActive(false);
            Questions.SetActive(false);
            Explain.SetActive(false);
            CanCloseBackToExplain = false;

            Debug.Log("��^��B�J" + currentStep);
            Debug.Log("currentStep" + currentStep);
            Debug.Log("nextStepAssociated" + nextStepAssociated);
            BehaviorLogger.Instance.LogBehavior("�^�����T���ơG" + correctAnswerCount + " ��");
            BehaviorLogger.Instance.LogBehavior("�^�����~���ơG" + wrongAnswerCount + " ���A�ݾ\Ū" + wrongAnswerCount + " ���Ը�");
            BehaviorLogger.Instance.LogBehavior("�^�����T�v�G" + (float)correctAnswerCount / (correctAnswerCount + wrongAnswerCount) * 100 + " %");
            CalculateTotalStepsToReturn();
            // ���s���J����
            ReloadScene();
        }
        else
        {
            BehaviorLogger.Instance.LogBehavior("�ǥͤ��ݾ\Ū�ԸѡC");
            BehaviorLogger.Instance.LogBehavior("�ǥͤ��ݪ�^�B�J�C");
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
            PlayerPrefs.SetInt("ReviewMistakes", 1); // 1 ��� true
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
                Debug.Log("�Ҧ����~�B�J�w����");
                return; // �h�X�j��M��k
            }

            ReloadScene();
            return; // ��^�����ᤤ��j��
        }

        if (currentErrorIndex >= PersistenceManager.Instance.IncorrectSteps.Count - 1)
        {
            GoToExplain.SetActive(true);
            GotoNextExperiment.SetActive(false);
            CanCloseBackToExplain = true;
            Debug.Log("�Ҧ����~�B�J�w����");
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
            Debug.Log($"�D�ؽs��: {error.QuestionNum}, �B�J: {error.StepAssociated}");
        }
        //�b�o����o�Ĥ@�ӿ��D����T
        currentErrorIndex = 0;
        ShowErrorQuestion(currentErrorIndex);
    }
    private void ShowErrorQuestion(int errorIndex)  //���������������
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
        // �����e���ʳ������W��
        string sceneName = SceneManager.GetActiveScene().name;
        // ���s�[����e����
        SceneManager.LoadScene(sceneName);
    }
    // BTN_Onclick�ƥ�
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
            Debug.Log("���հݵ����T���� = " + Re_correctAnswerCount);
        }
        if(!isCorrect)
        {
            Re_wrongAnswerCount++;
            Debug.Log("���հݵ����~���� = " + Re_wrongAnswerCount);
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
            BehaviorLogger.Instance.LogBehavior("���զ^�����T���ơG" + Re_correctAnswerCount + " ��");
            BehaviorLogger.Instance.LogBehavior("���զ^�����~���ơG" + Re_wrongAnswerCount + " ��");
            BehaviorLogger.Instance.LogBehavior("���զ^�����T�v�G" + (float)Re_correctAnswerCount / (Re_correctAnswerCount + Re_wrongAnswerCount) * 100 + " %");
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
