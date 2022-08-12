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

    float watchingTimer = 0.0f;

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
        // while binoculars are on, see if birds are in view
        if (binocularsOn)
        {
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
