using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class CG_PourToTestTube : MonoBehaviour
{
    //本腳本放在固定試管身上。

    private float lastPositionY;
    public static int shakeCount = 0;
    private bool movingUp = false;
    bool Turbid;
    public ParticleSystem SaladOilParticle1, SaladOilParticle2, H2OParticle1, H2OParticle2;
    public static bool H2OTrigger, SaladOilTrigger;
    public static bool step6_1, step6_2;
    public static bool SaladOil_Notenough, H2O_Notenough;
    public static bool SaladOil_isPoured, H2O_isPoured, shakeTestTube;
    public GameObject ChangeStep_BTN, GoToQustion, BeakerHint;
    public GameObject H2O_Liquid, SaladOil_Liquid;
    public Animator TestTubeAnim;
    public static int triggerCount = 0;
    bool isreset;
    private Vector3 scaleChange;
    float SaladOil_rotationX1, SaladOil_rotationX2, H2O_rotationX1, H2O_rotationX2;
    public GameObject SaladOilPGM1, SaladOilPGM2, H2OPGM1, H2OPGM2;
    void Awake()
    {
        SaladOilParticle1.Stop();
        SaladOilParticle2.Stop();
        H2OParticle1.Stop();
        H2OParticle2.Stop();
        scaleChange = new Vector3(0, -0.01f, 0);
        lastPositionY = transform.position.y;
        shakeTestTube = false;
        ResetVariables();
    }
    void ResetVariables()
    {
        shakeCount = 0;
        lastPositionY = transform.position.y;
        shakeTestTube = false;
        H2OTrigger = false;
        SaladOilTrigger = false;
        step6_1 = false;
        step6_2 = false;
        SaladOil_Notenough = false;
        H2O_Notenough = false;
        SaladOil_isPoured = false;
        H2O_isPoured = false;
    }
    void Update()
    {
        /*if (PlayerPrefs.GetInt("ReviewMistakes", 0) == 1 && !isreset)
        {
            ResetVariables();
            isreset = true;
        }*/
        float currentPositionY = transform.position.y;

        // 檢測物件Up還是Down
        if (currentPositionY > lastPositionY + 40f) // Up
        {
            shakeTestTube = true;
            if (!movingUp)
            {
                movingUp = true;
                shakeCount++;
                Debug.Log("搖晃次數：" + shakeCount);
            }
        }
        else if (currentPositionY < lastPositionY - 40f) // Down
        {
            shakeTestTube = true;
            if (movingUp)
            {
                movingUp = false;
                shakeCount++;
                Debug.Log("搖晃次數：" + shakeCount);
            }
        }
        
        lastPositionY = currentPositionY; // 更新上一幀位置
    
        if (shakeCount >= 6 && CG_Spoon_Collision.isgotoTestTube && !Turbid) // 設1.0單位範圍有效
        {
            StartCoroutine(Observation());
        }
        SaladOil_rotationX1 = Mathf.Lerp(185f, 135f, Mathf.InverseLerp(270f, 320f, CG_ROT_Detect.rot_SaladOil));
        SaladOil_rotationX2 = Mathf.Lerp(40f, -10f, Mathf.InverseLerp(60f, 110f, CG_ROT_Detect.rot_SaladOil));
        H2O_rotationX1 = Mathf.Lerp(185f, 135f, Mathf.InverseLerp(270f, 320f, CG_ROT_Detect.rot_H2O));
        H2O_rotationX2 = Mathf.Lerp(40f, -10f, Mathf.InverseLerp(60f, 110f, CG_ROT_Detect.rot_H2O));

        /*if (CG_ROT_Detect.rot_SaladOil >= 270 && CG_ROT_Detect.rot_SaladOil <= 320)
        {
            SaladOilParticle1.Play();
            SaladOilPGM1.transform.localRotation = Quaternion.Euler(SaladOil_rotationX1, 0, 0);
        }
        if (CG_ROT_Detect.rot_SaladOil >= 60 && CG_ROT_Detect.rot_SaladOil <= 110)
        {
            SaladOilParticle2.Play();
            SaladOilPGM2.transform.localRotation = Quaternion.Euler(SaladOil_rotationX2, 0, 0);
        }
        if (CG_ROT_Detect.rot_H2O >= 270 && CG_ROT_Detect.rot_H2O <= 320)
        {
            H2OParticle1.Play();
            H2OPGM1.transform.localRotation = Quaternion.Euler(H2O_rotationX1, 0, 0);
        }
        if (CG_ROT_Detect.rot_H2O >= 60 && CG_ROT_Detect.rot_H2O <= 110)
        {
            H2OParticle2.Play();
            H2OPGM2.transform.localRotation = Quaternion.Euler(H2O_rotationX2, 0, 0);
        }*/
    }
    IEnumerator Observation()
    {
        //觀察3秒
        TestTubeAnim.Play("TakeOff_H2OOil");
        yield return null;
        yield return new WaitForSeconds(TestTubeAnim.GetCurrentAnimatorStateInfo(0).length + 4.0f);
        Turbid = true;
        if (!CG_QuestionsM2.ReStartExperiment)
        {
            CG_ResetAllValue.CloseHint = true;
            GoToQustion.SetActive(true);
            BeakerHint.SetActive(false);
        }
        else if (CG_QuestionsM2.ReStartExperiment)
        {
            SceneManager.LoadScene("CG_End");
        }
    }
    public void OnTriggerEnter(Collider other)  //拉溶液到燒杯旁、判定燒杯內溶液狀況變化
    {
        if (other.CompareTag("SaladOil"))
        {
            Debug.Log("有碰到沙拉油");
            SaladOilTrigger = true;
            triggerCount++;
            UpdateStepStatus();
        }
        if (other.CompareTag("H2O"))
        {
            Debug.Log("有碰到蒸餾水");
            H2OTrigger = true;
            triggerCount++;
            UpdateStepStatus();
        }
    }
    void UpdateStepStatus()
    {
        if (triggerCount == 1)
        {
            step6_1 = true;
            step6_2 = false;
        }
        else if (triggerCount == 2)
        {
            step6_1 = false;
            step6_2 = true;
        }
    }
    public void OnTriggerStay(Collider other)   //判斷溶液的倒入動畫
    {
        if (other.CompareTag("H2O"))
        {
            if (CG_ROT_Detect.rot_H2O >= 60 && CG_ROT_Detect.rot_H2O <= 110 || CG_ROT_Detect.rot_H2O >= 270 && CG_ROT_Detect.rot_H2O <= 320)
            {
                H2O_isPoured = true;
                H2O_Liquid.transform.localScale += scaleChange;
                if (H2O_Liquid.transform.localScale.y <= 0)
                {
                    H2O_Liquid.transform.localScale = new Vector3(1, 0, 1);
                }
                if (CG_ROT_Detect.rot_H2O >= 270 && CG_ROT_Detect.rot_H2O <= 320)
                {
                    H2OParticle1.Play();
                    H2OPGM1.transform.localRotation = Quaternion.Euler(-H2O_rotationX1 - 90.0f, 0, 0);
                }
                if (CG_ROT_Detect.rot_H2O >= 60 && CG_ROT_Detect.rot_H2O <= 110)
                {
                    H2OParticle2.Play();
                    H2OPGM2.transform.localRotation = Quaternion.Euler(-H2O_rotationX2 - 90.0f, 0, 0);
                }
            }
            else
            {
                H2OParticle1.Stop();
                H2OParticle2.Stop();
            }
        }
        if (other.CompareTag("SaladOil"))
        {
            if (CG_ROT_Detect.rot_SaladOil >= 60 && CG_ROT_Detect.rot_SaladOil <= 110 || CG_ROT_Detect.rot_SaladOil >= 270 && CG_ROT_Detect.rot_SaladOil <= 320)
            {
                SaladOil_isPoured = true;
                SaladOil_Liquid.transform.localScale += scaleChange;
                if (SaladOil_Liquid.transform.localScale.y <= 0)
                {
                    SaladOil_Liquid.transform.localScale = new Vector3(1, 0, 1);
                }
                if (CG_ROT_Detect.rot_SaladOil >= 270 && CG_ROT_Detect.rot_SaladOil <= 320)
                {
                    SaladOilParticle1.Play();
                    SaladOilPGM1.transform.localRotation = Quaternion.Euler(-SaladOil_rotationX1 - 90.0f, 0, 0);
                }
                if (CG_ROT_Detect.rot_SaladOil >= 60 && CG_ROT_Detect.rot_SaladOil <= 110)
                {
                    SaladOilParticle2.Play();
                    SaladOilPGM2.transform.localRotation = Quaternion.Euler(-SaladOil_rotationX2 - 90.0f, 0, 0);
                }
            }
            else
            {
                SaladOilParticle1.Stop();
                SaladOilParticle2.Stop();
            }
        }

    }
    public void OnTriggerExit(Collider other)
    {
        SaladOilParticle1.Stop();
        SaladOilParticle1.Clear();
        SaladOilParticle2.Stop();
        SaladOilParticle2.Clear();
        H2OParticle1.Stop();
        H2OParticle1.Clear();
        H2OParticle2.Stop();
        H2OParticle2.Clear();
        SaladOilTrigger = false;
        H2OTrigger = false;
    }


}
