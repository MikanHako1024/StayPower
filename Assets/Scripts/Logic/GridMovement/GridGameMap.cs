using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using MyGameplay.Signal;
using MyGameplay.Power;
using MyGameplay.Mechanism;

namespace GridMovement
{
	public class GridGameMap : MonoBehaviour
	{
		#region Instance

		protected static GridGameMap instance;

		public static GridGameMap Inst => GetInstance();

		protected static GridGameMap GetInstance()
		{
			if (instance)
				return instance;
			else
			{
				var go = GameObject.Find("GridGameMap");
				if (go)
					instance = go.GetComponent<GridGameMap>();
				return instance;
			}
		}

		protected void RegisterInstance(GridGameMap obj)
		{
			instance = obj;
		}

		protected void RegisterThisInst()
		{
			RegisterInstance(this);
		}

		#endregion Instance


		protected void Awake()
		{
			RegisterThisInst();
			InitGpTilemap();
			//InitCharacterContainer();
			InitContaienrPrefabs();
			InitCharacters();
		}


		#region GpTilemap

		[SerializeField]
		protected Tilemap m_gpTilemap1;

		[SerializeField]
		protected Tilemap m_gpTilemap2;

		protected void InitGpTilemap()
		{
			if (!m_gpTilemap1)
				//m_gpTilemap1 = transform.Find("GpTilemap1").GetComponent<Tilemap>();
				m_gpTilemap1 = GameObject.Find("GpTilemap1").GetComponent<Tilemap>();
			if (!m_gpTilemap2)
				//m_gpTilemap2 = transform.Find("GpTilemap2").GetComponent<Tilemap>();
				m_gpTilemap2 = GameObject.Find("GpTilemap2").GetComponent<Tilemap>();
		}

		#endregion GpTilemap


		#region GpTile

		public TileBase GetGp1Tile(int x, int y)
		{
			return m_gpTilemap1.GetTile<TileBase>(new Vector3Int(x, y, 0));
		}
		public TileBase GetGp1Tile(Vector2Int pos)
		{
			return m_gpTilemap1.GetTile<TileBase>(new Vector3Int(pos.x, pos.y, 0));
		}

		public TileBase GetGp2Tile(int x, int y)
		{
			return m_gpTilemap2.GetTile<TileBase>(new Vector3Int(x, y, 0));
		}
		public TileBase GetGp2Tile(Vector2Int pos)
		{
			return m_gpTilemap2.GetTile<TileBase>(new Vector3Int(pos.x, pos.y, 0));
		}

		#endregion GpTile


		#region SignalState

		//protected Dictionary<Vector2Int, SignalState> m_signalStateDict = new Dictionary<Vector2Int, SignalState>();
		protected Dictionary<Vector2Int, SignalState2> m_signalStateDict = new Dictionary<Vector2Int, SignalState2>();

		public void ClearSignalState()
		{
			m_signalStateDict.Clear();
		}

		//public SignalState TouchSignalState(Vector2Int pos)
		public SignalState2 TouchSignalState(Vector2Int pos)
		{
			if (!m_signalStateDict.ContainsKey(pos))
				//m_signalStateDict.Add(pos, new SignalState());
				m_signalStateDict.Add(pos, new SignalState2());
			return m_signalStateDict[pos];
		}

		//public SignalState GetSignalState(Vector2Int pos)
		public SignalState2 GetSignalState(Vector2Int pos)
		{
			if (m_signalStateDict.ContainsKey(pos))
				return m_signalStateDict[pos];
			else
				return null;
		}

		/*public void SetSignalState(Vector2Int pos, bool signalOn)
		{
			TouchSignalState(pos).SetState(signalOn);
		}
		public void SetSignalState(Vector2Int pos, bool signalOn, bool signal2On)
		{
			TouchSignalState(pos).SetState(signalOn, signal2On);
		}
		public void SetSignalState2(Vector2Int pos, bool signal2On)
		{
			TouchSignalState(pos).SetState2(signal2On);
		}*/

		public void SetSignalState(Vector2Int pos, SignalDir dir, bool isOn)
		{
			TouchSignalState(pos).SetState(dir, isOn);
		}

		#endregion SignalState


		#region CharacterContainer

