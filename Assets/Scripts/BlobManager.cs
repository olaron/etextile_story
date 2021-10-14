using System.Collections;
using System.Collections.Generic;
using MidiJack;
using UnityEngine;

public class Blob
{
    public bool active;
    public float x;
    public float y;
    public float z;
    public float w;
    public float h;

    public Blob(bool active, float x, float y, float z, float w, float h)
    {
        this.active = active;
        this.x = x;
        this.y = y;
        this.z = z;
        this.w = w;
        this.h = h;
    }
}

public class BlobManager : MonoBehaviour
{
    public const int MAX_BLOBS = 8; 
    private const MidiChannel BLOB_X_CHANNEL = MidiChannel.Ch3;
    private const MidiChannel BLOB_Y_CHANNEL = MidiChannel.Ch4;
    private const MidiChannel BLOB_Z_CHANNEL = MidiChannel.Ch5;
    private const MidiChannel BLOB_W_CHANNEL = MidiChannel.Ch6;
    private const MidiChannel BLOB_H_CHANNEL = MidiChannel.Ch7;

    public Blob[] blobs = new Blob[MAX_BLOBS];
    
    private static MidiChannel Channel(int i)
    {
        return (MidiChannel) i;
    }

    public Blob GetBlob(int i)
    {
        return blobs[i];
    }
    
    void Start()
    {
        for (int i = 0; i < MAX_BLOBS; i++)
        {
            blobs[i] = new Blob(false, 0,0,0,0,0);
        }
    }
    
    void Update()
    {
        for (int i = 0; i < MAX_BLOBS; i++)
        {
            bool active = MidiMaster.GetKey(MidiChannel.Ch1, i) > 0;
            float x = MidiMaster.GetKnob(BLOB_X_CHANNEL, i,0);
            float y = MidiMaster.GetKnob(BLOB_Y_CHANNEL, i,0);
            float z = MidiMaster.GetKnob(BLOB_Z_CHANNEL, i,0);
            float w = MidiMaster.GetKnob(BLOB_W_CHANNEL, i,0);
            float h = MidiMaster.GetKnob(BLOB_H_CHANNEL, i,0);
            blobs[i] = new Blob(active, x, y, z, w, h);
        }
    }
}