using UnityEngine;
using System.Collections;

public class SetParentBehavior : ControlBehavior
{
    public Transform target;

    public override void BeginBehavior()
    {
        base.BeginBehavior();
        this.ControlTransform.parent = this.target;
        this.EndBehavior();

    }  

}
