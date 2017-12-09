using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTutorial : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D coll)
    {
        SaveLoad.SaveTutorial(true);
    }
}