		/*[SerializeField]
		protected Transform characterContainerTf;

		protected void InitCharacterContainer()
		{
			if (characterContainerTf)
				return;

			for (int i = 0, l = transform.childCount; i < l; i++)
			{
				var each = transform.GetChild(i);
				if (each.name == "CharacterContainer")
				{
					characterContainerTf = each;
					return;
				}
			}

			var go = new GameObject("CharacterContainer");
			go.transform.SetParent(transform);
			characterContainerTf = go.transform;
		}

		protected void InitCharacters()
		{
			for (int i = 0, l = characterContainerTf.childCount; i < l; i++)
			{
				var each = characterContainerTf.GetChild(i);
				//var character = each.GetComponent<BaseCharacter>();
				var character = each.GetComponent<CharacterCommon>();
				if (character)
					// ？这里无视了 enabled ...
					RegisterCharacter(character);
			}
		}*/


		[SerializeField]
		//protected List<GameObject> levelContainerPrefabs = new List<GameObject>();
		//protected List<CharacterContainer> levelContainerPrefabs = new List<CharacterContainer>();
		//protected List<ContainerPrefabConfig> levelContainerPrefabs = new List<ContainerPrefabConfig>();
		protected List<ContainerPrefabConfig> levelContainerPrefabs = new(16);

		[System.Serializable]
		public struct ContainerPrefabConfig
		{
			public string name;
			public CharacterContainer prefab;

			// 增加绑定的范围配置
			//public MyGameplay.GameData.LevelRange range;
			public MyGameplay.GameData.LevelRangeRect range;

			[Multiline(2)]
			public string title;

			public int nextLevelId;
			public bool finishLevel;
		}
		// TODO : 提出一个 Setting 对象

		//protected Dictionary<string, GameObject> m_levelContainerPrefabDict = new Dictionary<string, GameObject>();
		//protected Dictionary<string, CharacterContainer> m_levelContainerPrefabDict = new Dictionary<string, CharacterContainer>();

		//protected Dictionary<string, CharacterContainer> m_levelContainerPrefabDict = new(16);
		//protected Dictionary<string, MyGameplay.GameData.LevelRangeRect> m_levelRangePrefabDict = new(16);
		//protected Dictionary<string, string> m_levelTitleTextDict = new(16);

		protected Dictionary<string, ContainerPrefabConfig> mlevelConfigDict = new(16);

		public void InitContaienrPrefabs()
		{
			//m_levelContainerPrefabDict.Clear();
			//m_levelRangePrefabDict.Clear();
			//m_levelTitleTextDict.Clear();
			mlevelConfigDict.Clear();
			for (int i = 0, l = levelContainerPrefabs.Count; i < l; i++)
			{
				//var prefab = levelContainerPrefabs[i];
				//if (m_levelContainerPrefabDict.ContainsKey(prefab.containerName))
				//	Debug.LogWarningFormat("The containerName of the CharacterContainer Prefab (index {0}) is already registered", i);
				//else
				//	m_levelContainerPrefabDict.Add(prefab.containerName, prefab);

				var cfg = levelContainerPrefabs[i];
				if (cfg.name == "")
					Debug.LogWarningFormat("The containerName of the CharacterContainer Prefab (index {0}) is empty", i);
				//else if (m_levelContainerPrefabDict.ContainsKey(cfg.name)
				//		|| m_levelRangePrefabDict.ContainsKey(cfg.name)
				//		|| m_levelTitleTextDict.ContainsKey(cfg.name))
				else if (mlevelConfigDict.ContainsKey(cfg.name))
					Debug.LogWarningFormat("The containerName of the CharacterContainer Prefab (index {0}) is already registered", i);
				else
				{
					//m_levelContainerPrefabDict.Add(cfg.name, cfg.prefab);
					//m_levelRangePrefabDict.Add(cfg.name, cfg.range);
					//m_levelTitleTextDict.Add(cfg.name, cfg.title);
					mlevelConfigDict.Add(cfg.name, cfg);
				}
			}
		}

		public CharacterContainer CreateCharacterContainer(CharacterContainer prefab)
		{
			var container = Instantiate(prefab);
			container.gameObject.SetActive(false);
			container.transform.SetParent(transform);
			return container;
		}


		[SerializeField]
		//protected string defaultContainerName = "Default";
		protected string titleContainerName = "Title";

