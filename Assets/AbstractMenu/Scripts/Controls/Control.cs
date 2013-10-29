using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Reflection;

public class SelectorEventArgs : EventArgs
{
    public SelectorEventArgs(Vector3 position)
    {
        this.Position = position;
    }
    public Vector3 Position;
}

public enum ControlEventType
{
    Load,
    Loading,
    Loaded,
    Enter,
    Hover,
    Leave,
    Click,
    Close,
    Closing,
    Closed,
    MouseMove,
    None
}

[Serializable]
public class EventHandlerContainer {
    public ControlEventType eventType;
    public List<ControlEventType> ExclusiveEventtypeList;
    public List<BehaviorType> behaviortypeList;
    public List<ControlBehavior> behaviorList;
}
public class Control : MonoBehaviour
{

    public List<EventHandlerContainer> behaviorHandlerContainers;

    private int numberOfChildren;
    public List<Control> children;

    public bool Show;
    public List<ControlEventType> eventtypeList;
    public List<BehaviorType> behaviortypelist;
    public List<ControlBehavior> behaviorlist;

    public ControlBehavior exclusiveBehavior;
    public ControlBehavior showBehavior;
    public ControlBehavior hideBehavior;

    public event EventHandler<SelectorEventArgs> HoverEventHandler;
    public event EventHandler<SelectorEventArgs> LeaveEventHandler;
    public event EventHandler<SelectorEventArgs> ClickEventHandler;
    public event EventHandler<SelectorEventArgs> EnterEventHandler;
    public event EventHandler CloseEventHandler;
    public event EventHandler ClosingEventHandler;
    public event EventHandler ClosedEventHandler;

    public event EventHandler BehaviorFinishedHandler;

    public bool hasChildren
    {
        get
        {
            return this.numberOfChildren > 0;
        }
    }

    // Use this for initialization
    void Start()
    {
        this.numberOfChildren = children.Count;
        if (Show)
        {
            ShowChildren(true);
        }

    }


    //Access ControlBehaviors--------------------------------------------------------------
    private void RemoveAllControlBehaviors()
    {
        this.behaviorlist.Clear();
        //Don't forget that once you destroy it the childcount is one less than before
        for (int i = this.transform.childCount - 1; i >= 0; i--)
        {
            ControlBehavior _cb = this.transform.GetChild(i).GetComponent<ControlBehavior>();
            if (_cb != null)
            {
                DestroyImmediate(_cb.gameObject);
            }
        }
    }

    public void ReBuildControlBehaviors()
    {
        this.RemoveAllControlBehaviors();
        for (int i = 0; i < this.eventtypeList.Count; i++)
        {
            this.behaviorlist.Add(ControlBehavior.AddControlBehavior(this, this.behaviortypelist[i]));
        }

        for (int i = 0; i < children.Count; i++)
        {
            children[i].ReBuildControlBehaviors();
        }
    }

    //if behavior type is changed, renew a behavior for that
    public void UpdateControlBehaviors()
    {
        for (int i = 0; i < this.eventtypeList.Count; i++)
        {
            if (i >= this.behaviorlist.Count)
            {
                this.behaviorlist.Add(ControlBehavior.AddControlBehavior(this, this.behaviortypelist[i]));
            }
            else if (this.behaviorlist[i].behaviorType != this.behaviortypelist[i])
            {
                ControlBehavior _cb = this.behaviorlist[i];
                this.behaviorlist[i] = ControlBehavior.AddControlBehavior(this, this.behaviortypelist[i]);
                DestroyImmediate(_cb.gameObject);
            }
        }

        for (int i = 0; i < children.Count; i++)
        {
            children[i].UpdateControlBehaviors();
        }
    }
    //--------------------------------------------------------------#

    //Interaction-------------------------------------------------

