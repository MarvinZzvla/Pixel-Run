using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckSwipe : MonoBehaviour {

    private Vector2 fingerDown, fingerUp;
    private Vector2 startPosition;
    //private bool detectSwipeMoviendose = false;
    private float swipe;
    private float TrazoSwipe = 20f; //Minimo de movimiento con el dedo
        
    // Update is called once per frame
    void Update () {
        

        foreach (Touch touch in Input.touches) //Por cada toque se guarda en toque la info
        {
            //Guardar las posiciones de los siguientes toques
            if (touch.phase == TouchPhase.Began) //Cuando empieza
            {
                fingerUp = touch.position;//Cuando el dedo cae sobre la pantalla
                fingerDown = touch.position;//Cuando se levanta
            }

            if (touch.phase == TouchPhase.Ended)//Cuando termina el toque
            {
               
                fingerDown = touch.position;//Guarda la ultima posicion
                
                CheckSwipeVal();//Checkea el toque
                

            }   
        }
	}

    void CheckSwipeVal()
    {
        /*Pide el valor para ver si cumple con el minimo trazo y luego compara que
         * movimiento fue mas dominante si en horizontal o vertical*/

        if (HorizontalVal() > TrazoSwipe && HorizontalVal() > VerticalVal())
        {
            if (fingerDown.x - fingerUp.x > 0) //Verifica si es derecha o izquierda
            {
                Dash();
            }
            else
            {
                //Debug.Log("Izquierda");
            }
        }

        else if(VerticalVal() > TrazoSwipe && VerticalVal() > HorizontalVal())
        {
            if (fingerDown.y - fingerUp.y > 0)
            {

                //Debug.Log("Arriba");
                Jump();
            }
            else
            {
                //Debug.Log("Abajo");
            }
        }

        fingerUp = fingerDown;
    }

    float HorizontalVal()
    {
        return Mathf.Abs(fingerDown.x - fingerUp.x);
    }

    float VerticalVal()
    {
        return Mathf.Abs(fingerDown.y - fingerUp.y);
    }


    void Jump()
    {
        if (!GameObject.Find("Eventos").GetComponent<EventosController>().isPause)
        {
            GetComponent<Player_Script>().Jump();
           
        }
        
             
    }

    void Dash()
    {
        if (!GameObject.Find("Eventos").GetComponent<EventosController>().isPause)
        {
            GetComponent<Player_Script>().StartCoroutine("Dash");
        }

    }
}
