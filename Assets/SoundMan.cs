using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundMan : MonoBehaviour
{
    public AudioSource playaudio;  
    public AudioSource Stopaudio;  
    // Start is called before the first frame update      
    void Start() {}  
    // Update is called once per frame      
    void Update() {}  
    public void PlayMusic() {  
        playaudio.Play();  
        Debug.Log("play");  
    }  
    
    public void StopMusic() {  
        Stopaudio.Stop();  
        Debug.Log("stop");  
    }  
}
