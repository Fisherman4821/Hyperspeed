using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class levelselection : MonoBehaviour
{
    public int level;
    void Start()
    {
        
    }

    
    public void OpenScene()
    {
        SceneManager.LoadScene("Level " + level.ToString());
    }
}
