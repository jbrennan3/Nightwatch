﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PhaseManager : MonoBehaviour {

	public static PhaseManager instance = null;
	public Text phaseText;
	public bool IsBuildPhase;
	public GameObject healthUI;
	public GameObject homeHealthUI;
	public GameObject scoreText;
	public GameObject buildCanvas;
	public GameObject AKUI;
	public GameObject SGUI;
	public GameObject LZUI;

	private GameObject enemyManager;
	private GameObject player;
	private Animator anim;

	void Awake() {
		anim = GetComponent<Animator> ();
		enemyManager = GameObject.Find ("EnemyManager");
		player = GameObject.Find ("Player");
		AKUI.SetActive (false);
		SGUI.SetActive (false);
		LZUI.SetActive (false);

		BeginBuildPhase ();
	}

	void BeginBuildPhase() {
		IsBuildPhase = true;
		enemyManager.SetActive (false);
		player.SetActive (false);

		phaseText.text = "Start Build Phase";
		anim.SetTrigger ("StartBuildPhase");
		buildCanvas.GetComponent<Animator> ().SetTrigger ("FadeIn");
	}

	public void BeginAttackPhase() {
		IsBuildPhase = false;
		GameObject nobuildzone = GameObject.Find ("NoBuildZone");
		nobuildzone.SetActive (false);
		enemyManager.SetActive (true);
		AKUI.SetActive (true);
		SGUI.SetActive (true);
		LZUI.SetActive (true);
		foreach (EnemyManager enemy in enemyManager.GetComponents<EnemyManager> ()) {
			enemy.BeginSpawning();
		}

		player.SetActive (true);
		healthUI.SetActive (true);
		homeHealthUI.SetActive (true);
		scoreText.SetActive (true);
		GameObject.Find ("BuildPanel").SetActive (false);
		//buildCanvas.SetActive (false);


		// Do the animation thing again
		//phaseText.text = "Attack Phase";
		phaseText.text = "";
	}

	public void MoveToNextLevel() {
		GameManager GM = GameObject.Find ("GameManager").GetComponent<GameManager> ();
		GM.GoToNextLevel ();
	}

	void Update () {
		
	}
}
