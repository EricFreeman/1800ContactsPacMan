﻿using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Managers
{
    public class MusicManager : MonoBehaviour
    {
        public List<AudioClip> Songs;

        private AudioSource _audio;
        private int _lastSong;

        void Start()
        {
            _audio = GetComponent<AudioSource>();

            // so it won't only play first song evey time level starts
            _lastSong = -1;

            DontDestroyOnLoad(gameObject);
            if (!AlreadyExists())
            {
                PlayNewSong();
            }
        }

        void Update()
        {
            if (!_audio.isPlaying || Input.GetKeyDown(KeyCode.Q))
            {
                PlayNewSong();
            }
        }

        private bool AlreadyExists()
        {
            // because this object isn't destroyed when switching levels,
            // there should already be one in the scene on load unless
            // you're starting a specific level from the editor
            if (FindObjectsOfType<MusicManager>().Length > 1)
            {
                DestroyImmediate(gameObject);
                return true;
            }

            return false;
        }

        private void PlayNewSong()
        {
            var newSong = _lastSong;
            var rand = new System.Random(DateTime.Now.Millisecond);

            while (newSong == _lastSong)
            {
                newSong = rand.Next(0, Songs.Count);
            }

            _audio.clip = Songs[newSong];
            _audio.Play();
            _lastSong = newSong;
        }
    }
}