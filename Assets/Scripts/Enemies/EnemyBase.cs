using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Animation;


namespace Enemy
{
    public class EnemyBase : MonoBehaviour
    {


        public float startLife = 10f;

        [SerializeField] private float _currentLive;

        [Header("Animation")]
        [SerializeField] private AnimationBae _animationBae;


        [Header("Start Animation")]
        public float startAnimationDuration = .2f;
        public Ease startAnimationEase = Ease.OutBack;
        public bool startWithBornAnimation = true;

        private void Awake()
        {
            Init();
        }

        protected virtual void ResetLife()
        {
            _currentLive = startLife;
        }
        protected virtual void Init()
        {
            ResetLife();

            if(startWithBornAnimation)
                BornAnimation();
        }
        protected virtual void kill()
        {
            Onkilll();
        }
        
        protected virtual void Onkilll() 
        {
            Destroy(gameObject, 3f);
            PlayAnimationByTrigger(AnimationType.DEATH);
        }

        public void OnDamage(float f)
        {
            _currentLive -= f;
            
            if( _currentLive <= 0)
            {
                kill();
            }
        }

        #region ANIMATION
        private void BornAnimation()
        {
            transform.DOScale(0, startAnimationDuration).SetEase(startAnimationEase).From();
        }

        public void PlayAnimationByTrigger(AnimationType animationType)
        {
            _animationBae.PlayAnimationByTrigger(animationType);
        }
        #endregion

        //debug
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.T))
            {
                OnDamage(5f);
            }
        }
    }
}
