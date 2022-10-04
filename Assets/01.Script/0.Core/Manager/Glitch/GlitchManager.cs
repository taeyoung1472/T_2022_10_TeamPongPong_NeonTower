using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal.Glitch;
using static DefineCamera;


namespace Glitch
{
    sealed class GlitchManager : MonoSingleTon<GlitchManager>
    {
        [SerializeField] DigitalGlitchFeature _digitalGlitchFeature = default;
        [SerializeField] AnalogGlitchFeature _analogGlitchFeature = default;

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
        void Update()
        {
            _digitalGlitchFeature.Intensity = _intensity;
        }
        public void ZeroValue()
        {
            if (cantDoZero) return;
            _digitalGlitchFeature.Intensity = 0;

            _analogGlitchFeature.ScanLineJitter = 0;
            _analogGlitchFeature.VerticalJump = 0;
            _analogGlitchFeature.HorizontalShake = 0;
            _analogGlitchFeature.ColorDrift = 0;
        }
        public void StartSceneValue()
        {
            _digitalGlitchFeature.Intensity = _intensity;

            _analogGlitchFeature.ScanLineJitter = _scanLineJitter;
            _analogGlitchFeature.VerticalJump = _verticalJump;
            _analogGlitchFeature.HorizontalShake = _horizontalShake;
            _analogGlitchFeature.ColorDrift = _colorDrift;
        }
        public void GraySceneValue()
        {
            _digitalGlitchFeature.Intensity = _intensity;

            _analogGlitchFeature.ScanLineJitter = _scanLineJitter;
            _analogGlitchFeature.VerticalJump = _verticalJump;
            _analogGlitchFeature.HorizontalShake = _horizontalShake;
            _analogGlitchFeature.ColorDrift = _colorDrift;
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
