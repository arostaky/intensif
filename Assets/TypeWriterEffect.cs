﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TypeWriterEffect : MonoBehaviour {

	public float delay = 0.1f;
	public string fullText;
	private string currentText = "";
	public float wait = 5.0f;

	// Use this for initialization
	void Start () {
		StartCoroutine(ShowText());
		StartCoroutine(DestroyMe());
	}
	
	IEnumerator ShowText(){
		for(int i = 0; i < fullText.Length; i++){
			currentText = fullText.Substring(0,i);
			this.GetComponent<Text>().text = currentText;
			yield return new WaitForSeconds(delay);
		}
	}
	IEnumerator DestroyMe()
    {
		
		yield return new WaitForSeconds(wait); 
		Destroy(gameObject);
	}
}
