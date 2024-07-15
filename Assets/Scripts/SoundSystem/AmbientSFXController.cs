using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbientSFXController : MonoBehaviour
{
    [Header("Ambient Music")]
    [SerializeField] AudioSource musicAudioSource;
    [SerializeField] float musicTargetVolume;
    [SerializeField] float MUSIC_VOLUME_MIN;
    [SerializeField] float MUSIC_VOLUME_MAX;
    [Space]
    [SerializeField] float musicTimer;
    [SerializeField] float MUSIC_TIMER_MIN;
    [SerializeField] float MUSIC_TIMER_MAX;

    [Header("Babbling Brook Ambient SFX")]
    [SerializeField] AudioSource ambientSFXAudioSource;
    [SerializeField] float ambientSFXTargetVolume;
    [SerializeField] float AMBIENT_SFX_VOLUME_MIN;
    [SerializeField] float AMBIENT_SFX_VOLUME_MAX;
    [Space]
    [SerializeField] float ambientSFXTimer;
    [SerializeField] float AMBIENT_SFX_TIMER_MIN;
    [SerializeField] float AMBIENT_SFX_TIMER_MAX;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MusicVolumeControl();
        AmbinetSFXVolumeControl();
    }

    private void MusicVolumeControl()
    {
        //if timer is less than or = 0
        if(musicTimer <= 0)
        {
            //find a new music volume
            musicTargetVolume = Random.Range(MUSIC_VOLUME_MIN, MUSIC_VOLUME_MAX);

            //find a new music timer
            musicTimer = Random.Range(MUSIC_TIMER_MIN, MUSIC_TIMER_MAX);
        }
        else
        {
            //decrement music timer;
            musicTimer -= Time.deltaTime;

            //slowly lerp volume
            musicAudioSource.volume = Mathf.Lerp(musicAudioSource.volume, musicTargetVolume, 0.01f);
        }
    }

    private void AmbinetSFXVolumeControl()
    {
        //if timer is less than or = 0
        if (ambientSFXTimer <= 0)
        {
            //find a new music volume
            ambientSFXTargetVolume = Random.Range(AMBIENT_SFX_VOLUME_MIN, AMBIENT_SFX_VOLUME_MAX);

            //find a new music timer
            ambientSFXTimer = Random.Range(AMBIENT_SFX_TIMER_MIN, AMBIENT_SFX_TIMER_MAX);
        }
        else
        {
            //decrement music timer;
            ambientSFXTimer -= Time.deltaTime;

            //slowly lerp volume
            ambientSFXAudioSource.volume = Mathf.Lerp(ambientSFXAudioSource.volume, ambientSFXTargetVolume, 0.01f);
        }
    }
}
