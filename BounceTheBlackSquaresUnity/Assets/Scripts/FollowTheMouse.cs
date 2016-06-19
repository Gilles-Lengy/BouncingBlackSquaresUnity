using UnityEngine;
using System.Collections;
 
public class FollowTheMouse : MonoBehaviour
{

    private Vector3 mousePosition;
    public float moveSpeed = 0.1f;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            mousePosition = Input.mousePosition;
            mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
            transform.position = Vector2.Lerp(transform.position, mousePosition, moveSpeed);
        }

    }
}

// http://forum.unity3d.com/threads/make-object-follow-mouse-2d-game.211186/ // Script d'origine, j'ai remplacé Input.GetMouseButton(1) par Input.GetMouseButton(0)...