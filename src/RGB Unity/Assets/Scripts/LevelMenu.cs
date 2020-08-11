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
	private List<Category> _categories;

	private int _actualScroll;
	private int _maxScroll;
	private int _selectorWidth;
	private int _selectorHeight;

	private Category GetCategoryWithName(string categoryName)
	{
		return _categories.FirstOrDefault(category => string.Compare(category.name, categoryName, StringComparison.Ordinal) == 0);
	}

	private void SetCategoryMenu(List<Category> categories) {
		menuText.text = "Menu";
		returnButton.onClick.RemoveAllListeners ();
		returnButton.onClick.AddListener (() => {
			SceneManager.LoadScene("StartMenu",LoadSceneMode.Single);
		});
		var i = 0;
		_maxScroll = 0;
		foreach (var category in categories) {
			_maxScroll += 1;
			
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

			var blocked = category.blocked;
			
			lockImg.gameObject.SetActive(blocked);
			categorySelName.gameObject.SetActive(!blocked);
			categorySelLevels.gameObject.SetActive(!blocked);
			categorySelLevelCount.gameObject.SetActive(!blocked);
			completedImg.gameObject.SetActive(!blocked);
			categorySel.interactable = !blocked;
			
			if (!blocked)
			{
				completedImg.gameObject.SetActive(category.completed);
				categorySelName.text = category.name;
				categorySelLevels.text = "Levels";
				categorySelLevelCount.text = category.GetUnblockedLevels().Count + " / " + category.levels.Count;
				categorySel.onClick.AddListener(() => { OnClickCategoryButton(categories, category); });
			}
		}
	}

	private void SetLevelMenu(List<Category> categories,Category category){
		returnButton.onClick.RemoveAllListeners ();
		returnButton.onClick.AddListener (() => {
			ResetMenuButton();
			SetCategoryMenu(categories);
		});
		
		if (category == null) return;
		
		menuText.text = category.name;
		var i = 0;
		_maxScroll = 0;
		foreach (var level in category.levels) {
			_maxScroll += 1;
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

			var blocked = level.blocked;
			var completed = level.completed;
				
			lockImg.gameObject.SetActive (blocked);
			levelSelName.gameObject.SetActive (!blocked);
			levelSelTimer.gameObject.SetActive (!blocked);
			completedImg.gameObject.SetActive (!blocked && completed);
			levelSel.interactable = !blocked;
				
			if (!blocked) {
				levelSelName.text = level.name;
				if (completed) {
					levelSelTimer.text = "Best time";
					levelSelTimerText.text = $"{Mathf.Round (level.timer * 100f) / 100f}";
				} else {
					levelSelTimer.gameObject.SetActive (false);
				}
				levelSel.onClick.AddListener (() => {
					OnClickLevelButton (level);
				});
			}
		}
	}

	private void ResetMenuButton(){
		_actualScroll = 1;
		selector.GetComponent<RectTransform> ().sizeDelta = _selectorWidth * Vector2.right + _selectorHeight * Vector2.up;
		var menuButtonList = selector.GetComponentsInChildren<Button> ();
		foreach (var menuButton in menuButtonList) {
			Destroy (menuButton.gameObject);
		}
	}

	private void Start () {
		_actualScroll = 1;
		_selectorWidth = 100;
		_selectorHeight = 90;
		var data = SaveLoad.Load ();
		if (data != null) {
			SetCategoryMenu (data.categories);
			_categories = data.GetCategories ();
		} else {
			SetCategoryMenu (_categories);
			SaveLoad.SaveInit (_categories);
		}
	}

	private void OnClickCategoryButton(List<Category> categories,Category category){
		ResetMenuButton ();
		SetLevelMenu (categories, category);
	}

	private void OnClickLevelButton(Level level)
	{
		var sceneName = level.sceneName;
        Debug.Log(sceneName);
        var levelGameObject = new GameObject(sceneName);
        levelGameObject.gameObject.tag = "LevelSelector";
        DontDestroyOnLoad(levelGameObject);
		SceneManager.LoadScene (sceneName, LoadSceneMode.Single);
	}

	public void OnClickRightButton(){
		var levelSelectors = selector.GetComponentsInChildren<Button> ();
		if (_actualScroll >= _maxScroll) return;
		
		foreach (var button in levelSelectors) {
			button.GetComponent<RectTransform>().localPosition -=
				(button.GetComponent<RectTransform>().rect.width + 10) * Vector3.right;
		}
		_actualScroll += 1;
	}

	public void OnClickLeftButton(){
		var levelSelectors = selector.GetComponentsInChildren<Button> ();
		if (_actualScroll <= 1) return;
		
		foreach (var button in levelSelectors) {
			button.GetComponent<RectTransform>().localPosition +=
				(button.GetComponent<RectTransform>().rect.width + 10) * Vector3.right;
		}
		_actualScroll -= 1;
	}
	

}
