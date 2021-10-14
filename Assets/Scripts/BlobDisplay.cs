using System.Collections;
using System.Collections.Generic;
using System.Security.Authentication.ExtendedProtection;
using MidiJack;
using UnityEngine;

public class BlobDisplay : MonoBehaviour
{

    public BlobManager blobManager;
    public GameObject objectToSpawn;
    public float mapScale = 16;

    private bool test = false;
    
    private GameObject[] objects = new GameObject[BlobManager.MAX_BLOBS];
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < BlobManager.MAX_BLOBS; i++)
        {
            objects[i] = Instantiate(objectToSpawn, Vector3.zero, Quaternion.identity);
            objects[i].GetComponent<Renderer>().enabled = false;
            objects[i].GetComponent<Renderer>().material.color = Color.HSVToRGB((float)i/BlobManager.MAX_BLOBS,1,1);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!test)
        {
            test = true;
            AkSoundEngine.PostEvent("play_mood_tension", gameObject);
        }
        for (int i = 0; i < BlobManager.MAX_BLOBS; i++)
        {
            Blob blob = blobManager.GetBlob(i);
            objects[i].GetComponent<Renderer>().enabled = blob.active;
            Vector3 newPos = new Vector3(blob.x * mapScale, 0, (1-blob.y) * mapScale);
            objects[i].transform.position = newPos;
            Vector3 newSize = new Vector3(0.1f + blob.w*10, 0.1f, 0.1f + blob.h*10);
            objects[i].transform.localScale = newSize;
        }
    }
}