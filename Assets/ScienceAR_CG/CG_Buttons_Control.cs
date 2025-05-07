using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CG_Buttons_Control : MonoBehaviour
{
    public Material TestTube_liquid;
    bool isTestTube_liquid = false;
    public static bool isOpenLitmusPaper;
    public GameObject LitmusPaper;
    float a = 5.0f;
    bool isreset;

    void Awake()
    {
        ResetVariables();
    }
    void ResetVariables()
    {
        isOpenLitmusPaper = false;
    }

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
            if(CG_Spoon_Collision.cancloseLitmusPaper)
            {
                if (PlayerPrefs.GetInt("ReviewMistakes", 0) != 1)
                {
                    CG_Hint_Manager.Currentstep = 6;
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
