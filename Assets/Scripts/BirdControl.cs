using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdControl : MonoBehaviour
{
    public UnityEngine.UI.Image watchMeter;
    public ScoreControl scoreController;
    public float waitTime = 10.0f;

    float birdExistTime = 0.0f;

    bool watched = false;

    // Start is called before the first frame update
    void Start()
    {
        birdExistTime = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        // always face camera
        transform.rotation = Quaternion.LookRotation(transform.position - Camera.main.transform.position);

        if (watchMeter.fillAmount > 0)
        {
            watchMeter.fillAmount -= Time.deltaTime * 0.1f;
        }

        birdExistTime += Time.deltaTime;

        if(birdExistTime >= waitTime)
        {
            Destroy(gameObject);
        }
    }

    public void CheckIfWatched()
    {
        // TODO: should this be checked on what calls CheckIfWatched()?
        if (!watched)
        {
            watchMeter.fillAmount += 0.1f;

            // check if bird fully watched
            if (watchMeter.fillAmount >= 1)
            {
                watched = true;

                scoreController.AddToScore(1);

                // make bird translucent and disappear watch meter
                watchMeter.gameObject.SetActive(false);
                SpriteRenderer sprite = GetComponent<SpriteRenderer>();
                sprite.color = new Color(1.0f, 1.0f, 1.0f, 0.5f);
            }
        }
    }
}
