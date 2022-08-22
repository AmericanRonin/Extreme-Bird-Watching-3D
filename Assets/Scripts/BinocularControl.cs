using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BinocularControl : MonoBehaviour
{
    public bool binocularsOn = false;
    public float normalScale = 60.0f;
    public float binocularZoom = 20.0f;
    public GameObject binocularPanel;
    public GameObject birds;

    public GameObject headGear = null;
    public GameObject rightController = null;
    public GameObject wholeRig = null;
    
    public float binocularsDist = 0.16f;

    float watchingTimer = 0.0f;

    Vector3 originalLocation;

    // Start is called before the first frame update
    void Start()
    {
        binocularsOn = false;
        Camera.main.orthographicSize = normalScale;
        binocularPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // TODO: If VR
        if(headGear != null && rightController != null)
        {
            // if right controller close enough, put binoculars on
            // TODO: make sure position right
            if(Vector3.Distance(headGear.transform.position, rightController.transform.position) < binocularsDist)
            {
                if(!binocularsOn)
                {
                    originalLocation = wholeRig.transform.position;
                    watchingTimer = 0.0f;

                    // make right controller invisible
                    foreach (Renderer r in rightController.GetComponentsInChildren<Renderer>())
                    {
                        r.enabled = false;
                    }

                    binocularsOn = true;
                    binocularPanel.SetActive(true);
                }
            }
            else
            {
                if (binocularsOn)
                {
                    binocularsOn = false;
                    binocularPanel.SetActive(false);
                    wholeRig.transform.position = originalLocation;

                    // make right controller visible again
                    foreach (Renderer r in rightController.GetComponentsInChildren<Renderer>())
                    {
                        r.enabled = true;
                    }
                }
            }
        }

        // while binoculars are on, see if birds are in view
        if (binocularsOn)
        {
            if(wholeRig != null)
            {
                // for VR, moveforward as if zoom in direction headgear is facing
                wholeRig.transform.position = originalLocation + (headGear.transform.forward) * 20; // TODO: should be based on bird distance and zoom factor 
                if(wholeRig.transform.position.y <= -1)
                {
                    wholeRig.transform.position = new Vector3(wholeRig.transform.position.x, -0.9f, wholeRig.transform.position.z);
                }
            }

            watchingTimer += Time.deltaTime;

            // only check watch on certain interval
            if (watchingTimer >= 0.01f)
            {
                watchingTimer = 0.0f;

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
    }

    public void OnButtonPress()
    {
        if(binocularsOn)
        {
            binocularsOn = false;
            Camera.main.fieldOfView = normalScale;
            binocularPanel.SetActive(false);
        }
        else
        {
            binocularsOn = true;
            binocularPanel.SetActive(true);
            Camera.main.fieldOfView = normalScale / binocularZoom;
            watchingTimer = 0.0f;
        }
    }
}
