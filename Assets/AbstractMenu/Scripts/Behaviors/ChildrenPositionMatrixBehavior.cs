using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ChildrenPositionMatrixBehavior : ControlBehavior
{
    public bool show;
    public bool inWorld;
    public Vector3 DestPosition = Vector3.zero;
    //public List<Control2> children;
    //public int col;
    //public int row;
    //public float distance = 0.5f;
    protected override void calledWhenStart()
    {
        base.calledWhenStart();

        this.behaviorType = BehaviorType.ChildrenPosition;
        //children = this.ControlTransform.GetComponent<Control2>().items;
        //col = (int)Mathf.Sqrt(children.Count);
        //row = children.Count % col + children.Count / col;
    }

    protected override void UpdateBehavior()
    {
        if (inWorld)
        {
            this.ControlTransform.position = Vector3.Lerp(this.ControlTransform.position,this.DestPosition,0.1f);
            //for (int i = 0; i < children.Count; i++) {
            //    //Todo: posite the children
            //}
            if(Vector3.Distance(this.ControlTransform.position, this.DestPosition)< 0.2f){
                this.ControlTransform.position = this.DestPosition;
                this.EndBehavior();
            }
        }
        else
        {
            this.ControlTransform.localPosition = Vector3.Lerp(this.ControlTransform.localPosition, this.DestPosition, 0.1f);

            if (Vector3.Distance(this.ControlTransform.localPosition, this.DestPosition) < 0.2f)
            {
                this.ControlTransform.localPosition = this.DestPosition;
                this.EndBehavior();
            }
        }
    }
}
