using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.SceneUtils;

public class MobilePlayer : MonoBehaviour {

    PlaceTargetWithMouse placeTarget;

    // Use this for initialization
    void Start () {

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
