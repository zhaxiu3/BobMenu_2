using UnityEngine;
using System.Collections;

public class RotateBehavior : ControlBehavior
{
    public bool rotateby = true;
    public bool inWorld;

    public Vector3 DifferEular = new Vector3(1,1,1);
    public Vector3 TargetEuler;
    public float speed = 0.5f;
    protected override void calledWhenStart()
    {
        base.calledWhenStart();
        this.behaviorType = BehaviorType.Rotate;
    }
    protected override void BeginBehavior()
    {
        if (null != this.ControlTransform)
        {
            if (rotateby)
            {
                if (inWorld) {
                    TargetEuler = (this.ControlTransform.rotation * Quaternion.Euler(DifferEular)).eulerAngles;
                }
                else
                {
                    TargetEuler = (this.ControlTransform.localRotation * Quaternion.Euler(DifferEular)).eulerAngles;
                }
            }
        }
    }
    protected override void UpdateBehavior()
    {
        Debug.Log(this.GetType().Name);
        if (inWorld)
        {
            this.ControlTransform.rotation = Quaternion.Lerp(this.ControlTransform.rotation, Quaternion.Euler(TargetEuler), speed);
            if (Vector3.Distance(this.ControlTransform.rotation.eulerAngles, TargetEuler) < 0.02f)
            {
                this.ControlTransform.rotation = Quaternion.Euler(TargetEuler);
                this.EndBehavior();
            }
        }
        else
        {
            this.ControlTransform.localRotation = Quaternion.Lerp(this.ControlTransform.localRotation, Quaternion.Euler(TargetEuler), speed);
            if (Vector3.Distance(this.ControlTransform.localRotation.eulerAngles, TargetEuler) < 0.02f)
            {
                this.ControlTransform.localRotation = Quaternion.Euler(TargetEuler);
                this.EndBehavior();
            }
        }
    }    

}
