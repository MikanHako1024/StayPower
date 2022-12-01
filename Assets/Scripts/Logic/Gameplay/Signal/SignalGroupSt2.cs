using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyGameplay.Signal
{
	public class SignalGroupSt2
	{
		// ？额外储存位置信息 ...

		public Vector2Int gridPos;

		public SignalGroupSt2(Vector2Int gridPos)
		{
			this.gridPos = gridPos;
		}


		// ？额外储存对应瓦片地图的序号 ...

		public int upSignalTilemapId = 0;
		public int downSignalTilemapId = 0;
		public int leftSignalTilemapId = 0;
		public int rightSignalTilemapId = 0;

		public void SetTilemapId(SignalDir dir, int tid)
		{
			if (dir == SignalDir.up)
				upSignalTilemapId = tid;
			else if (dir == SignalDir.down)
				downSignalTilemapId = tid;
			else if (dir == SignalDir.left)
				leftSignalTilemapId = tid;
			else if (dir == SignalDir.right)
				rightSignalTilemapId = tid;
			AddSignalTilemap(tid);
		}

		public int GetTilemapId(SignalDir dir)
		{
			if (dir == SignalDir.up)
				return upSignalTilemapId;
			else if (dir == SignalDir.down)
				return downSignalTilemapId;
			else if (dir == SignalDir.left)
				return leftSignalTilemapId;
			else if (dir == SignalDir.right)
				return rightSignalTilemapId;
			else
				return 0;
		}


		// ？缓存不重复的瓦片地图的序号 ...

		protected HashSet<int> signalTilemapSet = new HashSet<int>();

		protected void AddSignalTilemap(int tid)
		{
			if (!signalTilemapSet.Contains(tid))
				signalTilemapSet.Add(tid);
		}

		protected IEnumerable<int> EachSignalTilemap()
		{
			return signalTilemapSet;
		}


		// TODO : ？用一个泛型结构 管理任意 四方向的数据 ...


		//public int upSignalGroupId = -1;
		//public int downSignalGroupId = -1;
		//public int leftSignalGroupId = -1;
		//public int rightSignalGroupId = -1;

		public static int emptyGroupId = -1;

		public int upSignalGroupId = emptyGroupId;
		public int downSignalGroupId = emptyGroupId;
		public int leftSignalGroupId = emptyGroupId;
		public int rightSignalGroupId = emptyGroupId;

		/*public void SetGroupId(int upGid, int downGid, int leftGid, int rightGid)
		{
			upSignalGroupId = upGid;
			downSignalGroupId = downGid;
			leftSignalGroupId = leftGid;
			rightSignalGroupId = rightGid;
		}*/

		//public void SetGroupId(SignalDir dir, int groupId)
		protected void SetGroupId(SignalDir dir, int groupId)
		{
			if (dir == SignalDir.up)
				upSignalGroupId = groupId;
			else if (dir == SignalDir.down)
				downSignalGroupId = groupId;
			else if (dir == SignalDir.left)
				leftSignalGroupId = groupId;
			else if (dir == SignalDir.right)
				rightSignalGroupId = groupId;

			SetVisited(dir);
		}
		public void SetGroupId(SignalDir dir, int groupId, int tid)
		{
			SetTilemapId(dir, tid);
			SetGroupId(dir, groupId);
		}
		
		public void SetNoGroup(SignalDir dir)
		{
			//SetGroupId(dir, emptyGroupId);
			SetGroupId(dir, emptyGroupId, 0);
		}

		public int GetGroupId(SignalDir dir)
		{
			if (dir == SignalDir.up)
				return upSignalGroupId;
			else if (dir == SignalDir.down)
				return downSignalGroupId;
			else if (dir == SignalDir.left)
				return leftSignalGroupId;
			else if (dir == SignalDir.right)
				return rightSignalGroupId;
			else
				return emptyGroupId;
		}


		public IEnumerable<SignalDir> EachDirForGroupId(int groupId)
		{
			if (upSignalGroupId == groupId)
				yield return SignalDir.up;
			if (downSignalGroupId == groupId)
				yield return SignalDir.down;
			if (leftSignalGroupId == groupId)
				yield return SignalDir.left;
			if (rightSignalGroupId == groupId)
				yield return SignalDir.right;
		}

		/*public IEnumerable<int> EachTilemapIdForGroupId(int groupId)
		{
			if (upSignalGroupId == groupId)
				yield return upSignalTilemapId;
			if (downSignalGroupId == groupId)
				yield return downSignalTilemapId;
			if (leftSignalGroupId == groupId)
				yield return leftSignalTilemapId;
			if (rightSignalGroupId == groupId)
				yield return rightSignalTilemapId;
		}*/

		public IEnumerable<int> EachGroupId()
		{
			if (upSignalGroupId > emptyGroupId)
				yield return upSignalGroupId;
			if (downSignalGroupId > emptyGroupId)
				yield return downSignalGroupId;
			if (leftSignalGroupId > emptyGroupId)
				yield return leftSignalGroupId;
			if (rightSignalGroupId > emptyGroupId)
				yield return rightSignalGroupId;
		}


		public override string ToString()
		{
			return string.Format("U:{0},D:{1},L:{2},R:{3}",
				upSignalGroupId, downSignalGroupId, leftSignalGroupId, rightSignalGroupId);
		}


		public bool upVisited = false;
		public bool downVisited = false;
		public bool leftVisited = false;
		public bool rightVisited = false;

		public bool visitedCompleted = false;

		//public void SetVisited(SignalDir dir)
		// ？只允许在 SetGroupId 时 标记 visited ...
		protected void SetVisited(SignalDir dir)
		{
			if (dir == SignalDir.up)
				upVisited = true;
			else if (dir == SignalDir.down)
				downVisited = true;
			else if (dir == SignalDir.left)
				leftVisited = true;
			else if (dir == SignalDir.right)
				rightVisited = true;

			if (upVisited && downVisited && leftVisited && rightVisited)
				visitedCompleted = true;
		}

		public bool GetVisited(SignalDir dir)
		{
			if (dir == SignalDir.up)
				return upVisited;
			else if (dir == SignalDir.down)
				return downVisited;
			else if (dir == SignalDir.left)
				return leftVisited;
			else if (dir == SignalDir.right)
				return rightVisited;
			else
				return false;
		}

		public bool IsVisitedCompleted()
		{
			return visitedCompleted;
		}


		// ？每个瓦片图层 都对应一个tilemap 也共享一个groupId ...
		//public struct SignalLayerInfoSt
		public class SignalLayerInfoSt
		{
			//public int layerId;
			public int layerId = -1;

			//public int groupId;
			public int groupId = -1;
			public int tilemapId;

			public bool upLink;
			public bool downLink;
			public bool leftLink;
			public bool rightLink;

			public SignalLayerInfoSt(int layerId)
			{
				this.layerId = layerId;
			}

			public void SetLink(SignalDir dir)
			{
				if (dir == SignalDir.up)
					upLink = true;
				else if (dir == SignalDir.down)
					downLink = true;
				else if (dir == SignalDir.left)
					leftLink = true;
				else if (dir == SignalDir.right)
					rightLink = true;
			}

			public bool GetLink(SignalDir dir)
			{
				if (dir == SignalDir.up)
					return upLink;
				else if (dir == SignalDir.down)
					return downLink;
				else if (dir == SignalDir.left)
					return leftLink;
				else if (dir == SignalDir.right)
					return rightLink;
				else
					return false;
			}

			public IEnumerable<SignalDir> EachLink()
			{
				if (upLink)
					yield return SignalDir.up;
				if (downLink)
					yield return SignalDir.down;
				if (leftLink)
					yield return SignalDir.left;
				if (rightLink)
					yield return SignalDir.right;
			}
		}

		//public SignalLayerInfoSt signalLayerInfo;
		//public void SetLayerInfo(int groupId, int tilemapId, 

		public SignalLayerInfoSt[] signalLayerInfos = new SignalLayerInfoSt[4];
		public int signalLayerCount = 0;

		//public void AddLayerInfo(SignalLayerInfoSt layerInfo)
		//{
		//	if (signalLayerCount < 4)
		//		signalLayerInfos[signalLayerCount++] = layerInfo;
		//}

		public SignalLayerInfoSt CreateNewLayerInfo()
		{
			if (signalLayerCount < 4)
			{
				var layerId = signalLayerCount++;
				var layerInfo = new SignalLayerInfoSt(layerId);
				signalLayerInfos[layerId] = layerInfo;
				return layerInfo;
			}
			return null;
		}

		public SignalLayerInfoSt GetLayerInfo(int layerId)
		{
			if (0 <= layerId && layerId < signalLayerCount)
				return signalLayerInfos[layerId];
			else
				return null;
		}

		public IEnumerable<SignalLayerInfoSt> EachSignalLayer()
		{
			for (var i = 0; i < signalLayerCount; i++)
				yield return signalLayerInfos[i];
		}
		public IEnumerable<int> EachLayerGroupId()
		{
			for (var i = 0; i < signalLayerCount; i++)
				//yield return signalLayerInfos[i].groupId;
				if (signalLayerInfos[i].groupId >= 0)
					yield return signalLayerInfos[i].groupId;
		}


		// ？每个连接方向 也将属于一个图层 ...
		public int upLinkLayerId = -1;
		public int downLinkLayerId = -1;
		public int leftLinkLayerId = -1;
		public int rightLinkLayerId = -1;

		public void BindLinkLayer(SignalDir dir, int layerId)
		{
			if (dir == SignalDir.up)
				upLinkLayerId = layerId;
			else if (dir == SignalDir.down)
				downLinkLayerId = layerId;
			else if (dir == SignalDir.left)
				leftLinkLayerId = layerId;
			else if (dir == SignalDir.right)
				rightLinkLayerId = layerId;
		}
		public void BindLinkLayer(SignalDir dir, SignalLayerInfoSt layerInfo)
		{
			if (layerInfo != null)
				BindLinkLayer(dir, layerInfo.layerId);
		}

		public bool IsBindedDir(SignalDir dir)
		{
			if (dir == SignalDir.up)
				return upLinkLayerId >= 0;
			else if (dir == SignalDir.down)
				return downLinkLayerId >= 0;
			else if (dir == SignalDir.left)
				return leftLinkLayerId >= 0;
			else if (dir == SignalDir.right)
				return rightLinkLayerId >= 0;
			else
				return false;
		}

		public int GetLinkLayerId(SignalDir dir)
		{
			if (dir == SignalDir.up)
				return upLinkLayerId;
			else if (dir == SignalDir.down)
				return downLinkLayerId;
			else if (dir == SignalDir.left)
				return leftLinkLayerId;
			else if (dir == SignalDir.right)
				return rightLinkLayerId;
			else
				return -1;
		}
	}
}
