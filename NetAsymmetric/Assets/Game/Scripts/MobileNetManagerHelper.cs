using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MobileNetManagerHelper : MonoBehaviour {

    public GameObject helperPanel;
    public InputField ipAddressInputField;
    public Button connectToHostBtn;
    public Button playerReadyBtn;

    private NetworkManagerAsymmetric netManager;
	// Use this for initialization
	void Start () {

        if(Application.platform == RuntimePlatform.Android)
        {
            connectToHostBtn.gameObject.SetActive(false);
            netManager = GameObject.FindObjectOfType<NetworkManagerAsymmetric>();
            connectToHostBtn.onClick.AddListener(ConnectBtnHandler);
            ipAddressInputField.onValueChanged.AddListener(InputChangeHandler);
        }
        else
        {
            helperPanel.SetActive(false);
        }
    }

    void ConnectBtnHandler()
    {
        netManager.networkAddress = ipAddressInputField.text;
        netManager.StartClient();
    }

    void InputChangeHandler(string value)
    {
        connectToHostBtn.gameObject.SetActive(value != "");
    }
}
