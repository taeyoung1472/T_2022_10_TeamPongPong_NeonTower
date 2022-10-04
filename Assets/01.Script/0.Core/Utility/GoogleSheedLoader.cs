using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;

public static class GoogleSheedLoader
{
    // edit?~ 는 삭재한다
    // export?format=tsv&range{범위1}:{범위2} 추가한다
    readonly static string sheetURL = "https://docs.google.com/spreadsheets/d/10q0Ioy7eGeFtFkL0dwGrtQRC8uiFHQ69BTswHISNwPE/export?format=tsv&rangeA2:C";
    static string sheetData = "";

    static CorClass cor;

    [MenuItem("Taeyoung/Google/Load")]
    public static void Load()
    {
        if (cor == null)
        {
            cor = new GameObject("@ Cor").AddComponent<CorClass>();
        }
        cor.Load();
    }
    public static IEnumerator LoadCor()
    {
        Debug.Log("Load 시작");
        using (UnityWebRequest WWW = UnityWebRequest.Get(sheetURL))
        {
            yield return WWW.SendWebRequest();

            if (WWW.isDone)
            {
                sheetData = WWW.downloadHandler.text;
                Debug.Log("Load 완료");
            }
        }
    }

    class CorClass : MonoBehaviour
    {
        public void Load()
        {
            StartCoroutine(LoadCor());
        }
        public IEnumerator LoadCor()
        {
            Debug.Log("Load 시작");
            using (UnityWebRequest WWW = UnityWebRequest.Get(sheetURL))
            {
                yield return WWW.SendWebRequest();

                if (WWW.isDone)
                {
                    sheetData = WWW.downloadHandler.text;
                    Debug.Log("Load 완료");
                }
            }

            Save();
        }
        private void Save()
        {
            string[] row = sheetData.Split('\n');

            JsonManager.Load();

            JsonManager.Data.sheetData.Clear();

            for (int i = 0; i < row.Length; i++)
            {
                string[] colum = row[i].Split('\t');
                JsonManager.Data.sheetData.Add(new GoogleSheetData());
                for (int j = 0; j < colum.Length; j++)
                {
                    JsonManager.Data.sheetData[i].cell.Add(colum[j]);
                }
            }

            JsonManager.Save();

            DestroyImmediate(gameObject);
        }
    }
}