using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GridMovement
{
	// ？支持人物走到时 触发事件的物体 ...

	// ？改 物体自身检测人物 ...
	// ？为 人物检测物体 ...

	public class CharacterTrigger : CharacterCommon
	{
		[SerializeField]
		protected bool hideAfterTrigger = true;

		[SerializeField]
		protected bool onceTrigger = true;


		[SerializeField]
		public bool triigerEnabled = true;

		public void SetTriggerEnabled(bool enabled)
		{
			triigerEnabled = enabled;
		}

		protected bool CanTriggerCharacter()
		{
			return triigerEnabled && isRegistered && gameObject.activeSelf;
		}


		//public bool CanTriggerCharacter()
		public bool CanContactTriggerCharacter()
		{
			//return triigerEnabled && isRegistered && gameObject.activeSelf;
			//return !triggerByActionZ && triigerEnabled && isRegistered && gameObject.activeSelf;
			return !triggerByActionZ && CanTriggerCharacter();
		}

		//public void OnPlayerTriggerCharacter()
		//public void OnPlayerTriggerCharacter(BaseCharacterPlayer player)
		public void OnPlayerContactTriggerCharacter(BaseCharacterPlayer player)
		{
			//if (CanTriggerCharacter())
			if (CanContactTriggerCharacter())
			{
				//InvokeTriggerAction();
				InvokeTriggerEvent();
				OnTriggerSuccess();
			}
		}


		[SerializeField]
		//protected bool triggerByActionZ = false;
		public bool triggerByActionZ = false;

		public bool CanActionZTriggerCharacter()
		{
			return triggerByActionZ && CanTriggerCharacter();
		}

		public void OnPlayerActionZTriggerCharacter(BaseCharacterPlayer player)
		{
			if (CanActionZTriggerCharacter())
			{
				InvokeTriggerEvent();
				OnTriggerSuccess();
			}
		}


		protected void OnTriggerSuccess()
		{
			//if (hideAfterTrigger)
			if (onceTrigger)
			{
				triigerEnabled = false;
				//gameObject.SetActive(false);
				GridGameMap.Inst.DeregisterCharacter(this);
			}
			if (hideAfterTrigger)
				gameObject.SetActive(false);
		}


		[SerializeField]
		//protected System.Action onTriggerAction;
		protected UnityEngine.Events.UnityEvent onTriggerEvent;

		//protected void InvokeTriggerAction()
		//	onTriggerAction.Invoke();
		//}
		protected void InvokeTriggerEvent()
		{
			onTriggerEvent.Invoke();
		}


		//public void DebugPrint()
		//{
		//	Debug.Log(this);
		//}
	}
}
