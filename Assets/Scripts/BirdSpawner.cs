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
    float spawnVariance = 20.0f;

    GameObject spawnerHelper;

    // Start is called before the first frame update
    void Start()
    {
        spawnerHelper = new GameObject("Spawner Helper");

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
        spawnerHelper.transform.position = Vector3.zero;

        // rotate spawnHelper randomly on y
        spawnerHelper.transform.RotateAround(transform.position, transform.up, Random.Range(0.0f, 360.0f));

        // move forward random distance
        spawnerHelper.transform.position += spawnerHelper.transform.forward * (Random.Range(25.0f, 35.0f));

        float x = spawnerHelper.transform.position.x;
        float z = spawnerHelper.transform.position.z;

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
