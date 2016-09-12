using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

public class PanelManager : MonoBehaviour {




	public void OpenPanel (GameObject fromObj , GameObject toObj)
	{
		if (fromObj == null || toObj == null)
			return;
		fromObj.gameObject.SetActive (false);
		toObj.gameObject.SetActive (true);
	}
}
