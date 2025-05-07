using Lean.Touch;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CG_TestTube_Anim : MonoBehaviour
{
    public Animator TestTubeAnim;
    public static bool ispourstep6_1, ispourstep6_2;
    bool isreset;

    void Awake()
    {
        ResetVariables();
    }
    void ResetVariables()
    {
        ispourstep6_1 = false;
        ispourstep6_2 = false;
    }
    void Update()
    {
        /*if (PlayerPrefs.GetInt("ReviewMistakes", 0) == 1 && !isreset)
        {
            ResetVariables();
            isreset = true;
        }*/
        if (CG_PourToTestTube.step6_1 && CG_PourToTestTube.H2O_isPoured && !ispourstep6_1)   //倒入溶液的計量動畫
        {
            TestTubeAnim.Play("Step6Add_1");
            ispourstep6_1 = true;
            Debug.Log("6-1加水(水)");
        }
        else if (CG_PourToTestTube.step6_2 && CG_PourToTestTube.SaladOil_isPoured && !ispourstep6_2)
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
            CG_Hint_Manager.Currentstep = 7;
        }
    }

}
