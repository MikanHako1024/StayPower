using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GridMovement
{
	public abstract class BaseCharacter : MonoBehaviour
	{
		// 记录是否被注册
		public bool isRegistered = false;

		//protected virtual void RegisterThisCharacter()
		//{
		//}

		public virtual void OnRegisterCharacter()
		{
			isRegistered = true;
			InitGridPos();
		}

		public virtual void OnDeregisterCharacter()
		{
			isRegistered = false;
		}


		//protected virtual void Awake()
		//{
		//	InitGridPos();
		//}

		//protected virtual void Update()
		//{
		//	UpdateMoving();
		//}

		#region GridPos

		//protected Vector2 m_v2GridPos;
		protected Vector2Int m_v2GridPos;

		public Vector2Int GridPos { get => m_v2GridPos; }

		[ContextMenu("ResetGridPos")]
		protected virtual void InitGridPos()
		{
			Vector3 pos = transform.position;
			int x = Mathf.RoundToInt(pos.x);
			int y = Mathf.RoundToInt(pos.y);
			transform.position = new Vector3(x, y, pos.z);
			m_v2GridPos = new Vector2Int(x, y);

			OnGridPosChanged();
		}

		public virtual void SetGridPosNonAnim(Vector2Int pos)
		{
			m_v2GridPos = pos;
			transform.position = new Vector3(pos.x, pos.y, transform.position.z);

			OnGridPosChanged();
		}


		protected virtual void OnGridPosChanged()
		{
		}

		#endregion GridPos


		//protected Vector2Int MakeVectorFromDir(CharacterDir dir)
		public static Vector2Int MakeVectorFromDir(CharacterDir dir)
		{
			if (dir == CharacterDir.down)
				return Vector2Int.down;
			else if (dir == CharacterDir.left)
				return Vector2Int.left;
			else if (dir == CharacterDir.right)
				return Vector2Int.right;
			else if (dir == CharacterDir.up)
				return Vector2Int.up;
			else
				return Vector2Int.zero;
		}


		#region Movement

		//protected Vector2 m_v2MoveTarget;
		protected bool m_bMoving;

		//protected void Update()
		//{
		//    UpdateMoving();
		//}

		//[SerializeField, Min(0.00001f)]
		////protected float gridMoveSpeed = 0.04f;
		//protected float gridMoveSpeed = 0.06f;
		// 暂时
		//protected static float gridMoveSpeed = 0.06f;
		//protected static float gridMoveSpeed = 0.16f;
		//protected float gridMoveSpeed = 10f;
		//protected static float gridMoveSpeed = 8f;
		protected static float commonGridMoveSpeed = 8f;

		public virtual float gridMoveSpeed => commonGridMoveSpeed;

		protected void UpdateMoving()
		{
			if (m_bMoving)
			{
				Vector3 pos = transform.position;
				float moveDist = gridMoveSpeed * Time.deltaTime;
				//if (Vector2.Distance(pos, m_v2GridPos) <= gridMoveSpeed)
				if (Vector2.Distance(pos, m_v2GridPos) <= moveDist)
				{
					transform.position = new Vector3(m_v2GridPos.x, m_v2GridPos.y, pos.z);
					m_bMoving = false;
					OnMoveFinish();
				}
				else
				{
					//Vector2 newPos = (Vector2)pos + gridMoveSpeed * (m_v2GridPos - (Vector2)pos).normalized;
					Vector2 newPos = (Vector2)pos + moveDist * (m_v2GridPos - (Vector2)pos).normalized;
					transform.position = new Vector3(newPos.x, newPos.y, pos.z);
				}

				//Debug.Log(transform.position);
			}
		}

		protected virtual void OnMoveFinish()
		{
		}

		//public virtual bool CanMoveByDir(Vector2Int dir)
		public virtual bool CanMoveByDir(CharacterDir dir)
		{
			//return false;
			return true;
		}

		public virtual bool CanMoveByDir(CharacterDir dir, ref List<BaseCharacter> pushedList)
		{
			//return false;
			return true;
		}

		/*protected void GridMoveByPos(Vector2Int dir)
		{
			if (dir != Vector2Int.zero)
			{
				m_v2GridPos = m_v2GridPos + dir;
				m_bMoving = true;
			}
		}*/
		//protected void GridMoveByPos(CharacterDir dir)
		protected virtual void GridMoveByDir(CharacterDir dir)
		{
			/*if (dir == CharacterDir.down)
			{
				m_v2GridPos = m_v2GridPos + Vector2Int.down;
				m_bMoving = true;
			}
			else if (dir == CharacterDir.left)
			{
				m_v2GridPos = m_v2GridPos + Vector2Int.left;
				m_bMoving = true;
			}
			else if (dir == CharacterDir.right)
			{
				m_v2GridPos = m_v2GridPos + Vector2Int.right;
				m_bMoving = true;
			}
			else if (dir == CharacterDir.up)
			{
				m_v2GridPos = m_v2GridPos + Vector2Int.up;
				m_bMoving = true;
			}*/

			if (dir == CharacterDir.down || dir == CharacterDir.left
				|| dir == CharacterDir.right || dir == CharacterDir.up)
			{
				var oldPos = m_v2GridPos;
				m_v2GridPos = oldPos + MakeVectorFromDir(dir);
				m_bMoving = true;

				OnMoveStart(oldPos, m_v2GridPos, dir);

				OnGridPosChanged();
			}
		}

		protected virtual void OnMoveStart(Vector2Int oldPos, Vector2Int newPos, CharacterDir dir)
		{
		}

		//protected void GridMoveByPosIfCan(Vector2Int dir)
		//protected void GridMoveByPosIfCan(CharacterDir dir)
		protected void GridMoveIfCan(CharacterDir dir)
		{
			if (CanMoveByDir(dir))
				//GridMoveByPos(dir);
				GridMoveByDir(dir);
		}

		protected List<BaseCharacter> m_tempCharacterList = new List<BaseCharacter>();

		protected void GridMoveIfCan(CharacterDir dir, bool allowPush)
		{
			if (allowPush)
			{
				m_tempCharacterList.Clear();
				if (CanMoveByDir(dir, ref m_tempCharacterList))
				{
					GridMoveByDir(dir);
					foreach (var each in m_tempCharacterList)
						each.GridMoveByDir(dir);
				}
			}
			else
			{
				GridMoveIfCan(dir);
			}
		}


		/*public void GridMove(int dir)
		{
			if (dir == 2)
				GridMoveByPosIfCan(Vector2Int.down);
			else if (dir == 4)
				GridMoveByPosIfCan(Vector2Int.left);
			else if (dir == 6)
				GridMoveByPosIfCan(Vector2Int.right);
			else if (dir == 8)
				GridMoveByPosIfCan(Vector2Int.up);
		}*/
		/*public void GridMove(CharacterDir dir)
		{
			/*if (dir == CharacterDir.down)
				GridMoveByPosIfCan(Vector2Int.down);
			else if (dir == CharacterDir.left)
				GridMoveByPosIfCan(Vector2Int.left);
			else if (dir == CharacterDir.right)
				GridMoveByPosIfCan(Vector2Int.right);
			else if (dir == CharacterDir.up)
				GridMoveByPosIfCan(Vector2Int.up);* /
			//GridMoveByPosIfCan(dir);
			GridMoveIfCan(dir);
		}*/

		public bool IsMoving()
		{
			return m_bMoving;
		}

		#endregion Movement


		[SerializeField]
		protected bool pushable = false;

		public virtual bool isPushable()
		{
			return pushable;
		}

		[SerializeField]
		protected bool obstructable = false;

		public virtual bool isObstructable()
		{
			return obstructable;
		}

		[SerializeField]
		protected bool passable = false;

		public virtual bool isPassable()
		{
			return passable;
		}
	}
}
