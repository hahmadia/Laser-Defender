using UnityEngine;
using System.Collections;


public class MusicPlayer : MonoBehaviour {
	
	public static MusicPlayer instance = null;
	
	void Awake (){
	
		if (instance != null){
			Destroy (gameObject);
			Debug.Log("Destroyed a duplicate");
		}
		else{
			instance = this;
			GameObject.DontDestroyOnLoad(instance);
		}
	
	}
	
}
