using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyGameplay.Signal;
using MyGameplay.Power;
using MyGameplay.UI;

namespace GridMovement
{
	//public class CharacterPlayer : BaseCharacter
	//public class CharacterPlayer : CharacterCommon
	public class CharacterPlayer : BaseCharacterPlayer
	{
		//public SignalLinkGroup signalLinkGroup { get => SignalManager.Inst.signalLinkGroup; }


		/*protected override void Update()
		{
			UpdateInput();
			base.Update();
		}*/


		/*public override bool CanMoveByDir(Vector2Int dir)
		{
			return GridGameMap.Inst.IsPassableTile(m_v2GridPos + dir);
		}*/
		/*public override bool CanMoveByDir(CharacterDir dir)
		{
			//return GridGameMap.Inst.IsPassableTile(m_v2GridPos + MakeVectorFromDir(dir));
			return GridGameMap.Inst.CheckCanMove(m_v2GridPos, dir);
		}

		public override bool CanMoveByDir(CharacterDir dir, ref List<BaseCharacter> pushedList)
		{
			return GridGameMap.Inst.CheckCanMove(m_v2GridPos, dir, ref pushedList);
		}*/
		// 移到 CharacterCommon


		//[SerializeField]
		//protected SignalPowerCount powerCount = new SignalPowerCount(1, 1, 1, 1);

		[SerializeField, Min(0)]
		protected int upPowerCount = 1;
		[SerializeField, Min(0)]
		protected int leftPowerCount = 1;
		[SerializeField, Min(0)]
		protected int downPowerCount = 1;
		[SerializeField, Min(0)]
		protected int rightPowerCount = 1;

		/*protected bool CanInput()
		{
			return !IsMoving();
		}*/


		protected bool HasAnyPowerSupplyHere()
		{
			foreach (var each in GridGameMap.Inst.EachCharactersAt(m_v2GridPos))
			{
				if (each is PowerSupply)
					return true;
			}
			return false;
		}
		protected bool HasAnyPowerSupplyHere(out PowerSupply outResult)
		{
			foreach (var each in GridGameMap.Inst.EachCharactersAt(m_v2GridPos))
			{
				if (each is PowerSupply)
				{
					outResult = each as PowerSupply;
					return true;
				}
			}
			outResult = null;
			return false;
		}

		//protected bool CanPlacePowerSupply()
		//{
		//	return !HasAnyPowerSupplyHere();
		//}

		protected bool PlacePowerSupply(CharacterDir dir)
		{
			foreach (var each in GridGameMap.Inst.EachCharactersAt(m_v2GridPos))
			{
				if (each is PowerSupply)
					return false;
			}
			return true;
		}


		public bool HasSocketTileHere()
		{
			return GridGameMap.Inst.IsSocketTile(m_v2GridPos);
		}

		public bool CanPlacePowerSupply()
		{
			return HasSocketTileHere();
		}


		//protected void OnPickPowerSupply(SignalDir dir)
		protected void OnPickPowerSupply(SignalPowerType pType)
		{
			var changed = true;
			var delta = 1;
			if (pType == SignalPowerType.down)
				downPowerCount += delta;
			else if (pType == SignalPowerType.left)
				leftPowerCount += delta;
			else if (pType == SignalPowerType.right)
				rightPowerCount += delta;
			else if (pType == SignalPowerType.up)
				upPowerCount += delta;
			else
				changed = false;

			if (changed)
			{
				m_doingAction = false;
				m_theActionHasPowerSupply = false;
				GridGameMap.Inst.PickPowerSupply(m_theActionPowerSupplyTarget);

				// 暂时如此
				GameMainController.Inst.uiWheelDisc.AddNumber(pType);

				//OnSignalPowerCountChanged();
				//RefreshSignalPowerCount();

				RefreshForRestartAction();
			}
		}

