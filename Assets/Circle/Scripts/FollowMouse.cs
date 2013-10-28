using UnityEngine;
using System.Collections;

public class FollowMouse : MonoBehaviour {

    Vector3 MousePosition;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        MousePosition = Camera.mainCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z));
        transform.position = MousePosition;
	}
}
