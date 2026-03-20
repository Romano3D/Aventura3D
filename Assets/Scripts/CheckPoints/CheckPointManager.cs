using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ebac.Core.Singleton;
using UnityEditor.Purchasing;
using UnityEngine.UI;


public class CheckPointManager : Singleton<CheckPointManager>
{
    public int lastCheckPointKey = 0;

    public List<CheckPointBase> checkPoints;

    public CheckpointPopupUI popupUI;

    public bool HasCheckPoint()
    {
        return lastCheckPointKey > 0;
    }

      public void SaveCheckPoint(int i)
      {
          if (i > lastCheckPointKey)
          {
              lastCheckPointKey = i;
          }

          if (popupUI != null)
          {
              popupUI.Show(i);
          }
      }

    public Vector3 GetPositionFromLastCheckpoint()
    {
        var checkpoint = checkPoints.Find(i => i.key == lastCheckPointKey);
        return checkpoint.transform.position;
    }
}
