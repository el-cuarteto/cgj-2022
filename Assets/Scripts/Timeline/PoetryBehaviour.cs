using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class PoetryBehaviour : PlayableBehaviour
{
    private Poetry poetry;
    public float easeInTime;
    public float easeOutTime;
    public string poetryText;

    public override void ProcessFrame(Playable playable, FrameData info, object playerData)
    {
        Poetry bind = playerData as Poetry;
        if (bind == null)
            return;

        if (poetry != null)
            return;

        poetry = bind;
        poetry.ShowPoetry(easeInTime, easeOutTime, (float)playable.GetDuration(), poetryText);
    }
}
