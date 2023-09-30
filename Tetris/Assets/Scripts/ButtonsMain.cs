using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadTetris : MonoBehaviour
{
    public AudioSource LobbyMusic;
    public AudioSource ButtonClick;

    // Start is called before the first frame update
    public void Start()
    {
        LobbyMusic.Play();
    }

    public void LoadGame()
    {
        ButtonClick.Play();
        SceneManager.LoadSceneAsync(1);
    }

    public void Quit()
    {
        ButtonClick.Play();
        Application.Quit();
    }
}
