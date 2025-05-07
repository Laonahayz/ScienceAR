using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Lean.Touch;
using System;

public class CG_Hint_Level : MonoBehaviour
{
    [Header("大Canvas提示文字")]
    public TMP_Text Tips, ifOpenLamp;
    [Header("燒杯旁提示文字")]
    public TMP_Text B_Hint;
    //, NotEnough_Text;
    [Header("按鈕開關")]
    public GameObject AlcoholLamp_BTN, LitmusPaper_BTN;
    [Header("全部物件")]
    public GameObject[] All_GameOBJ;
    public GameObject StirringRod;
    [Header("溶液圖示替換")]
    public Image Solution_Drag_IMG, Solution_Rot_IMG;
    public Sprite[] PourSolutions;
    public GameObject Beaker_Drag_Ani, Beaker_Rot_Ani, Beaker_StirringAni, Beaker_ShakeAni;
    public GameObject Canvas_Hint, Beaker_Canvas, Quesion_Interface, HintTitle, BTN_Layout;
    [Header("提示動畫")]
    public Animator BeakerHint, ifOpenAni;
    public GameObject Hint_Title, GoToQustion, BeakerHint_Ani;

    private Dictionary<int, Action> stepHints;

    //時間計算
    private int errorCount = 0;
    private Coroutine hintCoroutine;
    private float startTime;    //開始計算時間
    private float elapsedTime;  //總經過時間
    private bool isTiming;
    void Start()
    {
        StartCoroutine(CallDetailedHintEveryTenSeconds());
        InitializeHintActions();
        InitializeUIComponents();
        StartTimer();
    }
    private void StartHintTimer()
    {
        if (hintCoroutine == null)
        {
            hintCoroutine = StartCoroutine(HintTimer());
        }
    }
    // 開始計時器
    public void StartTimer()
    {
        if (!isTiming)
        {
            startTime = Time.time;
            isTiming = true;
            Debug.Log("計時器已啟動！");
        }
    }

