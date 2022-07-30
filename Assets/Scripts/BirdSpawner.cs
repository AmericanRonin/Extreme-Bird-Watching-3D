using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdSpawner : MonoBehaviour
{
    public ScoreControl scoreController;
    public GameObject birdPrefab;

    float spawnDistance = 30.0f;
    float birdSpawnTime = 1.0f;
    float lastBirdSpawn = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        CreateBird();
    }

    // Update is called once per frame
    void Update()
    {
        lastBirdSpawn += Time.deltaTime;
        if(lastBirdSpawn >= birdSpawnTime)
        {
            CreateBird();
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

        // create new bird
        GameObject newBird = Instantiate(birdPrefab, new Vector3(x, 0, z), Quaternion.identity);
        // make this the parent so birds are all under same GameObject
        newBird.transform.parent = this.transform;
        // connect bird to score controller
        newBird.GetComponent<BirdControl>().scoreController = scoreController;
        lastBirdSpawn = 0.0f;
    }
}
