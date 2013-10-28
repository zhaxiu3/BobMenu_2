using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
public class DurationInvoker : MonoBehaviour {
    /// <summary>
    /// ToDo:Consider the finite-time timer that do something until canceled
    /// </summary>
    public List<BobMethodTimer> methods = new List<BobMethodTimer>();
	
	public int StartaTimer(string methodname, float time) {
		BobMethodTimer mt = new BobMethodTimer(methodname, time);
        mt.BobTimeUpdateEventHandler += this.OnBobUpdateEvent;
        mt.BobTimeUpEventHandler += this.OnBobTimeUpEvent;
		this.methods.Add(mt);
        return mt.ID;
	}


    /// <summary>
    /// deal with every frame event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public void OnBobUpdateEvent(object sender, BobEventArgs e) {
        gameObject.SendMessage(e.name, e.process, SendMessageOptions.DontRequireReceiver);
    }

    /// <summary>
    /// this method seems do the same thing with the OnBobUpdateEvent except that it ends the timer
    /// //ToDo:merge the two methods
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public void OnBobTimeUpEvent(object sender, BobEventArgs e) {
        gameObject.SendMessage(e.name, e.process, SendMessageOptions.DontRequireReceiver);
        CancelaTimer(e.ID);
    }

    public bool CancelaTimer(int id)
    {
        for (int i = methods.Count - 1; i >= 0; --i)
        {
            if (id == methods[i].ID)
            {
                methods[i].Cancel();
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
}
