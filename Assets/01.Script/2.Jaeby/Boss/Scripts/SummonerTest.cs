using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonerTest : MonoBehaviour
{
    [SerializeField]
    private GameObject _testBoss = null;
    [SerializeField]
    private AnimationClip _testClip = null;
    private int wave = 0;
    private int floor = 1;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            GameObject obj = Instantiate(_testBoss, new Vector3(-9.78f, 0, 9.25f), Quaternion.Euler(0f, 180f, 0f));
            CameraManager.Instance.TargetingBossCameraAnimation(obj.GetComponent<Boss>(), _testClip.length);
        }

        if (Input.GetKeyDown(KeyCode.Alpha9)
         )
        {
            wave++;
            if (wave % 4 == 0)
                floor++;
            WaveUIManager.Instance.WaveCount(floor, wave);
        }
    }
}
