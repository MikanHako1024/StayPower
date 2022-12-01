using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyGameplay.UI;
//using MyGameplay.Signal;
using MyGameplay.Power;
using MyGameplay.Logic;
//using GridMovement;

#if false
//#if !UNITY_EDITOR
using System.Runtime.InteropServices;
using System;
#endif

/// <summary>
/// GameMainController
/// 主控制
/// 
/// 需要在 Edit - Project Setting - Script Execution Order 将 GameMainController 的优先级 设为 小于0
/// </summary>
//public class GameMainController : BaseManagerMono<GameMainController>
public class GameMainController : MonoBehaviour
{
	#region Instance

	protected static GameMainController instance;

	public static GameMainController Inst => GetInstance();

	protected static GameMainController GetInstance()
	{
		if (instance)
			return instance;
		else
		{
			var go = GameObject.Find("GameMainController");
			if (go)
				instance = go.GetComponent<GameMainController>();
			return instance;
		}
	}

	protected void RegisterInstance(GameMainController obj)
	{
		instance = obj;
	}

	protected void RegisterThisInst()
	{
		RegisterInstance(this);
	}

	#endregion Instance


	[SerializeField]
	public UIWheelDisc uiWheelDisc;
	[SerializeField]
	public UICover uiCover;

	protected void Awake()
	{
		//EventSystem.Inst.InitManager();
		//GlobalData.Inst.InitManager();
		//MonoPoolManager.Inst.InitManager();
		//ResManager.Inst.InitManager();
		//SceneManager.Inst.InitManager();
		//UISystem.UIManager.Inst.InitManager();
		//InputManager.Inst.InitManager();
		//Logger.InitLogger();
		StorageManager.Inst.InitManager();
		AudioManager.Inst.InitManager();

		//PowerSupplyPool.Inst.OnRegisterToMain();
		//uiWheelDisc.RefreshAllNumber();

		InitScreenDisplay();
	}

	[SerializeField]
	protected int windowWidth = 1920;
	[SerializeField]
	protected int windowHeight = 1080;
	[SerializeField]
	protected FullScreenMode fullScreenMode = FullScreenMode.FullScreenWindow;

	//[SerializeField]
	//protected int windowWidth2 = 960;
	//[SerializeField]
	//protected int windowHeight2 = 540;

