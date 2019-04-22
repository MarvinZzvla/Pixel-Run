using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetButton : MonoBehaviour {
    int count = 0;


    public void ResetGame()
    {
        SceneManager.LoadScene("Level1-1");
    }

    public void PauseGame()
    {
        GameObject.Find("Eventos").GetComponent<EventosController>().PauseGame();    
    }


    public void OpenMenu()
    { 
        StartCoroutine("Rutina_Menu");
    }

    public IEnumerator Rutina_Menu()
    {
        if (count == 0)
        {
            AbrirMenu();
            yield return new WaitForSeconds(0.7f);
            StartCoroutine("CallAnimations");
            
            //GameObject.Find("Menu").active = true;
        }
        else if (count > 0)
        {
            StartCoroutine("CerrarMenu");
            
        }
    }
    
    IEnumerator CallAnimations()
    {
        GameObject.Find("Player_Menu").GetComponent<Animator>().Play("Player_Pause");
        GameObject.Find("MenuText").GetComponent<Animator>().Play("MenuText");
        GameObject.Find("ResetBtn").GetComponent<Animator>().Play("ResetBtn");
        yield return new WaitForSeconds(0.2f);
        GameObject.Find("HomeBtn").GetComponent<Animator>().Play("ResetBtn");
        yield return new WaitForSeconds(0.2f);
        GameObject.Find("ExitBtn").GetComponent<Animator>().Play("ResetBtn");
    }

    public void HomeButton() {
        
        GameObject.Find("CanvasMenuSalir").GetComponent<Canvas>().sortingOrder = 2;
        GameObject.Find("MenuSalir").GetComponent<Animator>().Play("MenuSalir");
    }

    public void ExitButton()
    {
        StartCoroutine("CerrarMenu");
    }

    void AbrirMenu()
    {
        count++;
        PauseGame();
        GameObject.Find("Menu").GetComponent<Animator>().Play("Abrir_Menu");
    }

     IEnumerator CerrarMenu()
    {
        count = 0;
        GameObject.Find("Menu").GetComponent<Animator>().Play("Cerrar_Menu");
        yield return new WaitForSeconds(0.3f);
        PauseGame();
    }


    public void ButtonSi()
    {
        SceneManager.LoadScene("Home");
    }

   public void ButtonNo() {
        StartCoroutine("BtnNoAnim");
    }

    IEnumerator BtnNoAnim()
    {
        GameObject.Find("MenuSalir").GetComponent<Animator>().Play("MenuSalirOff");
        yield return new WaitForSeconds(0.40f);
        GameObject.Find("CanvasMenuSalir").GetComponent<Canvas>().sortingOrder = 0;
        StartCoroutine("CerrarMenu");
    }
    
}
