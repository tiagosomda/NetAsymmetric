using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using NewtonVR;

public class VRPlayer : NetworkBehaviour {

    public Transform head;
    public Transform leftHand;
    public Transform rightHand;

    private NVRPlayer nvrPlayer;
    private Transform nvrHeadTransfrom;
    private Transform nvrLeftHandTransform;
    private Transform nvrRightHandTransform;

    // Use this for initialization
    void Start()
    {
        if (!isLocalPlayer)
            return;
        SetVRPlayer();

    }

    // Update is called once per frame
    void Update () {
        if (!isLocalPlayer)
            return;

        if (nvrHeadTransfrom)
        {
            head.position = nvrHeadTransfrom.position;
            head.rotation = nvrHeadTransfrom.rotation;
        }

        if (nvrLeftHandTransform)
        {
            leftHand.position = nvrLeftHandTransform.position;
            leftHand.rotation = nvrLeftHandTransform.rotation;
        }

        if (nvrRightHandTransform)
        {
            rightHand.position = nvrRightHandTransform.position;
            rightHand.rotation = nvrRightHandTransform.rotation;
        }

    }

    void SetVRPlayer()
    {
        StartCoroutine(SetNVRPlayer());
    }

    IEnumerator SetNVRPlayer()
    {
        while (nvrPlayer == null)
        {
            nvrPlayer = GameObject.FindObjectOfType<NVRPlayer>();
            yield return new WaitForEndOfFrame();
        }

        nvrHeadTransfrom = nvrPlayer.Head.transform;
        nvrLeftHandTransform = nvrPlayer.LeftHand.transform;
        nvrRightHandTransform = nvrPlayer.RightHand.transform;
    }
}
