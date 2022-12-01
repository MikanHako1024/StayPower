using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using GridMovement;

namespace MyGameplay.Signal
{
	//using TaskInfo = KeyValuePair<SignalGroupSt2, SignalSubTile>;
	using TileWithId = KeyValuePair<SignalSubTile, int>;
	using TaskInfo = KeyValuePair<SignalGroupSt2, KeyValuePair<SignalSubTile, int>>;

	//[RequireComponent(typeof(Tilemap))]
	public class SignalLinkGroup : MonoBehaviour
	{
		#region Instance

		protected static SignalLinkGroup instance;

		public static SignalLinkGroup Inst => GetInstance();

		protected static SignalLinkGroup GetInstance()
		{
			if (instance)
				return instance;
			else
			{
				var go = GameObject.Find("SignalLinkGroup");
				if (go)
					instance = go.GetComponent<SignalLinkGroup>();
				return instance;
			}
		}

		protected void RegisterInstance(SignalLinkGroup obj)
		{
			instance = obj;
		}

		protected void RegisterThisInst()
		{
			RegisterInstance(this);
		}

		#endregion Instance


		#region SignalTilemap

		//[SerializeField]
		//protected Tilemap m_signalTilemap;

		/*protected void InitSignalTilemap()
		{
			if (!m_signalTilemap)
			{
				//m_signalTilemap = GetComponent<Tilemap>();
				var go = GameObject.Find("SignalTilemap");
				if (go)
					m_signalTilemap = go.GetComponent<Tilemap>();
			}
		}*/

		[SerializeField]
		protected Tilemap m_signalLayer1Tilemap;
		[SerializeField]
		protected Tilemap m_signalLayer2Tilemap;
		[SerializeField]
		protected Tilemap m_signalLayer3Tilemap;
		[SerializeField]
		protected Tilemap m_signalLayer4Tilemap;

		protected Tilemap GetLayerTilemap(int layer)
		{
			if (layer == 1)
				return m_signalLayer1Tilemap;
			else if (layer == 2)
				return m_signalLayer2Tilemap;
			else if (layer == 3)
				return m_signalLayer3Tilemap;
			else if (layer == 4)
				return m_signalLayer4Tilemap;
			else
				return null;
		}

		protected void InitSignalTilemap()
		{
			if (!m_signalLayer1Tilemap)
			{
				var go = GameObject.Find("SignalLayer1Tilemap");
				m_signalLayer1Tilemap = go ? go.GetComponent<Tilemap>() : null;
			}
			if (!m_signalLayer2Tilemap)
			{
				var go = GameObject.Find("SignalLayer2Tilemap");
				m_signalLayer2Tilemap = go ? go.GetComponent<Tilemap>() : null;
			}
			if (!m_signalLayer3Tilemap)
			{
				var go = GameObject.Find("SignalLayer3Tilemap");
				m_signalLayer3Tilemap = go ? go.GetComponent<Tilemap>() : null;
			}
			if (!m_signalLayer4Tilemap)
			{
				var go = GameObject.Find("SignalLayer4Tilemap");
				m_signalLayer4Tilemap = go ? go.GetComponent<Tilemap>() : null;
			}
		}

		protected SignalSubTile GetLayerSignalTile(Vector2Int pos, int layer)
		{
			Vector3Int tmPos = new Vector3Int(pos.x, pos.y, 0);
			if (layer == 1)
				return m_signalLayer1Tilemap.GetTile<SignalSubTile>(tmPos);
			else if (layer == 2)
				return m_signalLayer2Tilemap.GetTile<SignalSubTile>(tmPos);
			else if (layer == 3)
				return m_signalLayer3Tilemap.GetTile<SignalSubTile>(tmPos);
			else if (layer == 4)
				return m_signalLayer4Tilemap.GetTile<SignalSubTile>(tmPos);
			else
				return null;
		}

		//protected Tile[] m_tempLayerTiles = new Tile[4];
		protected SignalSubTile[] m_tempLayerTiles = new SignalSubTile[4];

		/*protected SignalSubTile[] GetAllLayerSignalTiles(Vector2Int pos)
		{
			Vector3Int tmPos = new Vector3Int(pos.x, pos.y, 0);
			m_tempLayerTiles[0] = m_signalLayer1Tilemap.GetTile<SignalSubTile>(tmPos);
			m_tempLayerTiles[1] = m_signalLayer1Tilemap.GetTile<SignalSubTile>(tmPos);
			m_tempLayerTiles[2] = m_signalLayer1Tilemap.GetTile<SignalSubTile>(tmPos);
			m_tempLayerTiles[3] = m_signalLayer1Tilemap.GetTile<SignalSubTile>(tmPos);
			return m_tempLayerTiles;
		}*/

		/*protected IEnumerable<SignalSubTile> EachLayerSignalTile(Vector2Int pos)
		{
			Vector3Int tmPos = new Vector3Int(pos.x, pos.y, 0);
			SignalSubTile tile = null;
			tile = m_signalLayer1Tilemap.GetTile<SignalSubTile>(tmPos);
			if (tile) yield return tile;
			tile = m_signalLayer2Tilemap.GetTile<SignalSubTile>(tmPos);
			if (tile) yield return tile;
			tile = m_signalLayer3Tilemap.GetTile<SignalSubTile>(tmPos);
			if (tile) yield return tile;
			tile = m_signalLayer4Tilemap.GetTile<SignalSubTile>(tmPos);
			if (tile) yield return tile;
		}*/
		protected IEnumerable<TileWithId> EachLayerSignalTileWithId(Vector2Int pos)
		{
			Vector3Int tmPos = new Vector3Int(pos.x, pos.y, 0);
			SignalSubTile tile = null;
			tile = m_signalLayer1Tilemap.GetTile<SignalSubTile>(tmPos);
			if (tile) yield return new TileWithId(tile, 1);
			tile = m_signalLayer2Tilemap.GetTile<SignalSubTile>(tmPos);
			if (tile) yield return new TileWithId(tile, 2);
			tile = m_signalLayer3Tilemap.GetTile<SignalSubTile>(tmPos);
			if (tile) yield return new TileWithId(tile, 3);
			tile = m_signalLayer4Tilemap.GetTile<SignalSubTile>(tmPos);
			if (tile) yield return new TileWithId(tile, 4);
		}
		
		#endregion SignalTilemap


		protected void Awake()
		{
			InitSignalTilemap();
			//RefreshAllSignalPattern();
			ClearAllSignalPattern();
			// TODO : 进入场景时 已经存在的信号
		}


		#region Properties

