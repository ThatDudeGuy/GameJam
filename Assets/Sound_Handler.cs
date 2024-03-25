using UnityEngine;

public class Sound_Handler : MonoBehaviour
{
    public AudioSource gameMusic;
    public AudioClip yourSoundClip;

    void Start()
    {
        // Set the audio clip for the AudioSource
        gameMusic.clip = yourSoundClip;
        gameMusic.Play();
    }

    // Update is called once per frame
    // void Update()
    // {
        
    // }
}
