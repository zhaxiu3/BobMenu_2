using UnityEngine;
using System.Collections;

public class AbstractCursor2 : MonoBehaviour
{

    public Control2 menuScript;
    public int mLayerMask;
    void Start()
    {
        mLayerMask = 1 << LayerMask.NameToLayer("AbstractControl");

    }
    // Update is called once per frame
    void Update()
    {
        UpdateSelect();
        UpdateInput();

    }

    private void UpdateInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (null != menuScript)
            {
                menuScript.OnClick(this, new SelectorEventArgs(this.transform.position));
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
            Control2 _hittedMenu = hit.collider.gameObject.GetComponent<Control2>();

            if (menuScript != _hittedMenu)
            {
                if (null != menuScript)
                {
                    menuScript.OnLeave(this, new SelectorEventArgs(this.transform.position));
                }
                menuScript = _hittedMenu;
                menuScript.OnEnter(this, new SelectorEventArgs(this.transform.position));
            }
            else
            {
                menuScript.OnHover(this, new SelectorEventArgs(this.transform.position));
            }
        }
        else
        {
            if (null != menuScript)
            {
                menuScript.OnLeave(this, new SelectorEventArgs(this.transform.position));
                menuScript = null;
            }
        }
    }
}
