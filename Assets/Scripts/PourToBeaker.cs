using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PourToBeaker : MonoBehaviour
{
    //本腳本放在固定燒杯身上。


    public ParticleSystem CoconutOilParticle1, CoconutOilParticle2, AlcoholParticle1, AlcoholParticle2, NaOHParticle1, NaOHParticle2, NaClParticle1, NaClParticle2;
    public GameObject NaOH_Liquid, CoconutOil_Liquid, Alcohol_Liquid, NaCl_Liquid;
    public static bool CoconutOilTrigger, AlcoholTrigger, NaOHTrigger, NaClTrigger, H2OTrigger, SaladOilTrigger;
    public static bool step1_1, step1_2, step2, step3, step6 = false;
    public static bool Coconut_Notenough, Alcohol_Notenough, NaOH_Notenough, NaCl_Notenough;
    public static bool Coconut_isPoured, Alcohol_isPoured, NaOH_isPoured, NaCl_isPoured;
    private Vector3 scaleChange;
    public GameObject CoconutOilPGM1, CoconutOilPGM2, AlcoholPGM1, AlcoholPGM2, NaOHPGM1, NaOHPGM2, NaClPGM1, NaClPGM2;
    float CoconutOil_rotationX1, CoconutOil_rotationX2, Alcohol_rotationX1, Alcohol_rotationX2, NaOH_rotationX1, NaOH_rotationX2, NaCl_rotationX1, NaCl_rotationX2;

    private bool[] hasLogged; // 布林陣列，用來控制每個物件的紀錄狀態
    public float resetDelay = 3f; // 重置延遲時間（秒
    void Awake()
    {
        CoconutOilParticle1.Stop();
        CoconutOilParticle2.Stop();
        AlcoholParticle1.Stop();
        AlcoholParticle2.Stop();
        NaOHParticle1.Stop();
        NaOHParticle2.Stop();
        NaClParticle1.Stop();
        NaClParticle2.Stop();
        scaleChange = new Vector3(0, -0.01f, 0);
    }
    public void OnTriggerEnter(Collider other)  //拉溶液到燒杯旁、判定燒杯內溶液狀況變化
    {
        if (other.CompareTag("CoconutOil"))
        {
            CoconutOilTrigger = true;
        }
        if (other.CompareTag("Alcohol"))
        {
            AlcoholTrigger = true;
        }
        if (other.CompareTag("NaOH"))
        {
            NaOHTrigger = true;
        }
        if (other.CompareTag("NaCl"))
        {
            NaClTrigger = true;
        }
        
    }

    public void OnTriggerStay(Collider other)   //判斷溶液的倒入動畫
    {
        if (other.CompareTag("CoconutOil"))
        {
            if (ROT_Detect.rot_CoconutOil >= 60 && ROT_Detect.rot_CoconutOil <= 110 || ROT_Detect.rot_CoconutOil >= 270 && ROT_Detect.rot_CoconutOil <= 320)
            {
                Coconut_isPoured = true;
                CoconutOil_Liquid.transform.localScale += scaleChange;
                if(CoconutOil_Liquid.transform.localScale.y <= 0)
                {
                    CoconutOil_Liquid.transform.localScale = new Vector3(1, 0, 1);
                }
                if (ROT_Detect.rot_CoconutOil >= 270 && ROT_Detect.rot_CoconutOil <= 320)
                {
                    CoconutOilParticle1.Play();
                    CoconutOilPGM1.transform.localRotation = Quaternion.Euler(-CoconutOil_rotationX1 - 90.0f, 0, 0);
                }
                if (ROT_Detect.rot_CoconutOil >= 60 && ROT_Detect.rot_CoconutOil <= 110)
                {
                    CoconutOilParticle2.Play();
                    CoconutOilPGM2.transform.localRotation = Quaternion.Euler(-CoconutOil_rotationX2 - 90.0f, 0, 0);
                }
            }
            else
            {
                CoconutOilParticle1.Stop();
                CoconutOilParticle2.Stop();
            }
        }
        if (other.CompareTag("Alcohol"))
        {
            if (ROT_Detect.rot_Alcohol >= 60 && ROT_Detect.rot_Alcohol <= 110 || ROT_Detect.rot_Alcohol >= 270 && ROT_Detect.rot_Alcohol <= 320)
            {
                Alcohol_isPoured = true;
                Alcohol_Liquid.transform.localScale += scaleChange;
                if (Alcohol_Liquid.transform.localScale.y <= 0)
                {
                    Alcohol_Liquid.transform.localScale = new Vector3(1, 0, 1);
                }
                if (ROT_Detect.rot_Alcohol >= 270 && ROT_Detect.rot_Alcohol <= 320)
                {
                    AlcoholParticle1.Play();
                    AlcoholPGM1.transform.localRotation = Quaternion.Euler(-Alcohol_rotationX1 - 90.0f, 0, 0);
                }
                if (ROT_Detect.rot_Alcohol >= 60 && ROT_Detect.rot_Alcohol <= 110)
                {
                    AlcoholParticle2.Play();
                    AlcoholPGM2.transform.localRotation = Quaternion.Euler(-Alcohol_rotationX2 - 90.0f, 0, 0);
                }
            }
            else
            {
                AlcoholParticle1.Stop();
                AlcoholParticle2.Stop();
            }
        }
        if (other.CompareTag("NaOH"))
        {            
            if (ROT_Detect.rot_NaOH >= 60 && ROT_Detect.rot_NaOH <= 110 || ROT_Detect.rot_NaOH >= 270 && ROT_Detect.rot_NaOH <= 320)
            {
                NaOH_isPoured = true;
                
                NaOH_Liquid.transform.localScale += scaleChange;
                if (NaOH_Liquid.transform.localScale.y <= 0)
                {
                    NaOH_Liquid.transform.localScale = new Vector3(1, 0, 1);
                }
                if (ROT_Detect.rot_NaOH >= 270 && ROT_Detect.rot_NaOH <= 320)
                {
                    NaOHPGM1.transform.localRotation = Quaternion.Euler(-NaOH_rotationX1 - 90.0f, 0, 0);
                    NaOHParticle1.Play();
                }
                if (ROT_Detect.rot_NaOH >= 60 && ROT_Detect.rot_NaOH <= 110)
                {
                    NaOHPGM2.transform.localRotation = Quaternion.Euler(-NaOH_rotationX2 - 90.0f, 0, 0);
                    NaOHParticle2.Play();
                }
            }
            else
            {
                NaOHParticle1.Stop();
                NaOHParticle2.Stop();
            }
        }
        if (other.CompareTag("NaCl"))
        {            
            if (ROT_Detect.rot_NaCl >= 60 && ROT_Detect.rot_NaCl <= 110 || ROT_Detect.rot_NaCl >= 270 && ROT_Detect.rot_NaCl <= 320)
            {
                NaCl_isPoured = true;
                NaCl_Liquid.transform.localScale += scaleChange;
                if (NaCl_Liquid.transform.localScale.y <= 0)
                {
                    NaCl_Liquid.transform.localScale = new Vector3(1, 0, 1);
                }
                if (ROT_Detect.rot_NaCl >= 270 && ROT_Detect.rot_NaCl <= 320)
                {
                    NaClPGM1.transform.localRotation = Quaternion.Euler(-NaCl_rotationX1 - 90.0f, 0, 0);
                    NaClParticle1.Play();
                }
                if (ROT_Detect.rot_NaCl >= 60 && ROT_Detect.rot_NaCl <= 110)
                {
                    NaClPGM2.transform.localRotation = Quaternion.Euler(-NaCl_rotationX2 - 90.0f, 0, 0);
                    NaClParticle2.Play();
                }
            }
            else
            {
                NaClParticle1.Stop();
                NaClParticle2.Stop();
            }
        }
    }
    public void OnTriggerExit(Collider other)
    {
        CoconutOilParticle1.Stop();
        CoconutOilParticle1.Clear();
        CoconutOilParticle2.Stop();
        CoconutOilParticle2.Clear();
        AlcoholParticle1.Stop();
        AlcoholParticle1.Clear();
        AlcoholParticle2.Stop();
        AlcoholParticle2.Clear();
        NaOHParticle1.Stop();
        NaOHParticle1.Clear();
        NaOHParticle2.Stop();
        NaOHParticle2.Clear();
        NaClParticle1.Stop();
        NaClParticle1.Clear();
        NaClParticle2.Stop();
        NaClParticle2.Clear();

        AlcoholTrigger = false;
        CoconutOilTrigger = false;
        NaOHTrigger = false;
        NaClTrigger = false;
    }
    
    public void Update()        //還要再多一個判定才能確定是不是要顯示圖片?
    {
        /*if (PlayerPrefs.GetInt("ReviewMistakes", 0) == 1 && !isreset)
        {
            ResetVariables();
            isreset = true;
        }*/
        CoconutOil_rotationX1 = Mathf.Lerp(185f, 135f, Mathf.InverseLerp(270f, 320f, ROT_Detect.rot_CoconutOil));
        CoconutOil_rotationX2 = Mathf.Lerp(40f, -10f, Mathf.InverseLerp(60f, 110f, ROT_Detect.rot_CoconutOil));
        Alcohol_rotationX1 = Mathf.Lerp(185f, 135f, Mathf.InverseLerp(270f, 320f, ROT_Detect.rot_Alcohol));
        Alcohol_rotationX2 = Mathf.Lerp(40f, -10f, Mathf.InverseLerp(60f, 110f, ROT_Detect.rot_Alcohol));
        NaOH_rotationX1 = Mathf.Lerp(185f, 135f, Mathf.InverseLerp(270f, 320f, ROT_Detect.rot_NaOH));
        NaOH_rotationX2 = Mathf.Lerp(40f, -10f, Mathf.InverseLerp(60f, 110f, ROT_Detect.rot_NaOH));
        NaCl_rotationX1 = Mathf.Lerp(185f, 135f, Mathf.InverseLerp(270f, 320f, ROT_Detect.rot_NaCl));
        NaCl_rotationX2 = Mathf.Lerp(40f, -10f, Mathf.InverseLerp(60f, 110f, ROT_Detect.rot_NaCl));


        /*if (ROT_Detect.rot_CoconutOil >= 270 && ROT_Detect.rot_CoconutOil <= 320)
        {
            CoconutOilParticle1.Play();
            CoconutOilPGM1.transform.localRotation = Quaternion.Euler(CoconutOil_rotationX1, 0, 0);
        }
        if (ROT_Detect.rot_CoconutOil >= 60 && ROT_Detect.rot_CoconutOil <= 110)
        {
            CoconutOilParticle2.Play();
            CoconutOilPGM2.transform.localRotation = Quaternion.Euler(CoconutOil_rotationX2, 0, 0);
        }
        if (ROT_Detect.rot_Alcohol >= 270 && ROT_Detect.rot_Alcohol <= 320)
        {
            AlcoholParticle1.Play();
            AlcoholPGM1.transform.localRotation = Quaternion.Euler(Alcohol_rotationX1, 0, 0);
        }
        if (ROT_Detect.rot_Alcohol >= 60 && ROT_Detect.rot_Alcohol <= 110)
        {
            AlcoholParticle2.Play();
            AlcoholPGM2.transform.localRotation = Quaternion.Euler(Alcohol_rotationX2, 0, 0);
        }
        if (ROT_Detect.rot_NaOH >= 270 && ROT_Detect.rot_NaOH <= 320)
        {
            NaOHPGM1.transform.localRotation = Quaternion.Euler(NaOH_rotationX1, 0, 0);
            NaOHParticle1.Play();
        }
        if (ROT_Detect.rot_NaOH >= 60 && ROT_Detect.rot_NaOH <= 110)
        {
            NaOHPGM2.transform.localRotation = Quaternion.Euler(NaOH_rotationX2, 0, 0);
            NaOHParticle2.Play();
        }
        if (ROT_Detect.rot_NaCl >= 270 && ROT_Detect.rot_NaCl <= 320)
        {
            NaClPGM1.transform.localRotation = Quaternion.Euler(NaCl_rotationX1, 0, 0);
            NaClParticle1.Play();
        }
        if (ROT_Detect.rot_NaCl >= 60 && ROT_Detect.rot_NaCl <= 110)
        {
            NaClPGM2.transform.localRotation = Quaternion.Euler(NaCl_rotationX2, 0, 0);
            NaClParticle2.Play();
        }*/
    }


}
