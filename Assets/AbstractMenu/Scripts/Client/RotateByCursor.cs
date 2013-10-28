using UnityEngine;
using System.Collections;

public class RotateByCursor : MonoBehaviour {
    public Transform cursor;
    public float speed = 10;
    public float MaxAngle = 30;
	// Use this for initialization
	
	// Update is called once per frame
	void Update () {
        float _roty = cursor.position.x * speed;
        if (_roty > MaxAngle)
            _roty = MaxAngle;
        if (_roty < -MaxAngle)
            _roty = -MaxAngle;
        float _rotx = cursor.position.y * -speed;
        if (_rotx > MaxAngle)
            _rotx = MaxAngle;
        if (_rotx < -MaxAngle)
            _rotx = -MaxAngle;

        this.transform.localRotation = Quaternion.Euler(new Vector3(_rotx,_roty,0));
	}
}
