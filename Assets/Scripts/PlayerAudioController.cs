using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerAudioController : MonoBehaviour
{
    [SerializeField] NamedAudioClip[] audioClips;
    static NamedAudioClip[] staticAudioClips;

    private void Start()
    {
        staticAudioClips = audioClips;
    }

    public static void PlayClip(string name, Vector3 pos, float minVolume = .5f)
    {
        AudioClip clip = null;

        foreach (NamedAudioClip namedClip in staticAudioClips)
        {
            if (namedClip.name == name)
            {
                clip = namedClip.clip;  break;
            }
        }
        if (clip == null)   return;

        AudioSource.PlayClipAtPoint(clip, pos, Random.Range(minVolume, 1f));
    }
}

[System.Serializable]
struct NamedAudioClip
{
    public AudioClip clip;
    public string name;
}
