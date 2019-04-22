using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EventosController : MonoBehaviour {
   private bool PlayerDead;
    private int a = 0;
    public bool isPause = false;
    // Use this for initialization
    void Start () {
        //Debug.Log(System.DateTime.Now.Hour);
	}
	
	// Update is called once per frame
	void Update () {
        if (GameObject.FindGameObjectWithTag("Player"))
        {
            PlayerDead = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Script>().IsDeath();
        }

        if (PlayerDead && a <= 0)
        {
            GameOver();
            //Debug.Log("MUERTO");
        }
	}

    private void GameOver()
    {
        a++;
        PauseGame();

    }

    public void PauseGame()
    {
        if (!isPause)
            isPause = true;
        else if(isPause && !PlayerDead)
            isPause = false;
        
       
    }

    
}
