using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyGameplay.Mechanism;

namespace MyGameplay.UI
{
	public class UILockInfoPool : MonoBehaviour
	{
		#region Instance

		protected static UILockInfoPool instance;

		public static UILockInfoPool Inst => GetInstance();

		protected static UILockInfoPool GetInstance()
		{
			if (instance)
				return instance;
			else
			{
				var go = GameObject.Find("UILockInfoPool");
				if (go)
					instance = go.GetComponent<UILockInfoPool>();
				return instance;
			}
		}

		protected void RegisterInstance(UILockInfoPool obj)
		{
			instance = obj;
		}

		protected void RegisterThisInst()
		{
			RegisterInstance(this);
		}

		#endregion Instance


		[SerializeField]
		protected List<UILockInfo> itemList = new List<UILockInfo>();

		public void ShowAllInfo(IEnumerable<MechanismLock> eachLock)
		{
			int i = 0;
			int l = itemList.Count;
			foreach (var each in eachLock)
			{
				if (i >= l)
					break;
				itemList[i++].SetTarget(each);
			}
		}

		public void HideAllInfo()
		{
			foreach (var each in itemList)
			{
				each.SetTarget(null);
			}
		}
	}
}
