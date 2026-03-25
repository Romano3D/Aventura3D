using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Item
{

    public class ItemCollactableBase : MonoBehaviour
    {
        public ItemType itemType;

        public string compareTag = "Player";
        public ParticleSystem particleVFX;
        public float timToHide = 3;
        public GameObject graphicItem;

        public new Collider collider;

        [Header("Sounds")]
        public AudioSource audioSource;

        private void Awake()
        {
            // if (particleVFX != null) particleVFX.transform.SetParent(null);
        }

        private void OnTriggerEnter(Collider collision)
        {
            if (collision.transform.CompareTag(compareTag))
            {
                Collect();
            }
        }
        protected virtual void Collect()
        {
            if (collider != null) collider.enabled = false;
            if (graphicItem != null) graphicItem.SetActive(false);
            Invoke("HideObject", timToHide);
            gameObject.SetActive(false);
            OnCollect();
        }
        private void HideObject()
        {
            gameObject.SetActive(false);
        }
        protected virtual void OnCollect()
        {
            if (particleVFX != null) particleVFX.Play();
            if (audioSource != null) audioSource.Play();
            ItemManager.Instance.AddByType(itemType);
        }

    }
}
 


   





    

