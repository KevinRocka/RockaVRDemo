using UnityEngine;
using System.Collections;

public class GridItem : MonoBehaviour {

	public string ItemSprite;
	public string ItemName;

	public GridItem(string itemSprite , string itemName){
		this.ItemSprite = itemSprite;
		this.ItemName = itemName;
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
