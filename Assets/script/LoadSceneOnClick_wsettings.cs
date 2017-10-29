using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneOnClick_wsettings : MonoBehaviour {
    
	public void LoadByIndex(int sceneIndex){
        player.Set_continue_function(true);

        SceneManager.LoadScene (sceneIndex);
        
	}
    
}
