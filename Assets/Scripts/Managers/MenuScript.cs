﻿using UnityEngine;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

//This class is used for the splash screen.

public class MenuScript : MonoBehaviour {
	public GameObject HowToPlayPanel;

	private AudioSource menumusic;

	void Start(){
		menumusic = GameObject.Find ("MenuMusic").GetComponent<AudioSource> ();
		menumusic.loop = true;
		menumusic.enabled = true;
		menumusic.Play();
	}

	public void newGame()
	{
		SceneManager.LoadScene ("Level 01 5.x");
	}

	public void ToggleHowToPlayPanel() {
		if (HowToPlayPanel.activeSelf) {
			HowToPlayPanel.SetActive (false);
		} else {
			HowToPlayPanel.SetActive (true);
		}
	}

	public void Quit()
	{
		#if UNITY_EDITOR 
		EditorApplication.isPlaying = false;
		#else 
		Application.Quit();
		#endif
	}
}
