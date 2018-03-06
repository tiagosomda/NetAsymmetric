using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.SceneUtils;
using UnityEngine.Networking;

public class MobilePlayer : NetworkBehaviour {

    PlaceTargetWithMouse placeTarget;

    // Use this for initialization
    void Start () {

        if(!isLocalPlayer)
        {
            var cam = transform.GetComponentInChildren<Camera>();
            cam.gameObject.SetActive(false);
        }

        StartCoroutine(FindTargetWithMouse());
    }
	
	// Update is called once per frame
	void Update () {
		
	}


    IEnumerator FindTargetWithMouse()
    {
        while(placeTarget == null)
        {
            placeTarget = GameObject.FindObjectOfType<PlaceTargetWithMouse>();
            yield return new WaitForSeconds(0.5f);
        }

        placeTarget.setTargetOn = gameObject;
    }
}
