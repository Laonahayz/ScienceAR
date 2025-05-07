using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Buttons_Control : MonoBehaviour
{
    public Material TestTube_liquid;
    bool isTestTube_liquid = false;
    public static bool isOpenLitmusPaper;
    public GameObject LitmusPaper, BackToExplain;
    float a = 5.0f;


    public void LitmusPaperSetActive()
    {

        if (!isOpenLitmusPaper)
        {
            LitmusPaper.SetActive(true);
            isOpenLitmusPaper = true;
        }
        else if (isOpenLitmusPaper)
        {
            LitmusPaper.SetActive(false);
            isOpenLitmusPaper = false;
            if(Spoon_Collision.cancloseLitmusPaper)
            {
                if (PlayerPrefs.GetInt("ReviewMistakes", 0) != 1)
                {
                    Hint_Manager.Currentstep = 6;
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
    }


    // Update is called once per frame
    void Update()
    {
        /*if (PlayerPrefs.GetInt("ReviewMistakes", 0) == 1 && !isreset)
        {
            ResetVariables();
            isreset = true;
        }*/
        if (isTestTube_liquid)
        {
            a -= 0.2f * Time.deltaTime;
            TestTube_liquid.SetFloat("_ColorNum", a);
        }
        if(a <= 1.3f)
        {
            isTestTube_liquid = false;
        }
    }
    
}
