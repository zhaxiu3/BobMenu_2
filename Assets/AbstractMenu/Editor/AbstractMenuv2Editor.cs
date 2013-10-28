using UnityEngine;
using System.Collections;
using UnityEditor;

public class AbstractMenuv2Editor : Editor
{
    [MenuItem("AbstractMenuv2/RebuildMenu")]
    public static void RebuildMenu()
    {
        GameObject[] selectedObjects = Selection.gameObjects;
        for (int i = 0; i < selectedObjects.Length; i++)
        {
            Control2 _control = selectedObjects[i].GetComponent<Control2>();
            if (null != _control)
            {
                _control.ReBuildControlBehaviors();
            }
        }
    }


    [MenuItem("AbstractMenuv2/UpdateMenu")]
    public static void UpdateMenu()
    {
        GameObject[] selectedObjects = Selection.gameObjects;
        for (int i = 0; i < selectedObjects.Length; i++)
        {
            Control2 _control = selectedObjects[i].GetComponent<Control2>();
            if (null != _control)
            {
                _control.UpdateControlBehaviors();
            }
        }
    }
}
