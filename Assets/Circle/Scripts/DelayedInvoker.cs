using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

/// <summary>
/// this class provide you a class helps you
/// call a method after a period of time
/// 
/// usage: add a script with a method to be called
/// then use StartaTimer(methodname, delayedtime)
/// </summary>
public class DelayedInvoker : MonoBehaviour
{

    public event EventHandler<BobEventArgs> BobEventHandler; 
    public List<BobMethodTimer> methods = new List<BobMethodTimer>();

    public void StartaTimer(string methodname, float time) {
        BobMethodTimer mt = new BobMethodTimer(methodname, time);
        mt.BobTimeUpEventHandler += this.BobEventHandler;
        this.methods.Add(mt);
    }

    /// <summary>
    /// called when time is up
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e">a BobEventArgs with a method name to be called</param>
    public void OnBobTimeUpEvent(object sender, BobEventArgs e) {
        gameObject.SendMessage(e.name, null, SendMessageOptions.DontRequireReceiver);
        CancelaTimer(e.ID);
    }
    public bool CancelaTimer(int id) {
        for(int i = methods.Count-1; i>=0; --i){
            if (id == methods[i].ID) {
                methods[i].Cancel();
                methods[i] = null;
                methods.RemoveAt(i);
                return true;
            }
        }
        return false;
    }

    public bool CancelTimer(string methodname)
    {
        for (int i = methods.Count - 1; i >= 0; --i)
        {
            if (methodname == methods[i].methodname)
            {
                methods[i].Cancel();
                methods.RemoveAt(i);
                return true;
            }
        }
        return false;
    }

    public void ClearTimers()
    {
        for (int i = methods.Count - 1; i >= 0; --i)
        {
            methods[i].Cancel();
            methods.RemoveAt(i);
        }
    }
    void Awake()
    {
        this.BobEventHandler = this.OnBobTimeUpEvent;
    }
	
}
