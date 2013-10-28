using UnityEngine;
using System.Collections;

public class HideChildrenBehavior : ControlBehavior
{
    protected override void calledWhenStart()
    {
        base.calledWhenStart();
        this.behaviorType = BehaviorType.HideChildren;
    }

    protected override void UpdateBehavior()
    {
        base.UpdateBehavior();
        this.ControlTransform.GetComponent<Control>().ShowChildren(false);
        Finished = true;
    }
}
