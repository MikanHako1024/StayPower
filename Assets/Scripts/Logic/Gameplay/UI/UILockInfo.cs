using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MyGameplay.Signal;
using MyGameplay.Mechanism;

namespace MyGameplay.UI
{
	[RequireComponent(typeof(RectTransform))]
	public class UILockInfo : MonoBehaviour
	{
		[SerializeField]
		protected Image upButtonImage;
		[SerializeField]
		protected Image downButtonImage;
		[SerializeField]
		protected Image leftButtonImage;
		[SerializeField]
		protected Image rightButtonImage;

		//[SerializeField]
		//protected Image keyImage;

		[SerializeField]
		protected Text upNumberText;
		[SerializeField]
		protected Text downNumberText;
		[SerializeField]
		protected Text leftNumberText;
		[SerializeField]
		protected Text rightNumberText;

		//[SerializeField]
		//protected Color zeroNumberColor = new Color(1, 1, 1, 0);
		[SerializeField]
		protected Color normalNumberColor = new Color(1, 1, 1, 1);
		[SerializeField]
		protected Color okNumberColor = new Color(.2f, 1, 0, 1);
		[SerializeField]
		protected Color overNumberColor = new Color(1, 0, 0, 1);


		[SerializeField]
		protected MechanismLock target;

		public void SetTarget(MechanismLock target)
		{
			if (target)
			{
				gameObject.SetActive(true);
				this.target = target;
				RefreshTarget();
				RefreshAllNumber();
				ResetPosition();
			}
			else
			{
				gameObject.SetActive(false);
			}
		}


		protected void RefreshTarget()
		{
			SetDemandNumber(upButtonImage, upNumberText, target ? target.upPowerDemand : 0);
			SetDemandNumber(downButtonImage, downNumberText, target ? target.downPowerDemand : 0);
			SetDemandNumber(leftButtonImage, leftNumberText, target ? target.leftPowerDemand : 0);
			SetDemandNumber(rightButtonImage, rightNumberText, target ? target.rightPowerDemand : 0);
		}

		protected void SetDemandNumber(Image uiImage, Text uiText, int value)
		{
			if (value > 0)
			{
				uiImage.gameObject.SetActive(true);
				uiText.gameObject.SetActive(true);
				uiText.text = "x" + value;
			}
			else
			{
				uiImage.gameObject.SetActive(false);
				uiText.gameObject.SetActive(false);
			}
		}


		public void RefreshAllNumber()
		{
			if (target)
			{
				var powerCount = SignalLinkGroup.Inst.GetGroupPowerCount(target.GridPos);
				RefreshNumber(upButtonImage, target.upPowerDemand, powerCount.up);
				RefreshNumber(downButtonImage, target.downPowerDemand, powerCount.down);
				RefreshNumber(leftButtonImage, target.leftPowerDemand, powerCount.left);
				RefreshNumber(rightButtonImage, target.rightPowerDemand, powerCount.right);
			}
		}

		public void RefreshNumber(Image uiImage, int value, int power)
		{
			// ？不需要信号但是有信号时 也要显示 ...
			if (value <= 0 && power <= 0)
				uiImage.gameObject.SetActive(false);
			else
				uiImage.gameObject.SetActive(true);

			if (power < value)
				uiImage.color = normalNumberColor;
			else if (power > value)
				uiImage.color = overNumberColor;
			else
				uiImage.color = okNumberColor;
		}


		public void ResetPosition()
		{
			if (!target)
				return;

			transform.position = Camera.main.WorldToScreenPoint(target.transform.position);
			//transform.position = target.transform.position;
		}
	}
}
