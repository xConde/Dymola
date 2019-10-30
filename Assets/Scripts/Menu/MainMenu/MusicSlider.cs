using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicSlider : MonoBehaviour
{
    List<AudioSource> musicList = new List<AudioSource>();

    // Start is called before the first frame update
    public void Start()
    {
        AudioSource[] songOnly = GameObject.FindWithTag("Data").GetComponentsInChildren<AudioSource>();
        musicList.Add(songOnly[0]);

        Slider musicSlider = this.GetComponent<Slider>();

        if (PlayerPrefs.HasKey("musicvolume"))
        {
            musicSlider.value = PlayerPrefs.GetFloat("musicvolume");
            UpdateMusicVol(musicSlider.value);
        }
        else
        {
            musicSlider.value = 1;
            UpdateMusicVol(1);
        }



    }

    public void UpdateMusicVol(float value)
    {
        PlayerPrefs.SetFloat("musicvolume", value);
        foreach (AudioSource m in musicList)
        {
            m.volume = value;
        }
    }
}
