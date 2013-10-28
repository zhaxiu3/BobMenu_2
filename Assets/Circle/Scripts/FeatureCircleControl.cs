using UnityEngine;
using System.Collections;

public class FeatureCircleControl : MonoBehaviour {
    public GameObject progressbarObject;

	public GameObject[] 		features;
	public GameObject[] 		Edges;
	public GameObject[]			featurePrompts;
	public GameObject 			RotCenter;
	public GameObject			TargetUsr;
	public GameObject			TargetFullScreen;
    public GameObject session;
	public Transform[] 	featureVideos;
	public float[]				VideoLength;
	public float RotSpeed = 60.0f;
	public float lerpSpeed = 3.0f;
	protected int curIdex=0;
	protected Quaternion curRot=Quaternion.identity;
	protected Quaternion curRotFrameCenter=Quaternion.identity;
	protected Quaternion TargetRot=Quaternion.identity;
	Transform CurSelection;
	bool isSelected = false;
	bool playing = false;
	bool isOn=false;
	public Vector3 offset = new Vector3(0,0,-0.6f);
	Vector3 handPos = new Vector3(0,0,0);
	Transform parentCatch=null;


	void Start () {
		animation.Play("hdieF");
	}
	float round(float v, float shift=0.0f)
	{
		while(v<(0.0f+shift))
		{
			v+=360.0f;
		}
		while(v>(360.0f+shift))
		{
			v-=360.0f;
		}
		return v;
	}
	public void OnShow()
	{
		hideEdge();
		transform.position = TargetUsr.transform.position;
		animation.CrossFade("FeatureIn",0.3f);
		isOn=true;
	}
	public void OnHide()
	{
		animation.CrossFade("FeatureOut",0.3f);
		isOn=false;
	}
	Vector3 lerp(Vector3 src,Vector3 tar)
	{
		return src * (1-Time.deltaTime*lerpSpeed) + tar*Time.deltaTime*lerpSpeed;
	}
	Quaternion lerp(Quaternion src, Quaternion tar)
	{
		return Quaternion.Slerp(src,tar,Time.deltaTime*3.0f);
	}
	void Update ()
	{
		if(!isOn)
			return;
		foreach(GameObject o in features)
		{
			o.renderer.material.SetFloat("_FadeStart", session.transform.position.z - 1.5f);
			o.renderer.material.SetFloat("_FadeEnd", session.transform.position.z + 1.0f);
		}
		foreach(GameObject o in featurePrompts)
		{
			o.renderer.material.SetFloat("_FadeStart", session.transform.position.z - 1.5f);
			o.renderer.material.SetFloat("_FadeEnd", session.transform.position.z + 1.0f);
		}
		
		transform.position = lerp(transform.position,TargetUsr.transform.position);
		if(!isSelected)
		{
			RotCenter.transform.localRotation = lerp(RotCenter.transform.localRotation,TargetRot);
		}		
		if(playing)
		{
            CurSelection.position = lerp(CurSelection.position, TargetFullScreen.transform.position);
            CurSelection.rotation = lerp(CurSelection.rotation, TargetFullScreen.transform.rotation);
            CurSelection.localScale = lerp(CurSelection.localScale, TargetFullScreen.transform.localScale);
			return;
		}

		foreach(Transform vt in featureVideos)
		{
			if(!(CurSelection==vt&&isSelected))
			{
				vt.localScale = lerp(vt.localScale,Vector3.one);
				vt.localPosition = lerp(vt.localPosition,Vector3.zero);
				vt.localRotation = lerp(vt.localRotation,Quaternion.identity);
			}
		}
		foreach(Transform vt in featureVideos)
		{
			if(vt.animation.IsPlaying("VideoStop"))
				break;
			if(vt == CurSelection)
			{
				if(isSelected)
				{
					vt.animation.CrossFade("Attached");
					vt.position = lerp(vt.position,handPos+offset);
					vt.rotation = lerp(vt.rotation,TargetFullScreen.transform.localRotation);
                    //Debug.Log("Hand "+handPos);
				}
				else
				{
					vt.animation.CrossFade("Picked");
				}
			}
			else
			{
				vt.animation.CrossFade("notPicked");				
			}
		}
	}
	public void updateCircle(float interval)//(-1,1)
	{		
			TargetRot = curRotFrameCenter * Quaternion.AngleAxis(interval*RotSpeed*0.5f,Vector3.up);
	}
	public void goLeft()
	{		
			curRotFrameCenter *= Quaternion.AngleAxis(RotSpeed,Vector3.up);
			TargetRot = curRotFrameCenter;
	}
	public void goRight()
	{
			curRotFrameCenter *= Quaternion.AngleAxis(-RotSpeed,Vector3.up);
			TargetRot = curRotFrameCenter;
	}
	public void picked()
	{
		if(null==parentCatch)
		{
			isSelected = true;
			parentCatch = CurSelection.transform.parent;
			CurSelection.transform.parent = null;
			Debug.LogWarning("pick:"+CurSelection.name);
		}
		else
			Debug.LogError("Double pick");
	}
	public void canclePick()
	{
		if(null!=parentCatch)
		{
			CurSelection.transform.parent=parentCatch;
			isSelected = false;
			parentCatch=null;
			//CurSelection = null;
			//hideEdge();
			Debug.LogWarning("Cancle"+CurSelection.name);
		}
		else
			Debug.LogError("Double Cancle");
	}


	
	public void hideEdge()
	{
		foreach( GameObject obj in Edges)
		{
			Color c = obj.renderer.material.color;
			c.a = 0.0f;
			obj.renderer.material.color=c;
		}
	}
}
