using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class CG_BehaviorLogger : MonoBehaviour
{
    public string logFilePath;
    public static CG_BehaviorLogger Instance { get; private set; }

    // Start is called before the first frame update
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);      
        }

        logFilePath = Path.Combine(Application.persistentDataPath, "StudentBehaviorLog.txt");
        
        if (!File.Exists(logFilePath))
        {
            File.WriteAllText(logFilePath, "學生行為紀錄開始：\n\n");
        }
    }
    public void LogBehavior(string action)
    {
        string logEntry = System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + " - " + action + "\n";
        File.AppendAllText(logFilePath, logEntry);
        Debug.Log("Logged: " + action);
    }

    public void ShareLogFile()
    {
        if (File.Exists(logFilePath))
        {
            new NativeShare()
                .AddFile(logFilePath)
                .SetSubject("Student Behavior Log")
                .SetText("學生行為記錄檔案")
                .Share();

            Debug.Log("Log file shared from: " + logFilePath);
        }
        else
        {
            Debug.LogWarning("Log file not found, nothing to share.");
        }
    }
}
