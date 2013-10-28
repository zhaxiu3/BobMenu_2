using UnityEngine;
using System.Collections;
using System;

public class BobEventArgs : EventArgs{
    public int ID;
    public string name;
    public string state;
    public float process;
    public BobEventArgs(string name, string state, float process, int ID) {
        this.ID = ID;
        this.name = name;
        this.state = state;
        this.process = process;
    }
}
