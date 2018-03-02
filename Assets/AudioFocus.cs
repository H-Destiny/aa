using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class AudioFocus : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Vuforia.CameraDevice.Instance.SetFocusMode(CameraDevice.FocusMode.FOCUS_MODE_CONTINUOUSAUTO);
	}
	
	// Update is called once per frame
	void Update () {
        Vuforia.CameraDevice.Instance.SetFocusMode(CameraDevice.FocusMode.FOCUS_MODE_CONTINUOUSAUTO);
    }
}
