using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesGround : MonoBehaviour {
    public float speedObject;

    // Use this for initialization
    void Start () {

        
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
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        col.GetComponent<Player_Script>().StartCoroutine("PlayerHurts");
        GetComponent<Animator>().Play(this.name);
        StartCoroutine("ChangePlayer");
    }

    IEnumerator ChangePlayer()
    {
        Color OriginalColor = GameObject.FindGameObjectWithTag("Player").GetComponent<SpriteRenderer>().color;
        GameObject.FindGameObjectWithTag("Player").GetComponent<SpriteRenderer>().color = new Color(0.6039f, 0.3803f, 0.2117f, 1);
        yield return new WaitForSeconds(1.2f);
        GameObject.FindGameObjectWithTag("Player").GetComponent<SpriteRenderer>().color = OriginalColor;


    }
}
