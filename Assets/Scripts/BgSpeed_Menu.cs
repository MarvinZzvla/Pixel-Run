using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BgSpeed_Menu : MonoBehaviour {
    public float speed; 
    RawImage image;
    

	// Use this for initialization
	void Start () {
        image = GetComponent<RawImage>();
       
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        image.uvRect = new Rect
            (image.uvRect.x + (speed * Time.fixedDeltaTime), 0f, 1f, 1f); 
	}
}
