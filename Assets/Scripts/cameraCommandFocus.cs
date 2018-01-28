using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraCommandFocus : MonoBehaviour
{
    private Animator animator;
    public float currentFocus;
    public float halfPoint = 0.7f;
	// Use this for initialization
	void Start ()
    {
        animator = GetComponent<Animator>();
	}

    // Update is called once per frame
    void Update()
    {
        if (Input.mousePosition.x / Screen.width < halfPoint)
        {
            animator.SetFloat("focusOnMonitor", 0f, 0.25f, Time.deltaTime);
        }
        if (Input.mousePosition.x / Screen.width > halfPoint)
        {
            animator.SetFloat("focusOnMonitor", 0.6f, 0.25f, Time.deltaTime);
        }
    }
}
