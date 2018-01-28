using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraCommandFocus : MonoBehaviour
{
    private Animator animator;
    public float currentFocus;

	// Use this for initialization
	void Start ()
    {
        animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        //if(Input.mousePosition.x < Screen.width / 0.5f)
        //{
        //    animator.SetFloat("focusOnMonitor", Input.mousePosition.x / Screen.width, 0.25f, Time.deltaTime);
        //}

        animator.SetFloat("focusOnMonitor", Input.mousePosition.x / Screen.width, 0.25f, Time.deltaTime);


    }
}
