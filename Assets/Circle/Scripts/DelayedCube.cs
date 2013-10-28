using UnityEngine;
using System.Collections;

/// <summary>
/// this is a test for using DelayedInvoker
/// 
/// in this demo
/// 
/// the cube are scaled to 2 times big after 3 seconds 
/// and then go to position (10,10,10) after 3 seconds
/// </summary>
public class DelayedCube : MonoBehaviour {

    DelayedInvoker di;
	// Use this for initialization
	void Start () {
        di = this.gameObject.GetComponent<DelayedInvoker>();
        if (null != di)
        {
            di.StartaTimer("ScaleTo2Times", 3);
        }
	}

    void ScaleTo2Times()
    {
        this.transform.localScale *= 2;
        di.StartaTimer("GotoaPlace", 3);
    }
    void GotoaPlace() {
        this.transform.position = new Vector3(10, 10, 10);
    }
	// Update is called once per frame
	void Update () {
	
	}
}
