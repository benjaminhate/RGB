using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class LevelMenu : MonoBehaviour {

	public Canvas canvas;
	public Button levelButton;
	public Text levelText;
	public Button leftButton;
	public Button rightButton;
	public Text menuText;
	int divisionNum = 4;
	int numCol = 4;
	int numLine = 3;
	public List<string> LevelList;

	private List<string> GetLevelList (string levelDir){
		List<string> levelArr = new List<string> ();
		foreach (string name in LevelList) {
			if (name.Substring (name.Length - 1).CompareTo (levelDir.Substring (0, 1)) == 0)
				levelArr.Add (name);
		}
		return levelArr;
	}

	private List<int> GetLevelCount(List<string> levelArr){
		List<int> levelCount = new List<int> ();
		for (int i = 0; i < 3; i++)
			levelCount.Add (0);
		
		foreach (string levelName in levelArr) {
			string levelType = levelName.Substring (levelName.Length - 1);
			if (levelType.Contains ("E"))
				levelCount [0] += 1;
			if (levelType.Contains ("M"))
				levelCount [1] += 1;
			if (levelType.Contains ("H"))
				levelCount [2] += 1;
		}

		return levelCount;
	}

	private List<int> EuclideanDivision(int number){
		int q = 0;
		int r = 0;
		List<int> euclideanDiv = new List<int> ();

		while (number > divisionNum) {
			number -= divisionNum;
			q += 1;
		}
		r = number;
		euclideanDiv.Add (q);
		euclideanDiv.Add (r);

		return euclideanDiv;
	}

	private List<int> GetLevelPosition(string levelName){
		List<int> levelPos = new List<int> ();

		string levelStringNum = levelName.Substring (5);
		levelStringNum = levelStringNum.Substring (0, levelStringNum.Length - 1);
		int levelNum = int.Parse (levelStringNum);
		List<int> euclidDiv = EuclideanDivision (levelNum);
		levelPos.Add (euclidDiv [0] + 1);
		levelPos.Add (euclidDiv [1]);

		return levelPos;
	}

	private string GetLevelNum(string levelName){
		string num = levelName.Substring (5);
		num=num.Substring (0, num.Length - 1);
		return num;
	}

	private float GetScale(int num,float size,float buttonSize){
		float result = (size - num * buttonSize) / (num + 1);
		return result;
	}

	private float GetPos(int pos,float scale,float size){
		return pos*scale+(pos-1)*size;
	}

	private Scene SearchLevelNameInSave(string levelName){
		PlayerData data = SaveLoad.Load ();
		if (data != null) {
			foreach (Scene scene in data.scenes) {
				if (scene.getName().CompareTo (levelName) == 0)
					return scene;
			}
		}
		return null;
	}

	private void SetMenu(string levelType){
		List<string> levelArr = GetLevelList(levelType);
		menuText.text = levelType;
		foreach (string levelName in levelArr) {
			List<int> levelPos = GetLevelPosition (levelName);
			Vector3 vectorPos = new Vector3 (0, 0, 0);
			Button levelInsButton = Instantiate (levelButton, vectorPos, Quaternion.identity);
			Text levelInsText = Instantiate (levelText, vectorPos, Quaternion.identity);
			Scene scene = SearchLevelNameInSave (levelName);
			levelInsText.name = "TimerText";
			if (scene != null)
				levelInsText.text = scene.getTimer ().ToString ();
			else
				levelInsText.text = "";
			if (levelName.CompareTo ("Level1E") == 0 || scene != null) {
				levelInsButton.GetComponentInChildren<Text> ().text = GetLevelNum (levelName);
				levelInsButton.name = levelName;
				levelInsButton.onClick.AddListener (OnClickLevelButton);
				levelInsButton.interactable = true;
			}else{
				levelInsButton.GetComponentInChildren<Text> ().text = "";
			}

			RectTransform rectTransButt = levelInsButton.GetComponent<RectTransform> ();
			RectTransform rectTransText = levelInsText.GetComponent<RectTransform> ();
			rectTransButt.SetParent (canvas.transform);
			rectTransText.SetParent (levelInsButton.transform);
			RectTransform canvasRect = canvas.GetComponent<RectTransform> ();

			canvasRect.pivot = new Vector2 (0.1f, 0.825f);
			rectTransButt.anchorMin = new Vector2 (0f, 1f);
			rectTransButt.anchorMax = new Vector2 (0f, 1f);
			rectTransButt.pivot = new Vector2 (0f, 1f);
			rectTransButt.localScale *= canvas.scaleFactor;

			float posW = GetPos (levelPos [1], GetScale (numCol, canvasRect.rect.width*0.8f, rectTransButt.rect.width), rectTransButt.rect.width);
			float posH = GetPos (levelPos [0], GetScale (numLine, canvasRect.rect.height*0.8f, rectTransButt.rect.height), rectTransButt.rect.height);
			rectTransButt.localPosition = new Vector3 (posW,-posH, 0);
			rectTransText.anchoredPosition = new Vector3 (0, -rectTransButt.rect.height/2-2, 0);
		}

		if (levelType.CompareTo ("Easy") == 0) {
			leftButton.gameObject.SetActive (false);
			rightButton.gameObject.SetActive (true);
			rightButton.name = "Medium";
		}
		if (levelType.CompareTo ("Medium") == 0) {
			leftButton.gameObject.SetActive (true);
			leftButton.name = "Easy";
			rightButton.gameObject.SetActive (true);
			rightButton.name = "Hard";
		}
		if (levelType.CompareTo ("Hard") == 0) {
			leftButton.gameObject.SetActive (true);
			leftButton.name = "Medium";
			rightButton.gameObject.SetActive (false);
		}
	}

	private void ResetLevelButton(){
		Button[] levelButtonList = canvas.GetComponentsInChildren<Button> ();
		foreach (Button levelButton in levelButtonList) {
			if (levelButton.name.Contains ("Level")) {
				Destroy (levelButton.gameObject);
			}
		}
	}

	void Start () {
		PlayerData data = SaveLoad.Load ();
		if (data != null) {
			string lastLevelName = data.scenes[data.scenes.Count-1].getName();
			Debug.Log (lastLevelName);
			string lastLevelType = lastLevelName.Substring (lastLevelName.Length - 1);
			if (lastLevelType.CompareTo ("H") == 0) {
				SetMenu ("Hard");
			} else if (lastLevelType.CompareTo ("M") == 0) {
				SetMenu ("Medium");
			} else {
				SetMenu ("Easy");
			}
		} else {
			SetMenu ("Easy");
		}
	}

	void OnClickLevelButton(){
		string levelName = EventSystem.current.currentSelectedGameObject.name;
		SceneManager.LoadScene (levelName, LoadSceneMode.Single);
	}

	public void OnClickChangeButton(){
		ResetLevelButton ();
		string levelType = EventSystem.current.currentSelectedGameObject.name;
		SetMenu (levelType);
	}
	

}
