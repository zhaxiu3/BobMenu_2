using UnityEngine;
using System.Collections;

public class ShowChildrenBehavior : ControlBehavior {
    protected override void calledWhenStart()
    {
        base.calledWhenStart();
        this.behaviorType = BehaviorType.ShowChildren;
    }

    protected override void UpdateBehavior()
    {
        base.UpdateBehavior();
        this.ControlTransform.GetComponent<Control2>().ShowChildren(true);
        Finished = true;
    }
}
