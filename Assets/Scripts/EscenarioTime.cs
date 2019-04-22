using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class EscenarioTime : MonoBehaviour {

    private RawImage cielo;
    private RawImage mountains;
    private RawImage camino;
    private int hora;

   
    

    public Texture cielo_dia;
    public Texture cielo_tarde;
    public Texture cielo_noche;

    public Texture mountains_dia;
    //public Texture mountains_tarde;
    public Texture mountains_noche;

    public Texture camino_dia;
    public Texture camino_tarde;
    public Texture camino_noche;



    // Use this for initialization
    void Start () {
        cielo = GameObject.Find("Cielo").GetComponent<RawImage>();
        mountains = GameObject.Find("Montañas").GetComponent<RawImage>();
        camino = GameObject.Find("Camino").GetComponent<RawImage>();



        if (System.DateTime.Now.Hour >= 0 && System.DateTime.Now.Hour <= 5)
        {
            cielo.texture = cielo_noche;
            mountains.texture = mountains_noche;
            camino.texture = camino_noche;
            GameObject.Find("Luna").GetComponent<RawImage>().color = new Color(1f, 1f, 1f, 1f);
            GameObject.Find("Shadow").GetComponent<Text>().color = new Color(0.7075f, 0.1668f, 0.1368f, 1);
        }

        else if (System.DateTime.Now.Hour >= 6 && System.DateTime.Now.Hour <= 13)
        {
            cielo.texture = cielo_dia;
            mountains.texture = mountains_dia;
            camino.texture = camino_dia;
        }
        else if (System.DateTime.Now.Hour >= 14 && System.DateTime.Now.Hour <= 18)
        {
            cielo.texture = cielo_tarde;
            mountains.texture = mountains_dia;
            camino.texture = camino_tarde;
            GameObject.Find("Sol").GetComponent<RawImage>().color = new Color(1f, 1f, 1f, 1f);
            GameObject.Find("Shadow").GetComponent<Text>().color = new Color(0.0078f, 0.7607f, 0.2623f, 1);
            GameObject.Find("Pixel Text").GetComponent<Text>().color = new Color(0.0225f, 0.2075f, 0.0899f, 1);

        }
        else if (System.DateTime.Now.Hour >= 19)
        {
            cielo.texture = cielo_noche;
            mountains.texture = mountains_noche;
            camino.texture = camino_noche;
            GameObject.Find("Luna").GetComponent<RawImage>().color = new Color(1f, 1f, 1f, 1f);
            GameObject.Find("Shadow").GetComponent<Text>().color = new Color(0.7075f, 0.1668f, 0.1368f, 1);
        }




    }
	

    public void Continue()
    {
        StartCoroutine("AnimationContinue");
    }

    IEnumerator AnimationContinue()
    {
        GameObject.Find("FadeIn").GetComponent<Animator>().Play("FadeIn");
        GameObject.Find("FadeIn").GetComponent<Canvas>().sortingOrder = 2;
        yield return new WaitForSeconds(0.9f);
        SceneManager.LoadScene("Level1-1");

    }
	// Update is called once per frame
	void Update () {
		
	}
}
