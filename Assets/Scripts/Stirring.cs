using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Touch;


public class Stirring : MonoBehaviour
{
    public static int step1_times, step2_times, step3_times;
    public Material BeakerLiquid;
    public static bool Stirring1_NE, Stirring2_NE, Stirring3_NE; //���ӽT�{Hint�覡�O�_���ܡB����Ĳ�o�ĤT�B�J���ʵe
    public static bool StirringAni3;
    public static bool Step1isOK, Step2isOK;
    public static bool Step1_Stirring, Step2_Stirring, Step3_Stirring; //���ӽT�{���S���ͩհʧ@

    public static bool isTouch_DishBowlThing , isLitmusPaper, isinbeaker;
    public GameObject BackToExplain;

    void Awake()
    {
        step1_times = 0;
        step2_times = 0;
        step3_times = 0;
    }

    void Update()
    {
        /*if (PlayerPrefs.GetInt("ReviewMistakes", 0) == 1 && !isreset)
        {
            ResetVariables();
            isreset = true;
        }*/
        if (step1_times >= 2 && step1_times < 12)
        {
            Step1_Stirring = true;
            Stirring1_NE = true;
        }
        else if(step1_times >= 12)
        {
            Stirring1_NE = false;
            Step1isOK = true;
        }
        if (step2_times >= 2 && step2_times < 12)
        {
            Step2_Stirring = true;
            Stirring2_NE = true;
        }
        else if (step2_times >= 10)
        {
            Stirring2_NE = false;
            Step2isOK = true;
        }
        if (step3_times >= 2 && step3_times < 12)
        {
            Step3_Stirring = true;
            Stirring3_NE = true;
        }
        else if (step3_times >= 12)
        {
            Stirring3_NE = false;
        }

        if (Hint_Manager.Currentstep == 1 && !Stirring1_NE && Step1isOK)
        {
            if (PlayerPrefs.GetInt("ReviewMistakes", 0) != 1)   //�������`
            {
                Hint_Manager.Currentstep = 2;
            }
            else if (PlayerPrefs.GetInt("ReviewMistakes", 0) == 1)  //�������`
            {
                if(!QuestionsM2.CanCloseBackToExplain)
                {
                    ResetAllValue.CloseHint = true;
                    BackToExplain.SetActive(true);
                    //Debug.Log("Ĳ�oBackToExplain, Step 1 ����");
                }
                else
                {
                    BackToExplain.SetActive(false);
                }
            }
        }
        else if (Hint_Manager.Currentstep == 2 && !Stirring2_NE && Step2isOK)
        {
            if(!Beaker_Anim.LampOpen)
            {
                if (PlayerPrefs.GetInt("ReviewMistakes", 0) != 1)
                {
                    Hint_Manager.Currentstep = 3;
                    //Debug.Log("Ĳ�oBackToExplain, Step 2 ����");
                }
                else if (PlayerPrefs.GetInt("ReviewMistakes", 0) == 1)
                {
                    //Debug.Log("CanCloseBackToExplain" + QuestionsM2.CanCloseBackToExplain);
                    if (!QuestionsM2.CanCloseBackToExplain)
                    {
                        ResetAllValue.CloseHint = true;
                        BackToExplain.SetActive(true);
                    }
                    else
                    {
                        BackToExplain.SetActive(false);
                    }
                }
            }
        }
        else if (Hint_Manager.Currentstep == 3 && Beaker_Anim.step3isOK) //�쥻�Ostep3_times >= 6�������B�J�|
        {
            if (PlayerPrefs.GetInt("ReviewMistakes", 0) != 1)
            {
                Hint_Manager.Currentstep = 4;
            }
            else if (PlayerPrefs.GetInt("ReviewMistakes", 0) == 1)
            {
                if (!QuestionsM2.CanCloseBackToExplain)
                {
                    ResetAllValue.CloseHint = true;
                    BackToExplain.SetActive(true);
                }
                else
                {
                    BackToExplain.SetActive(false);
                }
            }
        }
        
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "InsideCollider")   //�令InsideCollider
        {
            switch (Hint_Manager.Currentstep)
            {
                case 1:
                    //Step1_Stirring = true;
                    step1_times++;
                    Debug.Log("step1_times:" + step1_times);
                    break;
                case 2:
                    //Step2_Stirring = true;
                    step2_times++;
                    Debug.Log("step2_times:" + step2_times);
                    break;
                case 3:
                    step3_times++;
                    Debug.Log("step3_times:" + step3_times);
                    break;
                    // ... add cases for additional steps as necessary ...
            }
        }
        
    }
    public void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Beaker")
        {
            isinbeaker = true;
        }
    }
    
    public void OnTriggerExit(Collider other)
    {
        isinbeaker = false;
    }
    
    
}
