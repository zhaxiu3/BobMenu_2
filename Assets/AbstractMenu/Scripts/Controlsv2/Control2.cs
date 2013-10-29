using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Reflection;


public class Control2 : MonoBehaviour
{

    public List<EventHandlerContainer> behaviorHandlerContainers;

    private int numberOfChildren;
    public List<Control2> items;
    public List<Transform> Trans;
    public List<ControlBehavior> avticeBehaviorList;
    public bool Show;

    public event EventHandler<SelectorEventArgs> HoverEventHandler;
    public event EventHandler<SelectorEventArgs> LeaveEventHandler;
    public event EventHandler<SelectorEventArgs> ClickEventHandler;
    public event EventHandler<SelectorEventArgs> EnterEventHandler;
    public event EventHandler LoadEventHandler;
    public event EventHandler LoadingEventHandler;
    public event EventHandler LoadedEventHandler;
    public event EventHandler CloseEventHandler;
    public event EventHandler ClosingEventHandler;
    public event EventHandler ClosedEventHandler;

    public event EventHandler BehaviorFinishedHandler;

    public List<ControlEventType> IgnoredEventList;
    public ControlEventType LastEvent;

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
        this.numberOfChildren = items.Count;
        if (Show)
        {
            OnLoad(null, null);
        }
        else {
            this.ShowChildren(false);
        }

    }


    //Access ControlBehaviors--------------------------------------------------------------
    private void RemoveAllControlBehaviors()
    {
        for (int i = 0; i < this.behaviorHandlerContainers.Count; i++)
        {
            this.behaviorHandlerContainers[i].behaviorList.Clear();
        }
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

        for (int i = 0; i < this.behaviorHandlerContainers.Count; i++) {
            EventHandlerContainer _elm = this.behaviorHandlerContainers[i];
            for (int j = 0; j < _elm.behaviortypeList.Count; j++)
            {
                _elm.behaviorList.Add(ControlBehavior.AddControlBehavior(this, _elm.eventType,_elm.behaviortypeList[j]));
            }
        }
        for (int i = 0; i < items.Count; i++)
        {
            items[i].ReBuildControlBehaviors();
        }
    }

    #region for Editor use
    //if behavior type is changed, renew a behavior for that
    public void UpdateControlBehaviors()
    {
        for (int i = 0; i < this.behaviorHandlerContainers.Count; i++) {
            EventHandlerContainer _elm = this.behaviorHandlerContainers[i];
            if (_elm.behaviorList.Count > _elm.behaviortypeList.Count) {
                for (int j = _elm.behaviorList.Count - 1; j >= _elm.behaviortypeList.Count; j--) {
                    ControlBehavior _cb = _elm.behaviorList[j];
                    _elm.behaviorList.RemoveAt(j);
                    DestroyImmediate(_cb.gameObject);
                }
            }
            for (int j =0 ; j < _elm.behaviortypeList.Count; j++)
            {
                if (j >= _elm.behaviorList.Count) {
                    _elm.behaviorList.Add(ControlBehavior.AddControlBehavior(this, _elm.eventType,_elm.behaviortypeList[j]));
                }
                else if (_elm.behaviorList[j].behaviorType != _elm.behaviortypeList[j]) {
                    ControlBehavior _cb = _elm.behaviorList[j];
                    _elm.behaviorList[j] = ControlBehavior.AddControlBehavior(this, _elm.eventType,_elm.behaviortypeList[j]);
                    DestroyImmediate(_cb.gameObject);
                }
            }
        }
            for (int i = 0; i < items.Count; i++)
            {
                items[i].UpdateControlBehaviors();
            }
    }
    //--------------------------------------------------------------#
    #endregion

    //Interaction-------------------------------------------------

    private void DisableExclusiveEvent(ControlEventType _eventType)
    {
        for (int i = 0; i < this.behaviorHandlerContainers.Count; i++)
        {
            EventHandlerContainer _elem = this.behaviorHandlerContainers[i];
            if (_eventType == _elem.eventType) {
                for (int j = 0; j < _elem.behaviorList.Count; j++) {
                    _elem.behaviorList[j].EndBehavior();
                }
                break;
            }
        }
    }

    private void setBehaviorForEventType(ControlEventType _eventType)
    {
        if (this.IgnoredEventList.Contains(_eventType)) {
            return;
        }

        if (this.LastEvent == _eventType) {
            return;
        }        
        this.LastEvent = _eventType;

        for (int i = 0; i < this.behaviorHandlerContainers.Count; i++) {
            EventHandlerContainer _elem = this.behaviorHandlerContainers[i];
            if (_elem.eventType == _eventType) {
                for (int j = 0; j < _elem.behaviorList.Count; j++) {
                    _elem.behaviorList[j].BeginBehavior();
                }
                for (int j = 0; j < _elem.ExclusiveEventtypeList.Count; j++) {
                    this.DisableExclusiveEvent(_elem.ExclusiveEventtypeList[j]);
                }
                break;
            }
        }
    }

    public void OnLoad(object sender, EventArgs args)
    {
        this.ShowChildren(true);
        setBehaviorForEventType(ControlEventType.Load);
        if (null != this.LoadEventHandler)
        {
            this.LoadEventHandler(sender, args);
        }
    }
    public void OnLoading(object sender, EventArgs args)
    {
        setBehaviorForEventType(ControlEventType.Loading);
        if (null != this.LoadingEventHandler)
        {
            this.LoadingEventHandler(sender, args);
        }
    }
    public void OnLoaded(object sender, EventArgs args)
    {
        setBehaviorForEventType(ControlEventType.Loaded);
        if (null != this.LoadedEventHandler)
        {
            this.LoadedEventHandler(sender, args);
        }
    }

    public void OnHover(object sender, SelectorEventArgs args)
    {
        Debug.Log(this.name+ "OnHover");
        ControlEventType _eventType = ControlEventType.Hover;
        setBehaviorForEventType(_eventType);
        if (null != this.HoverEventHandler)
        {
            this.HoverEventHandler(sender, args);
        }
    }


    public void OnLeave(object sender, SelectorEventArgs args)
    {
        Debug.Log(this.name + "OnLeave");
        ControlEventType _eventType = ControlEventType.Leave;
        setBehaviorForEventType(_eventType);
        if (null != this.LeaveEventHandler)
        {
            this.LeaveEventHandler(sender, args);
        }
    }

    public void OnClick(object sender, SelectorEventArgs args)
    {
        Debug.Log(this.name + "OnClick");
        ControlEventType _eventType = ControlEventType.Click;
        setBehaviorForEventType(_eventType);
        if (null != this.ClickEventHandler)
        {
            this.ClickEventHandler(sender, args);
        }
    }

    public void OnEnter(object sender, SelectorEventArgs args)
    {
        Debug.Log(this.name + "OnEnter");
        ControlEventType _eventType = ControlEventType.Enter;
        setBehaviorForEventType(_eventType);
        if (null != this.EnterEventHandler)
        {
            this.EnterEventHandler(sender, args);
        }
    }

    public void OnClose(object sender, EventArgs args)
    {
        ControlEventType _eventType = ControlEventType.Close;
        setBehaviorForEventType(_eventType);
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
        setBehaviorForEventType(_eventType);
        if (null != this.ClosingEventHandler)
        {
            this.ClosingEventHandler(sender, args);
        }
    }

    public void OnClosed(object sender, EventArgs args)
    {
        Debug.Log(this.name + "OnClosed");
        ControlEventType _eventType = ControlEventType.Closed;
        setBehaviorForEventType(_eventType);
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
        this.Show = toshow;
        if (toshow)
        {
            for (int i = 0; i < items.Count; i++)
            {
                items[i].gameObject.SetActive(true);
            }           
        }
        else
        {
            for (int i = 0; i < items.Count; i++) {
                items[i].gameObject.SetActive(false);
                items[i].ShowChildren(false);
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


    public void IgnoreEvent(List<ControlEventType> list)
    {
        foreach (ControlEventType elm in list)
        {
            if (this.IgnoredEventList.Contains(elm))
                continue;
            this.IgnoredEventList.Add(elm);
        }
    }
    public void removeIgnoreEvent(List<ControlEventType> list)
    {
        foreach (ControlEventType elm in list)
        {
            this.IgnoredEventList.Remove(elm);
        }
    }
}
