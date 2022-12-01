using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MyGameplay.Signal;

namespace MyGameplay.UI
{
	[RequireComponent(typeof(RectTransform))]
	public class UIWheelDisc : MonoBehaviour
	{
		[SerializeField]
		protected Image upButtonImage;
		[SerializeField]
		protected Image downButtonImage;
		[SerializeField]
		protected Image leftButtonImage;
		[SerializeField]
		protected Image rightButtonImage;

		[SerializeField]
		protected Image zButtonImage;
		[SerializeField]
		protected Image xButtonImage;

		[SerializeField]
		protected Text upNumberText;
		[SerializeField]
		protected Text downNumberText;
		[SerializeField]
		protected Text leftNumberText;
		[SerializeField]
		protected Text rightNumberText;

		//[SerializeField, Range(0, 1)]
		//protected float zeroNumberAlpha = .75f;
		[SerializeField]
		protected Color zeroNumberColor = new Color(1, 1, 1, .75f);

		[SerializeField]
		protected Color existsNumberColor = new Color(1, 1, 1, 1);

		[SerializeField]
		protected Color zeroButPowerColor = new Color(.2f, 1, 0, 1);


		protected int upNumber = 1;
		protected int downNumber = 1;
		protected int leftNumber = 1;
		protected int rightNumber = 1;

		protected bool upPower = false;
		protected bool downPower = false;
		protected bool leftPower = false;
		protected bool rightPower = false;

		//public void RefreshNumber(Image uiImage, Text uiText, int value)
		//public void RefreshNumber(Image uiImage, Text uiText, int value, bool power = false)
		public void RefreshNumber(Image uiImage, Text uiText, int value, bool power)
		{
			if (value > 0)
			{
				//var color = uiImage.color;
				//color.a = 1f;
				//uiImage.color = color;
				uiImage.color = existsNumberColor;

				uiText.text = "x" + value;
			}
			/*else
			{
				//var color = uiImage.color;
				//color.a = zeroNumberAlpha;
				//uiImage.color = color;
				uiImage.color = zeroNumberColor;

				uiText.text = "x0";
			}*/
			else if (power)
			{
				uiImage.color = zeroButPowerColor;
				uiText.text = "x0";
			}
			else
			{
				uiImage.color = zeroNumberColor;
				uiText.text = "x0";
			}
		}


		#region Refresh ByDir

		public void RefreshUpNumber()
		{
			//RefreshNumber(upButtonImage, upNumberText, upNumber);
			RefreshNumber(upButtonImage, upNumberText, upNumber, upPower);
		}
		public void RefreshUpNumber(int value)
		{
			upNumber = value;
			RefreshUpNumber();
		}
		public void RefreshUpNumber(bool power)
		{
			upPower = power;
			RefreshUpNumber();
		}

		public void RefreshDownNumber()
		{
			//RefreshNumber(downButtonImage, downNumberText, downNumber);
			RefreshNumber(downButtonImage, downNumberText, downNumber, downPower);
		}
		public void RefreshDownNumber(int value)
		{
			downNumber = value;
			RefreshDownNumber();
		}
		public void RefreshDownNumber(bool power)
		{
			downPower = power;
			RefreshDownNumber();
		}

		public void RefreshLeftNumber()
		{
			//RefreshNumber(leftButtonImage, leftNumberText, leftNumber);
			RefreshNumber(leftButtonImage, leftNumberText, leftNumber, leftPower);
		}
		public void RefreshLeftNumber(int value)
		{
			leftNumber = value;
			RefreshLeftNumber();
		}
		public void RefreshLeftNumber(bool power)
		{
			leftPower = power;
			RefreshLeftNumber();
		}

		public void RefreshRightNumber()
		{
			//RefreshNumber(rightButtonImage, rightNumberText, rightNumber);
			RefreshNumber(rightButtonImage, rightNumberText, rightNumber, rightPower);
		}
		public void RefreshRightNumber(int value)
		{
			rightNumber = value;
			RefreshRightNumber();
		}
		public void RefreshRightNumber(bool power)
		{
			rightPower = power;
			RefreshRightNumber();
		}

		#endregion Refresh ByDir


		#region SetNumber

		public void RefreshAllNumber()
		{
			RefreshUpNumber();
			RefreshDownNumber();
			RefreshLeftNumber();
			RefreshRightNumber();
		}

		public void SetAllNumber(int up, int down, int left, int right)
		{
			//SetNumber(SignalDir.up, up);
			//SetNumber(SignalDir.down, down);
			//SetNumber(SignalDir.left, left);
			//SetNumber(SignalDir.right, right);
			RefreshUpNumber(up);
			RefreshDownNumber(down);
			RefreshLeftNumber(left);
			RefreshRightNumber(right);
		}


