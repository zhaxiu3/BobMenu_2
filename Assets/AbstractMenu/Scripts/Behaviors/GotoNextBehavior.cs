using UnityEngine;
using System.Collections;

public class GotoNextBehavior : ControlBehavior {
    public Control nextControl = null;

    protected override void calledWhenStart()
    {
        base.calledWhenStart();
        this.behaviorType = BehaviorType.GotoNext;
    }

    protected override void UpdateBehavior()
    {
        base.UpdateBehavior();
        this.ControlTransform.parent.GetComponent<Control>().ShowChildren(false);
        nextControl.ShowChildren(true);
        Finished = true;
    }
}
