using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyGameplay.Signal;
using GridMovement;

namespace MyGameplay.Mechanism
{
	/*
	//[RequireComponent(typeof(SpriteRenderer))]
	//public class MechanismDoor : CharacterCommon
	public class MechanismDoor : BaseSignalMechanism
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
		protected int upPowerDemand = 0;
		[SerializeField, Min(0)]
		protected int downPowerDemand = 0;
		[SerializeField, Min(0)]
		protected int leftPowerDemand = 0;
		[SerializeField, Min(0)]
		protected int rightPowerDemand = 0;

		public bool CheckStateLock(SignalPowerCount powerCount)
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

		public bool CheckStateUnlock(SignalPowerCount powerCount)
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

		public bool CheckStateOverload(SignalPowerCount powerCount)
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


		[SerializeField]
		protected Color lockColor = new Color(1, 1, 1, 1);

		[SerializeField]
		protected Color unlockColor = new Color(.2f, 1, 0, 1);

		[SerializeField]
		protected Color overloadColor = new Color(.9f, 0, 0, 1);

		public void SetLockStateLock()
		{
			m_locked = true;
			m_spriteRenderer.color = lockColor;
		}
		public void SetLockStateUnlock()
		{
			m_locked = false;
			m_spriteRenderer.color = unlockColor;
		}
		public void SetLockStateOverload()
		{
			m_locked = true;
			m_spriteRenderer.color = overloadColor;
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
	}
	*/

	public class MechanismDoor : MechanismLock
	{
		public override bool isObstructable()
		{
			return obstructable;
		}

		public override bool isPassable()
		{
			return passable;
		}


		[SerializeField]
		public bool needKey = true;

		[SerializeField]
		public bool hasKey = false;


		public void SetHasKeyState(bool hasKey)
		{
			this.hasKey = hasKey;
			RefreshLockState();
		}

		public bool IsLockedByKey()
		{
			return needKey && !hasKey;
		}

		public override bool CheckStateLock(SignalPowerCount powerCount)
		{
			if (IsLockedByKey())
				return true;
			else
				return base.CheckStateLock(powerCount);
		}

		public override bool CheckStateUnlock(SignalPowerCount powerCount)
		{
			if (IsLockedByKey())
				return false;
			else
				return base.CheckStateUnlock(powerCount);
		}


		[SerializeField]
		protected TeleportTrigger teleTrigger;

		public override void SetLockStateLock()
		{
			base.SetLockStateLock();

			if (teleTrigger)
				teleTrigger.SetTriggerEnabled(false);
		}
		public override void SetLockStateUnlock()
		{
			base.SetLockStateUnlock();

			if (teleTrigger)
				teleTrigger.SetTriggerEnabled(true);
		}
	}
}
