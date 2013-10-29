using UnityEngine;
using System.Collections;

public class FollowScaleTargetBehavior : ControlBehavior
{
    public Transform target;

    protected override void UpdateBehavior()
    {
        this.ControlTransform.localScale = Vector3.Lerp(this.ControlTransform.localScale, target.localScale, 0.2f);
    }   
}
