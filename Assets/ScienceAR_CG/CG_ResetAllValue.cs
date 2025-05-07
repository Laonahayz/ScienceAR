using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CG_ResetAllValue : MonoBehaviour
{
    public GameObject DishesOBJ;
    public GameObject Hint_Ani;
    public static bool CloseHint;
    void Awake()
    {
        if (CG_QuestionsM2.ReStartExperiment)
        {
            ResetVariables();
        }
        CloseHint = false;
        Debug.Log(CG_Hint_Manager.Currentstep);
    }
    void Update()
    {
        if(CloseHint)
        {
            Hint_Ani.SetActive(false);
        }
    }
    void ResetVariables()
    {
        CG_Stirring.step1_times = 0;
        CG_Stirring.step2_times = 0;
        CG_Stirring.step3_times = 0;
        CG_Stirring.Stirring1_NE = false;
        CG_Stirring.Stirring2_NE = false;
        CG_Stirring.Stirring3_NE = false;
        CG_Stirring.StirringAni3 = false;
        CG_Stirring.Step1isOK = false;
        CG_Stirring.Step2isOK = false;
        CG_Stirring.Step1_Stirring = false;
        CG_Stirring.Step2_Stirring = false;
        CG_Stirring.Step3_Stirring = false;
        CG_Stirring.isTouch_DishBowlThing = false;
        CG_Stirring.isLitmusPaper = false;
        CG_Stirring.isinbeaker = false;
        CG_Buttons_Control.isOpenLitmusPaper = false;
        CG_Beaker_Anim.LampOpen = false;
        CG_Beaker_Anim.ispourstep3 = false;
        CG_Beaker_Anim.isstep3AniPlay = false;
        CG_Beaker_Anim.step3isOK = false;
        CG_PourToBeaker.CoconutOilTrigger = false;
        CG_PourToBeaker.AlcoholTrigger = false;
        CG_PourToBeaker.NaOHTrigger = false;
        CG_PourToBeaker.NaClTrigger = false;
        CG_PourToBeaker.H2OTrigger = false;
        CG_PourToBeaker.SaladOilTrigger = false;
        CG_PourToBeaker.step1_1 = false;
        CG_PourToBeaker.step1_2 = false;
        CG_PourToBeaker.step2 = false;
        CG_PourToBeaker.step3 = false;
        CG_PourToBeaker.step6 = false;
        CG_PourToBeaker.Coconut_Notenough = false;
        CG_PourToBeaker.Alcohol_Notenough = false;
        CG_PourToBeaker.NaOH_Notenough = false;
        CG_PourToBeaker.NaCl_Notenough = false;
        CG_PourToBeaker.Coconut_isPoured = false;
        CG_PourToBeaker.Alcohol_isPoured = false;
        CG_PourToBeaker.NaOH_isPoured = false;
        CG_PourToBeaker.NaCl_isPoured = false;
        CG_PourToTestTube.shakeCount = 0;
        CG_PourToTestTube.shakeTestTube = false;
        CG_PourToTestTube.H2OTrigger = false;
        CG_PourToTestTube.SaladOilTrigger = false;
        CG_PourToTestTube.step6_1 = false;
        CG_PourToTestTube.step6_2 = false;
        CG_PourToTestTube.SaladOil_Notenough = false;
        CG_PourToTestTube.H2O_Notenough = false;
        CG_PourToTestTube.SaladOil_isPoured = false;
        CG_PourToTestTube.H2O_isPoured = false;
        CG_Spoon_Collision.SpoonTimes = 1;
        CG_Spoon_Collision.is_scoop_up = false;
        CG_Spoon_Collision.isgotoLitmusPaper = false;
        CG_Spoon_Collision.isTouchingLitmusPaper = false;
        CG_Spoon_Collision.isgotoTestTube = false;
        CG_Spoon_Collision.cancloseLitmusPaper = false;
        CG_TestTube_Anim.ispourstep6_1 = false;
        CG_TestTube_Anim.ispourstep6_2 = false;
        CG_PourToTestTube.triggerCount = 0;
    }
}
