using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Boss;

public class BossTrigger : MonoBehaviour
{
    public BossBase boss;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            boss.ActivateBoss();
        }
    }
}
