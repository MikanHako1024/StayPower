using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyGameplay.GameData;

namespace MyGameplay.Logic
{
	[RequireComponent(typeof(Camera))]
	public class CameraLogic : MonoBehaviour
	{
		[SerializeField]
		public Transform followerTf;

		[SerializeField]
		public Camera targetCamera;

		[SerializeField, Range(1, 1024)]
		public int pixelsPerUnit = 32;

		//protected void Awake()
		//{
		//	if (!targetCamera)
		//		targetCamera = GetComponent<Camera>();
		//}


		protected RectInt viewRect;
		//protected RectInt posRangeRect;
		protected Rect posRangeRect;

		public void SetViewRange(RectInt range)
		{
			viewRect = range;
			RefreshPosRange();
		}

		public void RefreshPosRange()
		{
			//Debug.Log(targetCamera.pixelWidth + " " + targetCamera.pixelHeight);

			/*
			//float screenUnitX = targetCamera.pixelWidth / pixelsPerUnit;
			//float screenUnitY = targetCamera.pixelHeight / pixelsPerUnit;
			//float screenUnitX = targetCamera.pixelWidth / pixelsPerUnit - 1;
			//float screenUnitY = targetCamera.pixelHeight / pixelsPerUnit - 1;
			//float screenUnitX = 1f * targetCamera.pixelWidth / pixelsPerUnit - 1;
			//float screenUnitY = 1f * targetCamera.pixelHeight / pixelsPerUnit - 1;
			float screenUnitX = 1f * targetCamera.pixelWidth / pixelsPerUnit;
			float screenUnitY = 1f * targetCamera.pixelHeight / pixelsPerUnit;

			float sx = viewRect.x + screenUnitX / 2;
			float sy = viewRect.y + screenUnitY / 2;
			//float ex = viewRect.x + viewRect.width + 1 - screenUnitX / 2;
			//float ey = viewRect.x + viewRect.height + 1 - screenUnitY / 2;
			float ex = viewRect.x + viewRect.width - screenUnitX / 2;
			float ey = viewRect.y + viewRect.height - screenUnitY / 2;

			float rx = sx <= ex ? sx : ((sx + ex) / 2);
			float ry = sy <= ey ? sy : ((sy + ey) / 2);
			float rw = sx <= ex ? (ex - sx) : 0;
			float rh = sy <= ey ? (ey - sy) : 0;
			posRangeRect = new Rect(rx, ry, rw, rh);
			*/

			//float cx = viewRect.x + viewRect.width / 2f;
			//float cy = viewRect.y + viewRect.height / 2f;
			//float cx = (viewRect.x + viewRect.width) / 2f;
			//float cy = (viewRect.y + viewRect.height) / 2f;
			float cx = viewRect.x + (viewRect.width - 1) / 2f;
			float cy = viewRect.y + (viewRect.height - 1) / 2f;
			float dw = viewRect.width - 1f * targetCamera.pixelWidth / pixelsPerUnit;
			float dh = viewRect.height - 1f * targetCamera.pixelHeight / pixelsPerUnit;

			float rx = dw > 0 ? (cx - dw / 2) : cx;
			float ry = dh > 0 ? (cy - dh / 2) : cy;
			float rw = dw > 0 ? dw : 0;
			float rh = dh > 0 ? dh : 0;
			posRangeRect = new Rect(rx, ry, rw, rh);

			//Debug.Log(posRangeRect);
		}


		protected float MakeValueInRange(float val, float min, float max)
		{
			if (val < min)
				return min;
			else if (val > max)
				return max;
			else
				return val;
		}

		protected bool CheckValueInRange(float val, float min, float max)
		{
			return min <= val && val <= max;
		}

		protected Vector3 CalcTargetPos(in Vector3 pos)
		{
			return new Vector3(
				MakeValueInRange(pos.x, posRangeRect.x, posRangeRect.x + posRangeRect.width),
				MakeValueInRange(pos.y, posRangeRect.y, posRangeRect.y + posRangeRect.height),
				pos.z);
		}

		protected bool CheckNeedMove(in Vector3 pos)
		{
			return !CheckValueInRange(pos.x, posRangeRect.x, posRangeRect.x + posRangeRect.width)
				//&& !CheckValueInRange(pos.y, posRangeRect.y, posRangeRect.y + posRangeRect.height);
				|| !CheckValueInRange(pos.y, posRangeRect.y, posRangeRect.y + posRangeRect.height);
		}


		public void RefreshViewImmediately()
		{
			if (CheckNeedMove(transform.position))
				transform.position = CalcTargetPos(transform.position);
		}

		[SerializeField, Min(0.001f)]
		public float moveSpeed = 8f;

		public void RefreshViewOneTick(float deltaTime)
		{
			if (CheckNeedMove(transform.position))
			{
				var pos = transform.position;
				var target = CalcTargetPos(pos);
				var deltaPos = target - pos;
				var moveDist = moveSpeed * deltaTime;
				if (deltaPos.magnitude <= moveDist)
					transform.position = target;
				else
					//transform.position = moveDist * deltaPos.normalized;
					transform.position = pos + moveDist * deltaPos.normalized;
			}
		}

		protected void UpdateFollow()
		{
			RefreshViewOneTick(Time.deltaTime);
		}


		/*protected string rangeName = "";

		protected void UpdateRange()
		{
			string newRangeName = LevelRangeManager.Inst.currentRangeName;
			if (rangeName != newRangeName)
			{
				if (LevelRangeManager.Inst.HasRange(newRangeName))
				{
					rangeName = newRangeName;
					var range = LevelRangeManager.Inst.GetRange(rangeName);
					SetViewRange(range);
					if (LevelRangeManager.Inst.currentRangeImmediately)
						RefreshViewImmediately();
				}
			}
		}*/


		protected void Update()
		{
			//UpdateRange();
			UpdateFollow();
		}


		#region Debug

		/*[SerializeField]
		protected string debugRangeName;

		[ContextMenu("Debug SetViewRange")]
		public void DebugSetViewRange()
		{
			if (LevelRangeManager.Inst.HasRange(debugRangeName))
				SetViewRange(LevelRangeManager.Inst.GetRange(debugRangeName));
		}

		[ContextMenu("Debug SetViewRange Immediately")]
		public void DebugSetViewRangeImmediately()
		{
			if (LevelRangeManager.Inst.HasRange(debugRangeName))
				SetViewRange(LevelRangeManager.Inst.GetRange(debugRangeName));
			RefreshViewImmediately();
		}*/
		
		#endregion Debug
	}
}
