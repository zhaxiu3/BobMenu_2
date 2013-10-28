using UnityEngine;
using System.Collections;

public class HideChildrenBehavior : ControlBehavior
{


    protected override void UpdateBehavior()
    {
        base.UpdateBehavior();
        this.ControlTransform.GetComponent<Control>().ShowChildren(false);
        this.EndBehavior();
    }
}
