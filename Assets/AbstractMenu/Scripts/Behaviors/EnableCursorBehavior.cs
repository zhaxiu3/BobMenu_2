using UnityEngine;
using System.Collections;

public class EnableCursorBehavior : ControlBehavior
{
    public bool Enable;
    public AbstractCursor2 cursor;
    protected override void UpdateBehavior()
    {
        cursor.enabled = Enable;
        this.EndBehavior();
    }    

}
