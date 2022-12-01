using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyGameplay.Mechanism;
using GridMovement;
using MyGameplay.GameData;

namespace MyGameplay.Logic
{
	public class TitleLogic : GameLogicCommon
	{
		[SerializeField]
		public string rangeName;
		//[SerializeField]
		//public bool rangeImmediately;

		//[SerializeField]
		//protected LevelRange titleLevelRange;
		//[SerializeField]
		//protected LevelRangeRect titleLevelRange;

		public override void OnLogicActiveOn()
		{
			base.OnLogicActiveOn();
			if (rangeName != "")
				//LevelRangeManager.Inst.SetRangeName(rangeName, rangeImmediately);
				//LevelRangeManager.Inst.SetRangeName(rangeName, true);
				//LevelRangeManager.Inst.SetRange(titleLevelRange, true);
				LevelRangeManager.Inst.SetRange(GridGameMap.Inst.GetLevelRange(rangeName), true);
			//LevelRangeManager.Inst.SetRange(titleLevelRange, true);

			ReselectLevelButton();
			RefreshAllLevelButton();
			if (creatorInfo)
				creatorInfo.SetActive(false);
			robotGo.SetActive(true);

			GameMainController.Inst.uiWheelDisc.gameObject.SetActive(false);

			GridGameMap.Inst.SetTitleContainer();

			GridGameMap.Inst.DeactivatePlayer();
			GridGameMap.Inst.UnloadPowerSupplyPoolItems();

			//titleCursor.SetGridPosNonAnim(titleStartPos);
		}

		//public override void OnLogicActiveOff()
		//{
		//	base.OnLogicActiveOff();
		//}


		//[SerializeField]
		//protected Vector2Int titleStartPos;
		//
		//[SerializeField]
		//protected CharacterTitleCursor titleCursor;


		/*[SerializeField]
		protected MechanismText startText;
		[SerializeField]
		protected MechanismText levelText;
		[SerializeField]
		protected MechanismText creatorText;

		[SerializeField]
		protected Vector2Int startPos;
		[SerializeField]
		protected Vector2Int levelPos;
		[SerializeField]
		protected Vector2Int creatorPos;

		public enum TitleCmdType
		{
			None,
			Start,
			Level,
			Creator,
		}
		protected TitleCmdType selectedCmd = TitleCmdType.None;


		[SerializeField]
		//protected CharacterPlayer cursorCharacer;
		protected CharacterTitleCursor cursorCharacer;

		protected TitleCmdType GetCursorSelected()
		{
			if (!cursorCharacer)
				return TitleCmdType.None;
			else if(cursorCharacer.GridPos == startPos)
				return TitleCmdType.Start;
			else if (cursorCharacer.GridPos == levelPos)
				return TitleCmdType.Level;
			else if(cursorCharacer.GridPos == creatorPos)
				return TitleCmdType.Creator;
			else
				return TitleCmdType.None;

		}

		protected MechanismText GetCurrentText()
		{
			if (selectedCmd == TitleCmdType.Start)
				return startText;
			else if (selectedCmd == TitleCmdType.Level)
				return levelText;
			else if (selectedCmd == TitleCmdType.Creator)
				return creatorText;
			else
				return null;
		}

		protected void SetCurrentTextState(bool selected)
		{
			var text = GetCurrentText();
			if (text)
			{
				if (selected)
					text.SetSelected();
				else
					text.SetNoSelect();
			}
		}

		protected void UpdateSelectedCmd()
		{
			var newSelected = GetCursorSelected();
			if (selectedCmd != newSelected)
			{
				SetCurrentTextState(false);
				selectedCmd = newSelected;
				SetCurrentTextState(true);
			}
		}*/


		//protected void Update()
		//{
		//	UpdateSelectedCmd();
		//}

		/*protected override void UpdateGameLogic()
		{
			base.UpdateGameLogic();
			UpdateSelectedCmd();
		}*/


		/*public void InvokeTitleCommand()
		{
			if (selectedCmd == TitleCmdType.Start)
				InvokeCommandStart();
			else if (selectedCmd == TitleCmdType.Level)
				InvokeCommandLevel();
			else if (selectedCmd == TitleCmdType.Creator)
				InvokeCommandCreator();
		}*/