		//protected CharacterContainer currentCharacterContainer;
		public CharacterContainer currentCharacterContainer;

		//[SerializeField]
		//protected CharacterContainer tempCharacterContainer;

		[SerializeField]
		protected CharacterContainer tempOnCharacterContainer;
		[SerializeField]
		protected CharacterContainer tempOffCharacterContainer;

		/*
		protected Dictionary<string, CharacterContainer> m_characterContainerDict = new Dictionary<string, CharacterContainer>(8);

		protected void InitCharacterContainer()
		{
			m_characterContainerDict.Clear();
			for (int i = 0, l = transform.childCount; i < l; i++)
			{
				var container = transform.GetChild(i).GetComponent<CharacterContainer>();
				if (!container)
					continue;

				if (container.IsFixedContainer)
				{
					if (m_characterContainerDict.ContainsKey(container.containerName))
						Debug.LogWarningFormat("The containerName of the CharacterContainer (index {0}) is already registered", i);
					else
					{
						m_characterContainerDict.Add(container.containerName, container);
						container.gameObject.SetActive(false);
					}
				}
				/*else
				{
					//if (!tempCharacterContainer)
					//	tempCharacterContainer = container;

					if (!tempOnCharacterContainer)
						tempOnCharacterContainer = container;
					else if (!tempOffCharacterContainer)
						tempOffCharacterContainer = container;
				}* /
			}

			SetCurrentContainer(defaultContainerName);
		}
		*/

		protected void InitCharacters()
		{
			//foreach (var container in m_characterContainerDict.Values)
			//{
			//	foreach (var character in container.EachCharacterActive())
			//		RegisterCharacter(character);
			//}

			//ActivateCharacterContainer(tempCharacterContainer);
			ActivateCharacterContainer(tempOnCharacterContainer);
			//ActivateCharacterContainer(tempOffCharacterContainer);
			//ActivateCharacterContainer(currentCharacterContainer);

			//SetCurrentContainer(defaultContainerName);
			//SetCurrentContainer(titleContainerName);
			//SetTitleContainer();
			//ActivateCharacterContainer(currentCharacterContainer);
			// ？由 TitleLogic 完成 ...
			
			tempOffCharacterContainer.gameObject.SetActive(false);
		}

		/*public bool SetCurrentContainer(string containerName)
		{
			CharacterContainer container;
			if (m_characterContainerDict.TryGetValue(containerName, out container))
			{
				DeactivateCharacterContainer(currentCharacterContainer);
				currentCharacterContainer = container;
				ActivateCharacterContainer(currentCharacterContainer);
				return true;
			}
			else
			{
				Debug.LogErrorFormat("Don't found CharacterContainer named \"{0}\"", containerName);
				return false;
			}
		}*/

		public bool SetCurrentContainer(string containerName)
		{
			//CharacterContainer prefab;
			//if (m_levelContainerPrefabDict.TryGetValue(containerName, out prefab))
			//if (m_levelContainerPrefabDict.TryGetValue(containerName, out var prefab)
			//	&& m_levelRangePrefabDict.TryGetValue(containerName, out var levelRange))
			//if (m_levelContainerPrefabDict.TryGetValue(containerName, out var prefab))
			if (mlevelConfigDict.TryGetValue(containerName, out var cfg))
			{
				DeactivateCharacterContainer(currentCharacterContainer);
				//currentCharacterContainer = Instantiate(prefab);
				//currentCharacterContainer = CreateCharacterContainer(prefab);
				currentCharacterContainer = CreateCharacterContainer(cfg.prefab);
				ActivateCharacterContainer(currentCharacterContainer);
				//MyGameplay.GameData.LevelRangeManager.Inst.SetRange(levelRange);
				return true;
			}
			else
			{
				Debug.LogErrorFormat("Don't found CharacterContainer Prefab named \"{0}\"", containerName);
				return false;
			}
		}

		public void SetTitleContainer()
		{
			SetCurrentContainer(titleContainerName);
		}

		public void ActivateCharacterContainer(CharacterContainer container)
		{
			if (!container)
				return;

			// 暂时如此判断是否已被激活
			if (container.gameObject.activeSelf)
				return;

			container.gameObject.SetActive(true);
			foreach (var character in container.EachCharacterActive())
				RegisterCharacter(character);
			// ...
		}

