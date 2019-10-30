using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundSlider : MonoBehaviour
{
    List<AudioSource> soundfx = new List<AudioSource>();

    // Start is called before the first frame update
    public void Start()
    {
        AudioSource[] audioRepository = GameObject.FindWithTag("Data").GetComponentsInChildren<AudioSource>();
        for (int i = 1; i < audioRepository.Length; i++)
            soundfx.Add(audioRepository[i]);

        Slider soundfxSlider = this.GetComponent<Slider>();

        if (PlayerPrefs.HasKey("soundfxvolume"))
        {
            soundfxSlider.value = PlayerPrefs.GetFloat("soundfxvolume");
            UpdateSoundVolume(soundfxSlider.value);
        }
        else
        {
            soundfxSlider.value = 1;
            UpdateSoundVolume(1);
        }



    }
    public void UpdateSoundVolume(float value)
    {
        PlayerPrefs.SetFloat("sfxvolume", value);
        foreach (AudioSource s in soundfx)
        {
            s.volume = value;
        }
    }
}
