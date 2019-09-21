using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using Objects;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelMenu : MonoBehaviour {

	public Canvas canvas;
	public Image selector;
	public Button levelSelector;
	public Button categorySelector;
	public Button returnButton;
	public TextMeshProUGUI menuText;
	private List<Category> categories;

	private int actualScroll;
	private int maxScroll;
	private int selectorWidth;
	private int selectorHeight;

	private Category GetCategoryWithName(string categoryName)
	{
		return categories.FirstOrDefault(category => string.Compare(category.GetName(), categoryName, StringComparison.Ordinal) == 0);
	}

	private void SetCategoryMenu(List<Category> categories) {
		menuText.text = "Menu";
		returnButton.onClick.RemoveAllListeners ();
		returnButton.onClick.AddListener (() => {
			SceneManager.LoadScene("StartMenu",LoadSceneMode.Single);
		});
		var i = 0;
		maxScroll = 0;
		foreach (var category in categories) {
			maxScroll += 1;
			var categorySel = Instantiate (categorySelector, selector.transform);
			categorySel.gameObject.SetActive (true);
            var value = categorySel.GetComponent<RectTransform>().rect.width + 10;
			selector.GetComponent<RectTransform> ().sizeDelta += i * value * Vector2.right;
			categorySel.GetComponent<RectTransform> ().localPosition += value * i * Vector3.right;
			i += 1;
			var lockImg = categorySel.transform.Find ("Lock").GetComponent<Image> ();
			var completedImg = categorySel.transform.Find ("Completed").GetComponent<Image> ();
			var categorySelName = categorySel.transform.Find ("Name").GetComponent<TextMeshProUGUI> ();
			var categorySelLevels = categorySel.transform.Find ("Levels").GetComponent<TextMeshProUGUI> ();
			var categorySelLevelCount = categorySelLevels.transform.Find ("Level Count").GetComponent<TextMeshProUGUI> ();
			if (category.GetBlocked ()) {
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
				if (category.GetCompleted ()) {
					completedImg.gameObject.SetActive (true);
				} else {
					completedImg.gameObject.SetActive (false);
				}
				categorySelName.text = category.GetName ();
				categorySelLevels.text = "Levels";
				categorySelLevelCount.text = category.GetUnblockedLevels ().Count + " / " + category.GetLevels ().Count;
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
			menuText.text = category.GetName ();
			var i = 0;
			maxScroll = 0;
			foreach (var level in category.GetLevels()) {
				maxScroll += 1;
				var levelSel = Instantiate (levelSelector, selector.transform);
				levelSel.gameObject.SetActive (true);
                var value = levelSel.GetComponent<RectTransform>().rect.width + 10;
				selector.GetComponent<RectTransform> ().sizeDelta += i * value * Vector2.right;
				levelSel.GetComponent<RectTransform> ().localPosition += value * i * Vector3.right;
				i += 1;
				var lockImg = levelSel.transform.Find ("Lock").GetComponent<Image> ();
				var completedImg = levelSel.transform.Find ("Completed").GetComponent<Image> ();
				var levelSelName = levelSel.transform.Find ("Name").GetComponent<TextMeshProUGUI> ();
				var levelSelTimer = levelSel.transform.Find ("Timer").GetComponent<TextMeshProUGUI> ();
				var levelSelTimerText = levelSelTimer.transform.Find ("Timer Text").GetComponent<TextMeshProUGUI> ();
				if (level.GetBlocked ()) {
					lockImg.gameObject.SetActive (true);
					levelSelName.gameObject.SetActive (false);
					levelSelTimer.gameObject.SetActive (false);
					completedImg.gameObject.SetActive (false);
					levelSel.interactable = false;
				} else {
					lockImg.gameObject.SetActive (false);
					levelSelName.gameObject.SetActive (true);
					levelSelTimer.gameObject.SetActive (true);
					levelSelName.text = level.GetName ();
					if (level.GetCompleted()) {
						levelSelTimer.text = "Best time";
						levelSelTimerText.text = (Mathf.Round (level.GetTimer () * 100f) / 100f).ToString ();
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
		var menuButtonList = selector.GetComponentsInChildren<Button> ();
		foreach (var menuButton in menuButtonList) {
			Destroy (menuButton.gameObject);
		}
	}

	private void Start () {
		actualScroll = 1;
		selectorWidth = 100;
		selectorHeight = 90;
		var data = SaveLoad.Load ();
		if (data != null) {
			SetCategoryMenu (data.categories);
			categories = data.GetCategories ();
		} else {
			SetCategoryMenu (categories);
			SaveLoad.SaveInit (categories);
		}
	}

	private void OnClickCategoryButton(List<Category> categories,Category category){
		ResetMenuButton ();
		SetLevelMenu (categories, category);
	}

	private void OnClickLevelButton(Level level)
	{
		var sceneName = level.GetSceneName();
        Debug.Log(sceneName);
        var levelGameObject = new GameObject(sceneName);
        levelGameObject.gameObject.tag = "LevelSelector";
        DontDestroyOnLoad(levelGameObject);
		SceneManager.LoadScene (sceneName, LoadSceneMode.Single);
	}

	public void OnClickRightButton(){
		var levelSelectors = selector.GetComponentsInChildren<Button> ();
		if (actualScroll >= maxScroll) return;
		
		foreach (var button in levelSelectors) {
			button.GetComponent<RectTransform>().localPosition -=
				(button.GetComponent<RectTransform>().rect.width + 10) * Vector3.right;
		}
		actualScroll += 1;
	}

	public void OnClickLeftButton(){
		var levelSelectors = selector.GetComponentsInChildren<Button> ();
		if (actualScroll <= 1) return;
		
		foreach (var button in levelSelectors) {
			button.GetComponent<RectTransform>().localPosition +=
				(button.GetComponent<RectTransform>().rect.width + 10) * Vector3.right;
		}
		actualScroll -= 1;
	}
	

}
