using System.Collections;
using UnityEngine;
using URPGlitch.Runtime.DigitalGlitch;
using URPGlitch.Runtime.AnalogGlitch;
using static DefineCamera;
using UnityEngine.Rendering;

namespace Glitch
{
    sealed class GlitchManager : MonoSingleTon<GlitchManager>
    {
        //[SerializeField] DigitalGlitchFeature _digitalGlitchFeature = default;
        //[SerializeField] AnalogGlitchFeature _analogGlitchFeature = default;

        [SerializeField] Volume volume;

        private DigitalGlitchVolume _digitalGlitchFeature;
        private AnalogGlitchVolume _analogGlitchFeature;

        [Header("Digital")]
        [SerializeField, Range(0f, 1f)] public float _intensity = default;

        [Header("Analog")]
        [SerializeField, Range(0f, 1f)] float _scanLineJitter = default;
        [SerializeField, Range(0f, 1f)] float _verticalJump = default;
        [SerializeField, Range(0f, 1f)] float _horizontalShake = default;
        [SerializeField, Range(0f, 1f)] float _colorDrift = default;

        public bool cantDoZero = false;


        public UnityEngine.Rendering.Universal.UniversalAdditionalCameraData additionalCameraData;
        private void Awake()
        {
            additionalCameraData =
                MainCam.transform.GetComponent<UnityEngine.Rendering.Universal.UniversalAdditionalCameraData>();
        }
        private void Start()
        {
            volume.profile.TryGet<DigitalGlitchVolume>(out _digitalGlitchFeature);
            volume.profile.TryGet<AnalogGlitchVolume>(out _analogGlitchFeature);
        }
        void Update()
        {
            
            _digitalGlitchFeature.intensity.value = _intensity;
        }
        public void ZeroValue()
        {
            if (cantDoZero) return;
            _digitalGlitchFeature.intensity.value = 0;

            _analogGlitchFeature.scanLineJitter.value = 0;
            _analogGlitchFeature.verticalJump.value = 0;
            _analogGlitchFeature.horizontalShake.value = 0;
            _analogGlitchFeature.colorDrift.value = 0;
        }
        public void StartSceneValue()
        {
            _digitalGlitchFeature.intensity.value = _intensity;

            _analogGlitchFeature.scanLineJitter.value = _scanLineJitter;
            _analogGlitchFeature.verticalJump.value = _verticalJump;
            _analogGlitchFeature.horizontalShake.value = _horizontalShake;
            _analogGlitchFeature.colorDrift.value = _colorDrift;
        }
        public void GraySceneValue()
        {
            _digitalGlitchFeature.intensity.value = _intensity;

            _analogGlitchFeature.scanLineJitter.value = _scanLineJitter;
            _analogGlitchFeature.verticalJump.value = _verticalJump;
            _analogGlitchFeature.horizontalShake.value = _horizontalShake;
            _analogGlitchFeature.colorDrift.value = _colorDrift;
        }
        public void LoadGameCutScene()
        {
            StartCoroutine(StartCutScene());
        }
        public void StartGameCutScene()
        {
            _intensity = 0.8f;
            StartCoroutine(GameStartCutScene());
        }

        IEnumerator GameStartCutScene()
        {
            cantDoZero = true;
            while (_intensity > 0.01f)
            {
                _intensity -= 0.05f;

                yield return new WaitForSeconds(0.05f);
            }
            cantDoZero = false;
            _intensity = 0.001f;
        }
        IEnumerator StartCutScene()
        {
            while (_intensity < 1f)
            {
                _intensity += 0.05f;

                yield return new WaitForSeconds(0.05f);
            }
            _intensity = 0.001f;
        }
        public void ChangeRenderModeOne()
        {
            additionalCameraData.SetRenderer(1);
        }
        public void ChangeRenderModeZero()
        {
            additionalCameraData.SetRenderer(0);
        }
    }
}
