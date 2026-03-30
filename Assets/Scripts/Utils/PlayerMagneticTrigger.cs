using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Item;

public class PlayerMagneticTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        ItemCollactableBase i = other.transform.GetComponent<ItemCollactableBase>();
        if(i != null)
        {
            i.gameObject.AddComponent<Magnetic>();
        }
    }
}