		/*
		protected HashSet<Vector2Int> m_visitedPos = new HashSet<Vector2Int>(64);
		//protected Dictionary<Vector2Int, int> m_pos2groupDict = new Dictionary<Vector2Int, int>();
		////protected Dictionary<Vector2Int, KeyValuePair<int, int>> m_pos2groupDict = new Dictionary<Vector2Int, KeyValuePair<int, int>>();
		//protected Dictionary<Vector2Int, int> m_pos2group2Dict = new Dictionary<Vector2Int, int>();
		//protected Dictionary<Vector2Int, SignalGroupSt> m_pos2groupDict = new Dictionary<Vector2Int, SignalGroupSt>(64);
		protected Dictionary<Vector2Int, SignalGroupSt2> m_pos2groupDict = new Dictionary<Vector2Int, SignalGroupSt2>(64);
		//protected Dictionary<int, List<Vector2Int>> m_group2PosDict = new Dictionary<int, List<Vector2Int>>();
		//protected int nextGroupId = 0;
		// ？m_group2PosDict 可以改为 List 类型 同时可以用 list.Count 代替 nextGroupId ...
		protected List<List<Vector2Int>> m_group2PosList = new List<List<Vector2Int>>(16);

		// ？注意 groupId = 0 是合法的 需要与 SignalGroupSt 默认的 0 区分 ...
		*/

		protected Dictionary<Vector2Int, SignalGroupSt2> m_pos2groupDict = new Dictionary<Vector2Int, SignalGroupSt2>(64);
		//protected List<List<Vector2Int>> m_group2PosList = new List<List<Vector2Int>>(16);
		protected List<HashSet<Vector2Int>> m_group2PosSet = new List<HashSet<Vector2Int>>(16);

		protected List<SignalPowerCount> m_groupPowerCounter = new List<SignalPowerCount>(16);

		public void ClearGroupData()
		{
			//m_visitedPos.Clear();
			m_pos2groupDict.Clear();
			//m_group2PosDict.Clear();
			//nextGroupId = 1;
			//m_group2PosList.Clear();
			m_group2PosSet.Clear();

			m_groupPowerCounter.Clear();
		}

		public bool IsVisited(Vector2Int pos, SignalDir dir)
		{
			//if (m_pos2groupDict.ContainsKey(pos))
			//	return m_pos2groupDict[pos].GetVisited(dir);
			if (m_pos2groupDict.ContainsKey(pos))
			{
				if (m_pos2groupDict[pos] == null)
					return true;
				else
					return m_pos2groupDict[pos].GetVisited(dir);
			}
			else
				return false;
		}
		public bool IsVisitedCompleted(Vector2Int pos)
		{
			//if (m_pos2groupDict.ContainsKey(pos))
			//	return m_pos2groupDict[pos].IsVisitedCompleted();
			if (m_pos2groupDict.ContainsKey(pos))
			{
				if (m_pos2groupDict[pos] == null)
					return true;
				else
					return m_pos2groupDict[pos].IsVisitedCompleted();
			}
			else
				return false;
		}

		public bool CheckGroupIdValid(int groupId)
		{
			//return 0 <= groupId && groupId < m_group2PosList.Count;
			return 0 <= groupId && groupId < m_group2PosSet.Count; 
		}

		//public SignalGroupSt GetGroupStByPos(Vector2Int pos)
		public SignalGroupSt2 GetGroupStByPos(Vector2Int pos)
		{
			if (m_pos2groupDict.ContainsKey(pos))
				return m_pos2groupDict[pos];
			else
				//return SignalGroupSt.empty;
				return null;
		}

		public SignalGroupSt2 TouchGroupStByPos(Vector2Int pos)
		{
			if (!m_pos2groupDict.ContainsKey(pos))
				//m_pos2groupDict.Add(pos, new SignalGroupSt2());
				//m_pos2groupDict.Add(pos, new SignalGroupSt2(pos));
				m_pos2groupDict.Add(pos, CreateSignalGroupSt(pos));
			return m_pos2groupDict[pos];
		}

		public SignalGroupSt2 CreateSignalGroupSt(Vector2Int pos)
		{
			//return new SignalGroupSt2(pos);

			// ？事先对 SignalGroupSt2 处理 初始化 LayerInfo ...
			//var groupSt = new SignalGroupSt2(pos);
			SignalGroupSt2 groupSt = null;
			foreach (var each in EachLayerSignalTileWithId(pos))
			{
				var tile = each.Key;

				bool exists = false;
				foreach (var dir in tile.EachLinkDir())
				{
					if (groupSt == null)
						groupSt = new SignalGroupSt2(pos);

					// 初始化 SignalGroupSt 时 检查是否重复连接
					if (groupSt.IsBindedDir(dir))
					{
						Debug.LogWarningFormat("SignalTile {0} LinkDir {1} is readly used", pos, dir);
						continue;
					}
					exists = true;
				}
				if (!exists)
					continue;

				var layerInfo = groupSt.CreateNewLayerInfo();
				layerInfo.tilemapId = each.Value;
				foreach (var dir in tile.EachLinkDir())
				{
					if (groupSt.IsBindedDir(dir))
						continue;
					layerInfo.SetLink(dir);
					groupSt.BindLinkLayer(dir, layerInfo);
				}
			}
			return groupSt;
		}

		#endregion Properties


		#region SignalLink

		////[ContextMenu("Refresh SignalLinks")]
		//public void RefreshSignalLinks()
		//{
		//}

		// ？改用懒加载 ...

		public int CreateNewGroup()
		{
			//int groupId = m_group2PosList.Count;
			//m_group2PosList.Add(new List<Vector2Int>());
			int groupId = m_group2PosSet.Count;
			m_group2PosSet.Add(new HashSet<Vector2Int>(16));
			m_groupPowerCounter.Add(new SignalPowerCount());
			return groupId;
		}
		public void TouchPos(Vector2Int pos)
		{
			GetGroupId(pos);
		}

		//public int GetGroupId(Vector2Int pos)
		//public SignalGroupSt GetGroupId(Vector2Int pos)
		public SignalGroupSt2 GetGroupId(Vector2Int pos)
		{
			//if (m_pos2groupDict.ContainsKey(pos))
			//	return m_pos2groupDict[pos];
			if (m_pos2groupDict.ContainsKey(pos))
			{
				//if (m_pos2groupDict[pos] == null)
				//{
				//	Debug.Log(pos);
				//}
				return m_pos2groupDict[pos];
			}
			//else if (m_visitedPos.Contains(pos))
			//	//return SignalGroupSt.empty;
			//	return null;
			else
				return CalcSignalLink(pos);
		}

		// ？识别时遇到 signal2Mode 的 tile 也需要将其所有连接遍历 ...
		// ？计算 group 无关是否 signal on ...

