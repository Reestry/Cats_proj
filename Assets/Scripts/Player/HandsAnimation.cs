using DG.Tweening;
using UnityEngine;

public class HandsAnimation : MonoBehaviour
{
    [SerializeField] private GameObject _leftHand;
    [SerializeField] private GameObject _rightHand;

    [SerializeField] private float amplitude = 0.5f;   // Насколько далеко рука качается
    [SerializeField] private float duration = 0.4f;    // Время одного полушага

    private Tween _leftTween;
    private Tween _rightTween;

    public void Shake()
    {

        if (_leftTween != null && _leftTween.IsActive()) return;
        
        _leftTween = _leftHand.transform.DOLocalMoveZ(amplitude, duration)
            .SetLoops(-1, LoopType.Yoyo)
            .SetEase(Ease.InOutSine);


        _rightTween = _rightHand.transform.DOLocalMoveZ(-amplitude, duration)
            .SetLoops(-1, LoopType.Yoyo)
            .SetEase(Ease.InOutSine);
    }

    public void StopShake()
    {
        _leftTween?.Kill();
        _rightTween?.Kill();
        _leftTween = null;
        _rightTween = null;

        // Возвращаем руки в исходное положение
        _leftHand.transform.localPosition = Vector3.zero;
        _rightHand.transform.localPosition = Vector3.zero;
    }
}