    private void setExclusiveBehavior(ControlBehavior behavior)
    {
        if (this.exclusiveBehavior != behavior && null != this.exclusiveBehavior)
        {
            this.exclusiveBehavior.enabled = false;
        }
        this.exclusiveBehavior = behavior;
        if (null != this.exclusiveBehavior)
        {
            this.exclusiveBehavior.enabled = true;
        }
    }
    private void setCurrentBehaviorForEventType(ControlEventType _eventType)
    {
        if (this.eventtypeList.Contains(_eventType))
        {
            setExclusiveBehavior(this.behaviorlist[this.eventtypeList.IndexOf(_eventType)]);
        }
    }

    public void OnHover(object sender, SelectorEventArgs args)
    {
        //Debug.Log(this.name+ "OnHover");
        ControlEventType _eventType = ControlEventType.Hover;
        setCurrentBehaviorForEventType(_eventType);
        if (null != this.HoverEventHandler)
        {
            this.HoverEventHandler(sender, args);
        }
    }


    public void OnLeave(object sender, SelectorEventArgs args)
    {
        Debug.Log(this.name + "OnLeave");
        ControlEventType _eventType = ControlEventType.Leave;
        setCurrentBehaviorForEventType(_eventType);
        if (null != this.LeaveEventHandler)
        {
            this.LeaveEventHandler(sender, args);
        }
    }

    public void OnClick(object sender, SelectorEventArgs args)
    {
        Debug.Log(this.name + "OnClick");
        ControlEventType _eventType = ControlEventType.Click;
        setCurrentBehaviorForEventType(_eventType);
        if (null != this.ClickEventHandler)
        {
            this.ClickEventHandler(sender, args);
        }
    }

    public void OnEnter(object sender, SelectorEventArgs args)
    {
        Debug.Log(this.name + "OnEnter");
        ControlEventType _eventType = ControlEventType.Enter;
        setCurrentBehaviorForEventType(_eventType);
        if (null != this.EnterEventHandler)
        {
            this.EnterEventHandler(sender, args);
        }
    }

    public void OnClose(object sender, EventArgs args)
    {
        ControlEventType _eventType = ControlEventType.Close;
        setCurrentBehaviorForEventType(_eventType);
        Debug.Log(this.name + "OnClose");
        if (null != this.CloseEventHandler)
        {
            this.CloseEventHandler(sender, args);
        }
    }

    public void OnClosing(object sender, EventArgs args)
    {
        Debug.Log(this.name + "OnClosing");
        ControlEventType _eventType = ControlEventType.Closing;
        setCurrentBehaviorForEventType(_eventType);
        if (null != this.ClosingEventHandler)
        {
            this.ClosingEventHandler(sender, args);
        }
    }

    public void OnClosed(object sender, EventArgs args)
    {
        Debug.Log(this.name + "OnClosed");
        ControlEventType _eventType = ControlEventType.Closed;
        setCurrentBehaviorForEventType(_eventType);
        if (null != this.ClosedEventHandler)
        {
            this.ClosedEventHandler(sender, args);
        }
    }

    public void OnBehaviorFinished(object sender, EventArgs args)
    {
        if (null != this.BehaviorFinishedHandler)
        {
            this.BehaviorFinishedHandler(sender, args);
        }
    }
    //Interaction------------------------------------------------#

    public void ShowChildren(bool toshow)
    {
        if (toshow)
        {
            for (int i = 0; i < children.Count; i++)
            {
                children[i].gameObject.SetActive(true);
                children[i].ShowChildren(false);
            }
            if (null != this.showBehavior)
            {
                this.showBehavior.enabled = true;
            }
            else
            {
                Debug.Log("no showbehavior is attached to " + this.name);
            }
        }
        else
        {
            if (null != this.hideBehavior)
            {
                this.hideBehavior.enabled = true;
            }
            else
            {
                Debug.Log("no hideBehavior is attached to " + this.name);
            }
        }
    }

    public void Fade(bool isFadingIn)
    {
        if (isFadingIn)
        {
        }
        else
        {
        }
    }
    // Update is called once per frame
    void Update()
    {

    }

}