		//protected void OnPlacePowerSupply(SignalDir dir)
		protected void OnPlacePowerSupply(SignalPowerType pType)
		{
			var changed = true;
			var delta = -1;
			if (pType == SignalPowerType.down)
				downPowerCount += delta;
			else if (pType == SignalPowerType.left)
				leftPowerCount += delta;
			else if (pType == SignalPowerType.right)
				rightPowerCount += delta;
			else if (pType == SignalPowerType.up)
				upPowerCount += delta;
			else
				changed = false;

			if (changed)
			{
				m_doingAction = false;
				GridGameMap.Inst.PlacePowerSupply(pType, m_v2GridPos);

				// 暂时如此
				GameMainController.Inst.uiWheelDisc.DecNumber(pType);

				//OnSignalPowerCountChanged();
				//RefreshSignalPowerCount();

				RefreshForStartAction();
			}
		}

		// TODO : 修正其他地方的 SignalDir 和 SignalPowerType


		protected override void OnGridPosChanged()
		{
			base.OnGridPosChanged();

			//currentSignalPowerCount = signalLinkGroup.GetGroupPowerCount(m_v2GridPos);
			//
			//// 暂时如此
			//GameMainController.Inst.uiWheelDisc.SetPower(currentSignalPowerCount);
			//OnSignalPowerCountChanged();
			RefreshSignalPowerCount();

			RefreshForCheckActionZTrigger();

			RefreshForCheckActionXTrigger();
		}


		protected SignalPowerCount currentSignalPowerCount = new SignalPowerCount();

		/*protected void OnSignalPowerCountChanged()
		{
			//currentSignalPowerCount = signalLinkGroup.GetGroupPowerCount(m_v2GridPos);
			currentSignalPowerCount = SignalLinkGroup.Inst.GetGroupPowerCount(m_v2GridPos);

			// 暂时如此
			GameMainController.Inst.uiWheelDisc.SetPower(currentSignalPowerCount);
		}*/

		public override void OnSignalPowerHereChanged()
		{
			RefreshSignalPowerCount();
		}

		protected void RefreshSignalPowerCount()
		{
			//currentSignalPowerCount = signalLinkGroup.GetGroupPowerCount(m_v2GridPos);
			currentSignalPowerCount = SignalLinkGroup.Inst.GetGroupPowerCount(m_v2GridPos);

			// 暂时如此
			GameMainController.Inst.uiWheelDisc.SetPower(currentSignalPowerCount);

			RefreshForCheckActionXTrigger();
		}

		//protected override void GridMoveByDir(CharacterDir dir)
		//{
		//	base.GridMoveByDir(dir);
		//	OnGridPosChanged();
		//}
		// 移到 BaseCharacter


		protected void RefreshForCheckActionZTrigger()
		{
			if (GridGameMap.Inst.HasActionZTriggerCharacter(m_v2GridPos))
				GameMainController.Inst.uiWheelDisc.SetZButtonEnabledOn();
			else
				GameMainController.Inst.uiWheelDisc.SetZButtonEnabledOff();
		}


		protected bool m_infoShowing = false;

		protected bool m_holdRestartKey = false;
		protected bool m_holdTitleKey = false;


		protected bool m_doingAction = false;
		protected bool m_theActionHasPowerSupply = false;
		protected PowerSupply m_theActionPowerSupplyTarget = null;

		protected void RefreshForStartAction()
		{
			m_doingAction = true;
			m_theActionHasPowerSupply = HasAnyPowerSupplyHere(out m_theActionPowerSupplyTarget);
		}

		protected void RefreshForRestartAction()
		{
			RefreshForStartAction();

			//RefreshForCheckActionXTrigger();
		}

		protected void RefreshForEndAction()
		{
			m_doingAction = false;
			m_theActionHasPowerSupply = false;
			m_theActionPowerSupplyTarget = null;

			//RefreshForCheckActionXTrigger();
		}


		protected void RefreshForCheckActionXTrigger()
		{
			//if (m_theActionHasPowerSupply || CanPlacePowerSupply())
			if (HasAnyPowerSupplyHere() || CanPlacePowerSupply())
					GameMainController.Inst.uiWheelDisc.SetXButtonEnabledOn();
			else
				GameMainController.Inst.uiWheelDisc.SetXButtonEnabledOff();
		}


