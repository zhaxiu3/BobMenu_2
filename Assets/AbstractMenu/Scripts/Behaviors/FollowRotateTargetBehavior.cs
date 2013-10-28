using UnityEngine;
using System.Collections;

public class FollowRotateTargetBehavior : ControlBehavior
{
    public Transform target;

    protected override void calledWhenStart()
    {
        base.calledWhenStart();
        this.behaviorType = BehaviorType.FollowRotateTarget;
    }
    protected override void BeginBehavior()
    {
        base.BeginBehavior();
    }
    protected override void UpdateBehavior()
    {
        this.ControlTransform.rotation = Quaternion.Lerp(this.ControlTransform.rotation, target.rotation, 0.2f);
        Debug.Log(target.rotation.eulerAngles);
        Debug.Log(this.ControlTransform.rotation.eulerAngles);
    }   
}
