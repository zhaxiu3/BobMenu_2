using UnityEngine;
using System.Collections;

    public delegate void UpdateHandler();
public class BobMethodTimerManager : MonoBehaviour {
    public int count = 0;
    public UpdateHandler updateHander;
    private static BobMethodTimerManager instance;
    public static BobMethodTimerManager Instance {
        get {
            return instance;
        }
    }
    void Awake() {
        if (null == instance)
            instance = this;

    }
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (null != updateHander) {
            updateHander();
        }
	}
}
