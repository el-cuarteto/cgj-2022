using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class PoetryClipAsset : PlayableAsset
{
    public float easeInTime = 0.25f;
    public float easeOutTime = 0.25f;
    public string poetryText = "";

    public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
    {
        ScriptPlayable<PoetryBehaviour> playable = ScriptPlayable<PoetryBehaviour>.Create(graph);
        
        PoetryBehaviour poetryBehaviour = playable.GetBehaviour();
        poetryBehaviour.easeInTime = easeInTime;
        poetryBehaviour.easeOutTime = easeOutTime;
        poetryBehaviour.poetryText = poetryText;

        return playable;
    }
}