		public void DeactivateCharacterContainer(CharacterContainer container)
		{
			if (!container)
				return;

			// 暂时如此判断是否未被激活
			if (!container.gameObject.activeSelf)
				return;

			foreach (var character in container.EachCharacterRegistered())
				DeregisterCharacter(character);
			container.gameObject.SetActive(false);

			// 摧毁
			//Destroy(container, 0.1f);
			Destroy(container.gameObject, 0.1f);
		}


		#endregion CharacterContainer


		public MyGameplay.GameData.LevelRangeRect GetLevelRange(string containerName)
		{
			//if (m_levelRangePrefabDict.TryGetValue(containerName, out var levelRange))
			//	return levelRange;
			if (mlevelConfigDict.TryGetValue(containerName, out var cfg))
				return cfg.range;
			else
				return null;
		}

		public string GetLevelTitleText(string containerName)
		{
			//if (m_levelTitleTextDict.TryGetValue(containerName, out var titleText))
			//	return titleText;
			if (mlevelConfigDict.TryGetValue(containerName, out var cfg))
				return cfg.title;
			else
				return "";
		}

		public int GetNextLevelId(string containerName)
		{
			if (mlevelConfigDict.TryGetValue(containerName, out var cfg))
				return cfg.nextLevelId;
			else
				return 0;
		}
		public bool GetLevelIsFinish(string containerName)
		{
			if (mlevelConfigDict.TryGetValue(containerName, out var cfg))
				return cfg.finishLevel;
			else
				return false;
		}
		// TODO


		#region Passable/ImPassable Tiles

		/// <summary>
		/// 路 瓦片
		/// </summary>
		[SerializeField]
		protected List<TileBase> m_passableTiles = new List<TileBase>();

		/// <summary>
		/// 墙 瓦片
		/// </summary>
		[SerializeField]
		protected List<TileBase> m_impassableTiles = new List<TileBase>();

		public bool IsPassableTile(int x, int y)
		{
			return m_passableTiles.Contains(GetGp1Tile(x, y));
		}
		public bool IsPassableTile(Vector2Int pos)
		{
			return m_passableTiles.Contains(GetGp1Tile(pos));
		}

		#endregion Passable/ImPassable Tiles


		#region Socket Tiles

		/// <summary>
		/// 插座 瓦片
		/// </summary>
		[SerializeField]
		protected List<TileBase> m_socketTiles = new List<TileBase>();

		public bool IsSocketTile(int x, int y)
		{
			return m_socketTiles.Contains(GetGp2Tile(x, y));
		}
		public bool IsSocketTile(Vector2Int pos)
		{
			return m_socketTiles.Contains(GetGp2Tile(pos));
		}

		#endregion Socket Tiles


		#region Characters

		//protected List<BaseCharacter> m_characterList = new List<BaseCharacter>();
		protected List<CharacterCommon> m_characterList = new List<CharacterCommon>();

		public void ClearRegistered()
		{
			m_characterList.Clear();
		}

		//public void RegisterCharacter(BaseCharacter character)
		public void RegisterCharacter(CharacterCommon character)
		{
			//m_characterList.Add(character);
			//character.OnRegisterCharacter();
			if (!character.isRegistered)
			{
				m_characterList.Add(character);
				character.OnRegisterCharacter();
			}
		}

		//public bool DeregisterCharacter(BaseCharacter character)
		public bool DeregisterCharacter(CharacterCommon character)
		{
			//character.OnDeregisterCharacter();
			//return m_characterList.Remove(character);
			if (character.isRegistered)
			{
				character.OnDeregisterCharacter();
				return m_characterList.Remove(character);
			}
			return true;
		}

		//public IEnumerable<BaseCharacter> EachCharactersAt(Vector2Int pos)
		public IEnumerable<CharacterCommon> EachCharactersAt(Vector2Int pos)
		{
			foreach (var each in m_characterList)
			{
				if (each.GridPos == pos)
					yield return each;
			}
		}

		/*public List<BaseCharacter> AllCharactersAt(Vector2Int pos)
		{
			var list = new List<BaseCharacter>();
			foreach (var each in EachCharactersAt(pos))
			{
				list.Add(each);
			}
			return list;
		}*/

		// TODO : 优化索引

