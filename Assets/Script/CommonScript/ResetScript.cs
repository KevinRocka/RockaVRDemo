using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using VRStandardAssets.Utils;

public class ResetScript : MonoBehaviour {

	public Button m_BtnReset;
	public float time_All = 10;//计时的总时间（单位秒）  
	public float time_Left;//剩余时间
	public Text time;
	public GameObject m_MainCanvas;
	public GameObject m_Camera;
	public float m_DebugRayLength = 5f;        
	public float m_DebugRayDuration = 1f;
	public float m_RayLength = 500f;
	public bool m_ShowDebugRay;  


	bool isStart;
	SelectionRadial m_SelectionRadialPrevious;

	void Start () {
		time_Left = time_All;
		isStart = false;
	}

	private void OnEnable()
	{
		m_SelectionRadialPrevious = m_BtnReset.GetComponent<SelectionRadial>();
		m_SelectionRadialPrevious.OnSelectionComplete += HandleRadialCompletePrevious;
	}


	private void OnDisable()
	{
		m_SelectionRadialPrevious.OnSelectionComplete -= HandleRadialCompletePrevious;
	}

	private void HandleRadialCompletePrevious ()
	{	
		isStart = true;
		StartTimer ();
	}

	void Update () {
		if (m_ShowDebugRay) {
			Debug.DrawRay(m_Camera.transform.position, m_Camera.transform.forward * m_DebugRayLength, Color.blue, m_DebugRayDuration);
		}
		if (isStart) {
			if (time_Left > 0) {
				StartTimer ();
			} else {
				isStart = false;
			}
		} else if(time_Left <= 0){
			cameraNewPosition ();
			time_Left = time_All;
			isStart = false;
		}
	}

	void cameraNewPosition(){
		time.text = "Reset";
		Vector3 newPosition =m_Camera.transform.position + m_Camera.transform.forward * m_DebugRayLength;
		m_MainCanvas.transform.position = newPosition;
		m_MainCanvas.transform.rotation = m_Camera.transform.rotation;
	}


	/// <summary>
	/// 开始计时
	/// </summary>
	void StartTimer(){
		time_Left -= Time.deltaTime;  
		time.text = GetTime (time_Left);  
	}  
		
	/// <summary>  
	/// 获取总的时间字符串  
	/// </summary>  
	string GetTime(float time){  
		return GetSecond (time);  

	}

	/// <summary>  
	/// 获取秒  
	/// </summary>  
	string GetSecond(float time){  
		int timer = (int)((time % 3600)%60);  
		string timerStr = timer.ToString ();  
		return timerStr;  
	}
}