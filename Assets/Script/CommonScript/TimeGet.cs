using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TimeGet : MonoBehaviour {

	Text text;

	void Awake()
	{
		text = GetComponent<Text> ();
	}

	void Update () {
		System.DateTime dt = System.DateTime.Now;
		text.text = dt.ToLongTimeString ().ToString ();
	}

}
