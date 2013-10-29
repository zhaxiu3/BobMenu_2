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

    [MenuItem("AbstractMenuv2/CircleLocation")]
    public static void CircleLocation()
    {
        GameObject selectedObject = Selection.activeGameObject;
        Control2 _control = selectedObject.GetComponent<Control2>();
        for (int i = 0; i < _control.Trans.Count; i++ )
        {
            _control.Trans[i].localPosition = Quaternion.Euler(0, 360.0f / (float)_control.Trans.Count*i, 0)*Vector3.forward*4.0f;
            _control.Trans[i].localRotation = Quaternion.Euler(0, 360.0f / (float)_control.Trans.Count*i - 180, 0);
        }
    }

    [MenuItem("AbstractMenuv2/Batch Rename")]
public static void BatchRename() 
	{ // Renames all selected items in the Hierarchy to the first item selected, with numbers.
	string iname;
	string ispacer = "0"; 
    int icount = 0; 
	iname = Selection.activeGameObject.name;   // The item in the inspector
	
	int istuff = Selection.gameObjects.Length;  // if I wanted this to support renaming of > 99 objects correctly, I'd use this. 
	
	foreach (GameObject igo in Selection.gameObjects)
		{
		icount ++; 
		if (icount > 9) ispacer = ""; 
		igo.name =iname + "-" + ispacer + icount; 
		}
	}
}
