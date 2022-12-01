using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyGameplay.Signal
{
	//public struct SignalPowerCountSt
	public class SignalPowerCount
	{
		public int down;
		public int left;
		public int right;
		public int up;

		//public static SignalPowerCount empty = new SignalPowerCount();

		public SignalPowerCount()
		{
			down = 0;
			left = 0;
			right = 0;
			up = 0;
		}

		public SignalPowerCount(int down, int left, int right, int up)
		{
			this.down = down;
			this.left = left;
			this.right = right;
			this.up = up;
		}

		public override string ToString()
		{
			return string.Format("down:{0} left:{1} right:{2} up:{3}", down, left, right, up);
		}

		public void AddCount(SignalPowerType pType)
		{
			if (pType == SignalPowerType.down)
				down++;
			else if (pType == SignalPowerType.left)
				left++;
			else if (pType == SignalPowerType.right)
				right++;
			else if (pType == SignalPowerType.up)
				up++;
		}

		public void DecCount(SignalPowerType pType)
		{
			if (pType == SignalPowerType.down)
				down--;
			else if (pType == SignalPowerType.left)
				left--;
			else if (pType == SignalPowerType.right)
				right--;
			else if (pType == SignalPowerType.up)
				up--;
		}

		public void Clear()
		{
			down = 0;
			left = 0;
			right = 0;
			up = 0;
		}

		public bool HasAnyCount()
		{
			return down > 0|| left > 0 || right > 0 || up > 0;
		}

		public static SignalPowerCount zero = new SignalPowerCount(0, 0, 0, 0);

		public static SignalPowerCount operator +(SignalPowerCount a, SignalPowerCount b)
		{
			if (a == null)
				return b;
			if (b == null)
				return a;

			// ？需要创建一个新的对象 ...
			// ？可以不需要 ...

			var res = new SignalPowerCount();
			res.down = a.down + b.down;
			res.left = a.left + b.left;
			res.right = a.right + b.right;
			res.up = a.up + b.up;
			//res.down = (a != null ? a.down : 0) + (b != null ? b.down : 0);
			//res.left = (a != null ? a.left : 0) + (b != null ? b.down : 0);
			//res.right = (a != null ? a.right : 0) + (b != null ? b.right : 0);
			//res.up = (a != null ? a.up : 0) + (b != null ? b.up : 0);
			return res;
		}
	}
}