		/*
		//public int CalcSignalLink(Vector2Int pos)
		//public SignalGroupSt CalcSignalLink(Vector2Int pos)
		public SignalGroupSt2 CalcSignalLink(Vector2Int pos)
		{
			if (m_visitedPos.Contains(pos))
				//return m_pos2groupDict.ContainsKey(pos) ? m_pos2groupDict[pos] : SignalGroupSt.empty;
				//return m_pos2groupDict.ContainsKey(pos) ? m_pos2groupDict[pos] : null;
				return GetGroupStByPos(pos);
			m_visitedPos.Add(pos);

			var tile = m_signalTilemap.GetTile<SignalTile>(new Vector3Int(pos.x, pos.y, 0));
			if (tile == null)
				//return SignalGroupSt.empty;
				return null;

			//int groupId = nextGroupId++;
			//var list = new List<Vector2Int>();
			//m_group2PosDict.Add(groupId, list);
			//list.Add(pos);
			//m_pos2groupDict.Add(pos, new SignalGroupSt...);

			// ？signal2Mode 的 tile 事先分配两个 groupId ...
			// ？即使 两部分最终会连接 也可以让其拥有两个 groupId ...
			// ？tile 的两部分最终会连接时 迭代回到这个 tile 时会因 visited 而跳过 而事实上也无需再计算它 ...

			if (tile.signal2Mode)
			{
				//int groupId = nextGroupId++;
				//int groupId2 = nextGroupId++;
				//m_group2PosDict.Add(groupId, new List<Vector2Int>());
				//m_group2PosDict.Add(groupId2, new List<Vector2Int>());
				//int groupId = m_group2PosList.Count;
				//m_group2PosList.Add(new List<Vector2Int>());
				//int groupId2 = m_group2PosList.Count;
				//m_group2PosList.Add(new List<Vector2Int>());
				int groupId = CreateNewGroup();
				int groupId2 = CreateNewGroup();

				//m_group2PosDict[groupId].Add(pos);
				//m_group2PosDict[groupId2].Add(pos);
				m_group2PosList[groupId].Add(pos);
				m_group2PosList[groupId2].Add(pos);
				m_pos2groupDict.Add(pos, new SignalGroupSt(groupId, groupId2));

				if (tile.signalLinkD)
					CalcSignalLink_SubFunc(pos + Vector2Int.down, SignalDir.down, groupId);
				if (tile.signalLinkL)
					CalcSignalLink_SubFunc(pos + Vector2Int.left, SignalDir.left, groupId);
				if (tile.signalLinkR)
					CalcSignalLink_SubFunc(pos + Vector2Int.right, SignalDir.right, groupId);
				if (tile.signalLinkU)
					CalcSignalLink_SubFunc(pos + Vector2Int.up, SignalDir.up, groupId);

				if (tile.signal2LinkD)
					CalcSignalLink_SubFunc(pos + Vector2Int.down, SignalDir.down, groupId2);
				if (tile.signal2LinkL)
					CalcSignalLink_SubFunc(pos + Vector2Int.left, SignalDir.left, groupId2);
				if (tile.signal2LinkR)
					CalcSignalLink_SubFunc(pos + Vector2Int.right, SignalDir.right, groupId2);
				if (tile.signal2LinkU)
					CalcSignalLink_SubFunc(pos + Vector2Int.up, SignalDir.up, groupId2);
			}
			else
			{
				//int groupId = nextGroupId++;
				//m_group2PosDict.Add(groupId, new List<Vector2Int>());
				//int groupId = m_group2PosList.Count;
				//m_group2PosList.Add(new List<Vector2Int>());
				int groupId = CreateNewGroup();

				//m_group2PosDict[groupId].Add(pos);
				m_group2PosList[groupId].Add(pos);
				m_pos2groupDict.Add(pos, new SignalGroupSt(groupId));

				if (tile.signalLinkD)
					CalcSignalLink_SubFunc(pos + Vector2Int.down, SignalDir.down, groupId);
				if (tile.signalLinkL)
					CalcSignalLink_SubFunc(pos + Vector2Int.left, SignalDir.left, groupId);
				if (tile.signalLinkR)
					CalcSignalLink_SubFunc(pos + Vector2Int.right, SignalDir.right, groupId);
				if (tile.signalLinkU)
					CalcSignalLink_SubFunc(pos + Vector2Int.up, SignalDir.up, groupId);
			}

			return m_pos2groupDict[pos];
		}
		*/
		/*
		//public void CalcSignalLink_SubFunc(Vector2Int pos, int d, int groupId)
		public void CalcSignalLink_SubFunc(Vector2Int pos, SignalDir d, int groupId)
		// ？用深度优先遍历的方法 需要在遇到回路的时候 将两组内容合并 ...
		// ？改用广度优先遍历的方法 ...
		// ？这样每次都可以把当前连接的回路全部找完 并将新添加的组记录到任务队列中 ...
		// TODO
		//public void CalcSignalLink_SubFunc(Vector2Int pos, SignalDir d, int groupId, Queue<KeyValuePair<Vector2Int, int> taskQueue)
		{
			if (m_visitedPos.Contains(pos))
				return;
			//m_visitedPos.Add(pos);
			// ？不立即 标记 visited ...
			// ？存在 tile 但其不接受 方向d 的信号 此时不能将其标记 visited ...

			var tile = m_signalTilemap.GetTile<SignalTile>(new Vector3Int(pos.x, pos.y, 0));
			//if (tile == null)
			//	return;
			if (tile == null)
			{
				m_visitedPos.Add(pos);
				return;
			}

			/*bool checkOk = false;
			if (d == 10 - SignalDir.down && (tile.signalLinkD || (tile.signal2Mode && tile.signal2LinkD)))
				checkOk = true;
			else if (d == 10 - SignalDir.left && (tile.signalLinkL || (tile.signal2Mode && tile.signal2LinkL)))
				checkOk = true;
			else if (d == 10 - SignalDir.right && (tile.signalLinkR || (tile.signal2Mode && tile.signal2LinkR)))
				checkOk = true;
			else if (d == 10 - SignalDir.up && (tile.signalLinkU || (tile.signal2Mode && tile.signal2LinkU)))
				checkOk = true;* /

			// ？还要区分 signal 还是 signal2 ...

			//bool checkOk = false;
			//bool isSignal2 = false;
			int signalType = -1;
			//if (d == 10 - SignalDir.down)
			if (10 - (int)d == (int)SignalDir.down)
			{
				signalType = tile.signalLinkD ? 1
					 : (tile.signal2Mode && tile.signal2LinkD) ? 2 : -1;
			}
			else if (10 - (int)d == (int)SignalDir.left)
			{
				signalType = tile.signalLinkL ? 1
					 : (tile.signal2Mode && tile.signal2LinkL) ? 2 : -1;
			}
			else if (10 - (int)d == (int)SignalDir.right)
			{
				signalType = tile.signalLinkR ? 1
					 : (tile.signal2Mode && tile.signal2LinkR) ? 2 : -1;
			}
			else if (10 - (int)d == (int)SignalDir.up)
			{
				signalType = tile.signalLinkU ? 1
					 : (tile.signal2Mode && tile.signal2LinkU) ? 2 : -1;
			}

			/*if (signalType == 1)
			{
				m_group2PosDict[groupId].Add(pos);
				m_pos2groupDict.Add(pos, new SignalGroupSt(groupId));

				// ？信号来源方向可以再次检测 ...
				if (tile.signalLinkD)
					CalcSignalLink_SubFunc(pos + Vector2Int.down, SignalDir.down, groupId);
				if (tile.signalLinkL)
					CalcSignalLink_SubFunc(pos + Vector2Int.left, SignalDir.left, groupId);
				if (tile.signalLinkR)
					CalcSignalLink_SubFunc(pos + Vector2Int.right, SignalDir.right, groupId);
				if (tile.signalLinkU)
					CalcSignalLink_SubFunc(pos + Vector2Int.up, SignalDir.up, groupId);
			}
			else if (signalType == 2)
			{
				int groupId2 = nextGroupId++;
				m_group2PosDict.Add(groupId2, new List<Vector2Int>());

				m_group2PosDict[groupId].Add(pos);
				m_group2PosDict[groupId2].Add(pos);
				m_pos2groupDict.Add(pos, new SignalGroupSt(groupId, groupId2));

				if (tile.signalLinkD)
					CalcSignalLink_SubFunc(pos + Vector2Int.down, SignalDir.down, groupId);
				if (tile.signalLinkL)
					CalcSignalLink_SubFunc(pos + Vector2Int.left, SignalDir.left, groupId);
				if (tile.signalLinkR)
					CalcSignalLink_SubFunc(pos + Vector2Int.right, SignalDir.right, groupId);
				if (tile.signalLinkU)
					CalcSignalLink_SubFunc(pos + Vector2Int.up, SignalDir.up, groupId);

				if (tile.signal2LinkD)
					CalcSignalLink_SubFunc(pos + Vector2Int.down, SignalDir.down, groupId2);
				if (tile.signal2LinkL)
					CalcSignalLink_SubFunc(pos + Vector2Int.left, SignalDir.left, groupId2);
				if (tile.signal2LinkR)
					CalcSignalLink_SubFunc(pos + Vector2Int.right, SignalDir.right, groupId2);
				if (tile.signal2LinkU)
					CalcSignalLink_SubFunc(pos + Vector2Int.up, SignalDir.up, groupId2);
			}* /

			// ？signalType 指的是 来源方向是 signal1 还是 signal2 ...
			// ？signalType == 1 但 signal2Mode == true 时 依然要处理两种连接 ...

			if (signalType <= 0)
				return;

			if (tile.signal2Mode)
			{
				m_visitedPos.Add(pos);

				//int newGroupId = nextGroupId++;
				//m_group2PosDict.Add(newGroupId, new List<Vector2Int>());
				//int newGroupId = m_group2PosList.Count;
				//m_group2PosList.Add(new List<Vector2Int>());
				int newGroupId = CreateNewGroup();

				int groupId1 = signalType == 1 ? groupId : newGroupId;
				int groupId2 = signalType == 2 ? groupId : newGroupId;

				//m_group2PosDict[groupId1].Add(pos);
				//m_group2PosDict[groupId2].Add(pos);
				m_group2PosList[groupId1].Add(pos);
				m_group2PosList[groupId2].Add(pos);
				m_pos2groupDict.Add(pos, new SignalGroupSt(groupId1, groupId2));

				/*if (tile.signalLinkD)
					CalcSignalLink_SubFunc(pos + Vector2Int.down, SignalDir.down, groupId1);
				if (tile.signalLinkL)
					CalcSignalLink_SubFunc(pos + Vector2Int.left, SignalDir.left, groupId1);
				if (tile.signalLinkR)
					CalcSignalLink_SubFunc(pos + Vector2Int.right, SignalDir.right, groupId1);
				if (tile.signalLinkU)
					CalcSignalLink_SubFunc(pos + Vector2Int.up, SignalDir.up, groupId1);

				if (tile.signal2LinkD)
					CalcSignalLink_SubFunc(pos + Vector2Int.down, SignalDir.down, groupId2);
				if (tile.signal2LinkL)
					CalcSignalLink_SubFunc(pos + Vector2Int.left, SignalDir.left, groupId2);
				if (tile.signal2LinkR)
					CalcSignalLink_SubFunc(pos + Vector2Int.right, SignalDir.right, groupId2);
				if (tile.signal2LinkU)
					CalcSignalLink_SubFunc(pos + Vector2Int.up, SignalDir.up, groupId2);* /

				if (signalType == 1)
				{
					if (tile.signalLinkD)
						CalcSignalLink_SubFunc(pos + Vector2Int.down, SignalDir.down, groupId1);
					if (tile.signalLinkL)
						CalcSignalLink_SubFunc(pos + Vector2Int.left, SignalDir.left, groupId1);
					if (tile.signalLinkR)
						CalcSignalLink_SubFunc(pos + Vector2Int.right, SignalDir.right, groupId1);
					if (tile.signalLinkU)
						CalcSignalLink_SubFunc(pos + Vector2Int.up, SignalDir.up, groupId1);

					if (tile.signal2LinkD)
						CalcSignalLink_SubFunc(pos + Vector2Int.down, SignalDir.down, groupId2);
					if (tile.signal2LinkL)
						CalcSignalLink_SubFunc(pos + Vector2Int.left, SignalDir.left, groupId2);
					if (tile.signal2LinkR)
						CalcSignalLink_SubFunc(pos + Vector2Int.right, SignalDir.right, groupId2);
					if (tile.signal2LinkU)
						CalcSignalLink_SubFunc(pos + Vector2Int.up, SignalDir.up, groupId2);
				}
				else
				{
					// ？signalType == 2 时 需要优先处理来源方向的连接 再处理新创建的连接 ...

					if (tile.signal2LinkD)
						CalcSignalLink_SubFunc(pos + Vector2Int.down, SignalDir.down, groupId2);
					if (tile.signal2LinkL)
						CalcSignalLink_SubFunc(pos + Vector2Int.left, SignalDir.left, groupId2);
					if (tile.signal2LinkR)
						CalcSignalLink_SubFunc(pos + Vector2Int.right, SignalDir.right, groupId2);
					if (tile.signal2LinkU)
						CalcSignalLink_SubFunc(pos + Vector2Int.up, SignalDir.up, groupId2);

					if (tile.signalLinkD)
						CalcSignalLink_SubFunc(pos + Vector2Int.down, SignalDir.down, groupId1);
					if (tile.signalLinkL)
						CalcSignalLink_SubFunc(pos + Vector2Int.left, SignalDir.left, groupId1);
					if (tile.signalLinkR)
						CalcSignalLink_SubFunc(pos + Vector2Int.right, SignalDir.right, groupId1);
					if (tile.signalLinkU)
						CalcSignalLink_SubFunc(pos + Vector2Int.up, SignalDir.up, groupId1);
				}
			}
			else
			{
				m_visitedPos.Add(pos);

				//m_group2PosDict[groupId].Add(pos);
				m_group2PosList[groupId].Add(pos);
				m_pos2groupDict.Add(pos, new SignalGroupSt(groupId));

				// ？信号来源方向可以再次检测 ...
				if (tile.signalLinkD)
					CalcSignalLink_SubFunc(pos + Vector2Int.down, SignalDir.down, groupId);
				if (tile.signalLinkL)
					CalcSignalLink_SubFunc(pos + Vector2Int.left, SignalDir.left, groupId);
				if (tile.signalLinkR)
					CalcSignalLink_SubFunc(pos + Vector2Int.right, SignalDir.right, groupId);
				if (tile.signalLinkU)
					CalcSignalLink_SubFunc(pos + Vector2Int.up, SignalDir.up, groupId);
			}
		}

		*/

