using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class MenuController : MonoBehaviour
{
   	public Button yourButton;
	// AudioSource audio;
	void Start () {
		Button optionBtn = yourButton.GetComponent<Button>();
		optionBtn.onClick.AddListener(TaskOnClick);
		// audio = GetComponent<AudioSource>();    
	}

	void TaskOnClick(){
        SceneManager.LoadScene("Option_Menuv2");
		//  audio.Play(0);
		//  StartCoroutine(OptionMenu());
		 
	}
	// IEnumerator OptionMenu(){
    //     //Debug.Log("Started Coroutine at timestamp : " + Time.time);
    //     yield return new WaitForSeconds(1);
	// 	SceneManager.LoadScene("Option_Menuv2");
    // }
}
