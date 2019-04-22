using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeOut : MonoBehaviour {
    
	// Use this for initialization
	void Start () {
        
        Invoke("DisableFade", 1f);
	}

    void DisableFade()
    {
        gameObject.SetActive(false);
    }
	
	
}
