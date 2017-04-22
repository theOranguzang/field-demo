using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerCollider : MonoBehaviour {

    private SteamVR_TrackedObject trackedObj;
    private GameObject collidingObject;
    private GameObject objectInHand;

    private SteamVR_Controller.Device Controller
    {
        get { return SteamVR_Controller.Input((int)trackedObj.index); }
    }

    private void Awake()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
    }

    // Update is called once per frame
    void Update()
    {
        // 1
        if (Controller.GetHairTriggerDown())
        {
            if (collidingObject)
            {
                GrabObject();
            }
        }

        // 2
        if (Controller.GetHairTriggerUp())
        {
            if (objectInHand)
            {
                ReleaseObject();
            }
        }

        if (Controller.GetPressDown(SteamVR_Controller.ButtonMask.Grip))
        {
            Debug.Log("grip");
            GameObject.Find("Sphere").GetComponent<Ball>().resetBall();
        }
    }

    private void SetCollidingObject(Collider col)
    {
        // 1
        if (collidingObject || !col.GetComponent<Rigidbody>())
        {
            return;
        }
        // 2
        collidingObject = col.gameObject;
    }

    // 1
    public void OnTriggerEnter(Collider other)
    {
        SetCollidingObject(other);
    }

    // 2
    public void OnTriggerStay(Collider other)
    {
        SetCollidingObject(other);
    }

    // 3
    public void OnTriggerExit(Collider other)
    {
        if (!collidingObject)
        {
            return;
        }

        collidingObject = null;
    }

    private void GrabObject()
    {
        // 1
        objectInHand = collidingObject;
        collidingObject = null;
        // 2

        objectInHand.GetComponent<Ball>().grabBall();

        FixedJoint fixedJoint = gameObject.AddComponent<FixedJoint>();
        fixedJoint.breakForce = 99999;
        fixedJoint.breakTorque = 99999;

        fixedJoint.connectedBody = objectInHand.GetComponent<Rigidbody>();
    }

    private void ReleaseObject()
    {
        // 1
        if (GetComponent<FixedJoint>())
        {
            // 2
            FixedJoint fixedJoint = GetComponent<FixedJoint>();
            fixedJoint.connectedBody = null;
            Destroy(fixedJoint);
            // 3

            objectInHand.GetComponent<Ball>().throwBall();

            Rigidbody rb = objectInHand.GetComponent<Rigidbody>();
            rb.velocity = Controller.velocity;
            rb.angularVelocity = Controller.angularVelocity;

            //buff
            rb.AddForce(rb.velocity.normalized*1000);
        }
        // 4
        objectInHand = null;
    }

}
