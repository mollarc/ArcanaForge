using System.Collections.Generic;
using FMOD.Studio;
using UnityEngine;

public class MusicTrack : MonoBehaviour
{
    public static MusicTrack Instance;
    void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        EventInstance eventInstance = FMODUnity.RuntimeManager.CreateInstance("event:/Fantasy");
        if (eventInstance.isValid())
        {
            eventInstance.start();
        }
    }
}
