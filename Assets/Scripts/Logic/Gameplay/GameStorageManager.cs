using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MyGameplay.Signal;

namespace MyGameplay.GameData
{
	public class GameStorageManager
	{
		#region Instance

		protected static GameStorageManager instance;

		public static GameStorageManager Inst => GetInstance();

		protected static GameStorageManager GetInstance()
		{
			if (instance == null)
				instance = new GameStorageManager();
			return instance;
		}

		#endregion Instance


		public string MakeLevelDataKey(int levelId)
		{
			return "L" + levelId;
		}

		public bool LoadLevelData(int levelId)
		{
			return PlayerPrefs.GetInt(MakeLevelDataKey(levelId), 0) > 0;
		}

		public void SaveLevelData(int levelId, bool value)
		{
			PlayerPrefs.SetInt(MakeLevelDataKey(levelId), value ? 1 : 0);
		}
	}
}
