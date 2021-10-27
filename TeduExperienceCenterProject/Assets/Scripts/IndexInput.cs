using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class IndexInput : MonoBehaviour
{
    public SteamVR_Action_Vector2 ThumbstickAction;
    public SteamVR_Action_Vector2 TrackpadAction;

    public SteamVR_Action_Single SqueezeAction;
    public SteamVR_Action_Boolean GripAction;
    public SteamVR_Action_Boolean PinchAction;

    public SteamVR_Action_Skeleton SkeletonAction;

    // Update is called once per frame
    void Update()
    {
        //Thumbstick();
        //Trackpad();
        //Squeeze();
        //Grip();
        //Pinch();
        Skeleton();

    }

    void Thumbstick()
    {
        if (ThumbstickAction.axis == Vector2.zero) return;

        Debug.Log("Thumbstick: " + ThumbstickAction.axis);
    }

    void Trackpad()
    {
        if (TrackpadAction.axis == Vector2.zero) return;

        Debug.Log("Trackpad: " + TrackpadAction.axis);
    }

    void Squeeze()
    {
        if (SqueezeAction.axis == 0f) return;

        Debug.Log("Squeeze: " + SqueezeAction.axis);
    }

    void Grip()
    {
        if (GripAction.stateDown) Debug.Log("Grip Down");
        if (GripAction.stateUp) Debug.Log("Grip Up");  
    }

    void Pinch()
    {
        if (PinchAction.stateDown) Debug.Log("Pinch Down");
        if (PinchAction.stateUp) Debug.Log("Pinch Up");
    }

    void Skeleton()
    {
        if (SkeletonAction.indexCurl != 0.0f) Debug.Log("Index: " + SkeletonAction.indexCurl);
        if (SkeletonAction.middleCurl != 0.0f) Debug.Log("Middle: " + SkeletonAction.middleCurl);
        if (SkeletonAction.ringCurl != 0.0f) Debug.Log("Ring: " + SkeletonAction.ringCurl);
        if (SkeletonAction.pinkyCurl != 0.0f) Debug.Log("Pinky: " + SkeletonAction.pinkyCurl);
        if (SkeletonAction.thumbCurl != 0.0f) Debug.Log("Thumb: " + SkeletonAction.thumbCurl);

        Debug.Log("Thumb: " + SkeletonAction.thumbCurl + " Index: " + SkeletonAction.indexCurl + " Middle: " + SkeletonAction.middleCurl + " Ring: " + SkeletonAction.ringCurl + " Pinky: " + SkeletonAction.pinkyCurl);

        transform.GetChild(0).localEulerAngles = new Vector3(transform.GetChild(0).localEulerAngles.x, transform.GetChild(0).localEulerAngles.y, SkeletonAction.thumbCurl * 180);
        transform.GetChild(1).localEulerAngles = new Vector3(transform.GetChild(1).localEulerAngles.x, transform.GetChild(1).localEulerAngles.y, SkeletonAction.indexCurl * 180);
        transform.GetChild(2).localEulerAngles = new Vector3(transform.GetChild(2).localEulerAngles.x, transform.GetChild(2).localEulerAngles.y, SkeletonAction.middleCurl * 180);
        transform.GetChild(3).localEulerAngles = new Vector3(transform.GetChild(3).localEulerAngles.x, transform.GetChild(3).localEulerAngles.y, SkeletonAction.ringCurl * 180);
        transform.GetChild(4).localEulerAngles = new Vector3(transform.GetChild(4).localEulerAngles.x, transform.GetChild(4).localEulerAngles.y, SkeletonAction.pinkyCurl * 180);
    }
}

   
