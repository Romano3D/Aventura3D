using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Animation;
using Unity.VisualScripting;


namespace Enemy
{
    public class EnemyBase : MonoBehaviour, IDamageable
    {

        public Collider _collider;
        public FlashColor flashColor;
        public ParticleSystem _particleSystem;

        public float startLife = 10f;
        public bool lookAtPlayer = false;

        [SerializeField] private float _currentLive;

        [Header("Animation")]
        [SerializeField] private AnimationBae _animationBae;


        [Header("Start Animation")]
        public float startAnimationDuration = .2f;
        public Ease startAnimationEase = Ease.OutBack;
        public bool startWithBornAnimation = true;

        private Player _player;



        private void Awake()
        {
            Init();
        }

        private void Start()
        {
            _player = GameObject.FindObjectOfType<Player>();
        }

        protected virtual void ResetLife()
        {
            _currentLive = startLife;
        }
        protected virtual void Init()
        {
            ResetLife();

            if (startWithBornAnimation)
                BornAnimation();
        }
        protected virtual void Kill()
        {
            Onkilll();
        }

        protected virtual void Onkilll()
        {
            if (_collider != null) _collider.enabled = false;
            Destroy(gameObject, 3f);
            PlayAnimationByTrigger(AnimationType.DEATH);
        }

        public void OnDamage(float f)
        {
            if (flashColor != null) flashColor.Flash();
            if (_particleSystem != null) _particleSystem.Emit(15);

            transform.position -= transform.forward;

            _currentLive -= f;

            if (_currentLive <= 0)
            {
                Kill();
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

        public void Damage(float damage)
        {
            Debug.Log("Damage");
            OnDamage(damage);
        }
        public void Damage(float damage, Vector3 dir)
        {
            OnDamage(damage);
            transform.DOMove(transform.position - dir, .1f);
        }

        private void OnCollisionEnter(Collision collision)
        {
            Player p = collision.transform.GetComponent<Player>();

            if (p != null)

            {
                p.healthBase.Damage(1);
            }
        }

        public virtual void Update()
        {
            if (lookAtPlayer)
            {
                transform.LookAt(_player.transform.position);
            }
        }
    }
}
