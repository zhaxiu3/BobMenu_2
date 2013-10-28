using UnityEngine;
using System.Collections;

public class DownBehavior : ControlBehavior
{
    // Use this for initialization
    protected override void calledWhenStart()
    {
        base.calledWhenStart();
        this.behaviorType = BehaviorType.Down;
    }
    protected override void UpdateBehavior()
    {
        this.ControlTransform.Translate(0, -1, 0);
    }
}
