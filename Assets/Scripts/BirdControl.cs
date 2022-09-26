using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdControl : MonoBehaviour
{
    public UnityEngine.UI.Image watchMeter;
    public ScoreControl scoreController;
    public float waitTime = 15.0f;

    public Sprite birdPerchedSprite;
    public Sprite birdFlyingSprite = null;

    float birdExistTime = 0.0f;

    bool watched = false;

    float flyingTime = 5.0f;
    float timeInFlight = 0.0f;

    public Vector3 destPos;
    public Vector3 startPos;

    bool flying = false;
    bool leaving = false;

    public float watchIncrement = 0.01f;

    // Start is called before the first frame update
    void Start()
    {
        flying = true;
        leaving = false;

        if (birdFlyingSprite != null)
        {
            GetComponent<SpriteRenderer>().sprite = birdFlyingSprite;
        }
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
                // TODO: make sure flying bird faces correct direction

                // flies down
                if (!leaving)
                {
                    timeInFlight += Time.deltaTime / flyingTime;
                    transform.position = Vector3.Lerp(startPos, destPos, timeInFlight);

                    if (timeInFlight >= 1.0f)
                    {
                        flying = false;

                        GetComponent<SpriteRenderer>().sprite = birdPerchedSprite;

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
                    if(birdFlyingSprite != null)
                    {
                        GetComponent<SpriteRenderer>().sprite = birdFlyingSprite;
                    }
                    leaving = true;
                    timeInFlight = 0.0f;
                }
            }
        }
    }

    public void CheckIfWatched()
    {
        if (!scoreController.isGameOver)
        {
            // TODO: should this be checked on what calls CheckIfWatched()?
            if (!watched)
            {
                watchMeter.fillAmount += watchIncrement;

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
}
