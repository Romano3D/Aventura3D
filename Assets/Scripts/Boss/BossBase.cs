using DG.Tweening;
using Ebac.StateMachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;


namespace Boss
{

    public enum BossAction
    {
        INIT,
        IDLE,
        WALK,
        ATTACK,
        DEATH
    }


    public class BossBase : MonoBehaviour
    {
        [Header("Animation")]
        public float startAnimationDuration = .5f;
        public Ease startAnimationEsase = Ease.OutBack;
        public bool lookAtPlayer = false;

        private bool _isActive = false;

        private Player _player;

        [Header("Attack")]
        public int attackAmount = 5;
        public float timeBetweenAttacks = .5f;

        private StateMachine<BossAction> stateMachine;

        public float speed = 5f;
        public List<Transform> waypoints;

        public HealthBase healthBase;

        public void ActivateBoss()
        {
            _isActive = true;

            // Inicia animação inicial
            StartInitAnimation();

            // Começa pelo estado inicial
            SwichtState(BossAction.INIT);

            //Debug.Log("Boss ativado!");
        }

        private void Awake()
        {
            {
                Init();
                _player = FindObjectOfType<Player>();

               // Debug.Log(_player == null ? "Player NULL" : "Player OK");

                healthBase.OnKill += OnBossKill;
            }
        }

        private void Init()
        {
            stateMachine = new StateMachine<BossAction>();
            stateMachine.Init();

            stateMachine.RegisterStates(BossAction.INIT, new BossStateInit());
            stateMachine.RegisterStates(BossAction.WALK, new BossStateWalk());
            stateMachine.RegisterStates(BossAction.ATTACK, new BossStateAttack());
            stateMachine.RegisterStates(BossAction.DEATH, new BossStateDeath());
        }

        private void OnBossKill(HealthBase h)
        {
            SwichtState(BossAction.DEATH);
        }

        #region ATTACK
        public void StartAttack(Action endCallback = null)
        {
            StartCoroutine(AttackCoroutine(endCallback));
        }

        IEnumerator AttackCoroutine(Action endCallback)
        {
            int attacks = 0;
            while (attacks < attackAmount)
            {
                attacks++;
                transform.DOScale(1.1f, .1f).SetLoops(2, LoopType.Yoyo);
                yield return new WaitForSeconds(timeBetweenAttacks);
            }

            endCallback?.Invoke();
        }
        #endregion

        #region WALK
        public void GoToRandomPoint(Action onArrive = null)
        {
            StartCoroutine(GoToPointCoroutine(waypoints[UnityEngine.Random.Range(0, waypoints.Count)], onArrive));
        }

        IEnumerator GoToPointCoroutine(Transform t, Action onArrive = null)
        {
            while (Vector3.Distance(transform.position, t.position) > 1f)
            {
                transform.position = Vector3.MoveTowards(transform.position, t.position, Time.deltaTime * speed);
                yield return new WaitForEndOfFrame();
            }
            onArrive?.Invoke();
        }

        #endregion

        #region ANIMATION
        public void StartInitAnimation()
        {
            transform.localScale = Vector3.zero;
            transform.DOScale(1f, startAnimationDuration).SetEase(startAnimationEsase);
        }
        #endregion

        #region DEBUG
        [NaughtyAttributes.Button]
        private void SwichtInit()
        {
            SwichtState(BossAction.INIT);
        }
        [NaughtyAttributes.Button]
        private void SwichtWalk()
        {
            SwichtState(BossAction.WALK);
        }
        [NaughtyAttributes.Button]
        private void SwichtAttack()
        {
            SwichtState(BossAction.ATTACK);
        }
        #endregion

        #region STATE MACHINE
        public void SwichtState(BossAction state)
        {
            stateMachine.SwitchState(state, this);
        }
        #endregion

        public virtual void Update()
        {
            if (!_isActive) return;

            if (lookAtPlayer)
            {
                transform.LookAt(_player.transform.position);
            }
        }
    }
}
