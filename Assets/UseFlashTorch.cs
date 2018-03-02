using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class UseFlashTorch : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

   public void SetFlashTorch(bool isOpen)
    {
        Vuforia.CameraDevice.Instance.SetFlashTorchMode(isOpen);
    }
}
