using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyGameplay.Logic;

namespace MyGameplay.GameData
{
	public class LevelRangeManager : MonoBehaviour
	{
		#region Instance

		protected static LevelRangeManager instance;

		public static LevelRangeManager Inst => GetInstance();

		protected static LevelRangeManager GetInstance()
		{
			if (instance)
				return instance;
			else
			{
				var go = GameObject.Find("LevelRangeManager");
				if (go)
					instance = go.GetComponent<LevelRangeManager>();
				return instance;
			}
		}

		protected void RegisterInstance(LevelRangeManager obj)
		{
			instance = obj;
		}

		protected void RegisterThisInst()
		{
			RegisterInstance(this);
		}

		#endregion Instance


#if false
		//public GameLevelRanges levelRangesData;
		public Dictionary<string, RectInt> m_rangeDict = new Dictionary<string, RectInt>();

		//public List<LevelRange> rangeList = new List<LevelRange>();

#if false
		//public LevelRange[] GetRangeList()
		public List<LevelRange> GetRangeList()
		{
			/*var tf = transform.Find("Container");

			//var goList = GameObject.FindGameObjectsWithTag("RangeMark");
			//var rangeList = transform.GetComponentsInChildren<LevelRange>();
			var result = new List<LevelRange>(8);
			for (int i = 0, l = tf.childCount; i < l; i++)
			{
				var range = tf.GetChild(i).GetComponent<LevelRange>();
				result.Add(range);
			}
			return result;*/

			return new List<LevelRange>(transform.GetComponentsInChildren<LevelRange>());
		}
#else
		public IEnumerable<LevelRangeRect> GetRangeList()
		{
			return transform.GetComponentsInChildren<LevelRangeRect>();
		}
#endif

		/*[ContextMenu("GameObject To RangeData")]
		public void GameObjectToRangeData()
		{
			var resList = levelRangesData.rectList;
			resList.Clear();
			foreach (var range in GetRangeList())
			{
				if (range.IsValid())
					resList.Add(range.GetRange());
			}
		}

		[ContextMenu("RangeData To GameObject")]
		public void RangeDataToGameObject()
		{
		}*/

		public void InitRangeData()
		{
			m_rangeDict.Clear();
			foreach (var range in GetRangeList())
			{
				if (range.IsValid() && !m_rangeDict.ContainsKey(range.rangeName))
					m_rangeDict.Add(range.rangeName, range.GetRange());
			}
		}

		[ContextMenu("Debug InitRangeData")]
		public void DebugInitRangeData()
		{
			InitRangeData();
			foreach (var each in m_rangeDict)
			{
				Debug.Log(each.Key + " " + each.Value);
			}
		}

		public bool HasRange(string rangeName)
		{
			return m_rangeDict.ContainsKey(rangeName);
		}

		public RectInt GetRange(string rangeName)
		{
			return m_rangeDict[rangeName];
		}


		protected void Awake()
		{
			InitRangeData();
		}


		[SerializeField]
		public CameraLogic targetCameraLogic;

		//public string currentRangeName = "";
		protected string currentRangeName = "";
		//public bool currentRangeImmediately = false;

		public void SetRangeName(string rangeName, bool immediately = false)
		{
			if (currentRangeName != rangeName && HasRange(rangeName))
			{
				currentRangeName = rangeName;
				//currentRangeImmediately = immediately;

				var range = GetRange(currentRangeName);
				targetCameraLogic.SetViewRange(range);
				//if (currentRangeImmediately)
				if (immediately)
						targetCameraLogic.RefreshViewImmediately();
			}
		}
#endif

		// ？改 一次性初始化获取所有范围配置 之后再根据name对应设置范围配置 ...
		// ？为 将范围配置放在关卡中 加载关卡后 使用关卡中的范围配置进行设置 ...
		// ？没必要立刻知道所有范围配置 ...
		// ？范围配置和关卡总是一一对应的 ...
		// ？或者是增加一个用来绑定关卡预设体和范围配置对象的配置 ...

		[SerializeField]
		public CameraLogic targetCameraLogic;

		//public void SetRange(LevelRange levelRange, bool immediately = false)
		public void SetRange(LevelRangeRect levelRange, bool immediately)
		{
			if (!levelRange)
				return;
			var range = levelRange.GetRange();
			targetCameraLogic.SetViewRange(range);
			if (immediately)
				targetCameraLogic.RefreshViewImmediately();
		}
	}
}
