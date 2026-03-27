using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using DG.Tweening;

public class ChestBase : MonoBehaviour
{
    public KeyCode keyCode = KeyCode.O;

    public Animator animator;
    public string triggerOpen = "Open";

    [Header("Notification")]
    public GameObject notification;
    public float tweeDuration = .2f;
    public Ease tweeEase = Ease.OutBack;
    private float startScale;

    private bool _chestOpened = false;

    [Space]
    public ChestItemBase chestItem;

    private void Start()
    {
        startScale = notification.transform.localScale.x;
        HideNotification();
    }

    [NaughtyAttributes.Button]
    private void OpenChest()
    {
        if (_chestOpened) return;

        animator.SetTrigger(triggerOpen);
        _chestOpened = true;
        HideNotification() ;
        Invoke(nameof(ShowItem), 1f);
    }

    private void ShowItem()
    {
        chestItem.ShowItem();
    }
    public void OnTriggerEnter(Collider other)
    {
        Player p = other.transform.GetComponent<Player>();
        if (p != null)
        {
            ShowNotification();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        Player p = other.transform.GetComponent<Player>();
        if (p != null)
        {
            HideNotification();
        }
    }
    [NaughtyAttributes.Button]
    private void ShowNotification()
    {
        notification.SetActive(true);
        notification.transform.localScale = Vector3.zero;
        notification.transform.DOScale(startScale, tweeDuration);
    }
    [NaughtyAttributes.Button]
    private void HideNotification()
    {
        notification.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(keyCode) && notification.activeSelf)
        {
            OpenChest();
        }
    }

}

  
 

