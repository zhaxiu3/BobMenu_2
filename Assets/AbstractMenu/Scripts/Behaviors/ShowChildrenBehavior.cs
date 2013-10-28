using UnityEngine;
using System.Collections;

public class ShowChildrenBehavior : ControlBehavior {


    protected override void UpdateBehavior()
    {
        base.UpdateBehavior();
        this.ControlTransform.GetComponent<Control2>().ShowChildren(true);
        this.EndBehavior();
    }
}
