using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour {

    public GameObject sphere;
    public Material red;
    public Material green;

    [ContextMenu("red")]
    public void changeRed()
    {
        sphere.GetComponent<Renderer>().material = red;
    }

    [ContextMenu("green")]
    public void changeGreen()
    {
        sphere.GetComponent<Renderer>().material = green;
    }
}
