using UnityEngine;
using System.Collections;

public class ChangePlayerColor : MonoBehaviour {
    void OnCollisionEnter2D(Collision2D collission)
    {
        GetComponent<SpriteRenderer>().color = Color.red;
    }
    void OnCollisionExit2D(Collision2D collission)
    {
        GetComponent<SpriteRenderer>().color = Color.black;
    }
}
