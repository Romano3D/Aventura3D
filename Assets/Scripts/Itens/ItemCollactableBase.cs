using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Item
{
    public class ItemCollactableBase : MonoBehaviour
    {
        public SFXType sfxType;
        public ItemType itemType;

        public string compareTag = "Player";
        public ParticleSystem particleVFX;
        public float timToHide = 3;
        public GameObject graphicItem;

        private Collider _collider;

        [Header("Sounds")]
        public AudioSource audioSource;

        private void Awake()
        {
            _collider = GetComponent<Collider>();
            // if (particleVFX != null) particleVFX.transform.SetParent(null);
        }

        private void OnTriggerEnter(Collider collision)
        {
            if (collision.transform.CompareTag(compareTag))
            {
                Collect();
            }
        }

        private void PlaySFX()
        {
            Debug.Log(SFXPool.Instance);
            SFXPool.Instance.Play(sfxType);
        }
        protected virtual void Collect()
        {
            PlaySFX();

            if (_collider != null) _collider.enabled = false;

            Rigidbody rb = GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = true;
                rb.detectCollisions = false;
            }

            if (graphicItem != null) graphicItem.SetActive(false);

            OnCollect();

            Invoke(nameof(HideObject), timToHide);
        }
        private void HideObject()
        {
            Invoke(nameof(HideObject), timToHide);
        }
        protected virtual void OnCollect()
        {
            if (particleVFX != null) particleVFX.Play();
            if (audioSource != null) audioSource.Play();
            ItemManager.Instance.AddByType(itemType);
        }

    }
}
 


   





    

