using UnityEngine;
using System.Collections;
using UnityEditor;

public class AbstractMenuEditor : Editor
{
    [MenuItem("AbstractMenu/RebuildMenu")]
    public static void RebuildMenu()
    {
        GameObject[] selectedObjects = Selection.gameObjects;
        for (int i = 0; i < selectedObjects.Length; i++)
        {
            Control _control = selectedObjects[i].GetComponent<Control>();
            if (null != _control)
            {
                _control.ReBuildControlBehaviors();
            }
        }
    }


    [MenuItem("AbstractMenu/UpdateMenu")]
    public static void UpdateMenu()
    {
        GameObject[] selectedObjects = Selection.gameObjects;
        for (int i = 0; i < selectedObjects.Length; i++)
        {
            Control _control = selectedObjects[i].GetComponent<Control>();
            if (null != _control)
            {
                _control.UpdateControlBehaviors();
            }
        }
    }
}
