using UnityEngine;
using System.Collections;

public class GobackBehaviorv2 : ControlBehavior {
    protected override void calledWhenStart()
    {
        base.calledWhenStart();
        this.behaviorType = BehaviorType.GoBack;
    }
    protected override void UpdateBehavior()
    {
        base.UpdateBehavior();

    }
}
