using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// this class provide a Timer for you to fire an event when a period of time passed
/// it also fire an event when each frame finishes
/// </summary>
/// 
[System.Serializable]
public class BobMethodTimer{
    public string methodname;
    public int ID;
    float time;
    float timer;
    bool TimeUp;

    public event EventHandler<BobEventArgs> BobTimeUpEventHandler;
    public event EventHandler<BobEventArgs> BobTimeUpdateEventHandler;
    public BobMethodTimer(string name, float time) {
        this.methodname = name;
        this.time = time;
        this.ID = BobMethodTimerManager.Instance.count++;
        this.timer = 0;
        this.TimeUp = false;
        BobMethodTimerManager.Instance.updateHander += this.UpdateTime;
    }
    public void Cancel(){
        BobMethodTimerManager.Instance.updateHander -= this.UpdateTime;
    }
    public void UpdateTime() {
        if (this.TimeUp)
            return;
        if (this.timer < this.time)
        {
            this.timer += Time.deltaTime;
            if (null != this.BobTimeUpdateEventHandler) { 
                this.BobTimeUpdateEventHandler(this, new BobEventArgs(this.methodname, "Update", this.timer/this.time, this.ID));
            }
        }
        else {
            this.TimeUp = true;
            BobMethodTimerManager.Instance.updateHander -= this.UpdateTime;
            if (null != this.BobTimeUpEventHandler) {
                //Debug.Log(string.Format("{0} is timed up", this.methodname));
                this.BobTimeUpEventHandler(this, new BobEventArgs(this.methodname, "End", 1f, this.ID));
            }
            this.BobTimeUpEventHandler = null;
            this.BobTimeUpdateEventHandler = null;
        }
    }
}
