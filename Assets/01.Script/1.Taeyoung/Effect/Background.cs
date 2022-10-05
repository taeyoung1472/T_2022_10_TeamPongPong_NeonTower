using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;

public class Background : MonoBehaviour
{
    [SerializeField] private float speed;
    private MeshRenderer meshRenderer;

    public void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    public void FloorChange()
    {
        Sequence seq = DOTween.Sequence();
        CameraManager.Instance.ZoomCamera(150f, 3f);
        seq.AppendInterval(3f);
        seq.AppendCallback(() =>
        {
            CameraManager.Instance.CameraShake(4f, 4f, 4f);
            FloorMove();
        });
    }

    private void FloorMove()
    {
        meshRenderer.material.mainTextureOffset = Vector2.zero;
        Sequence seq = DOTween.Sequence();
        seq.Append(DOTween.To(() => meshRenderer.material.mainTextureOffset, x => meshRenderer.material.mainTextureOffset = x, new Vector2(0, -30), 4f));
        seq.AppendCallback(() =>
        {
            CameraManager.Instance.ZoomCamera(60f, 1f);
        });
    }
}
