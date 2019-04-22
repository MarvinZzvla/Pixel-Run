using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnbreakObject : MonoBehaviour {
   // private Vector3 originalPos;
    private bool isDeath;
    public float speedObject;


    // Use this for initialization
    void Start()
    {
        //originalPos = transform.position;

    }

    // Update is called once per frame
    void Update()
    {

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

        }*/
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "Player")
        {
            
        }

    }

    private void OnBecameInvisible()
    {
        
        Destroy(gameObject);
    }
}
