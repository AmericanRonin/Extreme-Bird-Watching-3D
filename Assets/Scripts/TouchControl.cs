using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchControl : MonoBehaviour
{
    public BinocularControl binoculars;
    public GameObject birds;

    public float moveSpeed = 5.0f;

    float tapTime = 0.0f;

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
