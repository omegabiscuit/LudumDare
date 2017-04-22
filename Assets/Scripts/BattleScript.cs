using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleScript : MonoBehaviour {

	private GUIStyle defaultStyle = new GUIStyle();
	private GUIStyle boldStyle = new GUIStyle();
	private GUIStyle selectorStyle = new GUIStyle();
	private GUIStyle statStyle = new GUIStyle();

	public GameObject menuSelectionSound;
	public GameObject textContinueSound;

	public GameObject enemy;
	EnemyScript enemyScript;

	public Dictionary<string, int> wordToPower;

	AudioSource menuSound;

	/****Player Stats****/
	public int HP;
	public int HPMax;
	public int riledUpPercent;
	public int powerMultiplier;


	/***States***/
	private int defaultState = 1;
	private int insultState = 2;
	private int handlingInsultState = 3;
	private int enemyAttackState = 4;
	



	/****Default State****/
	private int insultSelection = 1;
	private int buffSelection = 2;
	private int itemSelection = 3;
	private int humdingerSelection = 4;
	private bool humdingerReady;

	/****Insult State****/
	public string[] insults;
	public string insultChosen;
	public bool insulted;

	/****Enemy Attack State****/
	public string enemyInsultText;
	public string playerResponseText;

	/***Current Stuff****/
	public int currentState;
	public int currentSelection;

	/****Insult Handling****/
	public string insultToEnemyText;
	public string insultToEnemyResultText;
	public int displayNumber;


	// Use this for initialization
	void Start () {
		//Maps words to magnitude of the effect they have on the player
		wordToPower = new Dictionary<string,int>();
		wordToPower.Add("Jerk", 1);
		wordToPower.Add("Poot", 1);
		wordToPower.Add("Milk", 0);
		wordToPower.Add("Bubby", 0);


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

		insulted = false;
		insultChosen = "";

		powerMultiplier = 1;

		enemyScript = enemy.GetComponent<EnemyScript>();
	}
	
	// Update is called once per frame
	void Update () {
		
		if(currentState == defaultState) { /****Default State****/
			defaultStateNavigation();
		} else if (currentState == insultState) { /****Insult State****/
			insultStateNavigation();
		} else if (currentState == handlingInsultState) {/****Insult To Enemy****/
			handleInsultToEnemy();
		} else if (currentState == enemyAttackState) {/****Enemy Attack State****/
			handleInsultToPlayer();
		}

		if(riledUpPercent == 100) {
			humdingerReady = true;
		} else {
			humdingerReady = false;
		}

	}

	void OnGUI(){


		/****Player Stats****/
		GUI.Label(new Rect(161, 32, 50, 30), HP.ToString(), statStyle);
		GUI.Label(new Rect(214, 32, 50, 30), HPMax.ToString(), statStyle);
		GUI.Label(new Rect(180, 90, 50, 30), riledUpPercent.ToString(), statStyle);

		/****Enemy Stats****/
		GUI.Label(new Rect(707, 290, 50, 30), enemyScript.HP.ToString(), statStyle);
		GUI.Label(new Rect(757, 290, 50, 30), enemyScript.MaxHP.ToString(), statStyle);

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
			GUI.Label(new Rect(245, 500, 100, 20), insults[0], defaultStyle);
			GUI.Label(new Rect(245, 550, 100, 20), insults[1], defaultStyle);
			GUI.Label(new Rect(495, 500, 100, 20), insults[2], defaultStyle);
			GUI.Label(new Rect(495, 550, 100, 20), insults[3], defaultStyle);

			if(currentSelection == 0) {
				GUI.Label(new Rect(225, 495, 100, 20), ">", selectorStyle);
			}
			if(currentSelection == 1) {
				GUI.Label(new Rect(225, 545, 100, 20), ">", selectorStyle);
			}
			if(currentSelection == 2) {
				GUI.Label(new Rect(475, 495, 100, 20), ">", selectorStyle);
			}
			if(currentSelection == 3) {
				GUI.Label(new Rect(475, 545, 100, 20), ">", selectorStyle);
			}
		}

		/****Handle insult to enemy****/
		if(currentState == handlingInsultState && insulted == false) {
			if(displayNumber == 0) {
				GUI.Label(new Rect(210, 500, 100, 20), insultToEnemyText, defaultStyle);
			} else if(displayNumber == 1) {
				GUI.Label(new Rect(210, 500, 100, 20), insultToEnemyResultText, defaultStyle);
			}
		}

		/****Enemy attack state****/
		if(currentState == enemyAttackState && insulted == false) {
			if(displayNumber == 0) {
				GUI.Label(new Rect(210, 500, 100, 20), enemyInsultText, defaultStyle);
			} else if(displayNumber == 1) {
				GUI.Label(new Rect(210, 500, 100, 20), playerResponseText, defaultStyle);
			}
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
			menuSelectionSound.GetComponent<AudioSource>().Play();
			if(currentSelection == insultSelection){
				currentState = insultState;
				currentSelection = 0;
			}
		}
	}

	void insultStateNavigation() {
		if(Input.GetKeyDown(KeyCode.W)) {
			menuSound.Play();
			if(currentSelection == 1) {
				currentSelection = 0;
			} else if(currentSelection == 3) {
				currentSelection = 2;
			}

		}
		if(Input.GetKeyDown(KeyCode.A)) {
			menuSound.Play();
			if(currentSelection == 2) {
				currentSelection = 0;
			} else if(currentSelection == 3) {
				currentSelection = 1;
			}
		}
		if(Input.GetKeyDown(KeyCode.S)) {
			menuSound.Play();
			if(currentSelection == 0) {
				currentSelection = 1;
			} else if(currentSelection == 2) {
				currentSelection = 3;
			}
		}
		if(Input.GetKeyDown(KeyCode.D)) {
			menuSound.Play();
			if(currentSelection == 0) {
				currentSelection = 2;
			} else if(currentSelection == 1) {
				currentSelection = 3;
			}
		}
		if(Input.GetKeyDown(KeyCode.E)) {
			menuSelectionSound.GetComponent<AudioSource>().Play();
			insultChosen = insults[currentSelection];
			insulted = true;
			currentState = handlingInsultState;
		}
	}

	void handleInsultToEnemy() {
		if(insulted) {
			insultToEnemyText = "You called " + enemyScript.enemyName + " " + insultChosen +  ".";
			int damage = Random.Range(0, 3);
			if(arrayContains(enemyScript.criticalWords, insultChosen)) {
				damage = Random.Range(3, 6);
			}

			if(damage == 0) {
				insultToEnemyResultText = enemyScript.enemyName + " wasn't really listening.";
			} else if(damage > 0 && damage <= 2) {
				insultToEnemyResultText = enemyScript.enemyName + " has low self esteem, and that " + System.Environment.NewLine + "didn't help.";
			} else if(damage >= 3 && damage <= 5) {
				insultToEnemyResultText = enemyScript.enemyName + " is suffering some severe " + System.Environment.NewLine + "emotional damage.";
			}
			displayNumber = 0;
			insulted = false;
			enemyScript.HP -= damage;
		}

		if(Input.GetKeyDown(KeyCode.E)) {
			textContinueSound.GetComponent<AudioSource>().Play();
			if(displayNumber == 1) {
				displayNumber = 0;
				currentState = enemyAttackState;
				insulted = true;
			} else {
				displayNumber++;
			}
		}
	}

	void handleInsultToPlayer() {
		if(insulted) {
			int insultIndx = Random.Range(0, 5);
			string insult = enemyScript.vocabulary[insultIndx];
			if(insult == "") {
				enemyInsultText = enemyScript.enemyName + " would say something, but he is just " + System.Environment.NewLine + "really into his lollypop right now.";
				playerResponseText = enemyScript.enemyName + "'s impartiality only strengthens " + System.Environment.NewLine + "your resolve.";
				riledUpPercent += Random.Range(5, 11);
			} else {
				int insultPower = wordToPower[insult];
				riledUpPercent += Random.Range((insultPower + 1)*5, (insultPower + 1)*11);

				enemyInsultText = enemyScript.enemyName + " called you " + insult;
				if(insultPower == 0) {
					playerResponseText = "What?";
				} else if(insultPower == 1) {
					HP -= Random.Range(1, 3);
					playerResponseText = "You don't really know what that means," + System.Environment.NewLine + "but you didn't like the tone.";
				}
			}
			insulted = false;
		}

		if(Input.GetKeyDown(KeyCode.E)) {
			textContinueSound.GetComponent<AudioSource>().Play();
			if(displayNumber == 1) {
				displayNumber = 0;
				currentState = defaultState;
				insulted = true;
				currentSelection = insultSelection;
			} else {
				displayNumber++;
			}
		}
	}

	bool arrayContains(string[] array, string str) {
		bool exists = false;
		for(int i = 0; i < array.Length; i++) {
			if(array[i] == str) {
				exists = true;
				break;
			}
		}
		return exists;
	}
}
