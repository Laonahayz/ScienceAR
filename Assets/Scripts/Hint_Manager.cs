using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hint_Manager : MonoBehaviour
{
    //遊戲狀態或使用者行為的當前狀態
    public static int Currentstep = 1;     //當前步驟
    public static bool T_TestTube, T_Beaker, T_StirringRod, T_Spoon, T_Alcohol, T_CoconutOil, T_NaOH, T_NaCl, T_Dishes, T_H2O, T_SaladOil;

    public void isStrringRod() { T_StirringRod = true;  }
    public void NoStrringRod() { T_StirringRod = false;  }
    public void isSpoon() { T_Spoon = true;  }
    public void NoSpoon() { T_Spoon = false;  }
    public void isAlcohol() { T_Alcohol = true;  }
    public void NoAlcohol() { T_Alcohol = false;  }
    public void isCoconutOil() { T_CoconutOil = true;  }
    public void NoCoconutOil() { T_CoconutOil = false;  }
    public void isNaOH() { T_NaOH = true;  }
    public void NoNaOH() { T_NaOH = false;  }
    public void isNaCl() { T_NaCl = true;  }
    public void NoNaCl() { T_NaCl = false;  }
    public void isH2O() { T_H2O = true;  }
    public void NoH2O() { T_H2O = false;  }
    public void isSaladOil() { T_SaladOil = true;  }
    public void NoSaladOil() { T_SaladOil = false;  }

    
}


