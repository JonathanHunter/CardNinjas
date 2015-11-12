﻿using UnityEngine;

namespace Assets.Scripts.Util
{
    public class SoundPlayer : MonoBehaviour
    {
        public AudioClip[] song;
        public new AudioSource audio;
        public bool SFX;
        public bool playOnLoad;
        public bool loop;
        public bool intro;
        public int loopSong;
        public bool dontDestroy;

        void Start()
		{
			Managers.GameManager.SFXVol = 1;
			Managers.GameManager.MusicVol = 1;

            audio = GetComponent<AudioSource>();
            if (playOnLoad)
                PlaySong(0);
            if (dontDestroy)
                DontDestroyOnLoad(this.gameObject);
            if (SFX)
                audio.volume = Managers.GameManager.SFXVol;
            else
                audio.volume = Managers.GameManager.MusicVol;
        }

        void Update()
        {
            if (intro && !audio.isPlaying)
            {
                intro = false;
                PlaySong(1);
            }
        }

        public void PlaySong(int index)
        {
            audio.Stop();
            audio.loop = loop && index == loopSong;
            audio.clip = song[index];
            audio.Play();
        }

        public void Pause()
        {
            audio.Pause();
        }

        public void SetVolume(float vol)
        {
            audio.volume = vol;
        }
    }
}

