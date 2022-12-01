using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyGameplay.Signal;
using MyGameplay.Power;

namespace GridMovement
{
	public class BaseCharacterPlayer : CharacterCommon
	{
		protected override void OnMoveFinish()
		{
			base.OnMoveFinish();
			RefreshForTrigger();
		}

		protected virtual void RefreshForTrigger()
		{
			//GridGameMap.Inst.RefreshForTriggerCharacter(this);
			GridGameMap.Inst.RefreshForContactTriggerCharacter(this);
		}


		protected virtual bool CanInput()
		{
			//return !IsMoving();
			return !IsMoving() && !GameMainController.Inst.uiCover.IsAnimating();
		}

		//protected virtual void UpdateInputIfCan()
		//{
		//	if (CanInput())
		//		UpdatePlayerInput();
		//}

		protected virtual void UpdatePlayerInput()
		{
		}


		protected override void Update()
		{
			//UpdateInputIfCan();
			UpdatePlayerInput();
			base.Update();
		}
	}
}
