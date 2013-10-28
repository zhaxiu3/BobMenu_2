using UnityEngine;
using System.Collections;

public class ScaleBehavior : ControlBehavior
{
    public Vector3 DstScalor = new Vector3(1,1,1);

    protected override void calledWhenStart()
    {
        base.calledWhenStart();
        this.behaviorType = BehaviorType.Scale;
    }
    protected override void UpdateBehavior()
    {
        this.ControlTransform.localScale = Vector3.Lerp(this.transform.parent.localScale, DstScalor, .5f);
        if (Vector3.Distance(this.ControlTransform.localScale, DstScalor).CompareTo(0) == 0)
        {
            Finished = true;
        }
    }    

}
