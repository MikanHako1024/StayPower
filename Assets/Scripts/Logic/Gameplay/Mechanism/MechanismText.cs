using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyGameplay.Signal;

namespace MyGameplay.Mechanism
{
	public class MechanismText : BaseSignalMechanism
	{
		[SerializeField]
		protected bool selected = false;

		[SerializeField]
		protected Sprite normalSprite;
		[SerializeField]
		protected Sprite selectedSprite;

		public void SetNoSelect()
		{
			selected = false;
			//m_spriteRenderer.sprite = normalSprite;
			RefreshForSprite();
		}
		public void SetSelected()
		{
			selected = true;
			//m_spriteRenderer.sprite = selectedSprite;
			RefreshForSprite();
		}

		public bool IsSelected()
		{
			return selected;
		}


		[SerializeField]
		protected bool buttonEnabled = true;

		[SerializeField]
		protected Color enabledColor = new Color(1, 1, 1, 1);
		[SerializeField]
		protected Color disabledColor = new Color(1, 1, 1, .5f);

		public void SetButtonEnabled(bool enabled)
		{
			buttonEnabled = enabled;
			RefreshForSprite();
		}

		public bool IsButtonEnabled()
		{
			return buttonEnabled;
		}


		public void RefreshForSprite()
		{
			/*if (buttonEnabled)
			{
				m_spriteRenderer.color = enabledColor;
				if (selected)
					m_spriteRenderer.sprite = selectedSprite;
				else
					m_spriteRenderer.sprite = normalSprite;
			}
			else
			{
				m_spriteRenderer.color = disabledColor;
				m_spriteRenderer.sprite = normalSprite;
			}*/

			if (buttonEnabled)
				m_spriteRenderer.color = enabledColor;
			else
				m_spriteRenderer.color = disabledColor;

			if (selected)
				m_spriteRenderer.sprite = selectedSprite;
			else
				m_spriteRenderer.sprite = normalSprite;
		}
	}
}
