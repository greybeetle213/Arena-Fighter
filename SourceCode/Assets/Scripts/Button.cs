using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class Button : MonoBehaviour
{
    public void PlayGame() {
        SceneManager.LoadScene("Arena");
    }// Start is called before the first frame update

    public void LoadHelpMenu() {
        SceneManager.LoadScene("Help");
    }

    public void LoadMenu() {
        SceneManager.LoadScene("StartScreen");
    }
}
