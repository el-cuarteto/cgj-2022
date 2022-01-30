using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using UnityEngine.SceneManagement;

public class EndDirector : MonoBehaviour
{
    public PlayableDirector director = null;
    public TimelineAsset timeline = null;
    public int menuScene = 0;


    public void StartCredits()
    {
        director.Play(timeline);
    }


    public void GoToMenu()
    {
        SceneManager.LoadScene(menuScene);
    }
}
