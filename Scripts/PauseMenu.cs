using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{

    public GameObject panel;

    public void activate_pausemenu()
    {
          
            panel.SetActive(true);            
        
    }
    public void disable_pausemenu()
    {
          
            panel.SetActive(false);            
        
    }  
    public void Start() {
        panel.SetActive(false);
    }
    
    public void Update()
    {
      if(Time.timeScale == 0f) //game_pasused
      {
       activate_pausemenu();
      }
      if(Time.timeScale == 1) //game_pasused
      {
       disable_pausemenu();
      }
    } 

    public void SwapPause ()
    {
        if(Time.timeScale == 1)
        {
            Time.timeScale = 0f;
            AudioListener.pause = true;
        }
        else 
        {
            Time.timeScale = 1;
            AudioListener.pause = false;
        }
    }
    
} 