using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class QuitButton : MonoBehaviour
{
   	public Button yourButton;
	//AudioSource audio;

	void Start () {
		Button btn = yourButton.GetComponent<Button>();
		btn.onClick.AddListener(TaskOnClick);
		//audio = GetComponent<AudioSource>();   
	}

	void TaskOnClick(){
	 	// audio.Play(0);
		 Application.Quit();
	}
}
