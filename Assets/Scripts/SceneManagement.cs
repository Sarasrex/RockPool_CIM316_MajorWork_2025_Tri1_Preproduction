using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
 
    // Update is called once per frame
    public void LoadSceneByName(string sceneName)
    {
        SceneManager.LoadScene(sceneName);        
        // using name as scene desgin is not linear
    }
}
