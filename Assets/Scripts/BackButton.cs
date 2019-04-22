using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackButton : MonoBehaviour {
    private string nameScene;
    private GameObject Menu;
	// Use this for initialization
	void Start () {
       nameScene = SceneManager.GetActiveScene().name;
        Menu = GameObject.Find("Canvas");
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape) && nameScene == "Home")
        {
            Application.Quit();
             
        }

        else if (Input.GetKeyDown(KeyCode.Escape) && nameScene != "Home")
        {
            Menu.GetComponent<ResetButton>().OpenMenu();
        }


    }
}
