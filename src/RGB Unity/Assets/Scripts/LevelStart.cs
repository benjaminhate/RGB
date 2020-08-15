using System.Collections;
using Objects;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelStart : MonoBehaviour {

	public float startX;
	public float startY;
	public float startRot;

	public PlayerController player;
	
	private LevelManager _levelManager;
	private static readonly int IsSpawnAnimator = Animator.StringToHash("IsSpawn");
	private static readonly int FaceYAnimator = Animator.StringToHash("FaceY");
	private static readonly int FaceXAnimator = Animator.StringToHash("FaceX");

	private void Start(){
		Cursor.visible = false;
		_levelManager = LevelManager.Instance;
		var levelName = _levelManager.LevelName;
        Debug.Log(levelName);
        SaveLoad.SaveLevel (levelName);
		PlacePlayer ();
	}

	private void Update(){
		if (player.respawn) {
			Respawn();
		}
	}

	private void Respawn(){
		PlacePlayer ();
		player.respawn = false;
		player.Animator.SetBool(IsSpawnAnimator, true);
		player.Animator.SetFloat(FaceYAnimator,1);
		player.Animator.SetFloat(FaceXAnimator,0);
	}

	public void OnPlayerRespawnAnimationEnd()
	{
		player.Animator.SetBool(IsSpawnAnimator, false);
		player.respawn = false;
		player.dead = false;
	}

	private void PlacePlayer(){
		player.transform.position = new Vector3 (startX, startY, 0);
		player.transform.eulerAngles = new Vector3 (0, 0, startRot);
        player.GetComponent<ColorElement>().ChangeColor(GetComponent<ColorElement>().colorSo);
	}
}