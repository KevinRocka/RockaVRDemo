using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class NetImageGet : MonoBehaviour {

	string url = "http://img.hb.aicdn.com/240136a8caf6ae05d38f2f57d596aec10c44d1ff112df-4XaoQJ_fw580";
	private Material material;
	private Image image;

	IEnumerator Start () {
		WWW www = new WWW(url);
		yield return www;
		if (www != null && string.IsNullOrEmpty (www.error)) 
		{
			image = GetComponent<Image> ();
			Texture2D texture = www.texture; 
			Sprite sprite = Sprite.Create (texture, new Rect(0, 0, texture.width, texture.height) , new Vector2(0.5f, 0.5f));
			image.sprite = sprite;
		}
	}
}
