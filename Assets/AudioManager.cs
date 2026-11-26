using UnityEngine;

public class AudioManager : MonoBehaviour
{
    AudioSource music;
    public AudioClip titleMusic;
    public AudioClip bgm;
    //Awake函式會比Start更早執行
    void Awake()  
    {
        music = GetComponent<AudioSource>();
        music.volume = PlayerPrefs.GetFloat("MusicVolume" , music.volume);
    }
    public void SetVolume (float value)
    {
        music.volume = value;
        PlayerPrefs.SetFloat("MusicVolume", value);
    }
    public float GetVolume()
    {
        return music.volume;
    }
        public void PlayTitle()
    {
        music.clip = titleMusic;
        music.loop = true;
        music.Play();
    }
    public void PlayBgm()
    {
        music.clip = bgm;
        music.loop = true;
        music.Play();
    }
    public void StopAudio()
    {
    music.Stop();
    }
}