		/*public void SetNumber(SignalDir dir, int value)
		{
			if (dir == SignalDir.up)
				RefreshUpNumber(value);
			else if (dir == SignalDir.down)
				RefreshDownNumber(value);
			else if (dir == SignalDir.left)
				RefreshLeftNumber(value);
			else if (dir == SignalDir.right)
				RefreshRightNumber(value);
		}*/
		public void SetNumber(SignalPowerType pType, int value)
		{
			if (pType == SignalPowerType.up)
				RefreshUpNumber(value);
			else if (pType == SignalPowerType.down)
				RefreshDownNumber(value);
			else if (pType == SignalPowerType.left)
				RefreshLeftNumber(value);
			else if (pType == SignalPowerType.right)
				RefreshRightNumber(value);
		}

		/*public void AddNumber(SignalDir dir)
		{
			int delta = 1;
			if (dir == SignalDir.up)
				RefreshUpNumber(upNumber + delta);
			else if (dir == SignalDir.down)
				RefreshDownNumber(downNumber + delta);
			else if (dir == SignalDir.left)
				RefreshLeftNumber(leftNumber + delta);
			else if (dir == SignalDir.right)
				RefreshRightNumber(rightNumber + delta);
		}*/
		public void AddNumber(SignalPowerType pType)
		{
			int delta = 1;
			if (pType == SignalPowerType.up)
				RefreshUpNumber(upNumber + delta);
			else if (pType == SignalPowerType.down)
				RefreshDownNumber(downNumber + delta);
			else if (pType == SignalPowerType.left)
				RefreshLeftNumber(leftNumber + delta);
			else if (pType == SignalPowerType.right)
				RefreshRightNumber(rightNumber + delta);
		}

		/*public void DecNumber(SignalDir dir)
		{
			int delta = -1;
			if (dir == SignalDir.up)
				RefreshUpNumber(upNumber + delta);
			else if (dir == SignalDir.down)
				RefreshDownNumber(downNumber + delta);
			else if (dir == SignalDir.left)
				RefreshLeftNumber(leftNumber + delta);
			else if (dir == SignalDir.right)
				RefreshRightNumber(rightNumber + delta);
		}*/
		public void DecNumber(SignalPowerType pType)
		{
			int delta = -1;
			if (pType == SignalPowerType.up)
				RefreshUpNumber(upNumber + delta);
			else if (pType == SignalPowerType.down)
				RefreshDownNumber(downNumber + delta);
			else if (pType == SignalPowerType.left)
				RefreshLeftNumber(leftNumber + delta);
			else if (pType == SignalPowerType.right)
				RefreshRightNumber(rightNumber + delta);
		}

		/*public void RefreshNumber(SignalDir dir)
		{
			if (dir == SignalDir.up)
				RefreshUpNumber();
			else if (dir == SignalDir.down)
				RefreshDownNumber();
			else if (dir == SignalDir.left)
				RefreshLeftNumber();
			else if (dir == SignalDir.right)
				RefreshRightNumber();
		}*/
		public void RefreshNumber(SignalPowerType pType)
		{
			if (pType == SignalPowerType.up)
				RefreshUpNumber();
			else if (pType == SignalPowerType.down)
				RefreshDownNumber();
			else if (pType == SignalPowerType.left)
				RefreshLeftNumber();
			else if (pType == SignalPowerType.right)
				RefreshRightNumber();
		}

		#endregion SetNumber


		#region SetPower

		public void SetPower(SignalPowerType pType, bool power)
		{
			if (pType == SignalPowerType.up)
				RefreshUpNumber(power);
			else if (pType == SignalPowerType.down)
				RefreshDownNumber(power);
			else if (pType == SignalPowerType.left)
				RefreshLeftNumber(power);
			else if (pType == SignalPowerType.right)
				RefreshRightNumber(power);
		}
		public void SetPower(SignalPowerType pType, int powerCount)
		{
			SetPower(pType, powerCount > 0);
		}
		public void SetPower(SignalPowerCount powerCount)
		{
			SetPower(SignalPowerType.up, powerCount != null ? powerCount.up : 0);
			SetPower(SignalPowerType.down, powerCount != null ? powerCount.down : 0);
			SetPower(SignalPowerType.left, powerCount != null ? powerCount.left : 0);
			SetPower(SignalPowerType.right, powerCount != null ? powerCount.right : 0);
		}

		#endregion SetPower


		//protected void Awake()
		//{
		//	RefreshAllNumber();
		//}


		//public void SetZButtonEnabled(bool enabled)
		//{
		//}

		public void SetZButtonEnabledOn()
		{
			zButtonImage.enabled = true;
		}

		public void SetZButtonEnabledOff()
		{
			zButtonImage.enabled = false;
		}


		public void SetXButtonEnabledOn()
		{
			xButtonImage.enabled = true;
		}

		public void SetXButtonEnabledOff()
		{
			xButtonImage.enabled = false;
		}
	}
}