	protected void InitScreenDisplay()
	{
#if !UNITY_WEBGL
		//Screen.SetResolution(1200, 720, FullScreenMode.Windowed);
		//Screen.SetResolution(1200, 720, FullScreenMode.FullScreenWindow);
		//Screen.SetResolution(1920, 1080, FullScreenMode.FullScreenWindow);
		//Screen.SetResolution(600, 360, FullScreenMode.FullScreenWindow);
		//Screen.SetResolution(windowWidth, windowHeight, FullScreenMode.FullScreenWindow);
		Screen.SetResolution(windowWidth, windowHeight, fullScreenMode);
		//Screen.SetResolution(windowWidth/2, windowHeight/2, fullScreenMode);

		//Debug.Log(Camera.main.pixelRect);
		//Debug.Log(Camera.main.scaledPixelWidth);
		//Debug.Log(Camera.main.scaledPixelHeight);
		//Debug.Log(Camera.main.pixelWidth);
		//Debug.Log(Camera.main.pixelHeight);

		//var pCamera = Camera.main.GetComponent<UnityEngine.U2D.PixelPerfectCamera>();
		//pCamera.refResolutionX = windowWidth / 2;
		//pCamera.refResolutionY = windowHeight / 2;

		List<DisplayInfo> list = new List<DisplayInfo>(2);
		Screen.GetDisplayLayout(list);
		//foreach (var each in list)
		//	Debug.Log(list);

		// ？Screen.mainWindowDisplayInfo ...

		if (list.Count > 0)
		{
			/*var info = list[0];
			var pos = new Vector2Int(
				Mathf.RoundToInt((info.width - windowWidth) / 2),
				Mathf.RoundToInt((info.height - windowHeight) / 2));
			Screen.MoveMainWindowTo(info, pos);*/

			var info = list[0];
			/*if (true)
			{
				var pCamera = Camera.main.GetComponent<UnityEngine.U2D.PixelPerfectCamera>();
				pCamera.refResolutionX = windowWidth * 2;
				pCamera.refResolutionY = windowHeight * 2;
				pCamera.assetsPPU = 64 * 2;

				var rectTf = uiWheelDisc.GetComponent<RectTransform>();
				rectTf.localScale = new Vector2(1, 1);

				Screen.SetResolution(windowWidth * 2, windowHeight * 2, fullScreenMode);
				var pos = new Vector2Int(
					Mathf.RoundToInt((info.workArea.width - windowWidth * 2) / 2),
					Mathf.RoundToInt((info.workArea.height - windowHeight * 2) / 2));
				Screen.MoveMainWindowTo(info, pos);
			}
			//if (windowWidth <= info.width && windowHeight <= info.height)
			else */
			if (windowWidth <= info.workArea.width && windowHeight <= info.workArea.height)
			//if (false)
			{
				var pCamera = Camera.main.GetComponent<UnityEngine.U2D.PixelPerfectCamera>();
				pCamera.refResolutionX = windowWidth;
				pCamera.refResolutionY = windowHeight;
				pCamera.assetsPPU = 64;

				var rectTf = uiWheelDisc.GetComponent<RectTransform>();
				rectTf.localScale = new Vector2(1, 1);

				Screen.SetResolution(windowWidth, windowHeight, fullScreenMode);
				var pos = new Vector2Int(
					Mathf.RoundToInt((info.workArea.width - windowWidth) / 2),
					Mathf.RoundToInt((info.workArea.height - windowHeight) / 2));
				Screen.MoveMainWindowTo(info, pos);
			}
			else
			{
				var pCamera = Camera.main.GetComponent<UnityEngine.U2D.PixelPerfectCamera>();
				pCamera.refResolutionX = windowWidth / 2;
				pCamera.refResolutionY = windowHeight / 2;
				pCamera.assetsPPU = 64 / 2;

				var rectTf = uiWheelDisc.GetComponent<RectTransform>();
				//var size = rectTf.sizeDelta;
				//rectTf.sizeDelta = new Vector2(size.x / 2, size.y / 2);
				rectTf.localScale = new Vector2(.5f, .5f);

				Screen.SetResolution(windowWidth / 2, windowHeight / 2, fullScreenMode);
				var pos = new Vector2Int(
					Mathf.RoundToInt((info.workArea.width - windowWidth / 2) / 2),
					Mathf.RoundToInt((info.workArea.height - windowHeight / 2) / 2));
				Screen.MoveMainWindowTo(info, pos);
			}
		}
		else
		{
			/*var text = "";

			string arg = System.Environment.GetEnvironmentVariable("half");
			string[] args = System.Environment.GetCommandLineArgs();
			text = "args : ";
			text += arg + "\n";
			foreach (var each in args)
				text += each + "\n";

			//text += list[0].workArea.ToString() + "\n";
			text = Screen.mainWindowDisplayInfo + "\n";
			text = Screen.mainWindowDisplayInfo.width + "\n";
			text = Screen.mainWindowDisplayInfo.height + "\n";

			GameObject.Find("TestText").GetComponent<UnityEngine.UI.Text>().text = text;*/

			var pCamera = Camera.main.GetComponent<UnityEngine.U2D.PixelPerfectCamera>();
			pCamera.refResolutionX = windowWidth;
			pCamera.refResolutionY = windowHeight;
			pCamera.assetsPPU = 64;

			var rectTf = uiWheelDisc.GetComponent<RectTransform>();
			rectTf.localScale = new Vector2(1, 1);

			Screen.SetResolution(windowWidth, windowHeight, fullScreenMode);
			//var pos = new Vector2Int(
			//	Mathf.RoundToInt((info.workArea.width - windowWidth) / 2),
			//	Mathf.RoundToInt((info.workArea.height - windowHeight) / 2));
			//Screen.MoveMainWindowTo(info, pos);
		}


		/*string text = "";

		/*string arg = System.Environment.GetEnvironmentVariable("half");
		string[] args = System.Environment.GetCommandLineArgs();
		text = "args : ";
		text += arg + "\n";
		foreach (var each in args)
			text += each + "\n";* /

		//text += list[0].workArea.ToString() + "\n";
		text = list.Count.ToString();

		GameObject.Find("TestText").GetComponent<UnityEngine.UI.Text>().text = text;*/

#else

		/*var pCamera = Camera.main.GetComponent<UnityEngine.U2D.PixelPerfectCamera>();
		pCamera.refResolutionX = windowWidth / 2;
		pCamera.refResolutionY = windowHeight / 2;
		pCamera.assetsPPU = 64 / 2;

		var rectTf = uiWheelDisc.GetComponent<RectTransform>();
		//var size = rectTf.sizeDelta;
		//rectTf.sizeDelta = new Vector2(size.x / 2, size.y / 2);
		rectTf.localScale = new Vector2(.5f, .5f);

		Screen.SetResolution(windowWidth / 2, windowHeight / 2, fullScreenMode);*/
#endif


#if false
//#if !UNITY_EDITOR
		StartCoroutine("Setposition");
		//Setposition();
#endif
	}

	protected void Start()
	{
		//InitScreenDisplay();

		PowerSupplyPool.Inst.OnRegisterToMain();

		uiWheelDisc.RefreshAllNumber();
		uiWheelDisc.gameObject.SetActive(false);

		InvokeGameStartEvent();

		SetGameLogic(LogicType.Title);
	}

#if false
//#if !UNITY_EDITOR
	[DllImport("user32.dll")]
	static extern IntPtr SetWindowLong(IntPtr hwnd, int _nIndex, int dwNewLong);
	[DllImport("user32.dll")]
	static extern bool SetWindowPos(IntPtr hWnd, int hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);
	[DllImport("user32.dll")]
	static extern IntPtr GetForegroundWindow();

