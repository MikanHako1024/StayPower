using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MyGameplay.UI
{
	[RequireComponent(typeof(RectTransform))]
	public class UICover : MonoBehaviour
	{
		[SerializeField]
		//protected RectTransform mainPanel;
		protected CanvasGroup mainPanel;

		[SerializeField]
		protected Image coverImage;

		[SerializeField]
		protected Text coverText;


		public void SetCoverText(string text)
		{
			coverText.text = text;
		}
		//public void SetCoverText(string text, string titleName)
		//{
		//	if (titleName != "")
		//		coverText.text = text + "\n" + titleName;
		//	else
		//		coverText.text = text;
		//}
		// £¿levelTitleName µÄÂß¼­ ½»¸ø LevelLogic ...


		protected void Awake()
		{
			mainPanel.alpha = 0;
			SetCoverText("");
			//waitRemainTime = 0f;
			//fadeRemainTime = 0f;

			fadeOutRemainTime = 0f;
			waitRemainTime = 0f;
			fadeInRemainTime = 0f;
		}


		//[SerializeField, Min(0)]
		//protected float waitTime = 1f;
		//[SerializeField, Min(0)]
		//protected float fadeTime = 1f;
		//
		//protected float waitRemainTime = 0f;
		//protected float fadeRemainTime = 0f;

		[SerializeField, Min(0)]
		//protected float fadeOutTime = 1f;
		protected float fadeOutTime = 0f; // TODO
		[SerializeField, Min(0)]
		protected float waitTime = 1f;
		[SerializeField, Min(0)]
		protected float fadeInTime = 1f;

		protected float fadeOutRemainTime = 0f;
		protected float waitRemainTime = 0f;
		protected float fadeInRemainTime = 0f;

		public void StartCover()
		{
			//waitRemainTime = waitTime;
			//fadeRemainTime = fadeTime;
			fadeOutRemainTime = fadeOutTime;
			waitRemainTime = waitTime;
			fadeInRemainTime = fadeInTime;
			mainPanel.alpha = 1;
		}

		[SerializeField, Min(0)]
		protected float waitForThanksTime = 3f;

		public void StartCoverForThanks()
		{
			fadeOutRemainTime = fadeOutTime;
			//waitRemainTime = waitTime;
			waitRemainTime = waitForThanksTime;
			fadeInRemainTime = fadeInTime;
			mainPanel.alpha = 1;
		}

		public bool IsAnimating()
		{
			//return waitRemainTime > 0 || fadeRemainTime > 0;
			return fadeOutRemainTime > 0 || waitRemainTime > 0 || fadeInRemainTime > 0;
		}


		/*protected void Update()
		{
			float time = UpdateWaitAnim(Time.deltaTime);
			UpdateFadeAnim(time);
		}

		protected float UpdateWaitAnim(float time)
		{
			if (time <= 0)
			{
				return 0;
			}
			else if (waitRemainTime > time)
			{
				waitRemainTime -= time;
				return 0;
			}
			else if (waitRemainTime > 0)
			{
				float res = time - waitRemainTime;
				waitRemainTime = 0;
				return res;
			}
			else
			{
				return time;
			}
		}

		protected float UpdateFadeAnim(float time)
		{
			if (time <= 0)
			{
				return 0;
			}
			else if (fadeRemainTime > time)
			{
				fadeRemainTime -= time;
				mainPanel.alpha = fadeRemainTime / fadeTime;
				return 0;
			}
			else if (fadeRemainTime > 0)
			{
				float res = time - fadeRemainTime;
				fadeRemainTime = 0;
				mainPanel.alpha = 0;
				return res;
			}
			else
			{
				return time;
			}
		}*/

		protected void Update()
		{
			float time = Time.deltaTime;
			time = UpdateFadeOutAnim(time);
			time = UpdateWaitAnim(time);
			time = UpdateFadeInAnim(time);
			if (time > 0)
			{
			}
		}

		protected float UpdateFadeOutAnim(float time)
		{
			if (time <= 0)
			{
				return 0;
			}
			else if (fadeOutRemainTime > time)
			{
				fadeOutRemainTime -= time;
				//mainPanel.alpha = 1 - fadeOutRemainTime / fadeOutTime;
				RefreshFadeOut();
				return 0;
			}
			else if (fadeOutRemainTime > 0)
			{
				float res = time - fadeOutRemainTime;
				fadeOutRemainTime = 0;
				//mainPanel.alpha = 1;
				RefreshFadeOut();
				return res;
			}
			else
			{
				return time;
			}
		}

		protected float UpdateWaitAnim(float time)
		{
			if (time <= 0)
			{
				return 0;
			}
			else if (waitRemainTime > time)
			{
				waitRemainTime -= time;
				return 0;
			}
			else if (waitRemainTime > 0)
			{
				float res = time - waitRemainTime;
				waitRemainTime = 0;
				return res;
			}
			else
			{
				return time;
			}
		}

		protected float UpdateFadeInAnim(float time)
		{
			if (time <= 0)
			{
				return 0;
			}
			else if (fadeInRemainTime > time)
			{
				fadeInRemainTime -= time;
				mainPanel.alpha = fadeInRemainTime / fadeInTime;
				RefreshFadeIn();
				return 0;
			}
			else if (fadeInRemainTime > 0)
			{
				float res = time - fadeInRemainTime;
				fadeInRemainTime = 0;
				mainPanel.alpha = 0;
				RefreshFadeIn();
				return res;
			}
			else
			{
				return time;
			}
		}
		

		protected void RefreshFadeOut()
		{
			if (fadeOutRemainTime > 0)
				mainPanel.alpha = 1 - fadeOutRemainTime / fadeOutTime;
			else
				mainPanel.alpha = 1;
		}

		//protected void RefreshWait()
		//{
		//}
		protected void RefreshFadeIn()
		{
			if (fadeInRemainTime > 0)
				mainPanel.alpha = fadeInRemainTime / fadeInTime;
			else
				mainPanel.alpha = 0;
		}
	}
}
