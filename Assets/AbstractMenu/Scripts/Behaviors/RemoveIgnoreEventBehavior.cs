using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RemoveIgnoreEventBehavior : ControlBehavior
{
    public List<ControlEventType> cancelIgnoredlist;

    protected override void UpdateBehavior()
    {
        this.ControlTransform.GetComponent<Control2>().removeIgnoreEvent(this.cancelIgnoredlist);
        this.EndBehavior();
    }    

}
