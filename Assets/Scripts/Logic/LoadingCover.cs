using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyGameplay.Mechanism;
using GridMovement;
using MyGameplay.GameData;

namespace MyGameplay.UI
{
	public class LoadingCover : MonoBehaviour
	{
		protected void Awake()
		{
			gameObject.SetActive(true);
		}

		protected void Start()
		{
			gameObject.SetActive(false);
		}
	}
}
