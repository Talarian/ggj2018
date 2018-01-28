using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainSceneLoader : MonoBehaviour
{
    public string sceneToLoad;

	// Use this for initialization
	void Start ()
    {
        SceneManager.LoadScene(sceneToLoad, LoadSceneMode.Additive);
    }
}
