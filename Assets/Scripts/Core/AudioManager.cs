using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : BaseManagerMono<AudioManager>
{
	//protected AudioListener m_audioListener;

	public Camera GetAnyOneCamera()
	{
		if (Camera.current)
			return Camera.current;
		else if (Camera.allCameras.Length > 0)
			return Camera.allCameras[0];
		else
			return null;
	}

	/*public AudioListener GetAnyOneAudioListener()
	{
		//return GetAnyOneCamera()?.GetComponent<AudioListener>();
		var camera = GetAnyOneCamera();
		return camera ? camera.GetComponent<AudioListener>() : null;
	}*/

	//protected AudioSource m_cameraAudioSource;

	//public AudioSource GetCameraAudioSource()
	//{
	//	var camera = GetAnyOneCamera();
	//	return camera ? camera.GetComponent<AudioSource>() : null;
	//}

	public override void InitManager()
	{
		base.InitManager();
		//m_audioListener = GetAnyOneAudioListener();
		//m_cameraAudioSource = GetCameraAudioSource();

		InitAudioSources2dContainer();
	}

	protected GameObject audioSources2dContainer;

	protected AudioSource bgmAudioSource;
	protected List<AudioSource> seAudioSources;

	protected int seAudioSize = 10;

	protected int seAudioSourcesSize = 0;
	protected int seAudioSourcesNext = 0;

	public void InitAudioSources2dContainer()
	{
		var go = new GameObject("CameraAudioSourceContainer");
		go.transform.SetParent(transform);

		bgmAudioSource = go.AddComponent<AudioSource>();
		seAudioSources = new List<AudioSource>(seAudioSize);
		for (var i = 0; i < seAudioSize; i++)
			seAudioSources.Add(go.AddComponent<AudioSource>());
		seAudioSourcesSize = seAudioSize;

		audioSources2dContainer = go;
	}

	//public void PlayCameraAudio(AudioClip audio)
	//{
	//	m_cameraAudioSource.clip = audio;
	//	m_cameraAudioSource.Play();
	//}

	public void PlayBgm(AudioClip audio)
	{
		bgmAudioSource.Stop();
		bgmAudioSource.clip = audio;
		bgmAudioSource.loop = true;
		bgmAudioSource.Play();
	}
	public void ReplayBgm()
	{
		bgmAudioSource.Play();
	}
	public void PauseBgm()
	{
		bgmAudioSource.Pause();
	}
	public void StopBgm()
	{
		bgmAudioSource.Stop();
	}

	public AudioSource GetNextSeAudioSource()
	{
		var result = seAudioSources[seAudioSourcesNext];
		seAudioSourcesNext = (seAudioSourcesNext + 1) % seAudioSourcesSize;
		return result;
	}

	public void PlaySe(AudioClip audio)
	{
		var seAudioSource = GetNextSeAudioSource();
		seAudioSource.Stop();
		seAudioSource.clip = audio;
		seAudioSource.loop = false;
		seAudioSource.Play();
	}
	public void StopAllSe()
	{
		foreach (var each in seAudioSources)
			each.Stop();
	}

	// TODO : ½¥Èë½¥³ö
}
