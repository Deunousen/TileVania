using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenePersist : MonoBehaviour
{

    // Another Singleton pattern in Awake()

    void Awake()
    {
        int numScenePersists = FindObjectsOfType<ScenePersist>().Length;
        if (numScenePersists > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    // Coins collected and enemies killed will not persist when player dies (unless in first level and player loses all lives)

    public void ResetScenePersist()
    {
        Destroy(gameObject);
    }
}
