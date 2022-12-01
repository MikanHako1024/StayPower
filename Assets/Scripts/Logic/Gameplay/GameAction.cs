using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MyGameplay.Signal;

namespace MyGameplay.GameData
{
	public class GameAction
	{
		public ActionType actionType = ActionType.None;

		// TODO : 可序列化和反序列化的 character

		//public List<..

		// TODO : List 对象池 和 GameAction 对象池

		public SignalPowerType powerType;
	}
}
