using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

    public bool ballHasBeenThrown
    {
        get; private set;
    }

    public Vector3 releaseVelocity
    {
        get; private set;
    }

    private Rigidbody rb;

    public void grabBall()
    {
        rb.isKinematic = false;
    }

    public void throwBall()
    {
        ballHasBeenThrown = true;
        releaseVelocity = rb.velocity;
    }

    public void resetBall()
    {
        ballHasBeenThrown = false;
        rb.isKinematic = true;

        releaseVelocity = Vector3.zero;

        transform.position = new Vector3(0, 2.0f, 0);
        ballHasBeenThrown = false;
    }

    // Use this for initialization
    void Start () {
        ballHasBeenThrown = false;
        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {

        //if (ballHasBeenThrown && rb.velocity.magnitude < 0.1f)
        //{
        //    rb.velocity = Vector3.zero;
        //    rb.angularVelocity = Vector3.zero;

        //    transform.position = new Vector3(0, 0.5f, 0);
        //    ballHasBeenThrown = false;
        //}
    }

    

}
