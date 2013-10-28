using UnityEngine;
using System.Collections;

public class UpBehavior : ControlBehavior {


    protected override void calledWhenStart()
    {
        base.calledWhenStart();
        this.behaviorType = BehaviorType.Up;
    }
    public Vector3 EndPosition;
    protected override void UpdateBehavior()
    {
        this.ControlTransform.Translate(0, 0.5f, 0);
        if (this.ControlTransform.position.y > EndPosition.y)
        {
            this.Finished = true;
        }
    }
}
