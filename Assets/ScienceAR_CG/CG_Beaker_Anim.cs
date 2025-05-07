using Lean.Touch;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CG_Beaker_Anim : MonoBehaviour
{
    public Animator BeakerAnim;
    //public Animator All;
    public static bool LampOpen, ispourstep3, isstep3AniPlay, step3isOK;
    public GameObject BeakerLiquid, Lamp, BowlThing, step4OBJ;
    bool isCoconutAniPlay, isAlcoholAniPlay, isNaOHAniPlay, isNaClAniPlay;
    bool isreset;
    void Awake()
    {
        LampOpen = false;
        ResetVariables();
    }
    void ResetVariables()
    {
        LampOpen = false;
        ispourstep3 = false;
        isstep3AniPlay = false;
        step3isOK = false;
    }

    void Update()
    {
        /*if (PlayerPrefs.GetInt("ReviewMistakes", 0) == 1 && !isreset)
        {
            ResetVariables();
            isreset = true;
        }*/
        if (CG_Hint_Manager.Currentstep == 1 && CG_PourToBeaker.Coconut_isPoured && !isCoconutAniPlay)   //倒入溶液的計量動畫
        {
            BeakerAnim.Play("Step1Add_1");
            isCoconutAniPlay = true;
        }
        else if (CG_Hint_Manager.Currentstep == 1 && CG_PourToBeaker.Alcohol_isPoured && !isAlcoholAniPlay)
        {
            BeakerAnim.Play("Step1Add_2");
            isAlcoholAniPlay = true;
        }
        if (CG_Hint_Manager.Currentstep == 2 && CG_PourToBeaker.NaOH_isPoured && !isNaOHAniPlay)
        {
            BeakerAnim.Play("Step2Add");
            isNaOHAniPlay = true;
            if (!LampOpen)
            {
                BeakerLiquid.transform.localScale = new Vector3(BeakerLiquid.transform.localScale.x, 0.15f, BeakerLiquid.transform.localScale.z);
            }
        }

        if (CG_Hint_Manager.Currentstep == 3 && CG_PourToBeaker.NaCl_isPoured && !isNaClAniPlay)
        {
            BeakerAnim.Play("Step3Add");
            isNaClAniPlay = true;
        }
        if (CG_Stirring.step3_times >= 12 && !isstep3AniPlay)
        {
            isstep3AniPlay = true;
            StartCoroutine(PlayAnimationSequence()); 
            //共撥放兩個動畫，A是鹽析動畫、B是白色固體浮到上層，最好再設定等B動畫撥完才能用湯匙撈東西
        }
        if (CG_Hint_Manager.Currentstep == 4)
        {
            BeakerAnim.Play("step4OBJ");
        }

    }

    public void AlcoholLamp_Open_Anim()
    {
        if (!LampOpen)
        {
            LampOpen = true;
            BeakerAnim.Play("Open_AlcoholLamp");
            Lamp.SetActive(true);
            Debug.Log("開");
        }
        else if (LampOpen)
        {
            LampOpen = false;
            BeakerAnim.Play("Close_AlcoholLamp");
            Lamp.SetActive(false);
            Debug.Log("關");
        }
    }

    IEnumerator PlayAnimationSequence()
    {
        //開大顆固體
        BowlThing.SetActive(true);
        BeakerAnim.Play("SeparateOut");
        yield return null;
        yield return new WaitForSeconds(BeakerAnim.GetCurrentAnimatorStateInfo(0).length + 0.5f);
        //關大顆固體，開小顆固體X5
        BowlThing.SetActive(false);
        step4OBJ.SetActive(true);
        BeakerAnim.Play("step4OBJ");
        yield return null;
        yield return new WaitForSeconds(BeakerAnim.GetCurrentAnimatorStateInfo(0).length + 3.0f);
        //湯匙現在才能動 
        step3isOK = true;
    }

}
