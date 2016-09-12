using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WifiGet : MonoBehaviour {

	Image imageWifi;

	public Sprite no_wifi;

	public Sprite wifi;

	// Use this for initialization
	void Start () {
		imageWifi = GetComponent <Image> ();
		if (Application.internetReachability == NetworkReachability.ReachableViaLocalAreaNetwork) {  
			imageWifi.sprite = wifi;
		} else {
			imageWifi.sprite = no_wifi;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
