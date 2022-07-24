using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BinocularControl : MonoBehaviour
{
    public bool binocularsOn = false;
    public float normalScale = 60.0f;
    public float binocularZoom = 20.0f;
    public GameObject binocularPanel;

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
        }
    }
}
