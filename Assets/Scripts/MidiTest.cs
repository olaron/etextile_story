using System.Collections;
using System.Collections.Generic;
using MidiJack;
using UnityEngine;

public class MidiTest : MonoBehaviour
{
    const MidiChannel BLOB_X_CHANNEL = MidiChannel.Ch3;
    const MidiChannel BLOB_Y_CHANNEL = MidiChannel.Ch4;
    const MidiChannel BLOB_Z_CHANNEL = MidiChannel.Ch5;
    const MidiChannel BLOB_W_CHANNEL = MidiChannel.Ch6;
    const MidiChannel BLOB_H_CHANNEL = MidiChannel.Ch7;

    public Renderer rend;
    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
        rend.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        // MidiMaster.GetKey(MidiChannel.Ch1, <0-7> (blob id))
        // MidiMaster.GetKnob(<blob parameter channel>,(blob id),0)
        rend.enabled = MidiMaster.GetKey(MidiChannel.Ch1, 0) > 0;
        float x = MidiMaster.GetKnob(BLOB_X_CHANNEL, 0,0) * 16;
        float y = MidiMaster.GetKnob(BLOB_Y_CHANNEL, 0,0) * 16;
        Vector3 newPos = new Vector3(x, 0, y);
        transform.position = newPos;
    }
}
