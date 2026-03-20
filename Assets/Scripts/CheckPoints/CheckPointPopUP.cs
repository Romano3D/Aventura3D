using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CheckpointPopupUI : MonoBehaviour
{
    public GameObject panel;
    public TMP_Text text;

    public float duration = 2f;

    private void Start()
    {
        Show(99);
    }
    public void Show(int checkpointNumber)
    {
        StopAllCoroutines();
        StartCoroutine(ShowRoutine(checkpointNumber));
    }

    private IEnumerator ShowRoutine(int number)
    {
        panel.SetActive(true);
        text.text = "Checkpoint " + number;

        yield return new WaitForSeconds(duration);

        panel.SetActive(false);
    }
}