		// ？visitedDict 的键 需要细化为 pos + dir 即每个坐标的四个方向都要独立记录是否已访问 ...
		// ？这样在遍历电路的时候 可以重复访问同一个坐标 只要这个坐标还有方向未被访问 ...

		// ？把 是否被访问的信息 放进 SignalGroupSt ...

		// ？仍需要记录任务队列 ...


		/*public SignalGroupSt2 CalcSignalLink(Vector2Int pos)
		{
			if (IsVisitedCompleted(pos))
				return GetGroupStByPos(pos);

			//var tiles = GetAllLayerSignalTiles(pos);

			//var taskQueue = new Queue<KeyValuePair<Vector2Int, SignalDir>>(8);
			//var taskQueue = new Queue<KeyValuePair<Vector2Int, SignalSubTile>>(8);
			//var taskQueue = new Queue<KeyValuePair<SignalGroupSt2, SignalSubTile>>(8);
			var taskQueue = new Queue<TaskInfo>(8);

			//SignalGroupSt2 groupSt = null;
			SignalGroupSt2 temp_groupSt = null;
			//foreach (var tile in EachLayerSignalTile(pos))
			//foreach (var temp_tile in EachLayerSignalTile(pos))
			foreach (var each in EachLayerSignalTileWithId(pos))
			{
				if (temp_groupSt == null)
					temp_groupSt = TouchGroupStByPos(pos);

				//foreach (var dir in tile.EachLinkDir())
				//{
				//	if (groupSt.GetVisited(dir))
				//	{
				//		//Debug.LogWarningFormat("SignalTile {0} LinkDir {1} is readly used", pos, dir);
				//		continue;
				//	}
				//	// ...
				//}

				taskQueue.Enqueue(new TaskInfo(temp_groupSt, new TileWithId(each.Key, each.Value)));
			}
			if (temp_groupSt == null)
				// 无信号瓦片
				return null;

			int loopCount = 0;
			int loopLimit = 1000;
			while (taskQueue.Count > 0)
			{
				var task = taskQueue.Dequeue();
				var groupSt = task.Key;
				//var tile = task.Value;
				var tile = task.Value.Key;
				//var tileId = task.Value.Key;

				foreach (var dir in tile.EachLinkDir())
				{
					if (groupSt.GetVisited(dir))
					{
						// 此时已访问是因为回路 是正常的
						//Debug.LogWarningFormat("SignalTile {0} LinkDir {1} is readly used", pos, dir);
						continue;
					}

					CalcSignalLink_ExecTask(task, taskQueue);
				}

				if (++loopCount >= loopLimit)
				{
					Debug.LogWarning("CalcSignalLink loop limit reached");
					break;
				}
			}

			//return m_pos2groupDict[pos];
			return GetGroupStByPos(pos);
		}*/
		
