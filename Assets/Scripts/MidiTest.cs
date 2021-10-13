using System.Collections;
using System.Collections.Generic;
using System.Security.Authentication.ExtendedProtection;
using MidiJack;
using UnityEngine;

public class MidiTest : MonoBehaviour
{

    public BlobManager blobManager;
    public GameObject objectToSpawn;
    public float mapScale = 16;
    
    private GameObject[] objects = new GameObject[BlobManager.MAX_BLOBS];
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < BlobManager.MAX_BLOBS; i++)
        {
            objects[i] = Instantiate(objectToSpawn, Vector3.zero, Quaternion.identity);
            objects[i].GetComponent<Renderer>().enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < BlobManager.MAX_BLOBS; i++)
        {
            Blob blob = blobManager.GetBlob(i);
            objects[i].GetComponent<Renderer>().enabled = blob.active;
            Vector3 newPos = new Vector3(blob.x * mapScale, 0, (1-blob.y) * mapScale);
            objects[i].transform.position = newPos;
        }
    }
}
