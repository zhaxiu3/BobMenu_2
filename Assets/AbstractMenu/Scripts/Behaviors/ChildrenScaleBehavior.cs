using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ChildrenScaleBehavior : ControlBehavior
{
    public List<Control2> children;
    public Vector3 targetscale;


    protected override void UpdateBehavior()
    {
        this.ControlTransform.localScale = Vector3.Lerp(this.ControlTransform.localScale, targetscale, 0.1f);
        for (int i = 0; i < children.Count; i++)
        {
            //Todo: posite the children
        }
        if (Vector3.Distance(this.ControlTransform.localScale, targetscale) < 0.02f)
        {
            this.ControlTransform.localScale = targetscale;
            this.EndBehavior();
        }
    }
}
