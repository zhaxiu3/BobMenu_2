using UnityEngine;
using System.Collections;

public class SetPArentBehavior : ControlBehavior
{
    public Transform target;
    protected override void calledWhenStart()
    {
        base.calledWhenStart();
        this.behaviorType = BehaviorType.ChangeParent;
    }
    protected override void UpdateBehavior()
    {
        this.ControlTransform.parent = this.target;
        this.EndBehavior();
    }    

}
