using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UFO;
public class UserGUI : MonoBehaviour {
    public int reset;
    GUIStyle style;
	GUIStyle buttonStyle;
    // Start is called before the first frame update
    void Start() {
        reset = 0;
        style = new GUIStyle();
		style.fontSize = 30;
		style.normal.textColor = Color.green;

		buttonStyle = new GUIStyle("button");
		buttonStyle.fontSize = 30;
		buttonStyle.normal.textColor = Color.green;
    }

    // Update is called once per frame
    void Update() {

    }

    private void OnGUI() {
        if(reset == 1) {
            if(GUI.Button(new Rect(615, 0, 120, 60), "Restart", buttonStyle)) {
                Director.getInstance().currentSceneController.Init();
                reset = 0;
            }
        }

        int round = Director.getInstance().currentSceneController.getSceneController().getRound();
        int total = Director.getInstance().currentSceneController.getSceneController().getTotal();
        int score = Director.getInstance().currentSceneController.getSceneController().getScore();

        string text = "Round: " + round.ToString() + "\nScores:  " + score.ToString();
        GUI.Label(new Rect(10, 10, Screen.width, 50),text,style);      
    }

}
