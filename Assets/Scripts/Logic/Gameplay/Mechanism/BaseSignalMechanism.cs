using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GridMovement;

namespace MyGameplay.Mechanism
{
	[RequireComponent(typeof(SpriteRenderer))]
	public class BaseSignalMechanism : CharacterCommon
	{
		[SerializeField]
		protected SpriteRenderer m_spriteRenderer;

		protected void InitSpriteRenderer()
		{
			if (!m_spriteRenderer)
				m_spriteRenderer = GetComponent<SpriteRenderer>();
		}


		protected virtual void Awake()
		{
			InitSpriteRenderer();
		}
	}
}
