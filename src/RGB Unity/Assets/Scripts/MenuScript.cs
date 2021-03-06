using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Linq;
using Objects;
using TMPro;

public class MenuScript : MonoBehaviour {

	[Header("Buttons")]
	public Button volumeButton;
	public Button resetButton;
    public Button languageButton;
    
    private bool _volume;
    private bool _tutorial;
    private string _language;
    private bool _settings;
    private LanguageController _lang;
    
    [Header("Levels")]
	public List<Category> categories;
	
	[Header("Images")]
	[SerializeField] private Image confirm;
	[SerializeField] private Image startTuto;

    [Header("Texts")]
    [SerializeField] private TextMeshProUGUI tutoButtonText;
    [SerializeField] private TextMeshProUGUI confirmationText;
    [SerializeField] private TextMeshProUGUI resetConfirmText;
    [SerializeField] private TextMeshProUGUI resetCancelText;
    [SerializeField] private TextMeshProUGUI startTutoText;
    [SerializeField] private TextMeshProUGUI startTutoConfirmText;
    [SerializeField] private TextMeshProUGUI startTutoCancelText;

    private void Start(){
        Cursor.visible = true;
		_settings = true;
		MenuStart ();
	}

    private void UpdateLanguageText()
    {
	    tutoButtonText.text = _lang.GetString("tutorial");
	    confirmationText.text = _lang.GetString("resetConfirmationText");
	    resetConfirmText.text = _lang.GetString("yes");
	    resetCancelText.text = _lang.GetString("no");
	    startTutoText.text = _lang.GetString("startTutoConfirmationText");
	    startTutoConfirmText.text = _lang.GetString("yes");
	    startTutoCancelText.text = _lang.GetString("no");
    }

    private void MenuStart(){
		SettingsGame ();
        var data = UpdateSavedData();
        if(GameObject.FindGameObjectWithTag("First Time"))
        {
            data = SaveLoad.SaveFirstTime(true);
            Destroy(GameObject.FindGameObjectWithTag("First Time"));
        }
		_volume = !data.GetVolume ();
        _tutorial = data.GetTutorial();
        _language = data.GetLanguage();
        _lang = new LanguageController();
        UpdateLanguage();
        VolumeGame ();
		confirm.gameObject.SetActive (false);
        startTuto.gameObject.SetActive(false);
	}

    private PlayerData UpdateSavedData()
    {
        var data = SaveLoad.Load();
        Debug.Log(data);
        if (data?.GetCategories() == null)
        {
            Debug.Log("Save Init");
            Debug.Log(categories);
            data = SaveLoad.SaveInit(categories);
        }
        else
        {
            var dataTemp = data;
            var categoriesTemp = categories.Select(cat => cat.Clone()).ToList();
            foreach (var catTemp in categoriesTemp)
            {
                var category = data.GetCategoryWithName(catTemp.name);
                if (category == null) continue;
                
                catTemp.blocked = category.blocked;
                catTemp.completed = category.completed;
                foreach (var levelTemp in catTemp.levels)
                {
                    var level = category.GetLevelWithName(levelTemp.name);
                    if (level == null) continue;
                    
                    levelTemp.blocked = level.blocked;
                    levelTemp.completed = level.completed;
                    levelTemp.timer = level.timer;
                }
            }
            dataTemp.SetCategories(categoriesTemp);
            data = SaveLoad.Save(dataTemp);
        }
        return data;
    }

	public void StartGame(){
        if (_tutorial)
        {
            SaveLoad.SaveTutorial(true);
            SceneManager.LoadScene ("LevelMenu", LoadSceneMode.Single);
        }
        else
        {
            StartTuto();
        }
	}

    private void StartTuto()
    {
        startTuto.gameObject.SetActive(true);
        confirm.gameObject.SetActive(false);
    }

    public void ConfirmStartTuto()
    {
        startTuto.gameObject.SetActive(false);
        TutoGame();
    }

    public void CancelStartTuto()
    {
        startTuto.gameObject.SetActive(false);
        SaveLoad.SaveTutorial(true);
        SceneManager.LoadScene("LevelMenu", LoadSceneMode.Single);
    }

    public void TutoGame()
    {
        SceneManager.LoadScene("LevelTuto", LoadSceneMode.Single);
    }

	public void QuitGame(){
		Application.Quit ();
	}

	public void SettingsGame(){
		_settings = !_settings;
		volumeButton.gameObject.SetActive (_settings);
		resetButton.gameObject.SetActive (_settings);
        languageButton.gameObject.SetActive (_settings);
	}

    public void LanguageGame()
    {
        var languages = new ArrayList();
        foreach(Transform child in languageButton.transform)
        {
            languages.Add(child.name);
        }
        var index = languages.IndexOf(_language) + 1;
        if (index >= languages.Count) index = 0;
        _language = (string) languages[index];
        Debug.Log(_language);
        SaveLoad.SaveLanguage(_language);
        UpdateLanguage();
    }

    private void UpdateLanguage()
    {
	    _lang.SetLanguage(_language);
	    UpdateLanguageImages();
	    UpdateLanguageText();
    }

    private void UpdateLanguageImages()
    {
        foreach(Transform child in languageButton.transform)
        {
            child.gameObject.SetActive(false);
        }
        languageButton.transform.Find(_language).gameObject.SetActive(true);
    }


    public void VolumeGame(){
		var volumeOn = volumeButton.transform.Find ("Volume").GetComponent<Image> ();
		var volumeOff = volumeButton.transform.Find ("Volume off").GetComponent<Image> ();
		_volume = !_volume;
		volumeOn.gameObject.SetActive (_volume);
		volumeOff.gameObject.SetActive (!_volume);
		SaveLoad.SaveVolume (_volume);
        GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioScript>().UpdateVolume();
	}

	public void ResetGame(){
		confirm.gameObject.SetActive (true);
        startTuto.gameObject.SetActive(false);
	}

	public void ConfirmationResetGame(){
        foreach(var cat in categories)
        {
            Debug.Log(cat.ToString());
        }
		SaveLoad.SaveInit (categories);
		_settings = false;
		MenuStart ();
		confirm.gameObject.SetActive (false);
	}

	public void CancelResetGame(){
		confirm.gameObject.SetActive (false);
	}
}
