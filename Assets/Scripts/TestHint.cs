using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class TestHint : MonoBehaviour
{
    public bool Turbid;
    public bool CoconutOilTrigger, AlcoholTrigger, NaOHTrigger, NaClTrigger, H2OTrigger, SaladOilTrigger;
    public bool Coconut_isPoured, Alcohol_isPoured, NaOH_isPoured, NaCl_isPoured, H2O_isPoured, SaladOil_isPoured;
    public bool Step1_Stirring, Step2_Stirring, Step3_Stirring, Stirring1_NE, Stirring2_NE, Stirring3_NE, LampOpen, Step1isOK, Step2isOK;
    public bool is_scoop_up, isOpenLitmusPaper, cancloseLitmusPaper, isTouchingLitmusPaper;
    public bool isgotoTestTube, shakeTestTube, step1_2, step2, step6_2, isstep3AniPlay, ispourstep6_2;
    public int SpoonTimes;
    public int shakeCount;
    // Start is called before the first frame update*/

    public TMP_Text TestT;

    void Start()
    {
        Stirring1_NE = true;
        Stirring2_NE = true;
        Stirring3_NE = true;
    }

    // Update is called once per frame
    void Update()
    {
        Spoon_Collision.isgotoTestTube = isgotoTestTube;
        PourToBeaker.CoconutOilTrigger = CoconutOilTrigger;
        PourToBeaker.AlcoholTrigger = AlcoholTrigger;
        PourToBeaker.NaOHTrigger = NaOHTrigger;
        PourToBeaker.NaClTrigger = NaClTrigger;
        PourToTestTube.H2OTrigger = H2OTrigger;
        PourToTestTube.SaladOilTrigger = SaladOilTrigger;

        PourToBeaker.Coconut_isPoured = Coconut_isPoured;
        PourToBeaker.Alcohol_isPoured = Alcohol_isPoured;
        PourToBeaker.NaOH_isPoured = NaOH_isPoured;
        PourToBeaker.NaCl_isPoured = NaCl_isPoured;
        PourToTestTube.H2O_isPoured = H2O_isPoured;
        PourToTestTube.SaladOil_isPoured = SaladOil_isPoured;

        Stirring.Step1_Stirring = Step1_Stirring;
        Stirring.Step2_Stirring = Step2_Stirring;
        Stirring.Step3_Stirring = Step3_Stirring;
        Stirring.Step1isOK = Step1isOK;
        Stirring.Step2isOK = Step2isOK;
        Stirring.Stirring1_NE = Stirring1_NE;
        Stirring.Stirring2_NE = Stirring2_NE;
        Stirring.Stirring3_NE = Stirring3_NE;

        Spoon_Collision.is_scoop_up = is_scoop_up;
        Beaker_Anim.LampOpen = LampOpen;
        Buttons_Control.isOpenLitmusPaper = isOpenLitmusPaper;
        Spoon_Collision.cancloseLitmusPaper = cancloseLitmusPaper;
        Spoon_Collision.isTouchingLitmusPaper = isTouchingLitmusPaper;
        
        PourToTestTube.shakeTestTube = shakeTestTube;

        PourToBeaker.step1_2 = step1_2;
        PourToBeaker.step2 = step2;
        Beaker_Anim.isstep3AniPlay = isstep3AniPlay;
        PourToTestTube.step6_2 = step6_2;
        TestTube_Anim.ispourstep6_2 = ispourstep6_2;

        /*if(Hint_Manager.Currentstep == 2)
        {
            Debug.Log("Stirring2_NE = " + Stirring2_NE);
            Debug.Log("LampOpen = " + LampOpen);
            Debug.Log("step2 = " + step2);
        }*/
        /*if (PourToBeaker.CoconutOilTrigger)
        {
            TestT.text = "CoTouch_Y";
        }
        if (PourToBeaker.Coconut_isPoured)
        {
            TestT.text = "CoPour_Y";
        }
        if (PourToBeaker.AlcoholTrigger)
        {
            TestT.text = "AlTouch_Y";
        }
        if (PourToBeaker.Alcohol_isPoured)
        {
            TestT.text = "AlPour_Y";
        }*/
    }
}
