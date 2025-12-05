using System;
using UnityEngine;
using DG.Tweening;

namespace Player
{
    public class PlayerMoveAnimation: MonoBehaviour
    {
        [SerializeField] private GameObject _playerModel;
        private PlayerMove _playerMove;
        Tween walkTween;
        private void OnEnable()
        {
            _playerMove = GetComponent<PlayerMove>();
            _playerMove.OnMoveAnimation += StartWalking;

        }

        void StartWalking()
        {
            walkTween?.Kill();
            
            //walkTween = DOTween.Sequence().Append(_playerModel.transform.DOScaleY(1.2f, 0.4f)).Append(_playerModel.transform.DOScaleY(1f, 0.4f)).SetLoops(-1, LoopType.Yoyo); 
        }
        

        void StopWalking()
        {
            walkTween.Kill(); // Остановить анимацию
           // _playerModel.localScale = Vector3.one; // Вернуть нормальный размер
        }
        private void OnDisable()
        {
            
            _playerMove.OnMoveAnimation -= StartWalking;
        }
    }
}