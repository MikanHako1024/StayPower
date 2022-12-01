using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyGameplay.Signal;
using MyGameplay.Power;

namespace GridMovement
{
	//public class CharacterTitleCursor : CharacterCommon
	public class CharacterTitleCursor : BaseCharacterPlayer
	{
		/*protected override void Update()
		{
			UpdateInput();
			base.Update();
		}*/

		/*protected bool CanInput()
		{
			return !IsMoving();
		}*/


		/*protected override void OnMoveFinish()
		{
			base.OnMoveFinish();
			RefreshForTrigger();
		}*/

		/*protected void RefreshForTrigger()
		{
			// ...
		}*/


		[SerializeField]
		public float m_titleCursorMoveSpeed = 4f;

		public override float gridMoveSpeed => m_titleCursorMoveSpeed;


		protected bool m_actionReady = false;

		//protected void UpdateInput()
		protected override void UpdatePlayerInput()
		{
			//if (!CanInput())
			//	return;
			if (GameMainController.Inst.uiCover.IsAnimating())
				return;

			//if (Input.GetKeyDown(KeyConfig.powerActionKey))
			//if (Input.GetKeyDown(KeyConfig.powerActionKey) || Input.GetKeyDown(KeyConfig.actionKey))
			if (Input.GetKeyDown(KeyConfig.okKey) || Input.GetKeyDown(KeyConfig.actionZKey))
			{
				m_actionReady = true;
			}
			//else if (Input.GetKeyUp(KeyConfig.powerActionKey) && m_actionReady)
			//else if ((Input.GetKeyUp(KeyConfig.powerActionKey) || Input.GetKeyDown(KeyConfig.actionKey)) && m_actionReady)
			else if (m_actionReady && (Input.GetKeyUp(KeyConfig.okKey) || Input.GetKeyUp(KeyConfig.actionZKey)))
			{
				m_actionReady = false;
				var titleLogic = GameMainController.Inst.GetCurrentTitleLogic();
				if (titleLogic)
					//titleLogic.InvokeTitleCommand();
					titleLogic.InvokeTitleCommand(m_v2GridPos);
			}
			//else
			//else if (CanInput())
			else if (!IsMoving())
			{
				var titleLogic = GameMainController.Inst.GetCurrentTitleLogic();
				if (Input.GetKey(KeyConfig.downKey))
				//if (Input.GetKeyDown(KeyConfig.downKey))
				{
					GridMoveIfCan(CharacterDir.down, false);
					m_actionReady = false;
					if (titleLogic)
						titleLogic.SelectTitleCommand(m_v2GridPos);
				}
				else if (Input.GetKey(KeyConfig.leftKey))
				//else if (Input.GetKeyDown(KeyConfig.leftKey))
				{
					GridMoveIfCan(CharacterDir.left, false);
					m_actionReady = false;
					if (titleLogic)
						titleLogic.SelectTitleCommand(m_v2GridPos);
				}
				else if (Input.GetKey(KeyConfig.rightKey))
				//else if (Input.GetKeyDown(KeyConfig.rightKey))
				{
					GridMoveIfCan(CharacterDir.right, false);
					m_actionReady = false;
					if (titleLogic)
						titleLogic.SelectTitleCommand(m_v2GridPos);
				}
				else if (Input.GetKey(KeyConfig.upKey))
				//else if (Input.GetKeyDown(KeyConfig.upKey))
				{
					GridMoveIfCan(CharacterDir.up, false);
					m_actionReady = false;
					if (titleLogic)
						titleLogic.SelectTitleCommand(m_v2GridPos);
				}
			}
		}
	}
}
