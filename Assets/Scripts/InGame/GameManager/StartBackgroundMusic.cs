using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartBackgroundMusic : MonoBehaviour
{

    [SerializeField] private AudioClip audioClip;
    [SerializeField] private float pitchChangeDuration = 2f;
    private float musicSpeed = 1f;
    
    private AudioSource audioSource;
    
    // Start is called before the first frame update
    void Start()
    {
        SubToEvents();
        
        musicSpeed = 1.5f;
        
        audioSource = GameObject.FindWithTag("AudioSource_Music").GetComponent<AudioSource>();
        audioSource.pitch = musicSpeed;
        audioSource.loop = true;
        audioSource.clip = audioClip;
        audioSource.Play();
    }

    private void SubToEvents()
    {

        GetComponent<State_Phase>().Phase1.AddListener((phaseObj) =>
        {
            musicSpeed = 1.4f;
            StartCoroutine(ChangePitchOverTime(musicSpeed));
            // audioSource.pitch = musicSpeed;
        });
        
        GetComponent<State_Phase>().Phase2.AddListener((phaseObj) =>
        {
            musicSpeed = 1.25f;
            StartCoroutine(ChangePitchOverTime(musicSpeed));
            // audioSource.pitch = musicSpeed;
        });
        
        GetComponent<State_Phase>().Phase2.AddListener((phaseObj) =>
        {
            musicSpeed = 1f;
            StartCoroutine(ChangePitchOverTime(musicSpeed));
            // audioSource.pitch = musicSpeed;
        });
    }

    private IEnumerator ChangePitchOverTime(float toPitch)
    {
        float currentTime = 0f;
        while (currentTime < pitchChangeDuration)
        {
            float lerpedPitch = Mathf.Lerp(audioSource.pitch, toPitch, currentTime / pitchChangeDuration);
            audioSource.pitch = lerpedPitch;
            currentTime += Time.deltaTime;
            yield return null; // Wait for the next frame
        }

        // Ensure that the pitch ends at the desired value
        audioSource.pitch = toPitch;
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
