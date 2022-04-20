using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Result : MonoBehaviour
{
    public Color winner;

    [SerializeField] GameObject Bwin;
    [SerializeField] GameObject Wwin;


    // Start is called before the first frame update
    void Start()
    {
        if(winner==Color.black) Bwin.SetActive(true);
        else if(winner==Color.white)    Wwin.SetActive(true);
    }

    public void Retry(){
        SceneManager.LoadScene("INgame");
    }

    public void Gotitle(){
        SceneManager.LoadScene("Title");
    }



}
