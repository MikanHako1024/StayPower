using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyGameplay.Signal;

namespace MyGameplay.Mechanism
{
	public class MechanismLock : BaseSignalMechanism
	{
		//[SerializeField]
		protected bool m_locked = true;

		public override bool isPushable()
		{
			return false;
		}

		public override bool isObstructable()
		{
			return m_locked;
		}

		public override bool isPassable()
		{
			return !m_locked;
		}


		[SerializeField, Min(0)]
		public int upPowerDemand = 0;
		[SerializeField, Min(0)]
		public int downPowerDemand = 0;
		[SerializeField, Min(0)]
		public int leftPowerDemand = 0;
		[SerializeField, Min(0)]
		public int rightPowerDemand = 0;

		public virtual bool CheckStateLock(SignalPowerCount powerCount)
		{
			if (upPowerDemand > (powerCount != null ? powerCount.up : 0))
				return true;
			if (downPowerDemand > (powerCount != null ? powerCount.down : 0))
				return true;
			if (leftPowerDemand > (powerCount != null ? powerCount.left : 0))
				return true;
			if (rightPowerDemand > (powerCount != null ? powerCount.right : 0))
				return true;
			return false;
		}

		public virtual bool CheckStateUnlock(SignalPowerCount powerCount)
		{
			if (upPowerDemand != (powerCount != null ? powerCount.up : 0))
				return false;
			if (downPowerDemand != (powerCount != null ? powerCount.down : 0))
				return false;
			if (leftPowerDemand != (powerCount != null ? powerCount.left : 0))
				return false;
			if (rightPowerDemand != (powerCount != null ? powerCount.right : 0))
				return false;
			return true;
		}

		public virtual bool CheckStateOverload(SignalPowerCount powerCount)
		{
			if (upPowerDemand < (powerCount != null ? powerCount.up : 0))
				return true;
			if (downPowerDemand < (powerCount != null ? powerCount.down : 0))
				return true;
			if (leftPowerDemand < (powerCount != null ? powerCount.left : 0))
				return true;
			if (rightPowerDemand < (powerCount != null ? powerCount.right : 0))
				return true;
			return false;
		}


		//[SerializeField]
		//protected Color lockColor = new Color(1, 1, 1, 1);
		//[SerializeField]
		//protected Color unlockColor = new Color(.2f, 1, 0, 1);
		//[SerializeField]
		//protected Color overloadColor = new Color(.9f, 0, 0, 1);

		[SerializeField]
		protected Sprite lockSprite;
		[SerializeField]
		protected Sprite unlockSprite;
		[SerializeField]
		protected Sprite overloadSprite;

		public virtual void SetLockStateLock()
		{
			m_locked = true;
			//m_spriteRenderer.color = lockColor;
			m_spriteRenderer.sprite = lockSprite;

			//InvokeLockEvent();
		}
		public virtual void SetLockStateUnlock()
		{
			m_locked = false;
			//m_spriteRenderer.color = unlockColor;
			m_spriteRenderer.sprite = unlockSprite;

			//InvokeUnlockEvent();
		}
		public virtual void SetLockStateOverload()
		{
			m_locked = true;
			//m_spriteRenderer.color = overloadColor;
			m_spriteRenderer.sprite = overloadSprite;
		}


		public void RefreshLockState()
		{
			var powerCount = SignalLinkGroup.Inst.GetGroupPowerCount(m_v2GridPos);
			// TODO : 优化缓存

			if (CheckStateUnlock(powerCount))
				SetLockStateUnlock();
			else if (CheckStateLock(powerCount))
				SetLockStateLock();
			else
				SetLockStateOverload();
		}

		public override void OnSignalPowerHereChanged()
		{
			RefreshLockState();
		}


		public override void OnRegisterCharacter()
		{
			base.OnRegisterCharacter();
			RefreshLockState();
		}


		/*[SerializeField]
		protected UnityEngine.Events.UnityEvent onUnlockEvent;
		[SerializeField]
		protected UnityEngine.Events.UnityEvent onLockEvent;

		protected void InvokeUnlockEvent()
		{
			onUnlockEvent.Invoke();
		}
		protected void InvokeLockEvent()
		{
			onLockEvent.Invoke();
		}*/
	}
}
