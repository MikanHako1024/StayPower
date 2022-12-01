using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyGameplay.Signal;

namespace MyGameplay.Power
{
	// MonoPoolManager
	public class PowerSupplyPool : MonoBehaviour
	{
		#region Instance

		protected static PowerSupplyPool instance;

		public static PowerSupplyPool Inst => GetInstance();

		protected static PowerSupplyPool GetInstance()
		{
			if (instance)
				return instance;
			else
			{
				var go = GameObject.Find("PowerSupplyPool");
				if (go)
					instance = go.GetComponent<PowerSupplyPool>();
				return instance;
			}
		}

		protected void RegisterInstance(PowerSupplyPool obj)
		{
			instance = obj;
		}

		protected void RegisterThisInst()
		{
			RegisterInstance(this);
		}

		#endregion Instance


		/*protected void Awake()
		{
			RegisterThisInst();
			InitPoolContainer();
			InitPoolItems();
		}*/

		public void OnRegisterToMain()
		{
			RegisterThisInst();
			InitPoolContainer();
			InitPoolItems();
		}


		protected Transform poolContainerTf;

		protected void InitPoolContainer()
		{
			var go = new GameObject("PoolContainer");
			go.transform.SetParent(transform);
			go.SetActive(false);
			poolContainerTf = go.transform;
		}


		[SerializeField]
		public GameObject powerSupplyPrefab;

		public PowerSupply CreatePowerSupply()
		{
			//var go = new GameObject("PowerSupply");
			//go.transform.SetParent(poolContainerTf);
			//return go.AddComponent<PowerSupply>();

			// ？改 从GameObject创建 为 用预设体创建 ...

			var go = Instantiate(powerSupplyPrefab, poolContainerTf);
			return go.GetComponent<PowerSupply>();
		}

		[SerializeField]
		protected int poolSize = 20;

		protected void InitPoolItems()
		{
			for (int i = 0; i < poolSize; i++)
				CreatePowerSupply();
		}


		public Sprite upSprite;
		public Sprite downSprite;
		public Sprite leftSprite;
		public Sprite rightSprite;

		public void SetPowerSupplyType(PowerSupply character, SignalPowerType pType)
		{
			character.powerType = pType;
			var sprite = character.GetComponent<SpriteRenderer>();
			if (sprite)
			{
				if (pType == SignalPowerType.up)
					sprite.sprite = upSprite;
				else if (pType == SignalPowerType.down)
					sprite.sprite = downSprite;
				else if (pType == SignalPowerType.left)
					sprite.sprite = leftSprite;
				else if (pType == SignalPowerType.right)
					sprite.sprite = rightSprite;
			}
		}


		public PowerSupply TouchIdleItem()
		{
			if (poolContainerTf.childCount > 0)
				return poolContainerTf.GetChild(0).GetComponent<PowerSupply>();
			else
				return CreatePowerSupply();
		}

		public PowerSupply GetPowerSupply(SignalPowerType pType, Vector2Int pos)
		{
			var item = TouchIdleItem();
			SetPowerSupplyType(item, pType);
			item.SetGridPosNonAnim(pos);
			item.transform.SetParent(null);
			return item;
		}

		public void ReleasePowerSupply(PowerSupply item)
		{
			item.transform.SetParent(poolContainerTf);
		}
	}
}
