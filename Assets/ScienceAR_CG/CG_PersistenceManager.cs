using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CG_IncorrectStepDetail
{
    public int QuestionNum { get; set; }
    public int StepAssociated { get; set; }
}

public class CG_PersistenceManager : MonoBehaviour
{
    public static CG_PersistenceManager Instance;
    public List<CG_IncorrectStepDetail> IncorrectSteps;
    public bool ReviewMistakes = false;
    void Awake()
    {
        
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);  // 確保此物件跨場景時不被銷毀
            IncorrectSteps = new List<CG_IncorrectStepDetail>();
        }
        else if (Instance != this)
        {
            Destroy(gameObject);  // 確保只有一個實例存在
        }
    }
}
