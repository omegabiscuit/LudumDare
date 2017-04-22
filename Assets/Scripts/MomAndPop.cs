using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class MomAndPop : MonoBehaviour {

	public GameObject textBox;
	private GUIStyle style = new GUIStyle();
	public string[] textLines;

	public string textToDisplay;

	public GameObject shart;
	bool shartDisplayed;



	int counter;

	// Use this for initialization
	void Start () {
		counter = 0;
		textToDisplay = textLines[0];
		textToDisplay = textToDisplay.Replace("@", System.Environment.NewLine);
		style.fontSize = 20;

		shartDisplayed = false;
	}
	
	// Update is called once per frame
	void Update () {

		if(counter == textLines.Length - 2) {
			style.fontSize = 20;
			style.fontStyle = FontStyle.Bold;
		} else if(counter == textLines.Length -1 && !shartDisplayed) {
			Instantiate(shart);
			shartDisplayed = true;
		}

		if (Input.GetKeyDown(KeyCode.E)) {
			if(counter < textLines.Length -1 ) {
				counter++;
    			textToDisplay = textLines[counter];
    			textToDisplay = textToDisplay.Replace("@", System.Environment.NewLine);

    			
    		} else {
    			textToDisplay = "";
    		}
		}
	}

	void OnGUI(){
		
		
		GUI.Label(new Rect(210, 500, 100, 20), textToDisplay, style);


 	}

 	void OnMouseDown()
    {
    	
    }
}
