using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class BinocularControl : MonoBehaviour
{
    public bool binocularsOn = false;
    public float normalScale = 60.0f;
    public float binocularZoom = 20.0f;
    public GameObject binocularPanel = null;
    public GameObject birds;

    public GameObject headGear = null;
    public GameObject rightController = null;
    public GameObject wholeRig = null;

    public GameObject vignette = null;

    public GameObject zoomPanelLeft = null;
    public GameObject zoomPanelRight = null;
    public Camera zoomCamera = null;

    public float binocularsDist = 0.16f;

    float watchingTimer = 0.0f;

    Vector3 originalLocation;

    bool vrTestBinocularOverride = false;

    // Start is called before the first frame update
    void Start()
    {
        binocularsOn = false;
        Camera.main.orthographicSize = normalScale;
        if (binocularPanel)
        {
            binocularPanel.SetActive(false);
        }

        if (vignette != null)
        {
            vignette.SetActive(false);
        }

        if(zoomPanelLeft)
        {
            zoomPanelLeft.SetActive(false);
            zoomPanelRight.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // TODO: If VR
        if(headGear != null && rightController != null)
        {
            // if right controller close enough, put binoculars on
            // TODO: make sure position right
            if(Vector3.Distance(headGear.transform.position, rightController.transform.position) < binocularsDist || vrTestBinocularOverride)
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

                    if(vignette != null)
                    {
                        vignette.SetActive(true);
                    }

                    // make zoom panel visible
                    zoomPanelLeft.SetActive(true);
                    zoomPanelRight.SetActive(true);
                }
            }
            else
            {
                if (binocularsOn)
                {
                    binocularsOn = false;
                    wholeRig.transform.position = originalLocation;

                    // make right controller visible again
                    foreach (Renderer r in rightController.GetComponentsInChildren<Renderer>())
                    {
                        r.enabled = true;
                    }

                    if (vignette != null)
                    {
                        vignette.SetActive(false);
                    }

                    zoomPanelLeft.SetActive(false);
                    zoomPanelRight.SetActive(false);
                }
            }
        }

        // while binoculars are on, see if birds are in view
        if (binocularsOn)
        {
            if(wholeRig != null)
            {
                // for VR, moveforward as if zoom in direction headgear is facing
                /*wholeRig.transform.position = originalLocation + (headGear.transform.forward) * 20; // TODO: should be based on bird distance and zoom factor 
                if(wholeRig.transform.position.y <= -1)
                {
                    wholeRig.transform.position = new Vector3(wholeRig.transform.position.x, -0.9f, wholeRig.transform.position.z);
                }*/
            }

            watchingTimer += Time.deltaTime;

            // only check watch on certain interval
            if (watchingTimer >= 0.01f)
            {
                watchingTimer = 0.0f;

                // go through birds and see if watched

                // check on zoom camera for VR using frustrum planes
                if (zoomCamera != null)
                {
                    Plane[] planes = GeometryUtility.CalculateFrustumPlanes(zoomCamera);

                    foreach (Transform child in birds.transform)
                    {
                        if (child.GetComponent<Renderer>().isVisible)
                        {
                            Collider2D birdCollider = child.GetComponent<Collider2D>();
                            if (GeometryUtility.TestPlanesAABB(planes, birdCollider.bounds))
                            {
                                // do watch logic
                                child.GetComponent<BirdControl>().CheckIfWatched();
                            }
                        }
                    }
                }
                // else simply check visibility
                else
                {
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
    }

    public void OnButtonPress()
    {
        if(binocularsOn)
        {
            if (headGear != null)
            {
                vrTestBinocularOverride = false;
            }
            else
            {
                binocularsOn = false;
                Camera.main.fieldOfView = normalScale;
                if (binocularPanel)
                {
                    binocularPanel.SetActive(false);
                }
            }
        }
        else
        {
            if (headGear != null)
            {
                vrTestBinocularOverride = true;
            }
            else
            {
                binocularsOn = true;
                if (binocularPanel)
                {
                    binocularPanel.SetActive(true);
                }
                Camera.main.fieldOfView = normalScale / binocularZoom;
                watchingTimer = 0.0f;
            }
        }
    }
}
