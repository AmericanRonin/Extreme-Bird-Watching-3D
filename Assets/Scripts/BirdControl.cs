using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdControl : MonoBehaviour
{
    public UnityEngine.UI.Image watchMeter;

    bool watched = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(watchMeter.fillAmount > 0)
        {
            watchMeter.fillAmount -= Time.deltaTime * 0.1f;
        }
    }

    public void CheckIfWatched()
    {
        watchMeter.fillAmount += 0.1f;

        // check if bird fully watched
        if(watchMeter.fillAmount >= 1)
        {
            watched = true;

            // make bird translucent and disappear watch meter
            watchMeter.gameObject.SetActive(false);
            SpriteRenderer sprite = GetComponent<SpriteRenderer>();
            sprite.color = new Color(1.0f, 1.0f, 1.0f, 0.5f);
        }
    }
}
