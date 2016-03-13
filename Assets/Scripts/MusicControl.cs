using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MusicControl : MonoBehaviour {
	
	public static bool musicOn = true;
	public Sprite [] onOff;
	
	private MusicPlayer musicPlayer;
	
	void Start(){
		musicPlayer = GameObject.FindObjectOfType<MusicPlayer>();
		
		// Music Player sprite
		if (musicOn){
			this.GetComponent<Image>().sprite = onOff[1];
		}
		else{
			this.GetComponent<Image>().sprite = onOff[0];
		}
		
		
	}
	
	public void TurnMusicOffOrOn () {
		
		if (musicOn){
			musicPlayer.GetComponent<AudioSource>().mute = true;
			musicOn = false;
			this.GetComponent<Image>().sprite = onOff[0] ;
		}
		else{
			musicPlayer.GetComponent<AudioSource>().mute = false;
			musicOn = true;
			this.GetComponent<Image>().sprite = onOff[1];
		}
	
	}

	
}
