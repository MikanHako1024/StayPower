using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyGameplay.Signal
{
	public class SignalState2
	{
		// ？改 记录所有的 信号状态 ...
		// ？为 分别记录四方向的 信号状态 ...

		public bool upSignalOn;
		public bool downSignalOn;
		public bool leftSignalOn;
		public bool rightSignalOn;

		public void SetState(bool upOn, bool downOn, bool leftOn, bool rightOn)
		{
			upSignalOn = upOn;
			downSignalOn = downOn;
			leftSignalOn = leftOn;
			rightSignalOn = rightOn;
		}

		public void SetState(SignalDir dir, bool isOn)
		{
			if (dir == SignalDir.up)
				upSignalOn = isOn;
			else if (dir == SignalDir.down)
				downSignalOn = isOn;
			else if(dir == SignalDir.left)
				leftSignalOn = isOn;
			else if(dir == SignalDir.right)
				rightSignalOn = isOn;
		}


		public override string ToString()
		{
			return string.Format("U:{0},D:{1},L:{2},R:{3}",
				upSignalOn ? "T" : "F", downSignalOn ? "T" : "F",
				leftSignalOn ? "T" : "F", rightSignalOn ? "T" : "F");
		}


		/*
		// ？在 SignalState 中记录 四方向的 groupId ...

		public int upSignalGroupId = -1;
		public int downSignalGroupId = -1;
		public int leftSignalGroupId = -1;
		public int rightSignalGroupId = -1;

		public void SetGroupId(int upGid, int downGid, int leftGid, int rightGid)
		{
			upSignalGroupId = upGid;
			downSignalGroupId = downGid;
			leftSignalGroupId = leftGid;
			rightSignalGroupId = rightGid;
		}

		public void SetGroupId(SignalDir dir, int groupId)
		{
			if (dir == SignalDir.up)
				upSignalGroupId = groupId;
			else if (dir == SignalDir.down)
				downSignalGroupId = groupId;
			else if (dir == SignalDir.left)
				leftSignalGroupId = groupId;
			else if (dir == SignalDir.right)
				rightSignalGroupId = groupId;
		}


		public override string ToString()
		{
			return string.Format("U:{0}{1},D:{2}{3},L:{4}{5},R:{6}{7}",
				upSignalOn ? "T" : "F", upSignalGroupId,
				downSignalOn ? "T" : "F", downSignalGroupId,
				leftSignalOn ? "T" : "F", leftSignalGroupId,
				rightSignalOn ? "T" : "F", rightSignalGroupId);
		}
		*/
	}
}
