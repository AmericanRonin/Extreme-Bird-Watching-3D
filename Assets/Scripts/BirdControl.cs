using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdControl : MonoBehaviour
{
    public UnityEngine.UI.Image watchMeter;
    public ScoreControl scoreController;
    public float waitTime = 15.0f;

    float birdExistTime = 0.0f;

    bool watched = false;

    float flyingTime = 5.0f;
    float timeInFlight = 0.0f;

    public Vector3 destPos;
    public Vector3 startPos;

    bool flying = false;
    bool leaving = false;

    // Start is called before the first frame update
    void Start()
    {
        flying = true;
        leaving = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!scoreController.isGameOver)
        {
            // always face camera
            transform.rotation = Quaternion.LookRotation(transform.position - Camera.main.transform.position);

            if (watchMeter.fillAmount > 0)
            {
                watchMeter.fillAmount -= Time.deltaTime * 0.1f;
            }

            if (flying)
            {
                // flies down
                if (!leaving)
                {
                    timeInFlight += Time.deltaTime / flyingTime;
                    transform.position = Vector3.Lerp(startPos, destPos, timeInFlight);

                    if (timeInFlight >= 1.0f)
                    {
                        flying = false;
                        birdExistTime = 0.0f;
                    }
                }
                // flies away
                else
                {
                    timeInFlight += Time.deltaTime / flyingTime;
                    transform.position = Vector3.Lerp(destPos, startPos, timeInFlight);

                    // bird has flown away, so remove
                    if (timeInFlight >= 1.0f)
                    {
                        Destroy(gameObject);
                    }
                }
            }
            else
            {

                birdExistTime += Time.deltaTime;

                if (birdExistTime >= waitTime)
                {
                    flying = true;
                    leaving = true;
                    timeInFlight = 0.0f;
                }
            }
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
