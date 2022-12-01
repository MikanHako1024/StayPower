using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if false
namespace MyGameplay.GameData
{
#if false
	public class LevelRange : MonoBehaviour
	{
		public string rangeName;

		public LevelRange endPoint;

		// ？改 手动绑定 结束点 ...
		// ？为 让第一个孩子作为结束点 ...

		public Vector2Int GetPos()
		{
			//Vector3 pos = transform.position;
			//Vector3 pos = transform.localPosition;
			Vector3 pos = transform.position;
			return new Vector2Int(Mathf.RoundToInt(pos.x), Mathf.RoundToInt(pos.y));
		}

		public LevelRange GetEndPoint()
		{
			//return transform.GetChild(0).GetComponent<LevelRange>();
			return endPoint;
		}

		public RectInt GetRange()
		{
			//return new RectInt(GetPos(), endPoint.GetPos());
			var endPoint = GetEndPoint();
			//return new RectInt(GetPos(), endPoint.GetPos());
			return new RectInt(GetPos(), endPoint.GetPos() - GetPos() + Vector2Int.one);
		}

		public bool IsValid()
		{
			//return endPoint != null;
			//return transform.childCount > 0 && GetEndPoint() != null;
			return rangeName != "" && GetEndPoint() != null;
		}

		[ContextMenu("Refresh All Pos")]
		public void RefreshAllPos()
		{
			RefreshPos();
			var endPoint = GetEndPoint();
			if (endPoint)
				endPoint.RefreshPos();
		}

		public void RefreshPos()
		{
			//Vector3 pos = transform.localPosition;
			//transform.localPosition = new Vector3Int(Mathf.RoundToInt(pos.x), Mathf.RoundToInt(pos.y), 0);
			Vector3 pos = transform.position;
			transform.position = new Vector3Int(Mathf.RoundToInt(pos.x), Mathf.RoundToInt(pos.y), 0);
		}
	}
#else
	public class LevelRange : MonoBehaviour
	{
		public Vector2Int GetPos()
		{
			Vector3 pos = transform.position;
			return new Vector2Int(Mathf.RoundToInt(pos.x), Mathf.RoundToInt(pos.y));
		}

		public void RefreshPos()
		{
			Vector3 pos = transform.position;
			transform.position = new Vector3Int(Mathf.RoundToInt(pos.x), Mathf.RoundToInt(pos.y), 0);
		}
	}
#endif
}
#endif
