using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreePlacement : MonoBehaviour
{
    public int numTrees = 20;

    public GameObject treePrefab;

    public List<GameObject> treeList;

    // Start is called before the first frame update
    void Start()
    {
        for (int i=0;i<numTrees;i++)
        {
            // create new tree
            GameObject newTree = Instantiate(treePrefab, Vector3.zero, Quaternion.identity);
            // make this the parent so trees are all under the same GameObject
            newTree.transform.SetParent(this.transform);

            // rotate spawnHelper randomly on y
            newTree.transform.RotateAround(transform.position, transform.up, Random.Range(0.0f, 360.0f));

            // move forward random distance
            newTree.transform.position += newTree.transform.forward * (Random.Range(35.0f, 45.0f));

            // set back to zero rotation to help with tree segment placement
            newTree.transform.rotation = Quaternion.identity;

            treeList.Add(newTree);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
