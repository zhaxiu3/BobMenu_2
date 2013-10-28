using UnityEngine;
using System.Collections;

public class FollowTargetBehavior : ControlBehavior
{
    public Transform target;
    public Vector3 Offset;

    protected override void calledWhenStart()
    {
        base.calledWhenStart();
        this.behaviorType = BehaviorType.FollowTarget;
    }
    protected override void BeginBehavior()
    {
        base.BeginBehavior();
    }
    protected override void UpdateBehavior()
    {
        this.ControlTransform.position = Vector3.Lerp(this.ControlTransform.position, target.position+this.Offset, 0.2f);
    }   
}
