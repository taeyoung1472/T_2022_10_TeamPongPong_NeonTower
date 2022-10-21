using System.Net.NetworkInformation;
using TMPro;
using UnityEditor;
using UnityEngine;

public class DangerEffect : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI dangerTmp;

    GameObject cam;

    private float time;
    private bool isInSector = false;

    private bool isDead = false;

    public void Awake()
    {
        cam = Camera.main.gameObject;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject != cam) return;
        time = 3;
        isInSector = true;
    }

    public void Update()
    {
        if (isInSector && !isDead)
        {
            dangerTmp.text = $"내려가십시오! {time:0.0}";
            time -= Time.deltaTime;
            if (time < 0)
            {
                isDead = true;
                Define.Instance.playerController.Dead();
            }
        }
        else
        {
            dangerTmp.text = "";
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject != cam) return;
        isInSector = false;
    }
}
