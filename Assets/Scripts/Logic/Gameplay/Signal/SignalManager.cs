using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyGameplay.Signal
{
	/*public class SignalManager : MonoBehaviour
	{
		#region Instance

		protected static SignalManager instance;

		public static SignalManager Inst => GetInstance();

		protected static SignalManager GetInstance()
		{
			if (instance)
				return instance;
			else
			{
				var go = GameObject.Find("SignalManager");
				if (go)
					instance = go.GetComponent<SignalManager>();
				return instance;
			}
		}

		protected void RegisterInstance(SignalManager obj)
		{
			instance = obj;
		}

		protected void RegisterThisInst()
		{
			RegisterInstance(this);
		}

		#endregion Instance


		[SerializeField]
		protected SignalLinkGroup m_signalLinkGroup;

		public SignalLinkGroup signalLinkGroup { get => m_signalLinkGroup; }

		protected void Awake()
		{
			RegisterThisInst();
			InitSignalLinkGroup();
		}

		protected void InitSignalLinkGroup()
		{
			if (!m_signalLinkGroup)
				m_signalLinkGroup = transform.Find("SignalTilemap").GetComponent<SignalLinkGroup>();
		}
	}*/
}
