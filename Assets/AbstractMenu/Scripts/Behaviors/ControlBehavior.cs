using UnityEngine;
using System.Collections;
using System.Reflection;
using System;


public enum BehaviorType
{
    None,
    Up,
    Down,
    FollowMouse,
    Scale,
    ShowChildren,
    HideChildren,
    GotoNext,
    GoBack,
    MoveTo,
    FollowRotateTarget, 
    FollowScaleTarget,
    ChildrenScale,
    FollowTarget,
    Rotate,
    SetParent,
    IgnoreEvent,
    RemoveIgnoreEvent
}

public class ControlBehavior : MonoBehaviour {

    public bool Finished = false;
    public BehaviorType behaviorType;
    public Transform ControlTransform;
    public float delay = 0;
    public float timer = 0;


    void Awake()
    {
        this.ControlTransform = this.transform.parent;
    }
	// Update is called once per frame
	void Update () {
        this.timer += Time.deltaTime;
        if (this.timer < this.delay) {
            return;
        }

        UpdateBehavior();
        if (Finished) {
            this.EndBehavior();
        }
	}

    public virtual void BeginBehavior() {
        if (this.ControlTransform.GetComponent<Control2>().avticeBehaviorList.Contains(this))
            return;
        this.enabled = true;
        this.ControlTransform.GetComponent<Control2>().avticeBehaviorList.Add(this);
    }
    protected virtual void UpdateBehavior() { 
    }
    public virtual void EndBehavior()
    {
        this.ControlTransform.GetComponent<Control2>().avticeBehaviorList.Remove(this);
        ControlTransform.GetComponent<Control2>().OnBehaviorFinished(this, null);
        this.Finished = false;
        this.timer = 0;
        this.enabled = false;
    }
    public static int count = 0;

    public static ControlBehavior AddControlBehavior(Control2 control, ControlEventType ctrleventtype, BehaviorType type)
    {
        if (type == BehaviorType.None)
            return null;
        ControlBehavior _controlbehavior = null;
        GameObject _obj = new GameObject();
        _obj.name = control.name + "_"+ctrleventtype.ToString()+"_"+type.ToString() + (count++);
        _obj.transform.parent = control.transform;
        switch (type)
        {
            case BehaviorType.FollowMouse:
                //_controlbehavior = _obj.AddComponent<FollowMouseBehavior>();
                break;
            case BehaviorType.Scale:
                _controlbehavior = _obj.AddComponent<ScaleBehavior>();
                break;
            case BehaviorType.ShowChildren:
                _controlbehavior = _obj.AddComponent<ShowChildrenBehavior>();
                break;
            case BehaviorType.HideChildren:
                _controlbehavior = _obj.AddComponent<HideChildrenBehavior>();
                break;
            case BehaviorType.GotoNext:
                _controlbehavior = _obj.AddComponent<GotoNextBehaviorv2>();
                break;
            case BehaviorType.GoBack:
                _controlbehavior = _obj.AddComponent<GobackBehaviorv2>();
                break;
            case BehaviorType.MoveTo:
                _controlbehavior = _obj.AddComponent<MoveToBehavior>();
                break;
            case BehaviorType.FollowRotateTarget:
                _controlbehavior = _obj.AddComponent<FollowRotateTargetBehavior>();
                break;
            case BehaviorType.FollowScaleTarget:
                _controlbehavior = _obj.AddComponent<FollowScaleTargetBehavior>();
                break;
            case BehaviorType.ChildrenScale:
                _controlbehavior = _obj.AddComponent<ChildrenScaleBehavior>();
                break;
            case BehaviorType.FollowTarget:
                _controlbehavior = _obj.AddComponent<FollowTargetBehavior>();
                break;
            case BehaviorType.Rotate:
                _controlbehavior = _obj.AddComponent<RotateBehavior>();
                break;
            case BehaviorType.SetParent:
                _controlbehavior = _obj.AddComponent<SetParentBehavior>();
                break;
            case BehaviorType.IgnoreEvent:
                _controlbehavior = _obj.AddComponent<IgnoreEventBehavior>();
                break;
            case BehaviorType.RemoveIgnoreEvent:
                _controlbehavior = _obj.AddComponent<RemoveIgnoreEventBehavior>();
                break;
        }

        if (null == _controlbehavior) {
            throw new Exception("No corresponded Behavior " + type.ToString());
        }

        _controlbehavior.behaviorType = type;
        _controlbehavior.enabled = false;

        return _controlbehavior;
    }

    public static ControlBehavior AddControlBehavior(Control control, BehaviorType type) {
        if (type == BehaviorType.None)
            return null;
        ControlBehavior _controlbehavior = null;
        GameObject _obj = new GameObject();
        _obj.name = control.name + type.ToString() + (count++);
        _obj.transform.parent = control.transform;
        switch (type)
        { 
            case BehaviorType.Scale:
                _controlbehavior = _obj.AddComponent<ScaleBehavior>();
                _controlbehavior.behaviorType = BehaviorType.Scale;
                _controlbehavior.enabled = false;
                break;
            case BehaviorType.ShowChildren:
                _controlbehavior = _obj.AddComponent<ShowChildrenBehavior>();
                _controlbehavior.behaviorType = BehaviorType.ShowChildren;
                _controlbehavior.enabled = false;
                break;
            case BehaviorType.HideChildren:
                _controlbehavior = _obj.AddComponent<HideChildrenBehavior>();
                _controlbehavior.behaviorType = BehaviorType.HideChildren;
                _controlbehavior.enabled = false;
                break;
            case BehaviorType.GotoNext:
                _controlbehavior = _obj.AddComponent<GotoNextBehavior>();
                _controlbehavior.behaviorType = BehaviorType.GotoNext;
                _controlbehavior.enabled = false;
                break;
            case BehaviorType.GoBack:
                _controlbehavior = _obj.AddComponent<GobackBehavior>();
                _controlbehavior.behaviorType = BehaviorType.GoBack;
                _controlbehavior.enabled = false;
                break;

        }
        return _controlbehavior;
    }
}
