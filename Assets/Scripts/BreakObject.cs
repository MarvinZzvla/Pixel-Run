using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakObject : MonoBehaviour {
   // private Vector3 originalPos;
    private bool isDeath;
    public float speedObject;
    public float points;
	
    
    // Use this for initialization
	void Start () {
      //  originalPos = transform.position;
       
	}
	
	// Update is called once per frame
	void Update () {

        

        if (!GameObject.Find("Eventos").GetComponent<EventosController>().isPause)
        {
            transform.position += Vector3.left * speedObject * Time.deltaTime;
        }
        else
        {
            transform.position += Vector3.left * 0f * Time.deltaTime;
        }

        /*Respawn Objects
        if (transform.position.x < -3.55f)
        {
            transform.position = originalPos;

            if (this)//Si object existe 
            {
                isDeath = false;
                GetComponent<Animator>().Play(this.name + "_Idle");
                this.gameObject.transform.GetChild(0).GetComponent<Animator>().Play(this.name+ "_Idle");
                
                
            }
            
            
        }*/
	}

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.GetComponent<Player_Script>().isDash())
        {
            StartCoroutine("KillObject");
            //isDeath = true;
           // GetComponent<Animator>().Play(this.name + "Die");
            //this.gameObject.transform.GetChild(0).GetComponent<Animator>().Play(this.name + "Die");
            
            
        }
        else if (!col.GetComponent<Player_Script>().isDash() && !isDeath)
        {
            col.GetComponent<Player_Script>().StartCoroutine("PlayerHurts");
        }
        
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
        //Debug.Log("Desaperecio");
    }

    private IEnumerator KillObject()
    {
        isDeath = true;
        GetComponent<Animator>().Play(this.name + "Die");
        this.gameObject.transform.GetChild(0).GetComponent<Animator>().Play(this.name + "Die");
        if(points > 0)
            this.gameObject.transform.GetChild(1).GetComponent<Animator>().Play(points+ "Points");
        yield return new WaitForSeconds(1.5f);
        GetComponent<SpriteRenderer>().enabled = false;   
        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
    }
}