		/*//public IEnumerable<BaseCharacter> EachCharactersIn(List<Vector2Int> list1, List<Vector2Int> list2 = null)
		public IEnumerable<CharacterCommon> EachCharactersIn(List<Vector2Int> list1, List<Vector2Int> list2 = null)
		{
			foreach (var each in m_characterList)
			{
				if (list1 != null && list1.Contains(each.GridPos))
					yield return each;
				else if (list2 != null && list2.Contains(each.GridPos))
					yield return each;
			}
		}*/
		public IEnumerable<CharacterCommon> EachCharactersIn(List<HashSet<Vector2Int>> posSetList)
		{
			foreach (var each in m_characterList)
			{
				foreach (var posSet in posSetList)
					if (posSet.Contains(each.GridPos))
						yield return each;
			}
		}

		public IEnumerable<MechanismLock> EachLockInCurContainer()
		{
			foreach (var each in currentCharacterContainer.EachCharacterActive())
			{
				if (each is MechanismLock)
					yield return each as MechanismLock;
			}
		}

		#endregion Characters


		#region CheckCanMove

		public bool CheckCanMove(Vector2Int pos, CharacterDir dir)
		{
			Vector2Int nextPos = pos + BaseCharacter.MakeVectorFromDir(dir);
			if (!IsPassableTile(nextPos))
				return false;

			foreach (var each in EachCharactersAt(nextPos))
			{
				if (each.isObstructable())
					return false;
			}

			var somePush = false;
			foreach (var each in EachCharactersAt(nextPos))
			{
				if (each.isPushable())
					somePush = true;
			}

			if (!somePush)
				return true;

			// 迭代检查是否能推动
			//if (!CheckCanMove(nextPos, dir))
			//	return false;
			//return true;
			return CheckCanMove(nextPos, dir);
		}

		public bool CheckCanMove(Vector2Int pos, CharacterDir dir, ref List<BaseCharacter> pushedList)
		{
			Vector2Int nextPos = pos + BaseCharacter.MakeVectorFromDir(dir);
			if (!IsPassableTile(nextPos))
				return false;

			foreach (var each in EachCharactersAt(nextPos))
			{
				if (each.isObstructable())
					return false;
			}

			var somePush = false;
			foreach (var each in EachCharactersAt(nextPos))
			{
				if (each.isPushable())
					somePush = true;
			}

			if (!somePush)
				return true;

			// 迭代检查是否能推动
			if (!CheckCanMove(nextPos, dir, ref pushedList))
				return false;

			//if (pushedList != null)
			//{
			foreach (var each in EachCharactersAt(nextPos))
			{
				if (each.isPushable())
					pushedList.Add(each);
			}
			//}

			return true;
		}

		#endregion CheckCanMove


		public void RefreshSignalPower(SignalGroupSt2 groupSt)
		{
			if (groupSt != null)
			{
				m_tempPosSetList.Clear();
				foreach (var groupId in groupSt.EachLayerGroupId())
					m_tempPosSetList.Add(SignalLinkGroup.Inst.GetPosSetByGroupId(groupId));

				var list = EachCharactersIn(m_tempPosSetList);
				foreach (var each in list)
					each.OnSignalPowerHereChanged();
			}
		}

		protected bool PowerSupplyIsPoolItem(CharacterCommon character)
		{
			return (character is PowerSupply) && ((character as PowerSupply).isPoolItem);
		}

		protected List<PowerSupply> m_tempPowerSupplyList = new List<PowerSupply>(4);

		public void UnloadPowerSupplyPoolItems()
		{
			//foreach (var each in m_characterList)
			//{
			//	if (PowerSupplyIsPoolItem(each))
			//		PickPowerSupply(each as PowerSupply);
			//}
			m_tempPowerSupplyList.Clear();
			foreach (var each in m_characterList)
			{
				if (PowerSupplyIsPoolItem(each))
					m_tempPowerSupplyList.Add(each as PowerSupply);
			}
			foreach (var each in m_tempPowerSupplyList)
				PickPowerSupply(each);
		}


		#region Place/Pick PowerSupply

