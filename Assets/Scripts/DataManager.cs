using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;


public class DataManager : MonoBehaviour {

    static DataManager dataManager = null;

    // DATA LIST
    /*
     * Tag :            태그
     * SpeakerName :    대화록에 띄울 이름
     * DialogueText :   대화문
     * Order :          명령
     * Selection :      선택지
     * Condition :      출현 조건
     * 
     * */

    static bool isInitalized = false;
    public static bool IsInitalized
    {
        get
        {
            if (!isInitalized)
            {
                Initalization();
                return false;
            }
            else return true;
        }
    }

    public static int Count
    {
        get
        {
            if (isInitalized)
            {
                return dataManager.csvData.Count;
            }
            else return 0;
        }
    }

    static string DATA_PATH = "Text/TextData";

    static string SPLIT_RE = @",(?=(?:[^""]*""[^""]*"")*(?![^""]*""))";
    static string LINE_SPLIT_RE = @"\r\n|\n\r|\n|\r";
    static char[] TRIM_CHARS = { '\"' };

    List<Dictionary<string, object>> csvData;

    public static Dictionary<string, object> GetData(int index)
    {
        if (isInitalized)
        {
            if (dataManager != null)
            {
                if (0 <= index && index < dataManager.csvData.Count && dataManager.csvData[index] != null)
                {
                    return dataManager.csvData[index];
                }
                else return null;
            }
            else return null;
        }
        else return null;
    }

    IEnumerator LoadData()
    {
        ResourceRequest resourceRequest = Resources.LoadAsync<TextAsset>(DATA_PATH);
        if (resourceRequest != null)
        {
            while (!resourceRequest.isDone) yield return null;

            TextAsset textAsset = resourceRequest.asset as TextAsset;

            dataManager.csvData = new List<Dictionary<string, object>>();

            string[] lines = Regex.Split(textAsset.text, LINE_SPLIT_RE);

            if (lines.Length > 1)
            {
                string[] header = Regex.Split(lines[0], SPLIT_RE);
                List<int> conIndex = new List<int>();
                Dictionary<int, string> conValue = new Dictionary<int, string>();
                int loadCount = 0;
                for (int i = 1; i < lines.Length; ++i)
                {
                    string[] values = Regex.Split(lines[i], SPLIT_RE);

                    if (values.Length == 0) continue;

                    Dictionary<string, object> entry = new Dictionary<string, object>();
                    for (int j = 0; j < header.Length && j < values.Length; ++j)
                    {
                        string value = values[j];
                        entry[header[j]] = value.TrimStart(TRIM_CHARS).TrimEnd(TRIM_CHARS).Replace("\\", "");
                        if (!string.IsNullOrEmpty(value))
                        {

                        }
                    }
                    dataManager.csvData.Add(entry);
                    if (++loadCount > 200)
                    {
                        loadCount = 0;
                        yield return null;
                    }
                }
            }
            Resources.UnloadUnusedAssets();
            isInitalized = true;
        }
    }

    public static void Initalization()
    {
        if (!isInitalized)
        {
            if (dataManager == null)
            {
                dataManager = new GameObject("DataManager").AddComponent<DataManager>();
                DontDestroyOnLoad(dataManager.gameObject);
                dataManager.StartCoroutine(dataManager.LoadData());
            }
        }
    }

}
