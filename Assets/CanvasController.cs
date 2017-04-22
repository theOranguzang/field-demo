using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasController : MonoBehaviour {

    public Text releaseVelocityTxt;
    public Text DisplacementTxt;
    public Text HeightTxt;

    public GameObject ball;
    public GameObject hmd;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        releaseVelocityTxt.GetComponent<Text>().text = "Release Velocity Magnitude: " + ball.GetComponent<Ball>().releaseVelocity.magnitude;
        DisplacementTxt.GetComponent<Text>().text = "Displacement: " + Vector3.Distance(ball.transform.position, hmd.transform.position).ToString();
        HeightTxt.GetComponent<Text>().text = "Height: " + ball.transform.position.y.ToString();
    }
}
