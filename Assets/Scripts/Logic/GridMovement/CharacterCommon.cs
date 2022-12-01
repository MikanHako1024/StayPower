using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GridMovement
{
	public class CharacterCommon : BaseCharacter
	{
		//protected override void RegisterThisCharacter()
		//{
		//	GridGameMap.Inst.RegisterCharacter(this);
		//}

		// TODO : 改成 GridGameMap 查找所有 Character ...

		//protected virtual void Awake()
		//{
		//	//InitGridPos();
		//	//RegisterThisCharacter();
		//}

		protected virtual void Update()
		{
			UpdateMoving();
		}
		//protected virtual void FixedUpdate()
		//{
		//	UpdateMoving();
		//}

		public override bool CanMoveByDir(CharacterDir dir)
		{
			//return GridGameMap.Inst.IsPassableTile(m_v2GridPos + MakeVectorFromDir(dir));
			return GridGameMap.Inst.CheckCanMove(m_v2GridPos, dir);
		}

		public override bool CanMoveByDir(CharacterDir dir, ref List<BaseCharacter> pushedList)
		{
			return GridGameMap.Inst.CheckCanMove(m_v2GridPos, dir, ref pushedList);
		}


		//public void UpdateForAction()
		public void RoundUpdateAfterAction()
		{
			RoundUpdateForPower();
			RoundUpdateForState();
		}

		/// <summary>
		/// 可获取GridPos相关 处理Power等
		/// </summary>
		//public virtual void UpdateAfterAction()
		public virtual void RoundUpdateForPower()
		{
		}

		/// <summary>
		/// 可获取Power相关 处理状态等
		/// </summary>
		//public virtual void UpdateAfterPowerUpdate()
		public virtual void RoundUpdateForState()
		{
		}

		// ...
		// ===  break  202210161530  ===


		public virtual void OnSignalPowerHereChanged()
		{
		}
	}
}
