using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CG_ROT_Detect : MonoBehaviour
{
    //本腳本放在椰子油、酒精、NaOH、NaCl
    public static float rot_CoconutOil, rot_Alcohol, rot_NaOH, rot_NaCl, rot_H2O, rot_SaladOil;
    public GameObject CoconutOil, Alcohol, NaOH, NaCl, H2O, SaladOil;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 CoconutOilDirection = Camera.main.transform.InverseTransformDirection(CoconutOil.transform.forward);
        Vector3 AlcoholDirection = Camera.main.transform.InverseTransformDirection(Alcohol.transform.forward);
        Vector3 NaOHDirection = Camera.main.transform.InverseTransformDirection(NaOH.transform.forward);
        Vector3 NaClDirection = Camera.main.transform.InverseTransformDirection(NaCl.transform.forward);
        Vector3 H2ODirection = Camera.main.transform.InverseTransformDirection(H2O.transform.forward);
        Vector3 SaladOilDirection = Camera.main.transform.InverseTransformDirection(SaladOil.transform.forward);
        rot_CoconutOil = Mathf.Atan2(CoconutOilDirection.x, CoconutOilDirection.y) * Mathf.Rad2Deg;
        rot_Alcohol = Mathf.Atan2(AlcoholDirection.x, AlcoholDirection.y) * Mathf.Rad2Deg;
        rot_NaOH = Mathf.Atan2(NaOHDirection.x, NaOHDirection.y) * Mathf.Rad2Deg;
        rot_NaCl = Mathf.Atan2(NaClDirection.x, NaClDirection.y) * Mathf.Rad2Deg;
        rot_H2O = Mathf.Atan2(H2ODirection.x, H2ODirection.y) * Mathf.Rad2Deg;
        rot_SaladOil = Mathf.Atan2(SaladOilDirection.x, SaladOilDirection.y) * Mathf.Rad2Deg;
        if (rot_CoconutOil < 0)
        {
            rot_CoconutOil += 360;
        }
        if (rot_Alcohol < 0)
        {
            rot_Alcohol += 360;
        }
        if (rot_NaOH < 0)
        {
            rot_NaOH += 360;
        }
        if (rot_NaCl < 0)
        {
            rot_NaCl += 360;
        }
        if (rot_H2O < 0)
        {
            rot_H2O += 360;
        }
        if (rot_SaladOil < 0)
        {
            rot_SaladOil += 360;
        }
    }
}