		public SignalGroupSt2 CalcSignalLink(Vector2Int pos)
		{
			if (IsVisitedCompleted(pos))
				return GetGroupStByPos(pos);

			SignalGroupSt2 groupSt0 = TouchGroupStByPos(pos);
			if (groupSt0 == null)
				return null;

			var taskQueue = new Queue<TaskInfo>(8);
			foreach (var each in EachLayerSignalTileWithId(pos))
				taskQueue.Enqueue(new TaskInfo(groupSt0, new TileWithId(each.Key, each.Value)));

			int loopCount = 0;
			int loopLimit = 1000;
			while (taskQueue.Count > 0 && loopCount < loopLimit)
			{
				var task = taskQueue.Dequeue();
				var groupSt = task.Key;
				var tile = task.Value.Key;

				foreach (var dir in tile.EachLinkDir())
				{
					if (groupSt.GetVisited(dir))
						// 此时已访问是因为回路 是正常的
						continue;

					CalcSignalLink_ExecTask(task, taskQueue);
				}

				if (++loopCount >= loopLimit)
				{
					Debug.LogWarning("CalcSignalLink loop limit reached");
					break;
				}
			}

			return GetGroupStByPos(pos);
		}

		/*public void CalcSignalLink_ExecTask(TaskInfo task, Queue<TaskInfo> taskQueue)
		{
			// 开始处理一个新的任务 即开始搜索一个新的组

			var groupSt = task.Key;
			//var tile = task.Value;
			var tile = task.Value.Key;
			var tileId = task.Value.Value;
			var pos = groupSt.gridPos;

			// 检查自身新的组的连接 是否已被占用
			// 此时理应未访问对应方向
			var hasNoUseLink = false;
			foreach (var dir in tile.EachLinkDir())
			{
				if (groupSt.GetVisited(dir))
					Debug.LogWarningFormat("SignalTile {0} LinkDir {1} is readly used", pos, dir);
				else
					hasNoUseLink = true;
			}
			if (!hasNoUseLink)
				return;

			int groupId = CreateNewGroup();
			foreach (var dir in tile.EachLinkDir())
			{
				// 判断是否已被访问 因为可能被自身回路访问
				if (!groupSt.GetVisited(dir))
				{
					//groupSt.SetVisited(dir);
					//groupSt.SetGroupId(dir, groupId);
					groupSt.SetGroupId(dir, groupId, tileId);
					//CalcSignalLink_SubFunc(pos, dir, groupId, taskQueue);
					CalcSignalLink_SubFunc(MakeNextPos(pos, dir), CalcRevDir(dir), groupId, taskQueue);
				}
			}
		}*/

