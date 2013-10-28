using UnityEngine;
using System.Collections;

public class Selector : MonoBehaviour {

    public GameObject menuScript;
    public int mLayerMask;
    void Start() {
        mLayerMask = 1<<LayerMask.NameToLayer("Menu");

    }
	// Update is called once per frame
	void Update () {        
        UpdateSelect();
        UpdateInput();
        
	}

    private void UpdateInput()
    {
        if (Input.GetMouseButtonDown(0)) {
            if (null != menuScript)
            {
                menuScript.SendMessage("OnPicked");
            }
        }
    }

    private void UpdateSelect()
    {
        Vector3 dir = transform.position - Camera.main.transform.position;
        Debug.DrawRay(transform.position, dir);
        RaycastHit hit;
        if (Physics.Raycast(transform.position, dir, out hit, 1000, mLayerMask))
        {
            if (menuScript != hit.collider.gameObject)
            {
                if (null != menuScript)
                {
                    menuScript.SendMessage("OnLeave", null, SendMessageOptions.DontRequireReceiver);
                }
                menuScript = hit.collider.gameObject;
                menuScript.SendMessage("OnEnter", null, SendMessageOptions.DontRequireReceiver);
            }
            else
            {
                menuScript.SendMessage("OnHover", null, SendMessageOptions.DontRequireReceiver);
            }
        }
        else
        {
            if (null != menuScript)
            {
                menuScript.SendMessage("OnLeave", null, SendMessageOptions.DontRequireReceiver);
                menuScript = null;
            }
        }
    }
}
