using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Player_Script : MonoBehaviour {

    private Rigidbody2D rbPlayer;
    private bool isJumping = false;
    private Animator animPlayer;
    private CircleCollider2D circleCol;
    private Vector2 DashOffset;
    private bool isDashing = false;
    private Vector2 original_Offset;
    private bool isDoubleDash, isGround;
    private int numDash = 0;
    private float timer = 0.0f;
    private readonly float error = -1.147f;
    private bool isDead = false;
    private bool pauseGame, onAir;
    private Vector2 initialPos; 
    private int a = 0;

    private float JumpForce = 1.4f;


    private void Awake()
    {
        if(SystemInfo.deviceType == DeviceType.Handheld)
        {
            JumpForce = 1.6f;
        }
        else { JumpForce = 1.4f; }
    }

    // Use this for initialization
    void Start() {

        initialPos = transform.position;
        rbPlayer = GetComponent<Rigidbody2D>();
        animPlayer = GetComponent<Animator>();
        circleCol = GetComponent<CircleCollider2D>();
        DashOffset = new Vector2(0.36f, -0.14f); //Colider cuando el player esta haciendo Dash
        original_Offset = new Vector2(0, -0.14f); //Coliider Orginal
        InvokeRepeating("CheckPositionPlayer", 0.02f, 0.02f);

        
        
    }

    private void Update()
    {

        pauseGame = GameObject.Find("Eventos").GetComponent<EventosController>().isPause;

        if (pauseGame && a <= 0)
        {
            StartCoroutine(GameIsPaused());
        }

        if (transform.position.x < initialPos.x - 0.02f) 
        {
            StartCoroutine("KillPlayer");
        }

        //Debug.Log(onAir);

        
    }

    void FixedUpdate()
    {
        if (Input.GetKeyDown("up"))
        {
            if (!GameObject.Find("Eventos").GetComponent<EventosController>().isPause)
            {
                Jump();
            }
        }

        if (Input.GetKeyDown("right"))
        {
            if (!GameObject.Find("Eventos").GetComponent<EventosController>().isPause)
            {
                StartCoroutine("Dash");
            }
        }

        if (isGround)
        {
            onGround();
        }

    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "Ground")
        {
            isGround = true;
            isJumping = false;
            circleCol.offset = original_Offset;
            animPlayer.Play("Player_Idle");
            isDoubleDash = false;
            numDash = 0;
            onAir = false;
            

        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if(col.tag == "Ground")
        {
            onAir = true;
          
            if (onAir && !isDashing)
            {
                animPlayer.Play("Player_Jump");
            }

        }
    }

    public void Jump()
    {
        if (!isJumping && !isDashing && Time.time > timer && !onAir)
        {
            timer = Time.time + 0.3f;
            animPlayer.Play("Player_Jump"); 
            isJumping = true;
            isGround = false;
            Vector2 move = new Vector2(0.0f, 1f);
            rbPlayer.AddForce(move * JumpForce * Time.deltaTime);
           

        }

    }

    public bool isDash()
    {
        return isDashing;
    }

    public IEnumerator Dash()
    {
        DetectDoubleDashing();//Detect Double Dashing

        if (!isDashing && !isDoubleDash)
        {

            FreezePositionY();//Congela la gravedad del jugador
            isDashing = true;
            animPlayer.Play("Player_Dash");
            circleCol.offset = DashOffset;
            yield return new WaitForSeconds(0.75f);

            if (!pauseGame)
            {
                UnFrezzePosition(); //Descongela la Gravedad del Jugador
                circleCol.offset = original_Offset;
                isDashing = false;
                if (!onAir)
                {
                    animPlayer.Play("Player_Idle");
                }
                else
                {
                    animPlayer.Play("Player_Jump");
                }

            }
        }
    }

    public IEnumerator PlayerHurts()
    {
        int x = 1;     
        for(int a = 0; a < 10;a++)
        {
            if(x == 1)
            {
                GetComponent<SpriteRenderer>().enabled = false;
                yield return new WaitForSeconds(0.2f);
            }
            else if(x == -1)
            {
                GetComponent<SpriteRenderer>().enabled = true;
                yield return new WaitForSeconds(0.2f);
            }

            x *= -1;
        }

    }

   void FreezePositionY()
    {
        
        rbPlayer.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
    }

    void UnFrezzePosition()
    {

        rbPlayer.constraints = RigidbodyConstraints2D.None;
        rbPlayer.constraints = RigidbodyConstraints2D.FreezeRotation;
       
    }

    void DetectDoubleDashing()
    {     
        numDash++;

        // if(numDash >= 2 && isJumping)
        // {
        //    isDoubleDash = true;
        //}

        if (numDash >= 2 && onAir)
        {
            isDoubleDash = true;  
        }

       else if (!onAir)
        { 
            isDoubleDash = false;
            numDash = 0;
        }
    }

    private void onGround()
    {
        isJumping = false;
        circleCol.offset = original_Offset;
        //animPlayer.Play("Player_Idle");
        isDoubleDash = false;
       // numDash = 0;
        
    }

    private void CheckPositionPlayer()
    {
        if (transform.position.y <= error)
        {
            onGround();
        }
    }

    public bool IsDeath()
    {
        return isDead;
    }

    public IEnumerator KillPlayer()
    {
        isDead = true;
        rbPlayer.constraints = RigidbodyConstraints2D.FreezeAll;
        animPlayer.Play("Player_Die");
        yield return new WaitForSeconds(0.45f);
        GameObject.FindGameObjectWithTag("Player").SetActive(false);

    }

   IEnumerator GameIsPaused()
    { 
        a++;
        FreezePositionY();
        yield return new WaitWhile(() => pauseGame);
        StartCoroutine("ContinueGame");
        
    }

    IEnumerator ContinueGame()
    {
        a = 0;
        animPlayer.speed = 1f;

        if (isDashing)
        {
            yield return new WaitForSeconds(0.5f);
            UnFrezzePosition(); //Descongela la Gravedad del Jugador
            circleCol.offset = original_Offset;
            isDashing = false;
            if (!isJumping)
            {
                animPlayer.Play("Player_Idle");
            }
            else
            {
                animPlayer.Play("Player_Jump");
            }
        }

        if(!isDashing && isJumping || !isDashing && !isJumping)
        {
            UnFrezzePosition();
        }

        
    }
}
