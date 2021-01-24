using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class BackBtn : MonoBehaviour
{
    // Start is called before the first frame update

   	public Button yourButton;
	// AudioSource audio;
	void Start () {
		Button optionBtn = yourButton.GetComponent<Button>();
		optionBtn.onClick.AddListener(TaskOnClick);
		// audio = GetComponent<AudioSource>();    
	}

	void TaskOnClick(){
	    SceneManager.LoadScene("MainMenuv2");
    }
}
