using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class CG_TestHint : MonoBehaviour
{
    public bool CoconutOilTrigger, AlcoholTrigger, NaOHTrigger, NaClTrigger, H2OTrigger, SaladOilTrigger;
    public bool Coconut_isPoured, Alcohol_isPoured, NaOH_isPoured, NaCl_isPoured, H2O_isPoured, SaladOil_isPoured;
    public bool Step1_Stirring, Step2_Stirring, Step3_Stirring, Stirring1_NE, Stirring2_NE, Stirring3_NE, LampOpen, Step1isOK, Step2isOK;
    public bool is_scoop_up, isOpenLitmusPaper, cancloseLitmusPaper, isTouchingLitmusPaper;
    public bool isgotoTestTube, shakeTestTube, step1_2, step2, step6_2, isstep3AniPlay, ispourstep6_2;
    public int SpoonTimes;

    // Start is called before the first frame update*/
    void Start()
    {
        Stirring1_NE = true;
        Stirring2_NE = true;
        Stirring3_NE = true;
    }

    // Update is called once per frame
    void Update()
    {
        CG_PourToBeaker.CoconutOilTrigger = CoconutOilTrigger;
        CG_PourToBeaker.AlcoholTrigger = AlcoholTrigger;
        CG_PourToBeaker.NaOHTrigger = NaOHTrigger;
        CG_PourToBeaker.NaClTrigger = NaClTrigger;
        CG_PourToTestTube.H2OTrigger = H2OTrigger;
        CG_PourToTestTube.SaladOilTrigger = SaladOilTrigger;

        CG_PourToBeaker.Coconut_isPoured = Coconut_isPoured;
        CG_PourToBeaker.Alcohol_isPoured = Alcohol_isPoured;
        CG_PourToBeaker.NaOH_isPoured = NaOH_isPoured;
        CG_PourToBeaker.NaCl_isPoured = NaCl_isPoured;
        CG_PourToTestTube.H2O_isPoured = H2O_isPoured;
        CG_PourToTestTube.SaladOil_isPoured = SaladOil_isPoured;

        CG_Stirring.Step1_Stirring = Step1_Stirring;
        CG_Stirring.Step2_Stirring = Step2_Stirring;
        CG_Stirring.Step3_Stirring = Step3_Stirring;
        CG_Stirring.Step1isOK = Step1isOK;
        CG_Stirring.Step2isOK = Step2isOK;
        CG_Stirring.Stirring1_NE = Stirring1_NE;
        CG_Stirring.Stirring2_NE = Stirring2_NE;
        CG_Stirring.Stirring3_NE = Stirring3_NE;

        CG_Spoon_Collision.is_scoop_up = is_scoop_up;
        CG_Beaker_Anim.LampOpen = LampOpen;
        CG_Buttons_Control.isOpenLitmusPaper = isOpenLitmusPaper;
        CG_Spoon_Collision.cancloseLitmusPaper = cancloseLitmusPaper;
        CG_Spoon_Collision.isTouchingLitmusPaper = isTouchingLitmusPaper;
        CG_Spoon_Collision.isgotoTestTube = isgotoTestTube;
        CG_PourToTestTube.shakeTestTube = shakeTestTube;

        CG_PourToBeaker.step1_2 = step1_2;
        CG_PourToBeaker.step2 = step2;
        CG_Beaker_Anim.isstep3AniPlay = isstep3AniPlay;
        CG_PourToTestTube.step6_2 = step6_2;
        CG_TestTube_Anim.ispourstep6_2 = ispourstep6_2;

        /*if(Hint_Manager.Currentstep == 2)
        {
            Debug.Log("Stirring2_NE = " + Stirring2_NE);
            Debug.Log("LampOpen = " + LampOpen);
            Debug.Log("step2 = " + step2);
        }*/
        /*if (CG_PourToBeaker.CoconutOilTrigger)
        {
            TestT.text = "CoTouch_Y";
        }
        if (CG_PourToBeaker.Coconut_isPoured)
        {
            TestT.text = "CoPour_Y";
        }
        if (CG_PourToBeaker.AlcoholTrigger)
        {
            TestT.text = "AlTouch_Y";
        }
        if (CG_PourToBeaker.Alcohol_isPoured)
        {
            TestT.text = "AlPour_Y";
        }*/
    }
}
