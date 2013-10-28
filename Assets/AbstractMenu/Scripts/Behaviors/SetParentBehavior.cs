using UnityEngine;
using System.Collections;

public class SetParentBehavior : ControlBehavior
{
    public Transform target;

    protected override void UpdateBehavior()
    {
        this.ControlTransform.parent = this.target;
        this.EndBehavior();
    }    

}