	const uint SWP_SHOWWINDOW = 0x0040;

	IEnumerator Setposition()
	{
		yield return new WaitForSeconds(0.1f); // 不知道为什么发布于行后，设置位置的不会生效，我延迟0.1秒就可以

		int width = Screen.width;
		int height = Screen.height;
		int x = Mathf.RoundToInt((Screen.currentResolution.width - width) / 2);
		int y = Mathf.RoundToInt((Screen.currentResolution.height - height) / 2);
		bool result = SetWindowPos(GetForegroundWindow(), 0, x, y, width, height, SWP_SHOWWINDOW); // 设置屏幕大小和位置
	}
#endif


	[SerializeField]
	protected TitleLogic titleLogic;
	[SerializeField]
	protected LevelLogic levelLogic;

	public enum LogicType
	{
		None,
		Title,
		Level,
	}


	protected LogicType currentLogicType = LogicType.None;
	protected GameLogicCommon currentLogic;

	protected void ActivateCurrnetLogic()
	{
		if (currentLogic)
			currentLogic.OnLogicActiveOn();
	}

	protected void DeactivateCurrnetLogic()
	{
		if (currentLogic)
			currentLogic.OnLogicActiveOff();
	}


	protected GameLogicCommon GetGameLogic(LogicType lType)
	{
		if (lType == LogicType.None)
			return null;
		else if (lType == LogicType.Title)
			return titleLogic;
		else if (lType == LogicType.Level)
			return levelLogic;
		else
			return null;
	}

	public void SetGameLogic(LogicType lType)
	{
		if (currentLogicType == lType)
			return;

		DeactivateCurrnetLogic();
		currentLogicType = lType;
		currentLogic = GetGameLogic(currentLogicType);
		ActivateCurrnetLogic();
	}
	public void SetGameLogic(LogicType lType, int levelId)
	{
		if (currentLogicType != lType)
		{
			//SetNextLevelId(levelId);

			DeactivateCurrnetLogic();
			currentLogicType = lType;
			currentLogic = GetGameLogic(currentLogicType);
			ActivateCurrnetLogic();

			//levelLogic.LoadNextGameLevel(levelId);
			//levelLogic.SetNextGameLevel(levelId);
			levelLogic.SetNextGameLevelForce(levelId);
		}
		else
		{
			// 已经是 levelLogic
			// 需要更换关卡

			//SetNextLevelId(levelId);
			//levelLogic.LoadNextGameLevel(levelId);
			levelLogic.SetNextGameLevel(levelId);
		}
	}

	public void SetLevelLogicLevelId(int levelId)
	{
		if (currentLogicType == LogicType.Level)
			SetGameLogic(LogicType.Level, levelId);
	}

	public void RestartCurrentLevel()
	{
		if (currentLogicType == LogicType.Level)
			levelLogic.RestartGameLevel();
	}


	//public void SetNextLevelId(int levelId)
	//{
	//	if (levelLogic)
	//		levelLogic.SetNextLevelId(levelId);
	//}


	public bool IsCurrentLogicScene(LogicType lType)
	{
		return currentLogicType == lType;
	}

	public T GetCurrentLogicScene<T>(LogicType lType) where T : GameLogicCommon
	{
		if (IsCurrentLogicScene(lType))
		{
			//var result = GetGameLogic(lType);
			//if (GetGameLogic(lType) == result)
			//	return result as T;
			//else
			//	return null;
			// ？忘记是何意了...

			return GetGameLogic(lType) as T;
		}
		else
			return null;
	}


	public TitleLogic GetCurrentTitleLogic()
	{
		return GetCurrentLogicScene<TitleLogic>(LogicType.Title);
	}
	public LevelLogic GetCurrentLevelLogic()
	{
		return GetCurrentLogicScene<LevelLogic>(LogicType.Level);
	}


	[SerializeField]
	public UnityEngine.Events.UnityEvent onGameStartEvent;

	protected void InvokeGameStartEvent()
	{
		onGameStartEvent.Invoke();
	}


	public void BackToTitle()
	{
		if (currentLogicType == LogicType.Level)
			SetGameLogic(LogicType.Title);
		else
		{
		}
	}

	//[SerializeField, Multiline(5)]
	//protected string thanksText;

	public void BackToTitleWithEnd()
	{
		if (currentLogicType == LogicType.Level)
		{
			SetGameLogic(LogicType.Title);
			//if (thanksText != "")
			//	uiCover.SetCoverText(thanksText);
			//uiCover.SetCoverText(thanksText);
			uiCover.SetCoverText(titleLogic.thanksText);
			//uiCover.StartCover();
			uiCover.StartCoverForThanks();
		}
		else
		{
		}
	}
}
