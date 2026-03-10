using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunBase : MonoBehaviour
{
        public ProjectileBase prefabProjectlie;

        public Transform positionToShoot;
        public float timeBetweenShoot = 3f;
        public float speed = 50f;

        private Coroutine _currentCoroutine;

        protected virtual IEnumerator ShootCoroutine()
        {
            while (true)
            {
                Shoot();
                yield return new WaitForSeconds(timeBetweenShoot);
            }
        }

        public virtual void Shoot()
        {
            var projectile = Instantiate(prefabProjectlie);
            projectile.transform.position = positionToShoot.position;
            projectile.transform.rotation = positionToShoot.rotation;
            projectile.speed = speed;
    }

    public void StartShoot()
    {
        CancelShoot();
        _currentCoroutine = StartCoroutine(ShootCoroutine());
    }

    public void CancelShoot()
    {
        if (_currentCoroutine != null)
            StopCoroutine(_currentCoroutine);
    }
    }