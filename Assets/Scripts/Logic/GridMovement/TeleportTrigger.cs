using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GridMovement
{
	public class TeleportTrigger : CharacterTrigger
	{
		public void TeleportLevel(int levelId)
		{
			GameMainController.Inst.SetLevelLogicLevelId(levelId);
		}

		public void TeleportForEnd()
		{
			GameMainController.Inst.BackToTitleWithEnd();
		}
	}
}
