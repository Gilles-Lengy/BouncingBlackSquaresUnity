using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class PlayerGameHandler : MonoBehaviour
{

    public float moveSpeed = 0.1f;
    public float squareGravityScale = 1.3f;
    public Text scoreText;
    //public Text countDownText;


    public float timeLeft = 300.0f;

    private Vector3 mousePosition;
    private int gameState;// 0 = Place the big square, 1 = Squares are bouncing, 3 = Displaying the score
    private GameObject[] topBouncingSquares;
    private GameObject[] bottomBouncingSquares;
    private int score;
    private string countDownString;

    private float minutes;
    private float seconds;



    // Use this for initialization
    void Start()
    {
        Debug.Log("Start !!!!");
        GetComponent<SpriteRenderer>().color = Color.black;
        gameState = 0;
        score = -777;
        setScoretext();
    }

    public void startTimer(float from)
    {

        timeLeft = from;
        Update();
        StartCoroutine(updateCoroutine());
    }

    void Update()
    {
        // Set the Gravity Scale of the little squares and the, the funny part of the game starts !!!
        if (Input.GetMouseButtonUp(0) && gameState == 0)
        {
            Debug.Log("Let's BOUNCE !!!!");
            GetComponent<AudioSource>().Play();
            score = 0;
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

        if (gameState == 3) return;
        if (gameState == 1)
        {
            timeLeft -= Time.deltaTime;

            minutes = Mathf.Floor(timeLeft / 60);
            seconds = timeLeft % 60;
            if (seconds > 59)
            {
                seconds = 59;
            }
            if (minutes < 0)
            {
                gameState = 3;
                minutes = 0;
                seconds = 0;



                Time.timeScale = 0.0F;// http://docs.unity3d.com/ScriptReference/Time-timeScale.html



            }
        }
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



    }

    /*************************************** 
     *  Change the color of the player
     *  Launch the flash FX 
     *  Scoring as well
     * ************************************/

    void OnCollisionEnter2D(Collision2D collission)
    {
        if (collission.gameObject.tag == "BouncingSquareTop" || collission.gameObject.tag == "BouncingSquareBottom")
        {

            if (gameState == 1)
            {
                FlashFX(true);
                GetComponent<AudioSource>().Play();
                score = score + 1;
                setScoretext();
                Debug.Log(score);
                startTimer(timeLeft);

            }

        }
        else
        {
            GetComponent<SpriteRenderer>().color = Color.red;
            if (gameState == 1)
            {
                collission.gameObject.GetComponent<AudioSource>().Play();
                score = score - 3;
                setScoretext();
                Debug.Log(score);
            }
        }
    }
    void OnCollisionExit2D(Collision2D collission)
    {
        FlashFX(false);
    }

    void setScoretext()
    {
        if (score != -777) { scoreText.text = score.ToString() + '/' + countDownString; }

        else
        {
            scoreText.text = "";
        }
    }

    /*****************************
    * Flash FX
    ****************************/

    private void FlashFX(bool fxState = false)
    {
        GetComponent<SpriteRenderer>().color = (fxState ? Color.white : Color.black);

        Camera.main.backgroundColor = (fxState ? Color.black : Color.white);

        int maxTop = topBouncingSquares.Length;
        for (int i = 0; i < maxTop; i++)
        {

            topBouncingSquares[i].GetComponent<SpriteRenderer>().color = (fxState ? Color.white : Color.black);

        }
        bottomBouncingSquares = GameObject.FindGameObjectsWithTag("BouncingSquareBottom");// Si on le fait dans le start, ça marche pas...
        int maxBottom = bottomBouncingSquares.Length;
        for (int i = 0; i < maxBottom; i++)
        {

            bottomBouncingSquares[i].GetComponent<SpriteRenderer>().color = (fxState ? Color.white : Color.black);

        }
    }

    /*****************************
     * Timer
     * ***************************/



    private IEnumerator updateCoroutine()
    {
        while (gameState == 1)
        {
            countDownString = string.Format("{0:0}:{1:00}", minutes, seconds);
            setScoretext();
            yield return new WaitForSeconds(0.2f);
        }
    }
}
