using System.Collections;
using UnityEngine;
using URPGlitch.Runtime.DigitalGlitch;
using URPGlitch.Runtime.AnalogGlitch;
using static DefineCamera;
using UnityEngine.Rendering;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

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
        [SerializeField, Range(0f, 1f)] public float _scanLineJitter = default;
        [SerializeField, Range(0f, 1f)] public float _verticalJump = default;
        [SerializeField, Range(0f, 1f)] public float _horizontalShake = default;
        [SerializeField, Range(0f, 1f)] public float _colorDrift = default;

        public Image fadeOutImage = null;

        public GameObject gameUi = null;

        public GameObject noClickPanel = null;
        private void Awake()
        {
            
        }
        private void Start()
        {
            volume.profile.TryGet<DigitalGlitchVolume>(out _digitalGlitchFeature);
            volume.profile.TryGet<AnalogGlitchVolume>(out _analogGlitchFeature);

            Scene scene = SceneManager.GetActiveScene();

            if(scene.name == "Game")
            {
                Debug.Log("Game¾À ÀüÈ¯");
                gameUi?.SetActive(false);
                StartGameCutScene();
            }
            else if(scene.name == "Game 1")
            {
                Debug.Log("Game¾À ÀüÈ¯");
                gameUi?.SetActive(false);
                StartGameCutScene();
            }
            else if(scene.name == "Jinwo")
            {
                ZeroValue();
            }
            else if(scene.name == "Menu")
            {
                StartCoroutine(MenuCutScene());
            }

        }
        void Update()
        {

            //_digitalGlitchFeature.intensity.value = _intensity;

            _digitalGlitchFeature.intensity.value = _intensity;

            _analogGlitchFeature.scanLineJitter.value = _scanLineJitter;
            _analogGlitchFeature.verticalJump.value = _verticalJump;
            _analogGlitchFeature.horizontalShake.value = _horizontalShake;
            _analogGlitchFeature.colorDrift.value = _colorDrift;


        }
        public void ZeroValue()
        {
            _intensity = 0;

            _scanLineJitter = 0;
            _verticalJump = 0;
            _horizontalShake = 0;
            _colorDrift = 0;
        }
        public void StartSceneValue()
        {
            _intensity = 0f;

            _scanLineJitter = 0.05f;
            _verticalJump = 0.01f;
            _horizontalShake = 0.007f;
            _colorDrift = 0.007f;
        }
        public void HitValue()
        {
            StartCoroutine(HitCoroutine());
        }
        public void OtherValue()
        {
            _intensity = 0.01f;

            //_scanLineJitter = 0.1f;
            //_verticalJump = 0.1f;
            //_colorDrift = 0.1f;
        }
        public void GrayValue()
        {
            _intensity = 0.03f;

            _scanLineJitter = 0.025f;
            _verticalJump = 0.025f;
            _horizontalShake = 0.025f;
            _colorDrift = 0.025f;
        }
        public void LoadGameCutScene()
        {
            StartCoroutine(StartCutScene());
        }
        public void StartGameCutScene()
        {
            _intensity = 0.8f;
            _scanLineJitter = 0.8f;
            _verticalJump = 0.8f;
            _horizontalShake = 0.8f;
            _colorDrift = 0.8f;
            StartCoroutine(GameStartCutScene());
        }
        IEnumerator MenuCutScene()
        {
            noClickPanel.SetActive(true);
            _intensity = 0;
            _scanLineJitter = 1f;
            _verticalJump = 1f;
            _horizontalShake = 1f;
            _colorDrift = 1f;

            yield return new WaitForSeconds(1.5f);

            while (true)
            {
                if (_verticalJump <= 0f)
                    break;
                _verticalJump -= 0.02f;
                _horizontalShake -= 0.02f;
                yield return new WaitForSeconds(0.02f);
            }

            yield return new WaitForSeconds(0.5f);

            while (true)
            {
                if (_scanLineJitter <= 0f)
                    break;
                _scanLineJitter -= 0.02f;
                _colorDrift -= 0.02f;
                yield return new WaitForSeconds(0.01f);
            }
            noClickPanel.SetActive(false);
            StartSceneValue();

        }
        IEnumerator HitCoroutine()
        {
            _intensity = 0.05f;

            _scanLineJitter = 0.1f;
            _horizontalShake = 0.1f;
            _colorDrift = 0.1f;
            yield return new WaitForSeconds(0.3f);
            ZeroValue();
        }
        IEnumerator GameStartCutScene()
        {
            while (_intensity > 0.01f)
            {
                //if(_intensity < 0.4f)
                //{

                //}
                fadeOutImage?.DOFade(0f, 3f);
                _intensity -= 0.02f;

                _scanLineJitter -= 0.02f;
                _verticalJump -= 0.02f;
                _horizontalShake -= 0.02f;
                _colorDrift -= 0.02f;

                yield return new WaitForSeconds(0.05f);
            }
            gameUi?.SetActive(true);
            //ZeroValue();
        }
        IEnumerator StartCutScene()
        {
            while (_intensity < 1f)
            {
                if(_scanLineJitter > 0.3f)
                {
                    _intensity += 0.05f;
                }

                _scanLineJitter += 0.05f;
                _verticalJump += 0.05f;
                _horizontalShake += 0.05f;
                _colorDrift += 0.05f;

                yield return new WaitForSeconds(0.05f);
            }
            //_intensity = 0.001f;
        }
        
    }
}