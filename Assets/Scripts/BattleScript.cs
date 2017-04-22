using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleScript : MonoBehaviour {

	private GUIStyle defaultStyle = new GUIStyle();
	private GUIStyle boldStyle = new GUIStyle();
	private GUIStyle selectorStyle = new GUIStyle();
	private GUIStyle statStyle = new GUIStyle();

	AudioSource menuSound;

	/****Player Stats****/
	public int HP;
	public int HPMax;
	public int riledUpPercent;


	/***States***/
	private int defaultState = 1;
	private int insultState = 2;

	



	/****Default State****/
	private int insultSelection = 1;
	private int buffSelection = 2;
	private int itemSelection = 3;
	private int humdingerSelection = 4;
	private bool humdingerReady;

	/****Insult State****/
	public string[] insults;

	/***Current Stuff****/
	public int currentState;
	public int currentSelection;

	// Use this for initialization
	void Start () {
		currentState = defaultState;
		currentSelection = 1;


		defaultStyle.fontSize = 20;	

		boldStyle.fontSize = 20;
		boldStyle.fontStyle = FontStyle.Bold;
		boldStyle.normal.textColor = Color.grey;

		selectorStyle.fontSize = 28;
		selectorStyle.fontStyle = FontStyle.Bold;

		statStyle.fontSize = 34;
		statStyle.fontStyle = FontStyle.Bold;

		menuSound = GetComponent<AudioSource>();

		humdingerReady = false;

		HP = 25;
		HPMax = 25;
		riledUpPercent = 0;
	}
	
	// Update is called once per frame
	void Update () {
		
		if(currentState == defaultState) { /****Default State****/
			defaultStateNavigation();
		} else if (currentState == insultState) { /****Insult State****/
			insultStateNavigation();
		}	

	}

	void OnGUI(){


		/****Player Stats****/
		GUI.Label(new Rect(161, 32, 50, 30), HP.ToString(), statStyle);
		GUI.Label(new Rect(214, 32, 50, 30), HPMax.ToString(), statStyle);
		GUI.Label(new Rect(180, 90, 50, 30), riledUpPercent.ToString(), statStyle);

		/****Default State****/
		if(currentState == defaultState) {
			GUI.Label(new Rect(245, 500, 100, 20), "Insult", defaultStyle);
			GUI.Label(new Rect(245, 550, 100, 20), "Raise Voice", defaultStyle);
			GUI.Label(new Rect(495, 500, 100, 20), "Use Item", defaultStyle);
			GUI.Label(new Rect(495, 550, 100, 20), "Humdinger!", boldStyle);

			if(currentSelection == insultSelection) {
				GUI.Label(new Rect(225, 495, 100, 20), ">", selectorStyle);
			}
			if(currentSelection == buffSelection) {
				GUI.Label(new Rect(225, 545, 100, 20), ">", selectorStyle);
			}
			if(currentSelection == itemSelection) {
				GUI.Label(new Rect(475, 495, 100, 20), ">", selectorStyle);
			}
			if(currentSelection == humdingerSelection) {
				GUI.Label(new Rect(475, 545, 100, 20), ">", selectorStyle);
			}
		}

		/****Insult State****/
		if(currentState == insultState) {

		}
	}


	void defaultStateNavigation() {
		if(Input.GetKeyDown(KeyCode.W)) {
			menuSound.Play();
			if(currentSelection == buffSelection) {
				currentSelection = insultSelection;
			} else if(currentSelection == humdingerSelection) {
				currentSelection = itemSelection;
			}

		}
		if(Input.GetKeyDown(KeyCode.A)) {
			menuSound.Play();
			if(currentSelection == itemSelection) {
				currentSelection = insultSelection;
			} else if(currentSelection == humdingerSelection) {
				currentSelection = buffSelection;
			}
		}
		if(Input.GetKeyDown(KeyCode.S)) {
			menuSound.Play();
			if(currentSelection == insultSelection) {
				currentSelection = buffSelection;
			} else if(currentSelection == itemSelection && humdingerReady) {
				currentSelection = humdingerSelection;
			}
		}
		if(Input.GetKeyDown(KeyCode.D)) {
			menuSound.Play();
			if(currentSelection == insultSelection) {
				currentSelection = itemSelection;
			} else if(currentSelection == buffSelection && humdingerReady) {
				currentSelection = humdingerSelection;
			}
		}

		if(Input.GetKeyDown(KeyCode.E)) {
			if(currentSelection == insultSelection){
				currentState = insultState;
			}
		}
	}

	void insultStateNavigation() {

	}
}
