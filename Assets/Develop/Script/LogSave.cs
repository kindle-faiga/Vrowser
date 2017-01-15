using UnityEngine;
using System.Collections;
using System.IO;
using System;
public class LogSave : MonoBehaviour
{
    void Start()
    {
        logSave("Log Start");
        logSave("---------");
    }

    public void logSave(string txt)
    {
        StreamWriter sw;
        FileInfo fi;
        fi = new FileInfo(Application.dataPath + "/Log.csv");
        sw = fi.AppendText();
        sw.WriteLine(txt);
        sw.Flush();
        sw.Close();
    }
}
