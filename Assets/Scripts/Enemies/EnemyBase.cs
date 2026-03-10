using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Enemy
{
    public class EnemyBase : MonoBehaviour
    {
        public float startLife = 10f;

        [SerializeField] private float _currentLive;

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
        }
        protected virtual void kill()
        {
            Onkilll();
        }
        
        protected virtual void Onkilll() 
        {
            Destroy(gameObject);
        }

        public void OnDamage(float f)
        {
            _currentLive -= f;
            
            if( _currentLive <= 0)
            {
                kill();
            }
        }

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
