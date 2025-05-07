using Lean.Touch;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TestTube_Anim : MonoBehaviour
{
    public Animator TestTubeAnim;
    public static bool ispourstep6_1, ispourstep6_2;
    public GameObject BackToExplain;

    void Update()
    {
        /*if (PlayerPrefs.GetInt("ReviewMistakes", 0) == 1 && !isreset)
        {
            ResetVariables();
            isreset = true;
        }*/
        if (PourToTestTube.step6_1 && PourToTestTube.H2O_isPoured && !ispourstep6_1)   //倒入溶液的計量動畫
        {
            TestTubeAnim.Play("Step6Add_1");
            ispourstep6_1 = true;
            Debug.Log("6-1加水(水)");
        }
        else if (PourToTestTube.step6_2 && PourToTestTube.SaladOil_isPoured && !ispourstep6_2)
        {
            TestTubeAnim.Play("Step6Add_2");
            ispourstep6_2 = true;
            StartCoroutine(PlayAnimationSequence());
            Debug.Log("6-2加水(油)");
        }
    }

    IEnumerator PlayAnimationSequence()
    {
        //觀察5秒，打開蓋子，切換到第七步驟
        yield return new WaitForSeconds(6);
        if (PlayerPrefs.GetInt("ReviewMistakes", 0) != 1)
        {
            Hint_Manager.Currentstep = 7;
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
