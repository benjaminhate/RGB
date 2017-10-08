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
	public Image selector;
	public Button levelSelector;
	public Button categorySelector;
	public Button returnButton;
	public Text menuText;
	private List<Category> categories;

	private int actualScroll;
	private int maxScroll;
	private int selectorWidth;
	private int selectorHeight;

	private Category GetCategoryWithName(string name) {
		foreach (Category category in categories) {
			if (category.getName ().CompareTo (name) == 0) {
				return category;
			}
		}
		return null;
	}

	private void SetCategoryMenu(List<Category> categories) {
		menuText.text = "Menu";
		returnButton.onClick.RemoveAllListeners ();
		returnButton.onClick.AddListener (() => {
			SceneManager.LoadScene("StartMenu",LoadSceneMode.Single);
		});
		int i = 0;
		maxScroll = 0;
		foreach (Category category in categories) {
			maxScroll += 1;
			Button categorySel = Instantiate (categorySelector, selector.transform);
			categorySel.gameObject.SetActive (true);
			selector.GetComponent<RectTransform> ().sizeDelta += i * 70 * Vector2.right;
			categorySel.GetComponent<RectTransform> ().localPosition += 70 * i * Vector3.right;
			i += 1;
			Image lockImg = categorySel.transform.Find ("Lock").GetComponent<Image> ();
			Image completedImg = categorySel.transform.Find ("Completed").GetComponent<Image> ();
			Text categorySelName = categorySel.transform.Find ("Name").GetComponent<Text> ();
			Text categorySelLevels = categorySel.transform.Find ("Levels").GetComponent<Text> ();
			Text categorySelLevelCount = categorySelLevels.transform.Find ("Level Count").GetComponent<Text> ();
			if (category.getBlocked ()) {
				lockImg.gameObject.SetActive (true);
				categorySelName.gameObject.SetActive (false);
				categorySelLevels.gameObject.SetActive (false);
				categorySelLevelCount.gameObject.SetActive (false);
				completedImg.gameObject.SetActive (false);
				categorySel.interactable = false;
			} else {
				lockImg.gameObject.SetActive (false);
				categorySelName.gameObject.SetActive (true);
				categorySelLevels.gameObject.SetActive (true);
				categorySelLevelCount.gameObject.SetActive (true);
				if (category.getCompleted ()) {
					completedImg.gameObject.SetActive (true);
				} else {
					completedImg.gameObject.SetActive (false);
				}
				categorySelName.text = category.getName ();
				categorySelLevels.text = "Levels";
				categorySelLevelCount.text = category.getUnblockedLevels ().Count + " / " + category.getLevels ().Count;
				categorySel.interactable = true;
				categorySel.onClick.AddListener (() => {
					OnClickCategoryButton (categories,category);
				});
			}
		}
	}

	private void SetLevelMenu(List<Category> categories,Category category){
		returnButton.onClick.RemoveAllListeners ();
		returnButton.onClick.AddListener (() => {
			ResetMenuButton();
			SetCategoryMenu(categories);
		});
		if (category != null) {
			menuText.text = category.getName ();
			int i = 0;
			maxScroll = 0;
			foreach (Level level in category.getLevels()) {
				maxScroll += 1;
				Button levelSel = Instantiate (levelSelector, selector.transform);
				levelSel.gameObject.SetActive (true);
				selector.GetComponent<RectTransform> ().sizeDelta += i * 70 * Vector2.right;
				levelSel.GetComponent<RectTransform> ().localPosition += 70 * i * Vector3.right;
				i += 1;
				Image lockImg = levelSel.transform.Find ("Lock").GetComponent<Image> ();
				Image completedImg = levelSel.transform.Find ("Completed").GetComponent<Image> ();
				Text levelSelName = levelSel.transform.Find ("Name").GetComponent<Text> ();
				Text levelSelTimer = levelSel.transform.Find ("Timer").GetComponent<Text> ();
				Text levelSelTimerText = levelSelTimer.transform.Find ("Timer Text").GetComponent<Text> ();
				if (level.getBlocked ()) {
					lockImg.gameObject.SetActive (true);
					levelSelName.gameObject.SetActive (false);
					levelSelTimer.gameObject.SetActive (false);
					completedImg.gameObject.SetActive (false);
					levelSel.interactable = false;
				} else {
					lockImg.gameObject.SetActive (false);
					levelSelName.gameObject.SetActive (true);
					levelSelTimer.gameObject.SetActive (true);
					levelSelName.text = level.getName ();
					if (level.getCompleted()) {
						levelSelTimer.text = "Best time";
						levelSelTimerText.text = (Mathf.Round (level.getTimer () * 100f) / 100f).ToString ();
						completedImg.gameObject.SetActive (true);
					} else {
						levelSelTimer.gameObject.SetActive (false);
						completedImg.gameObject.SetActive (false);
					}
					levelSel.interactable = true;
					levelSel.onClick.AddListener (() => {
						OnClickLevelButton (level);
					});
				}
			}
		}
	}

	private void ResetMenuButton(){
		actualScroll = 1;
		selector.GetComponent<RectTransform> ().sizeDelta = selectorWidth * Vector2.right + selectorHeight * Vector2.up;
		Button[] menuButtonList = selector.GetComponentsInChildren<Button> ();
		foreach (Button menuButton in menuButtonList) {
			Destroy (menuButton.gameObject);
		}
	}

	void Start () {
		actualScroll = 1;
		selectorWidth = 100;
		selectorHeight = 90;
		PlayerData data = SaveLoad.Load ();
		if (data != null) {
			SetCategoryMenu (data.categories);
			categories = data.getCategories ();
		} else {
			SetCategoryMenu (categories);
			SaveLoad.SaveInit (categories);
		}
	}

	void OnClickCategoryButton(List<Category> categories,Category category){
		ResetMenuButton ();
		SetLevelMenu (categories, category);
	}

	void OnClickLevelButton(Level level){
		SceneManager.LoadScene (level.getSceneName(), LoadSceneMode.Single);
	}

	public void OnClickRightButton(){
		Button[] levelSelectors = selector.GetComponentsInChildren<Button> ();
		if (actualScroll < maxScroll) {
			foreach (Button button in levelSelectors) {
				button.GetComponent<RectTransform> ().localPosition -= 70 * Vector3.right;
			}
			actualScroll += 1;
		}
	}

	public void OnClickLeftButton(){
		Button[] levelSelectors = selector.GetComponentsInChildren<Button> ();
		if (actualScroll > 1) {
			foreach (Button button in levelSelectors) {
				button.GetComponent<RectTransform> ().localPosition += 70 * Vector3.right;
			}
			actualScroll -= 1;
		}
	}
	

}
