using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BackMainMenu : MonoBehaviour
{
    public AudioSource ButtonClick;
    public void BackToMainMenu()
    {

        SceneManager.LoadSceneAsync(0);
        ButtonClick.Play();
    }
}
