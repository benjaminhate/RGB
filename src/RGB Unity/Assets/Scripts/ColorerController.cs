using Objects;
using UnityEngine;

public class ColorerController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player")){
            other.GetComponent<ColorElement>().ChangeColor(GetComponent<ColorElement>().colorSo);
        }
    }
}
