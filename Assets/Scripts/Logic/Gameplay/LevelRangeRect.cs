using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyGameplay.GameData
{
	public class LevelRangeRect : MonoBehaviour
	{
		public string rangeName;
		//public LevelRange startPoint;
		//public LevelRange endPoint;
		public Transform startPoint;
		public Transform endPoint;


		public bool IsValid()
		{
			return rangeName != "" && startPoint != null && endPoint != null;
		}

		public Vector2Int GetPointPos(Transform pointTf)
		{
			Vector3 pos = pointTf.position;
			return new Vector2Int(Mathf.RoundToInt(pos.x), Mathf.RoundToInt(pos.y));
		}
		public void RefreshPointPos(Transform pointTf)
		{
			Vector3 pos = pointTf.position;
			transform.position = new Vector3Int(Mathf.RoundToInt(pos.x), Mathf.RoundToInt(pos.y), 0);
		}

		public RectInt GetRange()
		{
			//return new RectInt(startPoint.GetPos(), endPoint.GetPos() - startPoint.GetPos() + Vector2Int.one);
			return new RectInt(GetPointPos(startPoint), GetPointPos(endPoint) - GetPointPos(startPoint) + Vector2Int.one);
		}

		[ContextMenu("Refresh All Pos")]
		public void RefreshAllPos()
		{
			if (startPoint)
				//startPoint.RefreshPos();
				RefreshPointPos(startPoint);
			if (endPoint)
				//endPoint.RefreshPos();
				RefreshPointPos(endPoint);
		}
	}
}
