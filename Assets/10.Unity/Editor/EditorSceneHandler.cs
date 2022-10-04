using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

public class EditorSceneHandler : MonoBehaviour
{
    #region Public
    [MenuItem("Taeyoung/Scene/Public/Game")]
    public static void LoadGame()
    {
        EditorSceneManager.OpenScene("Assets/00.Scenes/Main/Game.unity");
    }
    [MenuItem("Taeyoung/Scene/Public/Menu")]
    public static void LoadMenu()
    {
        EditorSceneManager.OpenScene("Assets/00.Scenes/Main/Menu.unity");
    }
    [MenuItem("Taeyoung/Scene/Public/Ending")]
    public static void LoadEnding()
    {
        EditorSceneManager.OpenScene("Assets/00.Scenes/Main/Ending.unity");
    }
    [MenuItem("Taeyoung/Scene/Public/Tutorial")]
    public static void LoadTutorial()
    {
        EditorSceneManager.OpenScene("Assets/00.Scenes/Main/Tutorial.unity");
    }
    #endregion

    #region Dev
    [MenuItem("Taeyoung/Scene/Dev/Taeyoung")]
    public static void LoadNaEun()
    {
        EditorSceneManager.OpenScene("Assets/00.Scenes/Dev/Taeyoung/Taeyoung.unity");
    }
    [MenuItem("Taeyoung/Scene/Dev/Jaeby")]
    public static void LoadHyunWoong()
    {
        EditorSceneManager.OpenScene("Assets/00.Scenes/Dev/Jaeby/Jaeby.unity");
    }
    [MenuItem("Taeyoung/Scene/Dev/Jinwo")]
    public static void LoadJunSeong()
    {
        EditorSceneManager.OpenScene("Assets/00.Scenes/Dev/Jinwo/Jinwo.unity");
    }
    [MenuItem("Taeyoung/Scene/Dev/Minyoung")]
    public static void LoadSeolAh()
    {
        EditorSceneManager.OpenScene("Assets/00.Scenes/Dev/Minyoung/Minyoung.unity");
    }
    #endregion
}
