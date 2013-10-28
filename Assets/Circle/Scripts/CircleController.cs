using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CircleController : MonoBehaviour {

    /// <summary>
    /// 图片的个数
    /// </summary>
    private int NumOfPictures = 10;

    /// <summary>
    /// 圆的半径
    /// </summary>
    public float Radius = 10;

    /// <summary>
    /// 最大的缩放
    /// </summary>
    public Vector3 MaxScale = new Vector3(10,10,1);

    /// <summary>
    ///最小的缩放 
    /// </summary>
    public Vector3 MinScale = new Vector3(0, 0, 0);

    /// <summary>
    /// 淡入淡出的时间长度
    /// </summary>
    public float FadeSpeed = 0.5f;

    public List<Transform> Pictures = new List<Transform>();
    private float angle = 0;
    private List<float> angles = new List<float>();
    private DurationInvoker dui;
    /// <summary>
    /// 圆环的状态:
    /// 0:正常
    /// 1:fade in
    /// 2:fade out
    /// 3:rotating
    /// </summary>
    public int state = 0;


    

    // Use this for initialization
    void Start()
    {
        NumOfPictures = transform.childCount;
        for (int i = 0; i < NumOfPictures; ++i)
        {
            Pictures.Add(transform.GetChild(i));
            angles.Add(360.0f / NumOfPictures * (float)i);
        }
        this.dui = this.GetComponent<DurationInvoker>();
        SetFadeIn();
	}

    private void SetRotateRight() {
        if (3 == this.state)
            return;
        EnableMenu(false);
        this.state = 3;
        this.angle += angles[1];
        this.dui.StartaTimer("Rotate", 0.5f);
    }

    private void SetRotateLeft()
    {
        if (3 == this.state)
            return;
        EnableMenu(false);
        this.state = 3;
        this.angle -= angles[1];
        this.dui.StartaTimer("Rotate", 0.5f);
    }
    private void Rotate(float process) {
        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.Euler(0, this.angle, 0), process);
        if (Mathf.Approximately(process, 1f)) {
            SetNormal();
        }
    }

    private void SetFadeIn() {
        EnableMenu(false);
        this.state = 1;
        this.dui.StartaTimer("FadeIn", 1);
    }
    private void FadeIn(float process) {
        for (int i = 0; i < NumOfPictures; ++i)
        {
            Pictures[i].localPosition = Vector3.Slerp(Pictures[i].localPosition, Quaternion.Euler(new Vector3(0, angles[i], 0)) * Vector3.forward * Radius, process);
            Pictures[i].localRotation = Quaternion.Slerp(Pictures[i].localRotation, Quaternion.Euler(new Vector3(0, angles[i] - 180, 0)), process);
            Pictures[i].localScale = Vector3.Lerp(Pictures[i].localScale, MaxScale, process);
        }
        if (Mathf.Approximately(process, 1f))
        {
            SetNormal();
        }
    }

    private void SetNormal()
    {
        EnableMenu(true);
        this.state = 0;
    }

    private void EnableMenu(bool enabled)
    {
        //TODO:activate menus
        for (int i = 0; i < NumOfPictures; ++i)
        {
            Pictures[i].GetChild(0).GetComponent<BobMenu>().enabled = enabled;
            if (enabled)
            {
                Pictures[i].GetChild(0).gameObject.AddComponent<MeshCollider>();
            }
            else
            {
                Destroy(Pictures[i].GetChild(0).gameObject.GetComponent<MeshCollider>());
            }
        }
    }

    private void SetFadeOut()
    {
        EnableMenu(false);
        this.state = 2;
        this.dui.StartaTimer("FadeOut", 1);
    }
    private void FadeOut(float process)
    {
        for (int i = 0; i < NumOfPictures; ++i)
        {
            Pictures[i].localPosition = Vector3.Slerp(Pictures[i].localPosition, Vector3.zero, process);
            Pictures[i].localRotation = Quaternion.Slerp(Pictures[i].localRotation, Quaternion.identity, process);
            Pictures[i].localScale = Vector3.Lerp(Pictures[i].localScale, MinScale, process);
        }
    }
    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.W))
        {
            SetFadeOut();
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            SetFadeIn();
        }
        if (Input.GetKeyDown(KeyCode.A)) {
            SetRotateRight();
        }
        if (Input.GetKeyDown(KeyCode.D)) {
            SetRotateLeft();
        }
	}
}
