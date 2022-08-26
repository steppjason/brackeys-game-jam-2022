using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
	[SerializeField] AudioSource _music;
	[SerializeField] AudioSource[] _sfx;

	[Range(0, 1)] public float masterVolume = 1f;
	[Range(0, 1)] public float musicVolume = 1f;
	[Range(0, 1)] public float sfxVolume = 1f;

	float MusicVolume { get { return musicVolume * masterVolume; } }
	float SFXVolume { get { return sfxVolume * masterVolume; } }

	void Update()
	{
		SetVolumes();
	}

	public void SetVolumes()
	{
		for (int i = 0; i < _sfx.Length; i++)
		{
			_sfx[i].volume = SFXVolume;
		}
		_music.volume = MusicVolume;
	}

	public void SetMasterVolume(float volume)
	{
		masterVolume += volume;
		SetVolumes();
	}

	public void SetMusicVolume(float volume)
	{
		musicVolume += volume;
		SetVolumes();
	}

	public void SetSFXVolume(float volume)
	{
		sfxVolume += volume;
		SetVolumes();
	}

	public void PlaySFX(int index, AudioClip audioClip)
	{
		if(!_sfx[index].isPlaying)
			_sfx[index].PlayOneShot(audioClip);
	}
}
