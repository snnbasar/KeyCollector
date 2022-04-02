using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum Soundlar
{
    Victory,
    Killed,
    Collect
}
public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    public List<AudioSource> sounds = new List<AudioSource>();

    private Soundlar soundlar;


    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            sounds.Add(transform.GetChild(i).GetComponent<AudioSource>());
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayMusic(Soundlar soundfx)
    {
        soundlar = soundfx;
        SoundsIndexes();
    }

    private void SoundsIndexes()
    {
        switch (soundlar)
        {
            case Soundlar.Victory:
                sounds[1].Play();
                break;
            case Soundlar.Killed:
                sounds[2].Play();
                break;
            case Soundlar.Collect:
                sounds[3].Play();
                break;
            default:
                break;
        }
    }

}