    // 停止計時器並計算總時間
    public void StopTimer()
    {
        if (isTiming)
        {
            elapsedTime = Time.time - startTime;
            isTiming = false;
            Debug.Log("計時器已停止！操作總時間：" + elapsedTime + " 秒");
            CG_BehaviorLogger.Instance.LogBehavior("操作總時間：" + elapsedTime + " 秒");
        }
    }
    private IEnumerator HintTimer()
    {
        while (true)
        {
            yield return new WaitForSeconds(10f);
            errorCount++;
            Debug.Log("錯誤次數+1，目前錯誤次數：" + errorCount);
        }
    }
    private void StopHintTimer()
    {
        if (hintCoroutine != null)
        {
            StopCoroutine(hintCoroutine);
            hintCoroutine = null;
            if (errorCount >= 25)
            {
                CG_BehaviorLogger.Instance.LogBehavior("操作錯誤次數：" + errorCount + " 次，操作成功率低。");
            }
            else
            {
                CG_BehaviorLogger.Instance.LogBehavior("操作錯誤次數：" + errorCount + " 次，操作成功率高。");
            }
        }
    }
    private void InitializeHintActions()
    {
        stepHints = new Dictionary<int, Action>()
        {
            {1, Step1Hints},
            {2, Step2Hints},
            {3, Step3Hints},
            {4, Step4Hints},
            {5, Step5Hints},
            {6, Step6Hints},
            {7, Step7Hints}
        };
    }
    private void InitializeUIComponents()
    {
        LitmusPaper_BTN.SetActive(false);
        Beaker_Drag_Ani.SetActive(false);
        Beaker_Rot_Ani.SetActive(false);
        Beaker_StirringAni.SetActive(false);
        Canvas_Hint.SetActive(false);
        AlcoholLamp_BTN.SetActive(false);
        //Quesion_Interface.SetActive(false);
    }
    private void Step1Hints()
    {
        SetActiveState(false, Beaker_Drag_Ani, Beaker_Rot_Ani, Beaker_StirringAni, Canvas_Hint);
        if (!CG_PourToBeaker.CoconutOilTrigger && !CG_PourToBeaker.Coconut_isPoured)
        {
            StirringRod.SetActive(false);
            UpdateBeakerHint("拿取椰子油至燒杯旁", true, false, false, false, PourSolutions[0], PourSolutions[0], "Hint_DragAni");
            //Debug.Log("沒有拖曳椰子油的動作");
            StartHintTimer();
        }
        else if (CG_PourToBeaker.CoconutOilTrigger && !CG_PourToBeaker.Coconut_isPoured)
        {
            UpdateBeakerHint("旋轉倒入椰子油", false, true, false, false, PourSolutions[0], PourSolutions[0], "Hint_RotAnianim");
            //Debug.Log("沒有倒入椰子油的動作");
            StartHintTimer();
        }
        else if (CG_PourToBeaker.Coconut_isPoured && !CG_PourToBeaker.AlcoholTrigger && !CG_PourToBeaker.Alcohol_isPoured)
        {
            UpdateBeakerHint("拿取酒精至燒杯旁", true, false, false, false, PourSolutions[1], PourSolutions[1], "Hint_DragAni");
            //Debug.Log("沒有拖曳酒精的動作");
            StartHintTimer();
        }
        else if (CG_PourToBeaker.AlcoholTrigger && !CG_PourToBeaker.Alcohol_isPoured)
        {
            UpdateBeakerHint("旋轉倒入酒精", false, true, false, false, PourSolutions[1], PourSolutions[1], "Hint_RotAnianim");
            //Debug.Log("沒有倒入酒精的動作");
            StartHintTimer();
        }
        else if (CG_PourToBeaker.Alcohol_isPoured && !CG_Stirring.Step1_Stirring)
        {
            StirringRod.SetActive(true);
            UpdateBeakerHint("長按以拖曳玻棒，並左右攪拌燒杯內溶液", false, false, true, false, null, null, "Hint_STI_RotAnianim");
            //Debug.Log("沒有攪拌動作");
            StartHintTimer();
        }
        else if (CG_Stirring.Step1_Stirring && CG_Stirring.Stirring1_NE)
        {
            UpdateBeakerHint("攪拌次數不夠", false, false, true, false, null, null, "Hint_STI_RotAnianim");
            //Debug.Log("沒有攪拌到指定次數");
            StartHintTimer();
        }
        else
        {
            StopHintTimer();
        }
    }
    private void Step2Hints()
    {
        SetActiveState(false, Beaker_Drag_Ani, Beaker_Rot_Ani, Beaker_StirringAni, Canvas_Hint);
        if (!CG_Beaker_Anim.LampOpen)
        {
            StirringRod.SetActive(false);
            ifOpenLamp.text = "請點擊按紐呼叫酒精燈\n↓";
            Canvas_Hint.SetActive(true);
            ifOpenAni.Play("UI_Ani");
            //Debug.Log("未開啟酒精燈");
            Beaker_Canvas.SetActive(false);
            StartHintTimer();
        }
        else if (CG_Beaker_Anim.LampOpen && !CG_PourToBeaker.NaOHTrigger && !CG_PourToBeaker.NaOH_isPoured)
        {
            Canvas_Hint.SetActive(false);
            Beaker_Canvas.SetActive(true);
            ifOpenAni.StopPlayback();
            UpdateBeakerHint("拿取NaOH至燒杯旁", true, false, false, false, PourSolutions[2], PourSolutions[2], "Hint_DragAni");
            //Debug.Log("沒有拖曳NaOH的動作");
            StartHintTimer();
        }
        else if (CG_PourToBeaker.NaOHTrigger && !CG_PourToBeaker.NaOH_isPoured)
        {
            UpdateBeakerHint("旋轉倒入NaOH", false, true, false, false, PourSolutions[2], PourSolutions[2], "Hint_RotAnianim");
            //Debug.Log("沒有倒入NaOH的動作");
            StartHintTimer();
        }
        else if (CG_PourToBeaker.NaOH_isPoured && !CG_Stirring.Step2_Stirring)
        {
            StirringRod.SetActive(true);
            UpdateBeakerHint("長按以拖曳玻棒，並左右攪拌燒杯內溶液", false, false, true, false, null, null, "Hint_STI_RotAnianim");
            //Debug.Log("沒有攪拌動作");
            StartHintTimer();
        }
        else if (CG_Stirring.Step2_Stirring && CG_Stirring.Stirring2_NE)
        {
            UpdateBeakerHint("攪拌次數不夠", false, false, true, false, null, null, "Hint_STI_RotAnianim");
            //Debug.Log("沒有攪拌到指定次數");
            StartHintTimer();
        }
        else if (CG_Stirring.Step2_Stirring && !CG_Stirring.Stirring2_NE)
        {
            ifOpenLamp.text = "請點擊按紐關閉酒精燈\n↓";
            Canvas_Hint.SetActive(true);
            ifOpenAni.Play("UI_Ani");
            //Debug.Log("請關閉酒精燈");
            Beaker_Canvas.SetActive(false);
            StartHintTimer();
        }
        else
        {
            Canvas_Hint.SetActive(false);
            StopHintTimer();
        }
    }
    private void Step3Hints()
    {
        SetActiveState(false, Beaker_Drag_Ani, Beaker_Rot_Ani, Beaker_StirringAni, Canvas_Hint);

        if (!CG_PourToBeaker.NaClTrigger && !CG_PourToBeaker.NaCl_isPoured)
        {
            StirringRod.SetActive(false);
            Beaker_Canvas.SetActive(true);
            UpdateBeakerHint("拿取飽和食鹽水至燒杯旁", true, false, false, false, PourSolutions[3], PourSolutions[3], "Hint_DragAni");
            //Debug.Log("沒有拖曳飽和食鹽水的動作");
            StartHintTimer();
        }
        else if (CG_PourToBeaker.NaClTrigger && !CG_PourToBeaker.NaCl_isPoured)
        {
            UpdateBeakerHint("旋轉倒入飽和食鹽水", false, true, false, false, PourSolutions[3], PourSolutions[3], "Hint_RotAnianim");
            //Debug.Log("沒有倒入飽和食鹽水的動作");
            StartHintTimer();
        }
        else if (CG_PourToBeaker.NaCl_isPoured && !CG_Stirring.Step3_Stirring)
        {
            StirringRod.SetActive(true);
            UpdateBeakerHint("長按以拖曳玻棒，並左右攪拌燒杯內溶液", false, false, true, false, null, null, "Hint_STI_RotAnianim");
            //Debug.Log("沒有攪拌動作");
            StartHintTimer();
        }
        else if (CG_Stirring.Step3_Stirring && CG_Stirring.Stirring3_NE)
        {
            UpdateBeakerHint("攪拌燒杯溶液，並觀察燒杯內溶液變化", false, false, true, false, null, null, "Hint_STI_RotAnianim");
            StartHintTimer();
        }
        else if (!CG_Stirring.Stirring3_NE && CG_Beaker_Anim.isstep3AniPlay)
        {
            UpdateBeakerHint("觀察燒杯內溶液變化", false, false, false, false, null, null, null);
            StartHintTimer();
        }
        else
        {
            Canvas_Hint.SetActive(false);
            StopHintTimer();
        }
    }

