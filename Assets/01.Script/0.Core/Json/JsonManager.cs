using UnityEngine;
using System.IO;
using UnityEngine.UI;
public static class JsonManager
{
    private static string SAVE_PATH = "";
    private static string SAVE_FILENAME = "/SaveFile.txt";
    private static JsonData data = null;
    public static JsonData Data { get { return data; } }

    private static JsonDataDisplay dataDisplay;

    [ContextMenu("불러오기")]
    public static void Load()
    {
        Init();
        string json = "";
        if (File.Exists(SAVE_PATH + SAVE_FILENAME) == true)
        {
            json = File.ReadAllText(SAVE_PATH + SAVE_FILENAME);
            data = JsonUtility.FromJson<JsonData>(json);
        }
    }
    [ContextMenu("저장")]
    public static void Save()
    {
        Init();
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(SAVE_PATH + SAVE_FILENAME, json, System.Text.Encoding.UTF8);
    }
    public static void Init()
    {
        SAVE_PATH = Application.dataPath + "/Save";
        if (Directory.Exists(SAVE_PATH) == false)
        {
            Directory.CreateDirectory(SAVE_PATH);
        }
    }

    public static void DisplayData()
    {
        if (dataDisplay == null)
        {
            dataDisplay = new GameObject("@ JsonDataDisplay").AddComponent<JsonDataDisplay>();
            dataDisplay.Data = data;
        }
    }

    public class JsonDataDisplay : MonoBehaviour
    {
        [SerializeField] private JsonData data;
        public JsonData Data { get { return data; } set { data = value; } }
    }
}