		public void CalcSignalLink_ExecTask(TaskInfo task, Queue<TaskInfo> taskQueue)
		{
			// 开始处理一个新的任务 即开始搜索一个新的组

			/*var groupSt = task.Key;
			var tile = task.Value.Key;
			var tileId = task.Value.Value;
			var pos = groupSt.gridPos;

			foreach (var dir in tile.EachLinkDir())
			{
				// 判断是否已被访问 因为可能被自身回路访问
				if (groupSt.GetVisited(dir))
					continue;

				int groupId = CreateNewGroup();
				groupSt.SetGroupId(dir, groupId, tileId);
				////groupSt.GetLayerInfo(layerId).groupId = groupId;
				//groupSt.GetLayerInfo(groupSt.GetLinkLayerId(dir)).groupId = groupId;
				//m_group2PosSet[groupId].Add(groupSt.gridPos);
				// 移到 SetGroupIdOnGridPos
				SetGroupIdOnGridPos(groupSt, dir, groupId);
				CalcSignalLink_SubFunc(MakeNextPos(pos, dir), CalcRevDir(dir), groupId, taskQueue);
			}*/

			// ？处理一个任务 应该只能创建一个组 ...
			// ？一个任务中 每个方向都是同一个组 ...

			var groupSt = task.Key;
			var layer = task.Value.Value;
			var tilemapId = task.Value.Value;
			var pos = groupSt.gridPos;

			int groupId = -1;
			foreach (var layerInfo in groupSt.EachSignalLayer())
			{
				// ？还需要检查是否为目标图层 ...
				//if (layerInfo.layerId != layer)
				if (layerInfo.tilemapId != tilemapId)
						continue;

				foreach (var dir in layerInfo.EachLink())
				{
					if (groupSt.GetVisited(dir))
						continue;

					if (groupId < 0)
						groupId = CreateNewGroup();
					groupSt.SetGroupId(dir, groupId, layer); // ? layer
					SetGroupIdOnGridPos(groupSt, dir, groupId);
					CalcSignalLink_SubFunc(MakeNextPos(pos, dir), CalcRevDir(dir), groupId, taskQueue);
				}
			}
		}

		protected Vector2Int MakeNextPos(Vector2Int pos, SignalDir dir)
		{
			var nextPos = pos;
			if (dir == SignalDir.up)
				nextPos = nextPos + Vector2Int.up;
			else if (dir == SignalDir.down)
				nextPos = nextPos + Vector2Int.down;
			else if (dir == SignalDir.left)
				nextPos = nextPos + Vector2Int.left;
			else if (dir == SignalDir.right)
				nextPos = nextPos + Vector2Int.right;
			return nextPos;
		}

		protected SignalDir CalcRevDir(SignalDir dir)
		{
			if (dir == SignalDir.up)
				return SignalDir.down;
			else if (dir == SignalDir.down)
				return SignalDir.up;
			else if (dir == SignalDir.left)
				return SignalDir.right;
			else if (dir == SignalDir.right)
				return SignalDir.left;
			return dir;
		}

		protected void SetGroupIdOnGridPos(SignalGroupSt2 groupSt, SignalDir dir, int groupId)
		{
			groupSt.GetLayerInfo(groupSt.GetLinkLayerId(dir)).groupId = groupId;
			m_group2PosSet[groupId].Add(groupSt.gridPos);
		}

		/*
		//public void CalcSignalLink_SubFunc(Vector2Int pos, SignalDir dir, int groupId, Queue<TaskInfo> taskQueue)
		public void CalcSignalLink_SubFunc(Vector2Int pos, SignalDir fromDir, int groupId, Queue<TaskInfo> taskQueue)
		{
			//var thisPos = MakeNextPos(pos, dir);
			//var revDir = CalcRevDir(dir);

			bool hasLayer = false;
			foreach (var each in EachLayerSignalTileWithId(pos))
			{
				hasLayer = true;
				break;
			}
			if (!hasLayer)
				return;

			//SignalGroupSt2 groupSt = null;
			SignalGroupSt2 groupSt = TouchGroupStByPos(pos);

			//foreach (var tile in EachLayerSignalTile(pos))
			foreach (var each in EachLayerSignalTileWithId(pos))
			{
				var tile = each.Key;
				if (tile.IsLinkToDir(fromDir))
				{
					//if (groupSt == null)
					//	groupSt = TouchGroupStByPos(pos);

					//groupSt.SetGroupId(fromDir, groupId);
					groupSt.SetGroupId(fromDir, groupId, each.Value);
					//groupSt.GetLayerInfo(groupSt.GetLinkLayerId(fromDir)).groupId = groupId;
					//m_group2PosSet[groupId].Add(groupSt.gridPos);
					SetGroupIdOnGridPos(groupSt, fromDir, groupId);
					// TODO : 交给 SignalGroupSt2 处理
					foreach (var linkDir in tile.EachLinkDir())
					{
						if (linkDir == fromDir)
							continue;
						//groupSt.SetGroupId(linkDir, groupId);
						groupSt.SetGroupId(linkDir, groupId, each.Value);
						//groupSt.GetLayerInfo(groupSt.GetLinkLayerId(linkDir)).groupId = groupId;
						//m_group2PosSet[groupId].Add(groupSt.gridPos);
						SetGroupIdOnGridPos(groupSt, linkDir, groupId);

						//CalcSignalLink_SubFunc(pos, linkDir, groupId, taskQueue);
						//CalcSignalLink_SubFunc(MakeNextPos(pos, linkDir), CalcRevDir(linkDir), groupId, taskQueue);
						// ？不能立即执行遍历 需要加入任务队列里 ...
						taskQueue.Enqueue(new TaskInfo(groupSt, each));
						// ？错了 ...
						// ？将会创建新的组 而非继续搜索 ...
					}
				}
				else
				{
					// 未连接的方向 加入到任务队列中
					// TODO : 考虑是否需要立即将所有方向的连接计算完毕

					//taskQueue.Enqueue(new TaskInfo(groupSt, tile));
					//taskQueue.Enqueue(new TaskInfo(groupSt, each));

					// ？未连接的方向不考虑 ...
				}
			}
		}
		*/

		public void CalcSignalLink_SubFunc(Vector2Int pos, SignalDir fromDir, int groupId, Queue<TaskInfo> taskQueue)
		{
			SignalGroupSt2 groupSt = TouchGroupStByPos(pos);
			if (groupSt == null)
				return;

			foreach (var layerInfo in groupSt.EachSignalLayer())
			{
				var layer = layerInfo.tilemapId;
				var tile = GetLayerSignalTile(groupSt.gridPos, layer);

				if (layerInfo.GetLink(fromDir))
				{
					// 连接的层
					// 继续搜索
					groupSt.SetGroupId(fromDir, groupId, layer);
					SetGroupIdOnGridPos(groupSt, fromDir, groupId);
					foreach (var linkDir in tile.EachLinkDir())
					{
						if (linkDir == fromDir)
							continue;
						groupSt.SetGroupId(linkDir, groupId, layer);
						SetGroupIdOnGridPos(groupSt, linkDir, groupId);

						CalcSignalLink_SubFunc(MakeNextPos(pos, linkDir), CalcRevDir(linkDir), groupId, taskQueue);
					}
				}
				else
				{
					// 非连接的层
					// 添加新的任务
					taskQueue.Enqueue(new TaskInfo(groupSt, new TileWithId(tile, layer)));
				}
			}
		}

