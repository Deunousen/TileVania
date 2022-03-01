using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{


    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.tag == "Player")
        StartCoroutine(LoadNextScene());
        
        
    }

    IEnumerator LoadNextScene()
    {
        yield return new WaitForSecondsRealtime(2);
        FindObjectOfType<ScenePersist>().ResetScenePersist();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        
        // Coroutine
        // LoadNextScene() runs after 2 seconds

        

    }

}
