using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class BlobToStep : MonoBehaviour
{
    public List<PathStep> PathSteps;
    public float MapSize = 32;
    public float TrackHeight = 0;
    public float MinDistToDetect = 2;
    
    private BlobManager BlobManager;
    void Awake()
    {
        BlobManager = GetComponent<BlobManager>();
    }

    void Update()
    {
           foreach(Blob b in BlobManager.blobs)
           {
               if (!b.active)
                   continue;
               
               Vector3 bPos = new Vector3(BlobCoordToWorld(b.x), TrackHeight, BlobCoordToWorld(b.y));
               foreach (PathStep pathStep in PathSteps)
               {
                   float dist = Vector3.Distance(bPos, pathStep.transform.position);
                   if (dist <= MinDistToDetect)
                   {
                       pathStep.StatusInRange();
                   }
               }
           }
    }

    private float BlobCoordToWorld(float coord)
    {
        return coord * MapSize - MapSize/2;
    }
}
