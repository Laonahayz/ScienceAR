using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetAllValue : MonoBehaviour
{
    public GameObject DishesOBJ;
    public GameObject Step4Obj;
    public GameObject H2O, Oil;
    public GameObject Hint_Ani;
    public static bool CloseHint;
    void Awake()
    {
        if (PlayerPrefs.GetInt("ReviewMistakes", 0) == 1)
        {
            ResetVariables();
        }
        CloseHint = false;
        Debug.Log(Hint_Manager.Currentstep);
    }
    void Update()
    {

        if (CloseHint)
        {
            Hint_Ani.SetActive(false);
        }
    }
    void ResetVariables()
    {
        Stirring.step1_times = 0;
        Stirring.step2_times = 0;
        Stirring.step3_times = 0;
        Stirring.Stirring1_NE = false;
        Stirring.Stirring2_NE = false;
        Stirring.Stirring3_NE = false;
        Stirring.StirringAni3 = false;
        Stirring.Step1isOK = false;
        Stirring.Step2isOK = false;
        Stirring.Step1_Stirring = false;
        Stirring.Step2_Stirring = false;
        Stirring.Step3_Stirring = false;
        Stirring.isTouch_DishBowlThing = false;
        Stirring.isLitmusPaper = false;
        Stirring.isinbeaker = false;
        Buttons_Control.isOpenLitmusPaper = false;
        Beaker_Anim.LampOpen = false;
        Beaker_Anim.ispourstep3 = false;
        Beaker_Anim.isstep3AniPlay = false;
        Beaker_Anim.step3isOK = false;
        PourToBeaker.CoconutOilTrigger = false;
        PourToBeaker.AlcoholTrigger = false;
        PourToBeaker.NaOHTrigger = false;
        PourToBeaker.NaClTrigger = false;
        PourToBeaker.H2OTrigger = false;
        PourToBeaker.SaladOilTrigger = false;
        PourToBeaker.step1_1 = false;
        PourToBeaker.step1_2 = false;
        PourToBeaker.step2 = false;
        PourToBeaker.step3 = false;
        PourToBeaker.step6 = false;
        PourToBeaker.Coconut_Notenough = false;
        PourToBeaker.Alcohol_Notenough = false;
        PourToBeaker.NaOH_Notenough = false;
        PourToBeaker.NaCl_Notenough = false;
        PourToBeaker.Coconut_isPoured = false;
        PourToBeaker.Alcohol_isPoured = false;
        PourToBeaker.NaOH_isPoured = false;
        PourToBeaker.NaCl_isPoured = false;
        PourToTestTube.shakeCount = 0;
        PourToTestTube.shakeTestTube = false;
        PourToTestTube.H2OTrigger = false;
        PourToTestTube.SaladOilTrigger = false;
        PourToTestTube.step6_1 = false;
        PourToTestTube.step6_2 = false;
        PourToTestTube.SaladOil_Notenough = false;
        PourToTestTube.H2O_Notenough = false;
        PourToTestTube.SaladOil_isPoured = false;
        PourToTestTube.H2O_isPoured = false;
        PourToTestTube.triggerCount = 0;
        if (Hint_Manager.Currentstep == 4)
        {
            Step4Obj.SetActive(true);
        }
        if (Hint_Manager.Currentstep == 5)
        {
            Spoon_Collision.isgotoLitmusPaper = true;
            DishesOBJ.SetActive(true);
            DishesOBJ.GetComponent<Transform>().localScale = new Vector3(7.4f, 9.3f, 4.5f);
        }
        else
        {
            Spoon_Collision.isgotoLitmusPaper = false;
        }
        if (Hint_Manager.Currentstep == 7)
        {
            DishesOBJ.SetActive(true);
            DishesOBJ.GetComponent<Transform>().localScale = new Vector3(7.4f, 9.3f, 4.5f);
            H2O.SetActive(true);
            Oil.SetActive(true);
        }
        Spoon_Collision.SpoonTimes = 1;
        Spoon_Collision.is_scoop_up = false;
        Spoon_Collision.isTouchingLitmusPaper = false;
        Spoon_Collision.isgotoTestTube = false;
        Spoon_Collision.cancloseLitmusPaper = false;
        TestTube_Anim.ispourstep6_1 = false;
        TestTube_Anim.ispourstep6_2 = false;
    }
}
