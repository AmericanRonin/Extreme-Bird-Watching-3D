using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR;

public class TouchControl : MonoBehaviour
{
    public BinocularControl binoculars;
    public GameObject birds;

    public float moveSpeed = 5.0f;

    float tapTime = 0.0f;

    private InputDevice rightController;
    private bool gotRightController = false;

    // Start is called before the first frame update
    void Start()
    {
        // if running in editor, move quicker
        if(Application.isEditor)
        {
            moveSpeed *= 10;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // logic for changing how fast to move camera based on zoom
        float zoomFactor = moveSpeed;
        if(binoculars.binocularsOn)
        {
            zoomFactor = zoomFactor / binoculars.binocularZoom;
        }

        // set a timer to check tap length
        if(Input.GetMouseButtonDown(0))
        {
            tapTime = 0.0f;
        }

        if (Input.GetKeyDown("space"))
        {
            binoculars.OnButtonPress();
        }

        // logic to move the camera -- includes zoomFactor for speed
        if (Input.GetMouseButton(0))
        {
            Vector3 rotateValue = new Vector3(Input.GetAxis("Mouse Y") * zoomFactor,
                -Input.GetAxis("Mouse X") * zoomFactor, 0);
            Camera.main.transform.eulerAngles = Camera.main.transform.eulerAngles + rotateValue;

            tapTime += Time.deltaTime;
        }

        if (!gotRightController)
        {
            List<InputDevice> devices = new List<InputDevice>();

            InputDeviceCharacteristics rightControllerCharacteristics = InputDeviceCharacteristics.Right | InputDeviceCharacteristics.Controller;
            InputDevices.GetDevicesWithCharacteristics(rightControllerCharacteristics, devices);

            if (devices.Count > 0)
            {
                rightController = devices[0];
                gotRightController = true;
            }
        }
        else
        {
            rightController.TryGetFeatureValue(CommonUsages.primaryButton, out bool primaryButtonValue);
            if (primaryButtonValue == true)
            {
                Debug.Log("Primary button pressed");
                SceneManager.LoadScene(0);
            }
        }

        // removing tag to watch bird
        /*if(Input.GetMouseButtonUp(0))
        {
            // check if short tap
            if(tapTime < 0.2f)
            {
                // check that binoculars are on
                if (bionculars.binocularsOn)
                {
                    // go through birds and see if watched
                    foreach (Transform child in birds.transform)
                    {
                        // check that bird is visible
                        if (child.GetComponent<Renderer>().isVisible)
                        {
                            // do watch logic
                            child.GetComponent<BirdControl>().CheckIfWatched();
                        }
                    }
                }
            }
        }*/
    }
}
