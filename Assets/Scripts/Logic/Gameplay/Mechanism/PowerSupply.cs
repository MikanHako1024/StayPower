using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GridMovement;
using MyGameplay.Signal;

namespace MyGameplay.Power
{
	//public class PowerSupply : PushableCharacter
	//public class PowerSupply : BaseCharacter
	public class PowerSupply : CharacterCommon
	{
		public SignalPowerType powerType;

		/*public void SetPowerType(SignalPowerType pType)
		{
			var sprite = GetComponent<SpriteRenderer>();
			if (sprite)
				sprite.sprite = 
			...
		}*/


		public bool HasSocketTile(Vector2Int pos)
		{
			return GridGameMap.Inst.IsSocketTile(pos);
		}
		
		//public SignalLinkGroup signalLinkGroup { get => SignalManager.Inst.signalLinkGroup; }

		//protected void OnPlacePower()
		public void OnPlacePower()
		{
			//signalLinkGroup.AddGroupPower(m_v2GridPos, powerType);
			//signalLinkGroup.AddGroupPower(m_v2GridPos, powerType, true);
			if (HasSocketTile(m_v2GridPos))
			{
				//signalLinkGroup.AddGroupPower(m_v2GridPos, powerType);
				SignalLinkGroup.Inst.AddGroupPower(m_v2GridPos, powerType);
				SetSocketState(true);
			}
			else
			{
				SetSocketState(false);
			}
		}

		//protected void OnPickPower()
		public void OnPickPower()
		{
			//signalLinkGroup.DecGroupPower(m_v2GridPos, powerType);
			//signalLinkGroup.DecGroupPower(m_v2GridPos, powerType, true);
			if (HasSocketTile(m_v2GridPos))
			{
				//signalLinkGroup.DecGroupPower(m_v2GridPos, powerType);
				SignalLinkGroup.Inst.DecGroupPower(m_v2GridPos, powerType);
				SetSocketState(true);
			}
			else
			{
				SetSocketState(false);
			}
		}


		protected override void OnMoveStart(Vector2Int oldPos, Vector2Int newPos, CharacterDir dir)
		{
			base.OnMoveStart(oldPos, newPos, dir);

			//signalLinkGroup.DecGroupPower(oldPos, powerType);
			//signalLinkGroup.AddGroupPower(newPos, powerType);
			SignalLinkGroup.Inst.DecGroupPower(oldPos, powerType);
			SignalLinkGroup.Inst.AddGroupPower(newPos, powerType);
			//signalLinkGroup.DecGroupPower(oldPos, powerType, false);
			//signalLinkGroup.DecGroupPower(oldPos, powerType, true);
			//signalLinkGroup.AddGroupPower(newPos, powerType, true);
		}

		//protected override void Awake()
		//{
		//	base.Awake();
		//	//OnPlacePower();
		//}


		//public bool IsLocatedSocketTile()
		public bool HasSocketTileHere()
		{
			return HasSocketTile(m_v2GridPos);
		}

		//[SerializeField, Range(0, 1)]
		//protected float socketNoActiveAlpha = .75f;
		[SerializeField]
		//protected Color socketNoActiveColor = new Color(1, 1, 1, .75f);
		protected Color socketNoActiveColor = new Color(1, 1, 1, 1);

		[SerializeField]
		protected Color socketActiveColor = new Color(.2f, 1, 0, 1);

		//[SerializeField]
		//protected Color socketNormalColor = new Color(1, 1, 1, 1);

		/*public void RefreshForLocatedSocketTile()
		{
			var sprite = GetComponent<SpriteRenderer>();
			if (sprite)
			{
				var color = sprite.color;
				color.a = IsLocatedSocketTile() ? 1f : socketNoActiveAlpha;
				sprite.color = color;
			}
		}*/

		//public void SetLocatedSocketState(bool isLocated)
		public void SetSocketState(bool hasHere)
		{
			var sprite = GetComponent<SpriteRenderer>();
			if (sprite)
			{
				//var color = sprite.color;
				////color.a = isLocated ? 1f : socketNoActiveAlpha;
				//color.a = hasHere ? 1f : socketNoActiveAlpha;
				//sprite.color = color;
				sprite.color = hasHere ? socketActiveColor : socketNoActiveColor;
			}
		}

		//public void RefreshForLocatedSocketTile()
		public void RefreshForSocketTileState()
		{
			SetSocketState(HasSocketTileHere());
		}


		protected virtual void Awake()
		{
			RefreshForSocketTileState();
		}


		public override void OnRegisterCharacter()
		{
			base.OnRegisterCharacter();
			OnPlacePower();
		}

		public override void OnDeregisterCharacter()
		{
			base.OnDeregisterCharacter();
			OnPickPower();
		}


		// 标记 是通过对象池创建的 还是原本设置在CharacterContainer的
		[SerializeField]
		public bool isPoolItem = false;


		#region DebugFunctions

		[ContextMenu("DebugOnPlacePower")]
		public void DebugOnPlacePower()
		{
			OnPlacePower();
		}

		[ContextMenu("DebugOnPickPower")]
		public void DebugOnPickPower()
		{
			OnPickPower();
		}

		#endregion DebugFunctions
	}
}
