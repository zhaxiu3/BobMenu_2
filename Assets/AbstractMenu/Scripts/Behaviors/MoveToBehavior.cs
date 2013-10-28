using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MoveToBehavior : ControlBehavior
{
    public bool show;
    public bool inWorld;
    public Vector3 DestPosition = Vector3.zero;


    protected override void UpdateBehavior()
    {
        if (inWorld)
        {
            this.ControlTransform.position = Vector3.Lerp(this.ControlTransform.position,this.DestPosition,0.1f);
            if (Vector3.Distance(this.ControlTransform.position, this.DestPosition) < 0.02f)
            {
                this.ControlTransform.position = this.DestPosition;
                this.EndBehavior();
            }
        }
        else
        {
            this.ControlTransform.localPosition = Vector3.Lerp(this.ControlTransform.localPosition, this.DestPosition, 0.1f);

            if (Vector3.Distance(this.ControlTransform.localPosition, this.DestPosition) < 0.02f)
            {
                this.ControlTransform.localPosition = this.DestPosition;
                this.EndBehavior();
            }
        }
    }
}
