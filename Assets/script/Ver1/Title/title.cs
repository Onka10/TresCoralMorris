using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class title : MonoBehaviour
{
    public void ChangeScene()
    {
        SceneManager.LoadScene("INgame");
    }

    public void End(){
        UnityEngine.Application.Quit();
    }
}
