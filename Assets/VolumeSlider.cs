using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    public AudioManager audioManager;
    public Slider slider;

    void Start()
    {
        slider.value = audioManager.GetVolume();   // 讓滑桿一開始就對應目前音量
        slider.onValueChanged.AddListener(audioManager.SetVolume);
    }
}