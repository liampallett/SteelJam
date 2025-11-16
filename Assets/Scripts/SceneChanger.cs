using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneChanger : MonoBehaviour
{
    public Button StartButton;
    public bool doThisChange;
    public int sceneIndex;
    private bool changed=false;
    private int i;
    void Start()
    {
        StartButton.onClick.AddListener(StartGame);
        
        
        
    }

    void keepUp(){

    }




    public void StartGame(){
        Debug.Log("Hello");
        SceneManager.LoadScene(1);
        doThisChange=true;

       

    }

    void Update()
    {
        

        
        
    }



}