		#endregion SignalLink


		#region SignalPattern

		[ContextMenu("Refresh AllSignalPattern")]
		public void RefreshAllSignalPattern()
		{
			//m_signalTilemap.RefreshAllTiles();

			m_signalLayer1Tilemap.RefreshAllTiles();
			m_signalLayer2Tilemap.RefreshAllTiles();
			m_signalLayer3Tilemap.RefreshAllTiles();
			m_signalLayer4Tilemap.RefreshAllTiles();
		}

		public void ClearAllSignalPattern()
		{
			/*var list = m_signalTilemap.GetTilesBlock(m_signalTilemap.cellBounds);
			foreach (var each in list)
			{
				if (each is SignalTile)
					(each as SignalTile).SetPattern2(false, false);
			}
			m_signalTilemap.RefreshAllTiles();*/
			// TODO : ...
		}

		/*
		public void SetSignalGroupPattern(int groupId, bool isOn)
		//public void SetSignalGroupPattern(int groupId, bool isOn, bool needRefresh = true)
		{
			//if (!m_group2PosDict.ContainsKey(groupId))
			//if (!(0 <= groupId && groupId < m_group2PosList.Count)) 
			if (!CheckGroupIdValid(groupId))
				return;

			//foreach (var each in m_group2PosDict[groupId])
			foreach (var each in m_group2PosList[groupId])
			{
				var pos = new Vector3Int(each.x, each.y, 0);
				var tile = m_signalTilemap.GetTile<SignalTile>(pos);
				if (tile == null)
					continue;

				if (tile.signal2Mode)
				{
					var groupSt = m_pos2groupDict[each];
					if (groupSt.groupId == groupId)
						//tile.SetPattern(isOn);
						GridGameMap.Inst.SetSignalState(each, isOn);
					else if (groupSt.group2Id == groupId)
						//tile.SetPattern2(isOn);
						GridGameMap.Inst.SetSignalState2(each, isOn);
				}
				else
				{
					//tile.SetPattern(isOn);
					GridGameMap.Inst.SetSignalState(each, isOn);
				}
				//if (needRefresh)
				//	m_signalTilemap.RefreshTile(pos);
				m_signalTilemap.RefreshTile(pos);
			}
		}

		public void SetSignalGroupPattern(Vector2Int pos, bool isOn)
		//public void SetSignalGroupPattern(Vector2Int pos, bool isOn, bool needRefresh = true)
		{
			var groupSt = GetGroupId(pos);
			//if (groupSt.hasGroup2)
			if (groupSt.hasGroup2 && groupSt.groupId != groupSt.group2Id)
			{
				SetSignalGroupPattern(groupSt.groupId, isOn);
				SetSignalGroupPattern(groupSt.group2Id, isOn);
			}
			//else
			else if (groupSt.hasGroup)
			{
				SetSignalGroupPattern(groupSt.groupId, isOn);
			}
		}
		*/

		public void SetSignalGroupPattern(int groupId, bool isOn)
		{
			if (!CheckGroupIdValid(groupId))
				return;

			//foreach (var pos in m_group2PosList[groupId])
			foreach (var pos in m_group2PosSet[groupId])
			{
				var groupSt = GetGroupId(pos);
				foreach (var dir in groupSt.EachDirForGroupId(groupId))
				{
					GridGameMap.Inst.SetSignalState(pos, dir, isOn);
					var tilemap = GetLayerTilemap(groupSt.GetTilemapId(dir));
					if (tilemap)
						tilemap.RefreshTile(new Vector3Int(pos.x, pos.y, 0));
				}
			}
		}

		protected HashSet<int> m_tempGroupSet = new HashSet<int>(4);

		public void SetSignalGroupPattern(Vector2Int pos, bool isOn)
		{
			var groupSt = GetGroupId(pos);
			m_tempGroupSet.Clear();
			foreach (var groupId in groupSt.EachGroupId())
			{
				if (m_tempGroupSet.Contains(groupId))
					continue;
				m_tempGroupSet.Add(groupId);
				SetSignalGroupPattern(groupId, isOn);
			}
		}

		#endregion SignalPattern
		// TODO : ？移到 SignalManager ...
		// ？SignalLinkGroup 只储存信号连接的数据 SignalManager 储存游戏逻辑相关数据 ...


		#region SignalPower

		public void AddGroupPower(int groupId, SignalPowerType pType)
		//public void AddGroupPower(int groupId, SignalPowerType pType, bool needRefresh = true)
		// ？总是刷新 ...
		{
			if (CheckGroupIdValid(groupId))
			{
				m_groupPowerCounter[groupId].AddCount(pType);
				OnPowerCountChanged(groupId);
			}
		}
		/*public void AddGroupPower(Vector2Int pos, SignalPowerType pType)
		//public void AddGroupPower(Vector2Int pos, SignalPowerType pType, bool needRefresh = true)
		{
			var groupSt = GetGroupId(pos);
			if (groupSt.hasGroup2 && groupSt.groupId != groupSt.group2Id)
			{
				m_groupPowerCounter[groupSt.groupId].AddCount(pType);
				m_groupPowerCounter[groupSt.group2Id].AddCount(pType);
				OnPowerCountChanged(groupSt.groupId);
				OnPowerCountChanged(groupSt.group2Id);
			}
			//else
			else if (groupSt.hasGroup)
			{
				m_groupPowerCounter[groupSt.groupId].AddCount(pType);
				OnPowerCountChanged(groupSt.groupId);
			}
			// ？无操作时 也要刷新 ...
			//else
			//{
			//	OnPowerCountChanged(..., needRefresh);
			//}
		}*/
		public void AddGroupPower(Vector2Int pos, SignalPowerType pType)
		{
			var groupSt = GetGroupId(pos);
			//foreach (var layerInfo in groupSt.EachSignalLayer())
			//{
			//	m_groupPowerCounter[layerInfo.groupId].AddCount(pType);
			//	OnPowerCountChanged(layerInfo.groupId);
			//}
			foreach (var groupId in groupSt.EachLayerGroupId())
			{
				m_groupPowerCounter[groupId].AddCount(pType);
				OnPowerCountChanged(groupId);
			}
			
		}

