using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyGameplay.Logic
{
	public abstract class BaseGameLogic : MonoBehaviour
	{
		[SerializeField]
		public string logicName;


		//[HideInInspector]
		public bool logicActived = false;

		public bool IsLogicActived()
		{
			return logicActived;
		}

		public void SetLogicActived(bool actived)
		{
			if (logicActived != actived)
			{
				logicActived = actived;
				//if (logicActived)
				//	OnLogicActiveOn();
				//else
				//	OnLogicActiveOff();
			}
		}

		public virtual void OnLogicActiveOn()
		{
			SetLogicActived(true);
		}
		public virtual void OnLogicActiveOff()
		{
			SetLogicActived(false);
		}


		protected void UpdateLogicIfActived()
		{
			if (IsLogicActived())
				UpdateGameLogic();
		}

		protected virtual void UpdateGameLogic()
		{
		}


		protected virtual void Update()
		{
			UpdateLogicIfActived();
		}
	}
}
