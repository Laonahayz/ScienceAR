using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CG_Spoon_Collision : MonoBehaviour
{
    //本腳本放在湯匙上
    public GameObject SpoonBowlthing, DishesOBJ;
    public GameObject Bowlthing1, Bowlthing2, Bowlthing3, Bowlthing4, Bowlthing5, Bowlthing6, TubeHat, BowlthingOnLP;
    public Material LitmusPaper, DishesOBJ_M;
    public static int SpoonTimes;
    float a;
    public static bool is_scoop_up, isgotoLitmusPaper, isTouchingLitmusPaper;
    public static bool isgotoTestTube, cancloseLitmusPaper;
    bool isreset;

    void Awake()
    {
        a = 0.8f;
        SpoonTimes = 1;
        LitmusPaper.SetFloat("_ColorNum", 0.8f);
        ResetVariables();
    }
    void ResetVariables()
    {
        a = 0.8f;
        LitmusPaper.SetFloat("_ColorNum", 0.8f);
        SpoonTimes = 1;
        is_scoop_up = false;
        isgotoLitmusPaper = false;
        isTouchingLitmusPaper = false;
        isgotoTestTube = false;
        cancloseLitmusPaper = false;
    }
    void Update()
    {
        /*if (PlayerPrefs.GetInt("ReviewMistakes", 0) == 1 && !isreset)
        {
            ResetVariables();
            isreset = true;
        }*/
        if (CG_Hint_Manager.Currentstep == 5 && SpoonTimes == 6 && !isgotoLitmusPaper)
        {
            isgotoLitmusPaper = true;
            SpoonTimes = 1;
            Bowlthing6.SetActive(false);
            SpoonBowlthing.SetActive(false);
        }
        if (isTouchingLitmusPaper)
        {
            a -= 0.2f * Time.deltaTime;
            LitmusPaper.SetFloat("_ColorNum", a);
        }
        if (a <= -0.03f && CG_Hint_Manager.Currentstep != 7)
        {
            isTouchingLitmusPaper = false;
            cancloseLitmusPaper = true;
            //Debug.Log("記得關試紙");
        }
        
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("BeakerToSpoon") && !is_scoop_up)  //燒杯白固體到湯匙
        {
            is_scoop_up = true;
            if (SpoonTimes == 1)
            {
                SpoonTimes++;
                Bowlthing1.SetActive(false);
                SpoonBowlthing.SetActive(true);
                Debug.Log(SpoonTimes);
            }
            else if (SpoonTimes == 2)
            {
                SpoonTimes++;
                Bowlthing2.SetActive(false);
                SpoonBowlthing.SetActive(true);
                Debug.Log(SpoonTimes);
            }
            else if (SpoonTimes == 3)
            {
                SpoonTimes++;
                Bowlthing3.SetActive(false);
                SpoonBowlthing.SetActive(true);
                Debug.Log(SpoonTimes);
            }
            else if (SpoonTimes == 4)
            {
                SpoonTimes++;
                Bowlthing4.SetActive(false);
                SpoonBowlthing.SetActive(true);
                Debug.Log(SpoonTimes);
            }
            else if (SpoonTimes == 5)
            {
                SpoonTimes++;
                Bowlthing5.SetActive(false);
                SpoonBowlthing.SetActive(true);
                Debug.Log(SpoonTimes);
            }
            else if (SpoonTimes == 6)
            {
                Bowlthing6.SetActive(false);
                SpoonBowlthing.SetActive(true);
                Debug.Log(SpoonTimes);
            }
        }
        if(other.CompareTag("Dishes"))  //白固體湯匙到蒸發皿
        {
            if(is_scoop_up)
            {
                Color color = DishesOBJ_M.color;
                SpoonBowlthing.SetActive(false);
                DishesOBJ.SetActive(true);
                //Dishes這邊做個水上升的動畫效果
                if (SpoonTimes == 1 && CG_Hint_Manager.Currentstep == 4)
                {
                    is_scoop_up = false;
                    color.a = 0.1f;
                    DishesOBJ.GetComponent<Transform>().localScale = new Vector3(3.4f, 2.1f, 6.18f); DishesOBJ_M.color = color;
                }
                else if (SpoonTimes == 2 && CG_Hint_Manager.Currentstep == 4)
                {
                    is_scoop_up = false;
                    color.a = 0.2f;
                    DishesOBJ.GetComponent<Transform>().localScale = new Vector3(5.4f, 4.0f, 6.1f);
                    DishesOBJ_M.color = color;
                }
                else if (SpoonTimes == 3 && CG_Hint_Manager.Currentstep == 4)
                {
                    is_scoop_up = false;
                    color.a = 0.3f;
                    DishesOBJ.GetComponent<Transform>().localScale = new Vector3(6.4f, 5.9f, 5.7f);
                    DishesOBJ_M.color = color;
                }
                else if (SpoonTimes == 4 && CG_Hint_Manager.Currentstep == 4)
                {
                    is_scoop_up = false;
                    color.a = 0.4f;
                    DishesOBJ.GetComponent<Transform>().localScale = new Vector3(7.2f, 7.8f, 5.0f);
                    DishesOBJ_M.color = color;
                }
                else if (SpoonTimes == 5 && CG_Hint_Manager.Currentstep == 4)
                {
                    is_scoop_up = false;
                    color.a = 0.5f;
                    DishesOBJ.GetComponent<Transform>().localScale = new Vector3(8.0f, 8.7f, 4.5f);
                    DishesOBJ_M.color = color;
                }
                else if (SpoonTimes == 6 && CG_Hint_Manager.Currentstep == 4)
                {
                    is_scoop_up = false;
                    color.a = 0.6f;
                    DishesOBJ.GetComponent<Transform>().localScale = new Vector3(7.4f, 9.3f, 4.5f);
                    DishesOBJ_M.color = color;
                    if (PlayerPrefs.GetInt("ReviewMistakes", 0) != 1)
                    {
                        CG_Hint_Manager.Currentstep = 5;
                    }
                }
            }
            if (SpoonTimes == 1 && isgotoLitmusPaper)   //蒸發皿to 湯匙 to 石蕊試紙
            {
                SpoonBowlthing.SetActive(true);
                Debug.Log(SpoonTimes);
            }
            if (CG_Hint_Manager.Currentstep == 7)   //蒸發皿to 湯匙 to 試管
            {
                is_scoop_up = true;
                SpoonBowlthing.SetActive(true);
                SpoonTimes++;
                Debug.Log(SpoonTimes);
            }
        }
        if (other.CompareTag("LitmusPaper"))
        {
            SpoonBowlthing.SetActive(false);     //湯匙 to 石蕊試紙
            Debug.Log("現在步驟是：" + CG_Hint_Manager.Currentstep);
            isTouchingLitmusPaper = true;
            BowlthingOnLP.SetActive(true);
        }
        if (other.CompareTag("TestTube") && CG_Hint_Manager.Currentstep == 7)
        {
            SpoonBowlthing.SetActive(false);     //湯匙 to 試管
            Debug.Log("現在步驟是：" + CG_Hint_Manager.Currentstep);
            isgotoTestTube = true;
            TubeHat.SetActive(true);
        }
    }
    public void OnTriggerExit()
    {

       
    }
}
