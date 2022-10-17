using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeGeneration : MonoBehaviour
{
    public Sprite thickTreeBase;
    public Sprite thickBranchRight;
    public Sprite thickBranchLeft;
    public Sprite thickBranchBoth;
    public Sprite thickTaper;
    public Sprite thinBase;
    public Sprite thinBranchRight;
    public Sprite thinBranchLeft;
    public Sprite thinBranchBoth;
    public Sprite treeLeaves;

    public List<Vector3> leafPlacementList;

    private int totalSmallSegments = 0;

    enum BranchDirection { Up, Right, Left };

    // Start is called before the first frame update
    void Start()
    {
        Vector3 nextLocale = new Vector3(0, 0, 0);
        int thickSegmentCount = 0;

        // make thick segments
        GameObject newTrunk = new GameObject("Trunk Base");
        newTrunk.AddComponent<SpriteRenderer>().sprite = thickTreeBase;
        newTrunk.transform.SetParent(this.transform);
        newTrunk.transform.localPosition = nextLocale;
        nextLocale.y += 1;
        thickSegmentCount++;

        bool endThick = false;

        do
        {
            int segmentNum = Random.Range(0, 5);

            switch(segmentNum)
            {
                case 0:
                    newTrunk = new GameObject("Thick Trunk " + thickSegmentCount.ToString());
                    newTrunk.AddComponent<SpriteRenderer>().sprite = thickTreeBase;
                    newTrunk.transform.SetParent(this.transform);
                    newTrunk.transform.localPosition = nextLocale;
                    nextLocale.y += 1;
                    thickSegmentCount++;
                    break;

                case 1:
                    newTrunk = new GameObject("Thick Trunk " + thickSegmentCount.ToString());
                    newTrunk.AddComponent<SpriteRenderer>().sprite = thickBranchRight;
                    newTrunk.transform.SetParent(this.transform);
                    newTrunk.transform.localPosition = nextLocale;
                    
                    thickSegmentCount++;

                    // call for thin segments
                    addThinSegments(new Vector3(nextLocale.x + 1, nextLocale.y, nextLocale.z), BranchDirection.Right);

                    nextLocale.y += 1;

                    // add regular segment after
                    newTrunk = new GameObject("Thick Trunk " + thickSegmentCount.ToString());
                    newTrunk.AddComponent<SpriteRenderer>().sprite = thickTreeBase;
                    newTrunk.transform.SetParent(this.transform);
                    newTrunk.transform.localPosition = nextLocale;
                    nextLocale.y += 1;
                    thickSegmentCount++;

                    break;

                case 2:
                    newTrunk = new GameObject("Thick Trunk " + thickSegmentCount.ToString());
                    newTrunk.AddComponent<SpriteRenderer>().sprite = thickBranchLeft;
                    newTrunk.transform.SetParent(this.transform);
                    newTrunk.transform.localPosition = nextLocale;

                    thickSegmentCount++;

                    // call for thin segments
                    addThinSegments(new Vector3(nextLocale.x - 1, nextLocale.y, nextLocale.z), BranchDirection.Left);

                    nextLocale.y += 1;

                    // add regular segment after
                    newTrunk = new GameObject("Thick Trunk " + thickSegmentCount.ToString());
                    newTrunk.AddComponent<SpriteRenderer>().sprite = thickTreeBase;
                    newTrunk.transform.SetParent(this.transform);
                    newTrunk.transform.localPosition = nextLocale;
                    nextLocale.y += 1;
                    thickSegmentCount++;

                    break;

                case 3:
                    newTrunk = new GameObject("Thick Trunk " + thickSegmentCount.ToString());
                    newTrunk.AddComponent<SpriteRenderer>().sprite = thickBranchBoth;
                    newTrunk.transform.SetParent(this.transform);
                    newTrunk.transform.localPosition = nextLocale;

                    thickSegmentCount++;

                    // call for thin segments
                    addThinSegments(new Vector3(nextLocale.x + 1, nextLocale.y, nextLocale.z), BranchDirection.Right);
                    addThinSegments(new Vector3(nextLocale.x - 1, nextLocale.y, nextLocale.z), BranchDirection.Left);

                    nextLocale.y += 1;

                    // add regular segment after
                    newTrunk = new GameObject("Thick Trunk " + thickSegmentCount.ToString());
                    newTrunk.AddComponent<SpriteRenderer>().sprite = thickTreeBase;
                    newTrunk.transform.SetParent(this.transform);
                    newTrunk.transform.localPosition = nextLocale;
                    nextLocale.y += 1;
                    thickSegmentCount++;

                    break;

                case 4:
                    newTrunk = new GameObject("Thick Trunk " + thickSegmentCount.ToString());
                    newTrunk.AddComponent<SpriteRenderer>().sprite = thickTaper;
                    newTrunk.transform.SetParent(this.transform);
                    newTrunk.transform.localPosition = nextLocale;
                    thickSegmentCount++;

                    // call for thin segments
                    addThinSegments(new Vector3(nextLocale.x, nextLocale.y + 1, nextLocale.z), BranchDirection.Up);

                    endThick = true;

                    break;

                default:
                    break;
            }

        } while (!endThick);
    }

    // Update is called once per frame
    void Update()
    {
        // always face camera
        transform.rotation = Quaternion.LookRotation(transform.position - Camera.main.transform.position);
    }

    void addThinSegments(Vector3 nextLocale, BranchDirection direction)
    {
        bool endThin = false;

        GameObject newTrunk = null;

        int thinSegmentCount = 0;

        do
        {
            int segmentNum = Random.Range(0, 6);

            switch(segmentNum)
            {
                case 0:
                    newTrunk = new GameObject("Thin Trunk " + thinSegmentCount.ToString());
                    newTrunk.AddComponent<SpriteRenderer>().sprite = thinBase;
                    newTrunk.transform.SetParent(this.transform);
                    newTrunk.transform.localPosition = nextLocale;

                    nextLocale = adjustForDirection(newTrunk, direction, nextLocale);
                    
                    thinSegmentCount++;
                    totalSmallSegments++;
                    break;

                case 1:
                    // don't use this when going right
                    if (direction != BranchDirection.Right)
                    {
                        newTrunk = new GameObject("Thin Trunk " + thinSegmentCount.ToString());
                        newTrunk.AddComponent<SpriteRenderer>().sprite = thinBranchRight;
                        newTrunk.transform.SetParent(this.transform);
                        newTrunk.transform.localPosition = nextLocale;

                        thinSegmentCount++;
                        totalSmallSegments++;

                        // if up branch right
                        if (direction == BranchDirection.Up)
                        {
                            addThinSegments(new Vector3(nextLocale.x + 1, nextLocale.y, nextLocale.z), BranchDirection.Right);
                        }
                        // else branch up
                        else
                        {
                            addThinSegments(new Vector3(nextLocale.x, nextLocale.y + 1, nextLocale.z), BranchDirection.Up);
                        }

                        nextLocale = adjustForDirection(newTrunk, direction, nextLocale);
                    }
                    break;

                case 2:
                    // don't use this when going left
                    if (direction != BranchDirection.Left)
                    {
                        newTrunk = new GameObject("Thin Trunk " + thinSegmentCount.ToString());
                        newTrunk.AddComponent<SpriteRenderer>().sprite = thinBranchLeft;
                        newTrunk.transform.SetParent(this.transform);
                        newTrunk.transform.localPosition = nextLocale;

                        thinSegmentCount++;
                        totalSmallSegments++;

                        // if up branch left
                        if (direction == BranchDirection.Up)
                        {
                            addThinSegments(new Vector3(nextLocale.x - 1, nextLocale.y, nextLocale.z), BranchDirection.Left);
                        }
                        // else branch up
                        else
                        {
                            addThinSegments(new Vector3(nextLocale.x, nextLocale.y + 1, nextLocale.z), BranchDirection.Up);
                        }

                        nextLocale = adjustForDirection(newTrunk, direction, nextLocale);
                    }
                    break;

                case 3:
                    // only use when adding up
                    if (direction == BranchDirection.Up)
                    {
                        newTrunk = new GameObject("Thin Trunk " + thinSegmentCount.ToString());
                        newTrunk.AddComponent<SpriteRenderer>().sprite = thinBranchBoth;
                        newTrunk.transform.SetParent(this.transform);
                        newTrunk.transform.localPosition = nextLocale;

                        thinSegmentCount++;
                        totalSmallSegments++;

                        // branch for both directions

                        addThinSegments(new Vector3(nextLocale.x + 1, nextLocale.y, nextLocale.z), BranchDirection.Right);

                        addThinSegments(new Vector3(nextLocale.x - 1, nextLocale.y, nextLocale.z), BranchDirection.Left);

                        nextLocale = adjustForDirection(newTrunk, direction, nextLocale);
                    }
                    break;
                    
                default:
                    newTrunk = new GameObject("Leaf End");
                    newTrunk.AddComponent<SpriteRenderer>().sprite = treeLeaves;
                    newTrunk.transform.SetParent(this.transform);
                    newTrunk.transform.localPosition = nextLocale;

                    leafPlacementList.Add(nextLocale);

                    endThin = true;
                    break;
            }

        } while (!endThin && totalSmallSegments < 100); // TODO: make better cutoff
    }

    Vector3 adjustForDirection(GameObject segment, BranchDirection direction, Vector3 nextLocale)
    {
        switch(direction)
        {
            case BranchDirection.Up:
                nextLocale.y += 1;
                break;
             
            case BranchDirection.Right:
                nextLocale.x += 1;
                segment.transform.Rotate(Vector3.forward * -90);
                break;
            case BranchDirection.Left:
                nextLocale.x -= 1;
                segment.transform.Rotate(Vector3.forward * 90);
                break;
            default:
                break;

        }

        return nextLocale;
    }
}
