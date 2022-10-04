using TMPro;
using UnityEngine;

public class UIManager : MonoSingleTon<UIManager>
{
    [Header("[UI Canvas]")]
    [SerializeField] private GameObject _escUI = null;

    private GameObject prevUI = null;

    [SerializeField]
    private AudioClip _lightClick = null;
    [SerializeField]
    private AudioClip _middleClick = null;
    [SerializeField]
    private AudioClip _HardClick = null;

    [Header("HPUI °ü·Ã")]
    [SerializeField]
    private Color _damagedColor = Color.white;
    [SerializeField]
    private Color _normalColor = Color.white;
    [SerializeField]
    private TextMeshProUGUI _hpText = null;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (prevUI == _escUI)
            {
                DeActiveUI();
            }
            else
            {
                ActiveUI(_escUI);
            }
        }
    }

    public void ActiveUI(GameObject targetUI)
    {
        if (prevUI != null)
            prevUI.GetComponent<IUserInterface>().CloseUI();
        targetUI.GetComponent<IUserInterface>().OpenUI();

        prevUI = targetUI;
    }

    public void DeActiveUI()
    {
        prevUI.GetComponent<IUserInterface>().CloseUI();
        prevUI = null;
    }

    public void SetHpUI(int curHp, int maxHp)
    {
        string str = "";

        for (int i = maxHp; i > 0; i--)
        {
            string colorCode = curHp >= i ? ColorUtility.ToHtmlStringRGBA(_normalColor) : ColorUtility.ToHtmlStringRGBA(_damagedColor);
            print($"I : {i}, color : {colorCode}");
            str += $"<#{colorCode}>¡Ü</color>";
        }

        _hpText.text = str;
    }

    #region ClickSound
    public void LightClickSoundPlay()
    {
        PoolManager.Instance.Pop(PoolType.Sound).GetComponent<AudioPoolObject>().Play(_lightClick);
    }
    public void MiddleClickSoundPlay()
    {
        PoolManager.Instance.Pop(PoolType.Sound).GetComponent<AudioPoolObject>().Play(_middleClick);
    }
    public void HardClickSoundPlay()
    {
        PoolManager.Instance.Pop(PoolType.Sound).GetComponent<AudioPoolObject>().Play(_HardClick);
    }
    #endregion
}
