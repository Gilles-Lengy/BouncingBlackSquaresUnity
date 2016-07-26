using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class PlayerGameHandler : MonoBehaviour
{

    public float moveSpeed = 0.1f;
    public float squareGravityScale = 1.3f;
    

    public float timeLeft = 300.0f;

    public Text scoreText;
    public Text instructionsText;

    public byte scoreTextColorR0 = 0;
    public byte scoreTextColorG0 = 0;
    public byte scoreTextColorB0 = 0;
    public byte scoreTextColorA0 = 18;

    public byte scoreTextColorR1 = 234;
    public byte scoreTextColorG1 = 173;
    public byte scoreTextColorB1 = 0;
    public byte scoreTextColorA1 = 255;



    

    private Vector3 mousePosition;
    private int gameState;// 0 = Place the big square, 1 = Squares are bouncing, 3 = Displaying the score
    private GameObject[] topBouncingSquares;
    private GameObject[] bottomBouncingSquares;
    private int bigBlackSquareOnMouse;// On the big black square :  0 = no mouse event, 1 OnMouseDown, 2 OnMouseUp
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
        bigBlackSquareOnMouse = 0;
    }

    void OnMouseDown()
    {
        Debug.Log("OnMouseDown the big black square");
        bigBlackSquareOnMouse = 1;
    }

    void OnMouseUp()
    {
        Debug.Log("OnMouseUp the big black square");
        bigBlackSquareOnMouse = 2;
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
        if (bigBlackSquareOnMouse == 2 && gameState == 0)
        {
            
            Debug.Log("Let's BOUNCE !!!!");
            GetComponent<AudioSource>().Play();
            instructionsText.color = new Color32(0, 0, 0, 0);
            score = 0;
            gameState = 1;
            topBouncingSquares = GameObject.FindGameObjectsWithTag("BouncingSquareTop");// Si on le fait dans le start, ça marche pas...
            int maxTop = topBouncingSquares.Length;
            for (int i = 0; i < maxTop; i++)
            {

                topBouncingSquares[i].GetComponent<Rigidbody2D>().gravityScale = squareGravityScale;
                Destroy(topBouncingSquares[i].GetComponent<DragMe>());// Destroy the script DragMe attached to the little black square duplicated

            }
            bottomBouncingSquares = GameObject.FindGameObjectsWithTag("BouncingSquareBottom");// Si on le fait dans le start, ça marche pas...
            int maxBottom = bottomBouncingSquares.Length;
            for (int i = 0; i < maxBottom; i++)
            {

                bottomBouncingSquares[i].GetComponent<Rigidbody2D>().gravityScale = -squareGravityScale;
                Destroy(bottomBouncingSquares[i].GetComponent<DragMe>());// Destroy the script DragMe attached to the little black square duplicated

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

                setScoretext();



                Time.timeScale = 0.0F;// http://docs.unity3d.com/ScriptReference/Time-timeScale.html



            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Moves the big black square before the game is launched and whistle the big black square is OnMouseDown
        if (bigBlackSquareOnMouse == 1 && gameState == 0)
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

        if (gameState == 3)
        {
            scoreText.color= new Color32(scoreTextColorR1, scoreTextColorG1, scoreTextColorB1, scoreTextColorA1);
        }
    }

    /*****************************
    * Flash FX
    ****************************/

    private void FlashFX(bool fxState = false)
    {
        GetComponent<SpriteRenderer>().color = (fxState ? Color.white : Color.black);

    scoreText.color = new Color32((fxState ? scoreTextColorR1 : scoreTextColorR0), (fxState ? scoreTextColorG1 : scoreTextColorG0), (fxState ? scoreTextColorB1 : scoreTextColorB0), (fxState ? scoreTextColorA1 : scoreTextColorA0));

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
