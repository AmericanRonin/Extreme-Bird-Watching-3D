using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceDetect : MonoBehaviour
{
    public GameObject headGear;
    public GameObject rightController;
    public TMPro.TMP_Text headPosText;
    public TMPro.TMP_Text rControllerPosText;
    public TMPro.TMP_Text distText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        headPosText.text = headGear.transform.position.x.ToString("F2") + " " + headGear.transform.position.y.ToString("F2") + " " + headGear.transform.position.z.ToString("F2");
        rControllerPosText.text = rightController.transform.position.x.ToString("F2") + " " + rightController.transform.position.y.ToString("F2") + " " + rightController.transform.position.z.ToString("F2") + " : " +
            rightController.transform.rotation.x.ToString("F2") + " " + rightController.transform.rotation.y.ToString("F2") + " " + rightController.transform.rotation.z.ToString("F2");
        distText.text = Vector3.Distance(headGear.transform.position, rightController.transform.position).ToString("F2");
    }
}
