using UnityEngine;

public class EndTutorial : MonoBehaviour {
    private void OnTriggerEnter2D(Collider2D coll)
    {
        SaveLoad.SaveTutorial(true);
    }
}