		/*public void PlacePowerSupply(SignalPowerType pType, Vector2Int pos)
		{
			var powerSupply = PowerSupplyPool.Inst.GetPowerSupply(pType, pos);
			powerSupply.transform.SetParent(characterContainerTf);
			RegisterCharacter(powerSupply);
			//powerSupply.OnPlacePower();

			var groupSt = SignalLinkGroup.Inst.GetGroupId(pos);
			if (groupSt.hasGroup2 && groupSt.groupId != groupSt.group2Id)
			{
				var list = EachCharactersIn(SignalLinkGroup.Inst.GetPosListByGroupId(groupSt.groupId),
					SignalLinkGroup.Inst.GetPosListByGroupId(groupSt.group2Id));
				foreach (var each in list)
					each.OnSignalPowerHereChanged();
			}
			else if (groupSt.hasGroup)
			{
				var list = EachCharactersIn(SignalLinkGroup.Inst.GetPosListByGroupId(groupSt.groupId));
				foreach (var each in list)
					each.OnSignalPowerHereChanged();
			}
		}*/

		protected List<HashSet<Vector2Int>> m_tempPosSetList = new List<HashSet<Vector2Int>>(4);

		public void PlacePowerSupply(SignalPowerType pType, Vector2Int pos)
		{
			var powerSupply = PowerSupplyPool.Inst.GetPowerSupply(pType, pos);
			//powerSupply.transform.SetParent(characterContainerTf);
			//tempCharacterContainer.OnRegisterCharacter(powerSupply);
			tempOnCharacterContainer.OnRegisterCharacter(powerSupply);
			RegisterCharacter(powerSupply);

			var groupSt = SignalLinkGroup.Inst.GetGroupId(pos);
			/*if (groupSt != null)
			{
				m_tempPosSetList.Clear();
				foreach (var groupId in groupSt.EachLayerGroupId())
					m_tempPosSetList.Add(SignalLinkGroup.Inst.GetPosSetByGroupId(groupId));

				var list = EachCharactersIn(m_tempPosSetList);
				foreach (var each in list)
					each.OnSignalPowerHereChanged();
			}*/
			RefreshSignalPower(groupSt);
		}

		/*public void PickPowerSupply(PowerSupply powerSupply)
		{
			var groupSt = SignalLinkGroup.Inst.GetGroupId(powerSupply.GridPos);

			//powerSupply.OnPickPower();
			DeregisterCharacter(powerSupply);
			PowerSupplyPool.Inst.ReleasePowerSupply(powerSupply);

			if (groupSt.hasGroup2 && groupSt.groupId != groupSt.group2Id)
			{
				var list = EachCharactersIn(SignalLinkGroup.Inst.GetPosListByGroupId(groupSt.groupId),
					SignalLinkGroup.Inst.GetPosListByGroupId(groupSt.group2Id));
				foreach (var each in list)
					each.OnSignalPowerHereChanged();
			}
			else if (groupSt.hasGroup)
			{
				var list = EachCharactersIn(SignalLinkGroup.Inst.GetPosListByGroupId(groupSt.groupId));
				foreach (var each in list)
					each.OnSignalPowerHereChanged();
			}
		}*/
		public void PickPowerSupply(PowerSupply powerSupply)
		{
			var groupSt = SignalLinkGroup.Inst.GetGroupId(powerSupply.GridPos);

			DeregisterCharacter(powerSupply);
			//PowerSupplyPool.Inst.ReleasePowerSupply(powerSupply);
			if (powerSupply.isPoolItem)
				//tempCharacterContainer.OnDeregisterCharacter(powerSupply);
				tempOnCharacterContainer.OnDeregisterCharacter(powerSupply);
			else
				currentCharacterContainer.OnDeregisterCharacter(powerSupply);

			/*if (groupSt != null)
			{
				m_tempPosSetList.Clear();
				foreach (var groupId in groupSt.EachLayerGroupId())
					m_tempPosSetList.Add(SignalLinkGroup.Inst.GetPosSetByGroupId(groupId));

				var list = EachCharactersIn(m_tempPosSetList);
				foreach (var each in list)
					each.OnSignalPowerHereChanged();
			}*/
			RefreshSignalPower(groupSt);
		}

		#endregion Place/Pick PowerSupply


		#region Trigger

		/*public IEnumerable<CharacterTrigger> EachTriggerCharactersAt(Vector2Int pos)
		{
			foreach (var each in EachCharactersAt(pos))
			{
				if (each is CharacterTrigger)
					yield return each as CharacterTrigger;
			}
		}*/
		// ？在循环IEnumerable时 不能移除对应list的元素 ...

