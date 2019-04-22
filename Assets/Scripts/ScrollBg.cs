using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollBg : MonoBehaviour {

    public float speedBg;
    private Renderer renderBg;

	// Use this for initialization
	void Start () {
        renderBg = GetComponent<Renderer>();
	}
	
	// Update is called once per frame
	void Update () {
        if (!GameObject.Find("Eventos").GetComponent<EventosController>().isPause)
        {
            renderBg.material.mainTextureOffset += new Vector2(speedBg * Time.deltaTime, 0f);
        }
        else
        {
            renderBg.material.mainTextureOffset += new Vector2(0f * Time.deltaTime, 0f);
        }

        
	}

    

}
