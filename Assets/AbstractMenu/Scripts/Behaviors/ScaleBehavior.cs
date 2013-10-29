using UnityEngine;
using System.Collections;

public class ScaleBehavior : ControlBehavior
{
    public Vector3 DstScalor = new Vector3(1,1,1);


    protected override void UpdateBehavior()
    {
        this.ControlTransform.localScale = Vector3.Lerp(this.ControlTransform.localScale, DstScalor, 0.1f);
        if (Vector3.Distance(this.ControlTransform.localScale, DstScalor) < 0.02f)
        {
            this.EndBehavior();
        }
    }    

}
