using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdSpawner : MonoBehaviour
{
    public ScoreControl scoreController;
    public GameObject[] birdPrefab;

    float spawnDistance = 30.0f;
    float birdSpawnTime = 1.0f;
    float lastBirdSpawn = 0.0f;
    float spawnHeight = 50.0f;
    float spawnVariance = 10.0f;

    // Start is called before the first frame update
    void Start()
    {
        CreateBird();
    }

    // Update is called once per frame
    void Update()
    {
        if (!scoreController.isGameOver)
        {
            lastBirdSpawn += Time.deltaTime;
            if (lastBirdSpawn >= birdSpawnTime)
            {
                CreateBird();
            }
        }
    }

    // create a new bird
    void CreateBird()
    {
        // for now, make random x z where it adds to spawnDistance;
        float x = Random.Range(0.0f, spawnDistance);
        float z = spawnDistance - x;

        // get random signs for them
        if(Random.value > 0.5) // 50/50 chance
        {
            x = x * -1;
        }
        if(Random.value > 0.5)
        {
            z = z * -1;
        }

        Vector3 destPos = new Vector3(x, 0, z);

        // get random starting point
        x += Random.Range(-spawnVariance, spawnVariance);
        z += Random.Range(-spawnVariance, spawnVariance);

        Vector3 startPos = new Vector3(x, spawnHeight, z);

        // create new bird
        GameObject newBird = Instantiate(birdPrefab[Random.Range(0,birdPrefab.Length)], startPos, Quaternion.identity);
        // make this the parent so birds are all under same GameObject
        newBird.transform.SetParent(this.transform);

        BirdControl newBirdController = newBird.GetComponent<BirdControl>();
        // give the new bird flight coordinates
        newBirdController.destPos = destPos;
        newBirdController.startPos = startPos;
        // connect bird to score controller
        newBirdController.scoreController = scoreController;

        lastBirdSpawn = 0.0f;
    }
}
