using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;

public class MenuScript : MonoBehaviour {

	public Button volumeButton;
	public Button resetButton;
    public Button startButton;
    public Button settingsButton;
	bool volume;
    bool tutorial;

	public List<Category> categories;

	private bool settings;
	private Image confirm;
    private Image startTuto;

	void Start(){
		settings = true;
		confirm = resetButton.transform.Find ("Confirmation").GetComponent<Image> ();
        startTuto = startButton.transform.Find("StartTuto").GetComponent<Image>();
		MenuStart ();
	}

	void MenuStart(){
		SettingsGame ();
        PlayerData data = UpdateSavedData();
		volume = !data.getVolume ();
        tutorial = data.getTutorial();
		VolumeGame ();
		confirm.gameObject.SetActive (false);
        startTuto.gameObject.SetActive(false);
	}

    private PlayerData UpdateSavedData()
    {
        PlayerData data = SaveLoad.Load();
        if (data == null)
        {
            data = SaveLoad.SaveInit(categories);
        }
        else
        {
            PlayerData data_temp = data;
            List<Category> categories_temp = categories;
            foreach (Category cat_temp in categories_temp)
            {
                Category category = data.getCategoryWithName(cat_temp.getName());
                if (category != null)
                {
                    cat_temp.setBlocked(category.getBlocked());
                    cat_temp.setCompleted(category.getCompleted());
                    foreach (Level level_temp in cat_temp.getLevels())
                    {
                        Level level = category.getLevelWithName(level_temp.getName());
                        if (level != null)
                        {
                            level_temp.setBlocked(level.getBlocked());
                            level_temp.setCompleted(level.getCompleted());
                            level_temp.setTimer(level.getTimer());
                        }
                    }
                }
            }
            data_temp.setCategories(categories_temp);
            data = SaveLoad.Save(data_temp);
        }
        return data;
    }

	public void StartGame(){
        if (tutorial)
        {
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
    }

    public void ConfirmStartTuto()
    {
        startTuto.gameObject.SetActive(false);
        TutoGame();
    }

    public void CancelStartTuto()
    {
        startTuto.gameObject.SetActive(false);
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
        
	}

	public void VolumeGame(){
		Image volumeOn = volumeButton.transform.Find ("Volume").GetComponent<Image> ();
		Image volumeOff = volumeButton.transform.Find ("Volume off").GetComponent<Image> ();
		volume = !volume;
		volumeOn.gameObject.SetActive (volume);
		volumeOff.gameObject.SetActive (!volume);
		SaveLoad.SaveVolume (volume);
        GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioScript>().UpdateVolume();
	}

	public void ResetGame(){
		confirm.gameObject.SetActive (true);
	}

	public void ConfirmationResetGame(){
		SaveLoad.SaveInit (categories);
		settings = false;
		MenuStart ();
		confirm.gameObject.SetActive (false);
	}

	public void CancelResetGame(){
		confirm.gameObject.SetActive (false);
	}
}