    private void Step4Hints()
    {
        SetActiveState(false, Beaker_Drag_Ani, Beaker_Rot_Ani, Beaker_StirringAni, Canvas_Hint);
        StirringRod.SetActive(false);

        if (!CG_Spoon_Collision.is_scoop_up)
        {
            UpdateBeakerHint("拿取湯匙至燒杯旁，並撈取白色固體", true, false, false, false, PourSolutions[4], PourSolutions[4], "Hint_DragAni");
            //Debug.Log("湯匙未撈取白色固體至燒杯");
            StartHintTimer();
        }
        else if (CG_Spoon_Collision.is_scoop_up)
        {
            UpdateBeakerHint("將白色固體放置於蒸發皿中", true, false, false, false, PourSolutions[5], PourSolutions[5], "Hint_DragAni");
            //Debug.Log("湯匙未撈取白色固體至蒸發皿");
            StartHintTimer();
        }
        else
        {
            Canvas_Hint.SetActive(false);
            StopHintTimer();
        }
    }
    private void Step5Hints()
    {
        SetActiveState(false, Beaker_Drag_Ani, Beaker_Rot_Ani, Beaker_StirringAni, Canvas_Hint);

        //以下是第五步驟的提示了
        if (!CG_Buttons_Control.isOpenLitmusPaper)
        {
            BeakerHint_Ani.SetActive(false);
            ifOpenLamp.text = "請點擊按紐打開石蕊試紙\n↓";
            Canvas_Hint.SetActive(true);
            ifOpenAni.Play("UI_Ani");
            //Debug.Log("試紙需打開");
            StartHintTimer();
        }
        else if (CG_Buttons_Control.isOpenLitmusPaper && !CG_Spoon_Collision.isTouchingLitmusPaper && !CG_Spoon_Collision.cancloseLitmusPaper)
        {
            BeakerHint_Ani.SetActive(true);
            Canvas_Hint.SetActive(false);
            UpdateBeakerHint("使用湯匙撈取白色固體至石蕊試紙", true, false, false, false, PourSolutions[6], PourSolutions[6], "Hint_DragAni");
            //Debug.Log("湯匙未撈取白色固體至燒杯");
            StartHintTimer();
        }
        else if (CG_Spoon_Collision.isTouchingLitmusPaper && !CG_Spoon_Collision.cancloseLitmusPaper)
        {
            Canvas_Hint.SetActive(false);
            UpdateBeakerHint("等待並觀察石蕊試紙的顏色變化", false, false, false, false, null, null, null);
            //Debug.Log("湯匙未撈取白色固體至燒杯");
            StartHintTimer();
        }
        else if (CG_Spoon_Collision.cancloseLitmusPaper)
        {
            ifOpenLamp.text = "請點擊按紐關閉石蕊試紙\n↓";
            Canvas_Hint.SetActive(true);
            ifOpenAni.Play("UI_Ani");
            //Debug.Log("試紙需關閉");
            StartHintTimer();
        }
        else
        {
            Canvas_Hint.SetActive(false);
            StopHintTimer();
        }


    }
    private void Step6Hints()
    {
        SetActiveState(false, Canvas_Hint);

        if (!CG_PourToTestTube.H2OTrigger && !CG_PourToTestTube.H2O_isPoured)
        {
            UpdateBeakerHint("拿取蒸餾水至試管旁", true, false, false, false, PourSolutions[7], PourSolutions[7], "Hint_DragAni");
            //Debug.Log("蒸餾水未接近試管");
            StartHintTimer();
        }
        else if (CG_PourToTestTube.H2OTrigger && !CG_PourToTestTube.H2O_isPoured)
        {
            UpdateBeakerHint("旋轉倒入蒸餾水", false, true, false, false, PourSolutions[7], PourSolutions[7], "Hint_RotAnianim");
            //Debug.Log("沒有倒入蒸餾水的動作");
            StartHintTimer();
        }
        else if (CG_PourToTestTube.H2O_isPoured && !CG_PourToTestTube.SaladOilTrigger && !CG_PourToTestTube.SaladOil_isPoured)
        {
            UpdateBeakerHint("拿取沙拉油至試管旁", true, false, false, false, PourSolutions[8], PourSolutions[8], "Hint_DragAni");
            //Debug.Log("沙拉油未接近試管");
            StartHintTimer();
        }
        else if (CG_PourToTestTube.SaladOilTrigger && !CG_PourToTestTube.SaladOil_isPoured)
        {
            UpdateBeakerHint("旋轉倒入沙拉油", false, true, false, false, PourSolutions[8], PourSolutions[8], "Hint_RotAnianim");
            //Debug.Log("沒有倒入沙拉油的動作");
            StartHintTimer();
        }
        else if (CG_TestTube_Anim.ispourstep6_2)
        {
            UpdateBeakerHint("觀察水及沙拉油的混和狀態", false, false, false, false, null, null, null);
            //Debug.Log("沒有倒入沙拉油的動作");
            StartHintTimer();
        }
        else
        {
            Canvas_Hint.SetActive(false);
            StopHintTimer();
        }
    }
    private void Step7Hints()
    {
        SetActiveState(false, Canvas_Hint);

        if (!CG_Spoon_Collision.is_scoop_up)
        {
            UpdateBeakerHint("使用湯匙從蒸發皿取得肥皂", true, false, false, false, PourSolutions[4], PourSolutions[4], "Hint_DragAni");
            //Debug.Log("湯匙未接近蒸發皿取得肥皂");
            StartHintTimer();
        }
        else if (CG_Spoon_Collision.is_scoop_up && !CG_Spoon_Collision.isgotoTestTube)
        {
            //這邊的圖好像是以試管為主才對??
            UpdateBeakerHint("將湯匙上的肥皂放入試管中", true, false, false, false, PourSolutions[5], PourSolutions[5], "Hint_DragAni");
            //Debug.Log("湯匙未接近試管放入肥皂");
            StartHintTimer();
        }
        else if (CG_Spoon_Collision.isgotoTestTube && !CG_PourToTestTube.shakeTestTube)
        {
            UpdateBeakerHint("長按試管以拖曳，並上下搖晃試管觀察變化", false, false, false, true, null, null, "Hint_ShakeAni");
            //Debug.Log("需要搖晃試管");
            StartHintTimer();
        }
        else if (CG_PourToTestTube.shakeTestTube && CG_PourToTestTube.shakeCount < 5)
        {
            UpdateBeakerHint("上下搖晃次數不夠", false, false, false, true, null, null, "Hint_ShakeAni");
            //Debug.Log("搖晃次數不足");
            StartHintTimer();
        }
        else if (CG_PourToTestTube.shakeCount > 5)
        {
            UpdateBeakerHint("觀察肥皂與沙拉油和水的混和狀態", false, false, false, false, null, null, null);
            //Debug.Log("搖晃次數不足");
            StartHintTimer();
        }
        else
        {
            Canvas_Hint.SetActive(false);
            StopHintTimer();
        }
    }
    private void UpdateBeakerHint(string text, bool showDrag, bool showRot, bool showStirring, bool showShake, Sprite sprite1, Sprite sprite2, string animation)
    {
        B_Hint.text = text;
        Beaker_Drag_Ani.SetActive(showDrag);
        Beaker_Rot_Ani.SetActive(showRot);
        Beaker_StirringAni.SetActive(showStirring);
        Beaker_ShakeAni.SetActive(showShake);
        Solution_Drag_IMG.sprite = sprite1;
        Solution_Rot_IMG.sprite = sprite2;
        BeakerHint.Play(animation);
    }

