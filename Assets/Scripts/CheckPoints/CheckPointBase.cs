using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointBase : MonoBehaviour
{
    public MeshRenderer meshRenderer;
    public int key = 01;

    private bool checkpointActived = false;
    private string checkpointkey = "Checkpointkey";

    private void OnTriggerEnter(Collider other)
    {
        if (!checkpointActived && other.transform.tag == "Player")

        {
            CheckCheckPoint();
        }
    }

    private void CheckCheckPoint()
    {
        TurnItOn();
        SaveCheckpoint();
    }
    [NaughtyAttributes.Button]
    private void TurnItOn()
    {
        meshRenderer.material.SetColor("_EmissionColor", Color.white);
        
    }
    private void TurnItOff()
    {
        meshRenderer.material.SetColor("_EmissionColor", Color.gray);
    }
    private void SaveCheckpoint()
    {
        /* if(PlayerPrefs.GetInt(checkpointkey, 0) > key)
             PlayerPrefs.SetInt(checkpointkey, key);*/

        CheckPointManager.Instance.SaveCheckPoint(key);

        checkpointActived = true;
    }
}
