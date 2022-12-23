using UnityEngine;
using UnityEngine.UI;

public class LoadingScreen : MonoBehaviour {
	public bool loading = false;
	public Transform loadingSprite;
	public Text uitext;
	public float speed = 300f;
	

	void Update () {
		if(loading){
			 loadingSprite.Rotate(Vector3.forward * Time.deltaTime * speed);
		}
	}

	void ShowRequest(string msg){
		loading = false;
		loadingSprite.gameObject.SetActive(false);
		uitext.text = msg;
	}
}