    private void SetActiveState(bool state, params GameObject[] objects)
    {
        foreach (var obj in objects)
        {
            obj.SetActive(state);
        }
    }

    public void Operation_Hint()
    {
        if (stepHints.ContainsKey(CG_Hint_Manager.Currentstep))
        {
            stepHints[CG_Hint_Manager.Currentstep]();
        }
        else
        {
            //Debug.Log("未知步骤");
            //Debug.Log(CG_Hint_Manager.Currentstep);
        }
    }

    // 定義Coroutine
    IEnumerator CallDetailedHintEveryTenSeconds()
    {
        while (true) // 無限循環，以確保持續執行
        {
            yield return new WaitForSeconds(0.5f); // 等待10秒
            Operation_Hint(); // 呼叫你的函式
        }
    }
    void Update()
    {

        switch (CG_Hint_Manager.Currentstep)
        {
            case 1:
                Tips.text = "第一步驟：放置燒杯，加入椰子油10毫升和酒精10毫升並攪拌均勻。";
                All_GameOBJ[0].SetActive(true);
                All_GameOBJ[1].SetActive(false);
                All_GameOBJ[2].SetActive(false); 
                HintTitle.SetActive(true);
                BTN_Layout.SetActive(true);
                break;
            case 2:
                Tips.text = "第二步驟：加熱燒杯，並緩慢加入氫氧化鈉水溶液10毫升，同時攪拌至溶液黏稠後停止加熱。";
                All_GameOBJ[0].SetActive(true);
                All_GameOBJ[1].SetActive(false);
                All_GameOBJ[2].SetActive(false);
                HintTitle.SetActive(true);
                BTN_Layout.SetActive(true);
                AlcoholLamp_BTN.SetActive(true);
                break;
            case 3:
                Tips.text = "第三步驟：靜待溶液冷卻，邊攪拌邊將100毫升的飽和食鹽水倒入溶液。";
                All_GameOBJ[0].SetActive(true);
                All_GameOBJ[1].SetActive(false);
                All_GameOBJ[2].SetActive(false);
                HintTitle.SetActive(true);
                BTN_Layout.SetActive(true);
                AlcoholLamp_BTN.SetActive(false);
                break;
            case 4:
                Tips.text = "第四步驟：靜置後，將浮於上層的白色固體撈出，並放置於蒸發皿。";
                All_GameOBJ[0].SetActive(true);
                All_GameOBJ[1].SetActive(true);
                All_GameOBJ[2].SetActive(false);
                break;
            case 5:
                //蒸發皿
                Tips.text = "第五步驟：以湯匙挖取少許蒸發皿中的固體肥皂，放置石蕊試紙上，觀察試紙的顏色變化。";
                LitmusPaper_BTN.SetActive(true);
                All_GameOBJ[0].SetActive(false);
                All_GameOBJ[1].SetActive(true);
                All_GameOBJ[2].SetActive(false);
                break;
            case 6:
                //試管
                Tips.text = "第六步驟：取一支試管加入10mL蒸餾水及1mL沙拉油，觀察試管液面。";
                LitmusPaper_BTN.SetActive(false);
                All_GameOBJ[0].SetActive(false);
                All_GameOBJ[1].SetActive(false);
                All_GameOBJ[2].SetActive(true);
                break;
            case 7:
                //試管
                Tips.text = "第七步驟：用湯匙加入一點肥皂到試管內，搖晃試管，觀察試管內的變化。";
                All_GameOBJ[0].SetActive(false);
                All_GameOBJ[1].SetActive(true);
                All_GameOBJ[2].SetActive(true);
                break;
            case 8: //步驟都結束之後
                Quesion_Interface.SetActive(true);
                All_GameOBJ[0].SetActive(false);
                All_GameOBJ[1].SetActive(false);
                All_GameOBJ[2].SetActive(false);
                Hint_Title.SetActive(false);
                BTN_Layout.SetActive(false);
                GoToQustion.SetActive(false);
                BeakerHint_Ani.SetActive(false);
                StopHintTimer();
                StopTimer();
                break;
            default:
                Debug.Log(CG_Hint_Manager.Currentstep);
                break;
        }
    }

    public void ChangeStep()
    {
        CG_Hint_Manager.Currentstep++;
    }
}
