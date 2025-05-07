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
            DontDestroyOnLoad(gameObject);  // �T�O�����������ɤ��Q�P��
            IncorrectSteps = new List<CG_IncorrectStepDetail>();
        }
        else if (Instance != this)
        {
            Destroy(gameObject);  // �T�O�u���@�ӹ�Ҧs�b
        }
    }
}