		//protected void UpdateInput()
		protected override void UpdatePlayerInput()
		{
			//if (!CanInput())
			//	return;
			// ？无法移动时 仍然需要记录 action键的按下和抬起 ...

			if (Input.GetKeyDown(KeyConfig.powerActionKey))
			//if (Input.GetKeyDown(KeyConfig.actionKey) && CanPlacePowerSupply())
			{
				//m_doingAction = true;
				//m_theActionHasPowerSupply = HasAnyPowerSupplyHere(out m_theActionPowerSupplyTarget);
				// 移到 RefreshForStartAction
				RefreshForStartAction();

				//RefreshForCheckActionXTrigger();
			}
			if (Input.GetKeyUp(KeyConfig.powerActionKey))
			{
				//m_doingAction = false;
				//m_theActionHasPowerSupply = false;
				//m_theActionPowerSupplyTarget = null;
				// 移到 RefreshForEndAction
				RefreshForEndAction();
			}
			//if (Input.GetKeyUp(KeyConfig.showInfoKey))
			if (m_infoShowing && Input.GetKeyUp(KeyConfig.showInfoKey))
			{
				m_infoShowing = false;
				UILockInfoPool.Inst.HideAllInfo();
			}

			if (!CanInput())
				return;

			if (m_infoShowing)
			{
			}
			else if (m_holdTitleKey)
			{
				//if (Input.GetKeyUp(KeyConfig.titleKey))
				if (Input.GetKeyUp(KeyConfig.titleKey1) || Input.GetKeyUp(KeyConfig.titleKey2))
					{
					m_holdTitleKey = false;
					GameMainController.Inst.BackToTitle();
				}
			}
			else if (m_holdRestartKey)
			{
				if (Input.GetKeyUp(KeyConfig.restartKey))
				{
					m_holdRestartKey = false;
					GameMainController.Inst.RestartCurrentLevel();
				}
			}
			else if (Input.GetKey(KeyConfig.powerActionKey))
			{
				if (m_doingAction)
				{
					if (m_theActionHasPowerSupply)
					{
						// CanPick PowerSupply
						if (Input.GetKeyDown(KeyConfig.downKey) || Input.GetKeyDown(KeyConfig.leftKey)
							 || Input.GetKeyDown(KeyConfig.rightKey) || Input.GetKeyDown(KeyConfig.upKey))
						{
							//m_doingAction = false;
							//m_theActionHasPowerSupply = false;
							//GridGameMap.Inst.PickPowerSupply(m_theActionPowerSupplyTarget);
							//
							//var pType = m_theActionPowerSupplyTarget.powerType;
							//if (pType == SignalPowerType.down)
							//	downPowerCount++;
							//else if (pType == SignalPowerType.left)
							//	leftPowerCount++;
							//else if (pType == SignalPowerType.right)
							//	rightPowerCount++;
							//else if (pType == SignalPowerType.up)
							//	upPowerCount++;

							OnPickPowerSupply(m_theActionPowerSupplyTarget.powerType);
						}
					}
					//else
					else if (CanPlacePowerSupply())
					{
						// CanPlace PowerSupply
						//if (Input.GetKey(KeyConfig.downKey))
						if (Input.GetKeyDown(KeyConfig.downKey))
						{
							if (downPowerCount > 0)
							{
								//m_doingAction = false;
								//GridGameMap.Inst.PlacePowerSupply(SignalPowerType.down, m_v2GridPos);
								//downPowerCount--;
								OnPlacePowerSupply(SignalPowerType.down);
							}
						}
						//else if (Input.GetKey(KeyConfig.leftKey))
						else if (Input.GetKeyDown(KeyConfig.leftKey))
						{
							if (leftPowerCount > 0)
							{
								//m_doingAction = false;
								//GridGameMap.Inst.PlacePowerSupply(SignalPowerType.left, m_v2GridPos);
								//leftPowerCount--;
								OnPlacePowerSupply(SignalPowerType.left);
							}
						}
						//else if (Input.GetKey(KeyConfig.rightKey))
						else if (Input.GetKeyDown(KeyConfig.rightKey))
						{
							if (rightPowerCount > 0)
							{
								//m_doingAction = false;
								//GridGameMap.Inst.PlacePowerSupply(SignalPowerType.right, m_v2GridPos);
								//rightPowerCount--;
								OnPlacePowerSupply(SignalPowerType.right);
							}
						}
						//else if (Input.GetKey(KeyConfig.upKey))
						else if (Input.GetKeyDown(KeyConfig.upKey))
						{
							if (upPowerCount > 0)
							{
								//m_doingAction = false;
								//GridGameMap.Inst.PlacePowerSupply(SignalPowerType.up, m_v2GridPos);
								//upPowerCount--;
								OnPlacePowerSupply(SignalPowerType.up);
							}
						}
					}
				}
			}
			else if (Input.GetKeyDown(KeyConfig.actionZKey))
			{
				GridGameMap.Inst.RefreshForActionZTriggerCharacter(this);
				RefreshForCheckActionZTrigger();
			}
			//else if (Input.GetKeyDown(KeyConfig.showInfoKey))
			else if (Input.GetKey(KeyConfig.showInfoKey))
			{
				m_infoShowing = true;
				UILockInfoPool.Inst.ShowAllInfo(GridGameMap.Inst.EachLockInCurContainer());
			}
			//else if (Input.GetKeyDown(KeyConfig.titleKey))
			else if (Input.GetKeyDown(KeyConfig.titleKey1) || Input.GetKeyDown(KeyConfig.titleKey2))
					{
				m_holdTitleKey = true;
			}
			else if (Input.GetKeyDown(KeyConfig.restartKey))
			{
				m_holdRestartKey = true;
			}
			else
			{
				//var powerCount = signalLinkGroup.GetGroupPowerCount(m_v2GridPos);
				// FINISH : 优化获取
				var powerCount = currentSignalPowerCount;

				if (Input.GetKey(KeyConfig.downKey))
				{
					//GridMove(2);
					//GridMove(CharacterDir.down);
					//GridMoveIfCan(CharacterDir.down);
					//GridMoveIfCan(CharacterDir.down, true);
					if (downPowerCount > 0 || (powerCount != null && powerCount.down > 0))
						GridMoveIfCan(CharacterDir.down, true);
				}
				else if (Input.GetKey(KeyConfig.leftKey))
				{
					//GridMove(4);
					//GridMove(CharacterDir.left);
					//GridMoveIfCan(CharacterDir.left);
					//GridMoveIfCan(CharacterDir.left, true);
					if (leftPowerCount > 0 || (powerCount != null && powerCount.left > 0))
						GridMoveIfCan(CharacterDir.left, true);
				}
				else if (Input.GetKey(KeyConfig.rightKey))
				{
					//GridMove(6);
					//GridMove(CharacterDir.right);
					//GridMoveIfCan(CharacterDir.right);
					//GridMoveIfCan(CharacterDir.right, true);
					if (rightPowerCount > 0 || (powerCount != null && powerCount.right > 0))
						GridMoveIfCan(CharacterDir.right, true);
				}
				else if (Input.GetKey(KeyConfig.upKey))
				{
					//GridMove(8);
					//GridMove(CharacterDir.up);
					//GridMoveIfCan(CharacterDir.up);
					//GridMoveIfCan(CharacterDir.up, true);
					if (upPowerCount > 0 || (powerCount != null && powerCount.up > 0))
						GridMoveIfCan(CharacterDir.up, true);
				}
			}
		}


		/*public void ShowUIWheelDisc()
		{
			GameMainController.Inst.uiWheelDisc.gameObject.SetActive(true);
			RefreshSignalPowerCount();
			RefreshForCheckActionZTrigger();
			RefreshForCheckActionXTrigger();
		}

		public void HideUIWheelDisc()
		{
			GameMainController.Inst.uiWheelDisc.gameObject.SetActive(false);
		}*/


		public void ResetPowerCount()
		{
			upPowerCount = 1;
			leftPowerCount = 1;
			downPowerCount = 1;
			rightPowerCount = 1;

			GameMainController.Inst.uiWheelDisc.SetAllNumber(upPowerCount, downPowerCount, leftPowerCount, rightPowerCount);
		}
	}
}
