using UnityEngine;
using System.Collections;

public class GotoNextBehaviorv2 : ControlBehavior {
    public Control2 nextControl = null;



    protected override void UpdateBehavior()
    {
        base.UpdateBehavior();
        this.ControlTransform.parent.GetComponent<Control2>().OnClose(this, null);
        nextControl.OnLoad(this, null); 
        this.EndBehavior();
    }
}