		protected List<CharacterTrigger> m_tempTriggerList = new List<CharacterTrigger>(4);

		public List<CharacterTrigger> AllTriggerCharactersAt(Vector2Int pos)
		{
			m_tempTriggerList.Clear();
			foreach (var each in EachCharactersAt(pos))
			{
				if (each is CharacterTrigger)
					m_tempTriggerList.Add(each as CharacterTrigger);
			}
			return m_tempTriggerList;
		}


		//public void RefreshForTriggerCharacter(Vector2Int pos)
		//public void RefreshForTriggerCharacter(BaseCharacterPlayer player)
		public void RefreshForContactTriggerCharacter(BaseCharacterPlayer player)
		{
			//foreach (var each in EachTriggerCharactersAt(pos))
			//foreach (var each in EachTriggerCharactersAt(player.GridPos))
			//{
			//	each.OnPlayerTriggerCharacter(player);
			//}
			foreach (var each in AllTriggerCharactersAt(player.GridPos))
			{
				//each.OnPlayerTriggerCharacter(player);
				//each.OnPlayerContactTriggerCharacter(player);
				//if (!each.triggerByActionZ)
				//	each.OnPlayerContactTriggerCharacter(player);
				//if (!each.triggerByActionZ)
				if (each.triigerEnabled && !each.triggerByActionZ)
				{
					each.OnPlayerContactTriggerCharacter(player);
					break;
				}
				// ？暂时 保证一次只有一个触发物 ...
			}
		}

		public void RefreshForActionZTriggerCharacter(BaseCharacterPlayer player)
		{
			foreach (var each in AllTriggerCharactersAt(player.GridPos))
			{
				//if (each.triggerByActionZ)
				//	each.OnPlayerActionZTriggerCharacter(player);
				// ？暂时 保证一次只有一个触发物 ...
				//if (each.triggerByActionZ)
				if (each.triigerEnabled && each.triggerByActionZ)
				{
					each.OnPlayerActionZTriggerCharacter(player);
					break;
				}
			}
		}

		// TODO : ？先找出所有需要触发的物体 再全部触发 ...

		// ？暂时 保证一次只有一个触发物 ...

		public bool HasActionZTriggerCharacter(Vector2Int pos)
		{
			foreach (var each in AllTriggerCharactersAt(pos))
			{
				//if (each.triggerByActionZ)
				if (each.triigerEnabled && each.triggerByActionZ)
						return true;
			}
			return false;
		}

		#endregion


		#region Player

		[SerializeField]
		//protected BaseCharacterPlayer singlePlayer;
		public BaseCharacterPlayer singlePlayer;

		public void ActivatePlayer(Vector2Int pos)
		{
			if (singlePlayer.isRegistered)
			{
				singlePlayer.SetGridPosNonAnim(pos);
			}
			else
			{
				singlePlayer.transform.position = new Vector3(pos.x, pos.y, singlePlayer.transform.position.z);
				singlePlayer.transform.SetParent(tempOnCharacterContainer.transform);
				RegisterCharacter(singlePlayer);
			}
		}

		public void DeactivatePlayer()
		{
			if (singlePlayer.isRegistered)
			{
				singlePlayer.transform.SetParent(tempOffCharacterContainer.transform);
				DeregisterCharacter(singlePlayer);
			}
		}

		#endregion Player


		#region Character Event

		#endregion Character Event


		#region DebugFunctions

		[SerializeField]
		public SignalPowerType debugPowerType;

		[SerializeField]
		public Vector2Int debugPos;

		[ContextMenu("TestPlacePowerSupply")]
		public void TestPlacePowerSupply()
		{
			PlacePowerSupply(debugPowerType, debugPos);
		}

		[ContextMenu("TestPickPowerSupply")]
		public void TestPickPowerSupply()
		{
			foreach (var each in EachCharactersAt(debugPos))
			{
				if (each is PowerSupply)
				{
					PickPowerSupply(each as PowerSupply);
					return;
				}
			}
		}

		#endregion DebugFunctions
	}

	// TODO : 提出一个 BaseGridMap 在 GridMovement 其他逻辑部分 新建一个派生类 放在 MyGameplay
}
