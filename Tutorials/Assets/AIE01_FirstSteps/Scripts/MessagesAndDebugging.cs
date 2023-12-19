using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AIE01_FirstSteps
{
    public class MessagesAndDebugging : MonoBehaviour
    {
        private void Awake()
        {
	        Debug.Log("I am the first function that gets called when an object spawns/loads. I only run once");
        }
        
	    // Start is called before the first frame update
	    private void Start()
	    {
		    Debug.Log("I am the second function that gets called on an object when it spawns/loads. I only run once.");
	    }

        private void OnEnable()
        {
	        Debug.Log("I am the third function that gets called when an object spawns/loads. I run every time I become active.");
        }

	    private void FixedUpdate()
        {
	        Debug.Log("I only run a fixed number of times per second. By default 6-. I am mainly used for Physics.");
        }
	    
	    // Update is called once per frame
	    private void Update()
	    {
		    Debug.LogWarning("I run EVERY frame.");
	    }

        private void LateUpdate()
        {
	        Debug.LogError("I run every frame, but I am the last update called within a frame.");
        }

        private void OnDisable()
        {
	        Debug.Log("I run when an object is disabled/destroyed. I run everytime this happens.");
        }

        private void OnDestroy()
        {
	        Debug.Log("I run when an object is destroyed / scene changes. I only run once.");
        }

        private void OnDrawGizmos()
        {
	        Debug.Log("I run continuously in the editor but only if the object is active no matter what object is selected.");
	        Gizmos.color = new Color(1f, 0f, 0f, .25f);
	        Gizmos.DrawCube(Vector3.zero, Vector3.one);
        }

        private void OnDrawGizmosSelected()
        {
	        Debug.Log("I run continuously in the editor but only if the object is active and selected.");
	        Gizmos.color = Color.red;
	        Gizmos.DrawWireCube(Vector3.zero, Vector3.one);
        }
    }
}