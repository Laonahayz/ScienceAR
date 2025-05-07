using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncorrectStepDetail
{
    public int QuestionNum { get; set; }
    public int StepAssociated { get; set; }
}

public class PersistenceManager : MonoBehaviour
{
    public static PersistenceManager Instance;
    public List<IncorrectStepDetail> IncorrectSteps;
    public bool ReviewMistakes = false;
    void Awake()
    {
        
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);  // �T�O�����������ɤ��Q�P��
            IncorrectSteps = new List<IncorrectStepDetail>();
        }
        else if (Instance != this)
        {
            Destroy(gameObject);  // �T�O�u���@�ӹ�Ҧs�b
        }
    }
}
