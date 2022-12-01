using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyGameplay.GameData;

namespace GridMovement
{
	public class LevelContainer : CharacterContainer
	{
		[SerializeField]
		public Vector2Int levelStartPos;

		[SerializeField]
		protected UnityEngine.Events.UnityEvent onLevelEnterEvent;
		[SerializeField]
		protected UnityEngine.Events.UnityEvent onLevelExitEvent;

		public void InvokeLevelEnterEvent()
		{
			onLevelEnterEvent.Invoke();
		}
		public void InvokeLevelExitEvent()
		{
			onLevelExitEvent.Invoke();
		}

		// TODO : onLevelSuccessEvent


		public void ShowUIWheelDisc()
		{
			GameMainController.Inst.uiWheelDisc.gameObject.SetActive(true);
		}

		public void HideUIWheelDisc()
		{
			GameMainController.Inst.uiWheelDisc.gameObject.SetActive(false);
		}

		public void ResetPlayerPowerCount()
		{
			//GridGameMap.Inst.ResetCurPlayerPowerCount();
			//GridGameMap.Inst.singlePlayer.ResetPowerCount();

			var player = GridGameMap.Inst.singlePlayer as CharacterPlayer;
			if (player)
				player.ResetPowerCount();
		}


		//[SerializeField]
		//public int maxLevelCount = 8;
		public static readonly int MaxLevelCount = 9;

		// ？实际是 LevelUnlock 解锁关卡 而非关卡过关 ...
		public void LevelClear(int levelId)
		{
			if (1 <= levelId && levelId <= MaxLevelCount)
				GameStorageManager.Inst.SaveLevelData(levelId, true);
		}
	}
}
