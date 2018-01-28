using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EventAnyKey : MonoBehaviour
{
    public EventSystem eventSystem;
    public GameObject newSelected;
	// Update is called once per frame
	void Update ()
    {
		if(Input.anyKey)
        {
            eventSystem.SetSelectedGameObject(newSelected);
            gameObject.SetActive(false);
        }
	}
}
