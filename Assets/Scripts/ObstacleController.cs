using System.Collections;
using Objects;
using UnityEngine;

public class ObstacleController : MonoBehaviour
{
	public float timeStop;
	protected bool stop;
	
	protected IEnumerator Wait(){
		stop = true;
		yield return new WaitForSeconds (timeStop);
		stop = false;
	}
	
	protected virtual void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("Player"))
		{
			var player = other.GetComponent<PlayerController>();
			if (!player.IsMoving) return;
        			
			player.obstacleCollide = true;
			player.obstacle = name;
			if (!GetComponent<ColorElement>().SameColor(other.GetComponent<ColorElement>().colorSo)) {
				player.Dead();
			}
		}
	}
}