using UnityEngine;
using System.Collections;

public class GotoNextBehaviorv2 : ControlBehavior {
    public Control2 nextControl = null;

    protected override void calledWhenStart()
    {
        base.calledWhenStart();
        this.behaviorType = BehaviorType.GotoNext;
    }

    protected override void UpdateBehavior()
    {
        base.UpdateBehavior();
        this.ControlTransform.parent.GetComponent<Control2>().OnClose(this, null);
        nextControl.OnLoad(this, null);
        Finished = true;
    }
}
