using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class IgnoreEventBehavior : ControlBehavior
{
    public List<ControlEventType> ignoredlist;
    protected override void BeginBehavior()
    {
        base.BeginBehavior();
        this.ControlTransform.GetComponent<Control2>().IgnoreEvent(this.ignoredlist);
        this.EndBehavior();
    }
    protected override void UpdateBehavior()
    {
    }    

}
