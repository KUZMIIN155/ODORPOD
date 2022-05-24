using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void Play(int index)
    {
        SceneManager.LoadScene(index);
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void Register()
    {
        Application.OpenURL("http://192.168.1.44/register.html");
    }

    public void Login()
    {
        Application.OpenURL("http://192.168.1.44/register.html");
    }
    
}
