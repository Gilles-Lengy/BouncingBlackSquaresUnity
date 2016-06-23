using UnityEngine;
using System.Collections;

public class PlayerGameHandler : MonoBehaviour
{

    private Vector3 mousePosition;
    private int gameState;// 0 = Place the big square, 1 = Squares are bouncing, 3 = Displaying the score
    private GameObject[] topBouncingSquares;
    private GameObject[] bottomBouncingSquares;

    public float moveSpeed = 0.1f;
    public float squareGravityScale = 1.3f;

    // Use this for initialization
    void Start()
    {
        Debug.Log("Start !!!!");
        GetComponent<SpriteRenderer>().color = Color.black;
        gameState = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Moves the player square before the game is launched
        if (Input.GetMouseButton(0) && gameState == 0)
        {
            mousePosition = Input.mousePosition;
            mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
            transform.position = Vector2.Lerp(transform.position, mousePosition, moveSpeed);
        }
        if (Input.GetMouseButtonUp(0) && gameState == 0)
        {
            Debug.Log("Let's BOUNCE !!!!");
            gameState = 1;
            topBouncingSquares = GameObject.FindGameObjectsWithTag("BouncingSquareTop");// Si on le fait dans le start, ça marche pas...
            int maxTop = topBouncingSquares.Length;
            for (int i = 0; i < maxTop; i++)
            {

                topBouncingSquares[i].GetComponent<Rigidbody2D>().gravityScale = squareGravityScale;

            }
            bottomBouncingSquares = GameObject.FindGameObjectsWithTag("BouncingSquareBottom");// Si on le fait dans le start, ça marche pas...
            int maxBottom = bottomBouncingSquares.Length;
            for (int i = 0; i < maxBottom; i++)
            {

                bottomBouncingSquares[i].GetComponent<Rigidbody2D>().gravityScale = -squareGravityScale;

            }

        }

    }

    /*************************************** 
     *  Change the color of the player 
     * ************************************/

    void OnCollisionEnter2D(Collision2D collission)
    {
        if (collission.gameObject.tag == "BouncingSquareTop" || collission.gameObject.tag == "BouncingSquareBottom")
        {
            GetComponent<SpriteRenderer>().color = Color.black;
        }
        else
        {
            GetComponent<SpriteRenderer>().color = Color.red;
        }
    }
    void OnCollisionExit2D(Collision2D collission)
    {
        GetComponent<SpriteRenderer>().color = Color.black;
    }

}