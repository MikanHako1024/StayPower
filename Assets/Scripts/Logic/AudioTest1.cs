using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI = UnityEngine.UI;

public class AudioTest1 : MonoBehaviour
{
	public AudioClip audio1;
	public AudioClip audio2;

	//[ContextMenu("PlayAudio1ToCamera")]
	//public void PlayAudio1ToCamera()
	//{
	//	if (audio1)
	//		AudioManager.Inst.PlayCameraAudio(audio1);
	//}

	[ContextMenu("PlayAudio1ToSelf")]
	public void PlayAudio1ToSelf()
	{
		var audioSource = GetComponent<AudioSource>();
		audioSource.clip = audio1;
		audioSource.Play();
	}


	[ContextMenu("PlayAudio1ToMgrBgm")]
	public void PlayAudio1ToMgrBgm()
	{
		AudioManager.Inst.PlayBgm(audio1);
	}

	[ContextMenu("PlayAudio2ToMgrSe")]
	public void PlayAudio2ToMgrSe()
	{
		AudioManager.Inst.PlaySe(audio2);
	}

	[ContextMenu("StopAllMgrSe")]
	public void StopAllMgrSe()
	{
		AudioManager.Inst.StopAllSe();
	}
}
