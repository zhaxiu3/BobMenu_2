using UnityEngine;
using System.Collections;

public class FollowRotateTargetBehavior : ControlBehavior
{
    public Transform target;


    public override void BeginBehavior()
    {
        base.BeginBehavior();
    }
    protected override void UpdateBehavior()
    {
        this.ControlTransform.rotation = Quaternion.Lerp(this.ControlTransform.rotation, target.rotation, 0.2f);
    }   
}