		/*protected void InvokeCommandStart()
		{
			//GameMainController.Inst.SetGameLogic(GameMainController.LogicType.Level1);
			//GameMainController.Inst.SetGameLogic(GameMainController.LogicType.Level);
			GameMainController.Inst.SetGameLogic(GameMainController.LogicType.Level, 1);
		}

		protected void InvokeCommandLevel()
		{
			// ¡Ÿ ±”√
			GameMainController.Inst.SetGameLogic(GameMainController.LogicType.Level, 3);
		}

		protected void InvokeCommandCreator()
		{
		}*/


		[SerializeField]
		protected List<MechanismText> levelTextList;
		[SerializeField]
		protected MechanismText creatorText;

		[SerializeField]
		protected GameObject creatorInfo;
		[SerializeField]
		protected GameObject robotGo;

		[SerializeField]
		protected List<Vector2Int> levelPosList;
		[SerializeField]
		protected Vector2Int creatorPos;

		public void InvokeTitleCommand(Vector2Int pos)
		{
			if (creatorInfo != null && pos == creatorPos)
				InvokeCommandCreatorIfCan();
			else
			{
				for (int i = 0, l = levelPosList.Count; i < l; i++)
				{
					if (levelPosList[i] == pos)
					{
						InvokeCommandLevelIfCan(i + 1);
						break;
					}
				}
			}
		}

		protected void InvokeCommandCreatorIfCan()
		{
			if (!creatorText)
				return;

			//if (creatorInfo)
			//	creatorInfo.SetActive(true);
			if (creatorText.IsButtonEnabled())
			{
				//if (creatorInfo)
				//	creatorInfo.SetActive(true);
				//creatorInfo.SetActive(true);
				//robotGo.SetActive(false);

				if (robotGo.activeSelf)
				{
					creatorInfo.SetActive(true);
					robotGo.SetActive(false);
				}
				else
				{
					creatorInfo.SetActive(false);
					robotGo.SetActive(true);
				}
			}
		}
		protected void InvokeCommandLevelIfCan(int levelId)
		{
			//GameMainController.Inst.SetGameLogic(GameMainController.LogicType.Level, levelId);
			if (1 <= levelId && levelId <= levelTextList.Count)
			{
				if (levelTextList[levelId - 1].IsButtonEnabled())
					GameMainController.Inst.SetGameLogic(GameMainController.LogicType.Level, levelId);
			}
		}


		[SerializeField]
		protected MechanismText lastSelectedText = null;

		public void SelectTitleCommand(Vector2Int pos)
		{
			if (lastSelectedText)
				lastSelectedText.SetNoSelect();

			if (pos == creatorPos)
				lastSelectedText = creatorText;
			else
			{
				for (int i = 0, l1 = levelPosList.Count, l2 = levelTextList.Count; i < l1 && i < l2; i++)
				{
					if (levelPosList[i] == pos)
					{
						lastSelectedText = levelTextList[i];
						break;
					}
				}
			}

			if (lastSelectedText)
				lastSelectedText.SetSelected();
		}


		public int DefaultSelectLevelId()
		{
			return 1;
		}

		public void ReselectLevelButton()
		{
			foreach (var each in levelTextList)
				each.SetNoSelect();

			int defLevelId = DefaultSelectLevelId();
			if (1 <= defLevelId && defLevelId <= levelTextList.Count)
			{
				lastSelectedText = levelTextList[defLevelId - 1];
				lastSelectedText.SetSelected();
			}
		}


		public void RefreshAllLevelButton()
		{
			for (int i = 0, l = levelTextList.Count; i < l; i++)
			{
				if (GameStorageManager.Inst.LoadLevelData(i + 1))
				{
					levelTextList[i].SetButtonEnabled(true);
				}
				else
				{
					if (i == 0)
					{
						GameStorageManager.Inst.SaveLevelData(1, true);
						levelTextList[i].SetButtonEnabled(true);
					}
					else
					{
						levelTextList[i].SetButtonEnabled(false);
					}
				}
			}
		}


		[SerializeField, Multiline(5)]
		public string thanksText;


#if UNITY_EDITOR
		[ContextMenu("UnlockAllLevel")]
		public void UnlockAllLevel()
		{
			int maxLevel = LevelContainer.MaxLevelCount;
			for (int i = 1; i <= maxLevel; i++)
				GameStorageManager.Inst.SaveLevelData(i, true);
		}

		[ContextMenu("LockAllLevel")]
		public void LockAllLevel()
		{
			int maxLevel = LevelContainer.MaxLevelCount;
			for (int i = 2; i <= maxLevel; i++)
				GameStorageManager.Inst.SaveLevelData(i, false);
		}
#endif
	}
}
