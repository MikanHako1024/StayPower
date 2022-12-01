using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyGameplay.GameData;

namespace MyGameplay.Mechanism
{
	public class MechanismLevelSele : MechanismText
	{
		protected bool m_levelUnlock = false;

		public override bool isObstructable()
		{
			return !m_levelUnlock;
		}

		public override bool isPassable()
		{
			return m_levelUnlock;
		}


		[SerializeField]
		protected int levelId;

		public void ResetLevelState()
		{
			m_levelUnlock = GameStorageManager.Inst.LoadLevelData(levelId);
		}

		protected override void Awake()
		{
			base.Awake();
			ResetLevelState();
		}
	}
}
