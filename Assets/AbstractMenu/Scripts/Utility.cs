using UnityEngine;
using System.Collections;
using System;
using System.Reflection;

public class Utility : MonoBehaviour {
    public static string[] getMethodsName(Type atype)
    {
        MethodInfo[] methods = atype.GetMethods();
        string[] names = new string[methods.Length];
        for (int i = 0; i < methods.Length; i++)
        {
            Debug.Log(methods[i].Name);
            names[i] = methods[i].Name;
        }
        return names;
    }
}
