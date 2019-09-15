using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Linq;
using Objects;

public class MenuScript : MonoBehaviour {

	public Button volumeButton;
	public Button resetButton;
    public Button startButton;
    public Button settingsButton;
    public Button languageButton;
    private bool volume;
    private bool tutorial;
    private string language;

	public List<Category> categories;

	private bool settings;
	private Image confirm;
    private Image startTuto;

    private GameObject tutoButton;
    private GameObject confirmationText;
    private GameObject resetConfirm;
    private GameObject resetCancel;
    private GameObject startTutoText;
    private GameObject startTutoConfirm;
    private GameObject startTutoCancel;

    private LanguageController lang;

    private void Awake()
    {
        tutoButton = GameObject.Find("TutoButton");
        confirmationText = GameObject.Find("ConfirmationText");
        resetConfirm = GameObject.Find("ResetConfirm");
        resetCancel = GameObject.Find("ResetCancel");
        startTutoText = GameObject.Find("StartTutoText");
        startTutoConfirm = GameObject.Find("StartTutoConfirm");
        startTutoCancel = GameObject.Find("StartTutoCancel");
    }

    private void Start(){
        Cursor.visible = true;
		settings = true;
		confirm = resetButton.transform.Find ("Confirmation").GetComponent<Image> ();
        startTuto = startButton.transform.Find("StartTuto").GetComponent<Image>();
		MenuStart ();
	}

    private void Update()
    {
        tutoButton.GetComponentInChildren<Text>().text = lang.GetString("tutorial");
        confirmationText.GetComponent<Text>().text = lang.GetString("resetConfirmationText");
        resetConfirm.GetComponentInChildren<Text>().text = lang.GetString("yes");
        resetCancel.GetComponentInChildren<Text>().text = lang.GetString("no");
        startTutoText.GetComponent<Text>().text = lang.GetString("startTutoConfirmationText");
        startTutoConfirm.GetComponentInChildren<Text>().text = lang.GetString("yes");
        startTutoCancel.GetComponentInChildren<Text>().text = lang.GetString("no");
    }

    private void MenuStart(){
		SettingsGame ();
        var data = UpdateSavedData();
        if(GameObject.FindGameObjectWithTag("First Time"))
        {
            data = SaveLoad.SaveFirstTime(true);
            Destroy(GameObject.FindGameObjectWithTag("First Time"));
        }
		volume = !data.GetVolume ();
        tutorial = data.GetTutorial();
        language = data.GetLanguage();
        lang = new LanguageController(language);
        UpdateLanguageImages();
        VolumeGame ();
		confirm.gameObject.SetActive (false);
        startTuto.gameObject.SetActive(false);
	}

    private PlayerData UpdateSavedData()
    {
        var data = SaveLoad.Load();
        Debug.Log(data);
        if (data == null || data.GetCategories() == null)
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
                var category = data.GetCategoryWithName(catTemp.GetName());
                if (category == null) continue;
                
                catTemp.SetBlocked(category.GetBlocked());
                catTemp.SetCompleted(category.GetCompleted());
                foreach (var levelTemp in catTemp.GetLevels())
                {
                    var level = category.GetLevelWithName(levelTemp.GetName());
                    if (level == null) continue;
                    
                    levelTemp.SetBlocked(level.GetBlocked());
                    levelTemp.SetCompleted(level.GetCompleted());
                    levelTemp.SetTimer(level.GetTimer());
                }
            }
            dataTemp.SetCategories(categoriesTemp);
            data = SaveLoad.Save(dataTemp);
        }
        return data;
    }

	public void StartGame(){
        if (tutorial)
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
		settings = !settings;
		volumeButton.gameObject.SetActive (settings);
		resetButton.gameObject.SetActive (settings);
        languageButton.gameObject.SetActive (settings);
	}

    public void LanguageGame()
    {
        var languages = new ArrayList();
        foreach(Transform child in languageButton.transform)
        {
            languages.Add(child.name);
        }
        var index = languages.IndexOf(language) + 1;
        if (index >= languages.Count) index = 0;
        language = (string) languages[index];
        UpdateLanguageImages();
        Debug.Log(language);
        SaveLoad.SaveLanguage(language);
        lang.SetLanguage(language);
    }

    private void UpdateLanguageImages()
    {
        foreach(Transform child in languageButton.transform)
        {
            child.gameObject.SetActive(false);
        }
        languageButton.transform.Find(language).gameObject.SetActive(true);
    }


    public void VolumeGame(){
		var volumeOn = volumeButton.transform.Find ("Volume").GetComponent<Image> ();
		var volumeOff = volumeButton.transform.Find ("Volume off").GetComponent<Image> ();
		volume = !volume;
		volumeOn.gameObject.SetActive (volume);
		volumeOff.gameObject.SetActive (!volume);
		SaveLoad.SaveVolume (volume);
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
		settings = false;
		MenuStart ();
		confirm.gameObject.SetActive (false);
	}

	public void CancelResetGame(){
		confirm.gameObject.SetActive (false);
	}
}
