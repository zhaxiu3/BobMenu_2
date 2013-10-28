using UnityEngine;
using System.Collections;
using System.Diagnostics;

public class BobMenu : MonoBehaviour
{

    private DurationInvoker dui;
    private DelayedInvoker di;
    private Vector3 originalScale;
    private float menuProcess;
    public float fadetime = 0.3f;
    public bool isSelected = false;
    public Transform Cursor;
    void Start() {
        this.dui = this.GetComponent<DurationInvoker>();
        this.di = this.GetComponent<DelayedInvoker>();
        this.originalScale = this.transform.localScale;
    }


    void Update() {
        if (!isSelected)
        {
            return;
        }
        else {
            this.transform.position = Vector3.Lerp(this.transform.position, Cursor.position, Time.deltaTime*3);
            this.transform.rotation = Quaternion.Lerp(this.transform.rotation, Cursor.rotation, Time.deltaTime * 3);
        }
    }
    private void OnEnter()
    {
        dui.ClearTimers();
        di.StartaTimer("OnPicked", 0.6f);
        dui.StartaTimer("Enlarge", fadetime - this.menuProcess);
    }

    private void Enlarge(float process) {
        this.menuProcess = fadetime * process;
        transform.localScale = Vector3.Lerp(transform.localScale, this.originalScale * 1.5f, process);
    }
    private void OnHover()
    {

    }
    private void OnLeave()
    {
        this.isSelected = false;
        di.ClearTimers();
        dui.ClearTimers();
        dui.StartaTimer("Narrow", this.menuProcess);
    }
    private void Narrow(float process) {
        this.menuProcess = fadetime * (1f - process);
        transform.localScale = Vector3.Lerp(transform.localScale, this.originalScale, process);
        transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(270, 0, 0), process);
        transform.localPosition = Vector3.Lerp(transform.localPosition, Vector3.zero, process);
    }

    private int count = 0;
    private void OnPicked() {
        UnityEngine.Debug.Log("OnPicked");
        this.isSelected = true;
        //CaptureAndPrintImage();

    }

    private void CaptureAndPrintImage()
    {
        Application.CaptureScreenshot("Screenshot" + count + ".png");
        System.Diagnostics.Process.Start("mspaint.exe", "/pt " + "Screenshot" + count + ".png");
        count++;
    }
}