		public void DecGroupPower(int groupId, SignalPowerType pType)
		//public void DecGroupPower(int groupId, SignalPowerType pType, bool needRefresh = true)
		{
			if (CheckGroupIdValid(groupId))
			{
				m_groupPowerCounter[groupId].DecCount(pType);
				OnPowerCountChanged(groupId);
			}
		}
		/*public void DecGroupPower(Vector2Int pos, SignalPowerType pType)
		//public void DecGroupPower(Vector2Int pos, SignalPowerType pType, bool needRefresh = true)
		{
			var groupSt = GetGroupId(pos);
			if (groupSt.hasGroup2 && groupSt.groupId != groupSt.group2Id)
			{
				m_groupPowerCounter[groupSt.groupId].DecCount(pType);
				m_groupPowerCounter[groupSt.group2Id].DecCount(pType);
				OnPowerCountChanged(groupSt.groupId);
				OnPowerCountChanged(groupSt.group2Id);
			}
			//else
			else if (groupSt.hasGroup)
			{
				m_groupPowerCounter[groupSt.groupId].DecCount(pType);
				OnPowerCountChanged(groupSt.groupId);
			}
		}*/
		public void DecGroupPower(Vector2Int pos, SignalPowerType pType, bool needRefresh = true)
		{
			var groupSt = GetGroupId(pos);
			//foreach (var layerInfo in groupSt.EachSignalLayer())
			//{
			//	m_groupPowerCounter[layerInfo.groupId].DecCount(pType);
			//	OnPowerCountChanged(layerInfo.groupId);
			//}
			foreach (var groupId in groupSt.EachLayerGroupId())
			{
				m_groupPowerCounter[groupId].DecCount(pType);
				OnPowerCountChanged(groupId);
			}
		}

		public SignalPowerCount GetGroupPowerCount(int groupId)
		{
			if (CheckGroupIdValid(groupId))
				return m_groupPowerCounter[groupId];
			else
				//return SignalPowerCount.empty;
				return null;
		}
		/*public SignalPowerCount GetGroupPowerCount(Vector2Int pos)
		{
			var groupSt = GetGroupId(pos);
			if (groupSt.hasGroup2 && groupSt.groupId != groupSt.group2Id)
				return GetGroupPowerCount(groupSt.groupId) + GetGroupPowerCount(groupSt.group2Id);
			//else
			else if (groupSt.hasGroup)
				return GetGroupPowerCount(groupSt.groupId);
			else
				//return SignalPowerCount.empty;
				return null;
		}*/
		public SignalPowerCount GetGroupPowerCount(Vector2Int pos)
		{
			var groupSt = GetGroupId(pos);
			if (groupSt == null)
				return SignalPowerCount.zero;
			SignalPowerCount result = null;
			//foreach (var layerInfo in groupSt.EachSignalLayer())
			//	result = result + GetGroupPowerCount(layerInfo.groupId);
			foreach (var groupId in groupSt.EachLayerGroupId())
				result = result + GetGroupPowerCount(groupId);
			return result;
		}

		//public void RefreshOnPowerCountChanged(int groupId, bool needRefresh = true)
		//public void OnPowerCountChanged(int groupId, bool needRefresh = true)
		public void OnPowerCountChanged(int groupId)
		{
			bool isOn = GetGroupPowerCount(groupId).HasAnyCount();
			//SetSignalGroupPattern(groupId, isOn, needRefresh);
			SetSignalGroupPattern(groupId, isOn);
		}
		//public void RefreshOnPowerCountChanged(Vector2Int pos, bool needRefresh = true)
		//{
		//	...
		//}

		#endregion SignalPower
		// TODO : ？移到 SignalManager ...


		/*public List<Vector2Int> GetPosListByGroupId(int groupId)
		{
			if (CheckGroupIdValid(groupId))
				return m_group2PosList[groupId];
			else
				return null;
		}*/
		public HashSet<Vector2Int> GetPosSetByGroupId(int groupId)
		{
			if (CheckGroupIdValid(groupId))
				return m_group2PosSet[groupId];
			else
				return null;
		}


		#region DebugFunctions

		/*[ContextMenu("Test1")]
		public void Test1()
		{
			//Debug.Log(m_signalTilemap.cellBounds);
			//Debug.Log(m_signalTilemap.localBounds);

			var list = m_signalTilemap.GetTilesBlock(m_signalTilemap.cellBounds);
			//Debug.Log(list);
			foreach (var each in list)
			{
				var tile = each as SignalTile;
				if (tile)
				{
					Debug.Log(tile + " " + tile.signalFlag);
				}
				else
				{
					Debug.Log(tile);
				}
			}

			// ...
		}*/


		[SerializeField]
		public Vector2Int debugPos;

		[SerializeField]
		public int debugGroupId;

		[SerializeField]
		public bool debugPatternOn;


		[ContextMenu("Debug GetGroupId")]
		public void DebugGetGroupId()
		{
			Debug.Log(GetGroupId(debugPos));
		}

		[ContextMenu("Debug Clear")]
		public void DebugClear()
		{
			ClearGroupData();
		}

		/*[ContextMenu("Debug PrintGroup")]
		public void DebugPrintGroup()
		{
			//if (m_group2PosDict.ContainsKey(debugGroupId))
			//if (0 <= debugGroupId && debugGroupId < m_group2PosList.Count)
			if (!CheckGroupIdValid(debugGroupId))
			{
				//string str = "Length " + m_group2PosDict[debugGroupId].Count + " : ";
				//foreach (var each in m_group2PosDict[debugGroupId])
				string str = "Length " + m_group2PosList[debugGroupId].Count + " : ";
				foreach (var each in m_group2PosList[debugGroupId])
				{
					str = str + each + " ";
				}
				Debug.Log(str);
			}
			else
			{
				Debug.Log("None");
			}
		}*/

		[ContextMenu("Debug ClearPattern")]
		public void DebugClearPattern()
		{
			ClearAllSignalPattern();
		}

		[ContextMenu("Debug SetGroupPattern ByGroup")]
		public void DebugSetGroupPatternByGroup()
		{
			SetSignalGroupPattern(debugGroupId, debugPatternOn);
		}

		[ContextMenu("Debug SetGroupPattern ByPos")]
		public void DebugSetGroupPatternByPos()
		{
			SetSignalGroupPattern(debugPos, debugPatternOn);
		}

		[ContextMenu("Debug GetGroupPowerCount ByGroup")]
		public void DebugGetGroupPowerCountByGroup()
		{
			Debug.Log(GetGroupPowerCount(debugGroupId));
		}

		[ContextMenu("Debug GetGroupPowerCount ByPos")]
		public void DebugGetGroupPowerCountByPos()
		{
			Debug.Log(GetGroupPowerCount(debugPos));
		}

		/*[ContextMenu("Debug Refresh SignalPattern")]
		public void DebugRefreshSignalPattern()
		{
			var pos = new Vector3Int(debugPos.x, debugPos.y, 0);
			var tile = m_signalTilemap.GetTile(pos) as SignalTile;
			m_signalTilemap.get
			if (tile)
			{
				tile.
			}
			m_signalTilemap.RefreshTile(pos);
		}*/

		#endregion DebugFunctions
	}
}
