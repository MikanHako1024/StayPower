using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using GridMovement;

namespace MyGameplay.Signal
{
	/*[CreateAssetMenu(fileName = "SignalTile", menuName = "2D/Tiles/Signal Tile")]
	//[CreateAssetMenu(fileName = "SignalTile", menuName = "Signal Tile")]
	public class SignalTile : Tile
	{
		//public string signalFlag = "";

		//public bool useSpriteIndex = true;
		// ？通过 有无 spriteSet 判断 即可 ...

		//public Sprite[] spriteList = new Sprite[] { };
		public SignalTileset spriteSet;

		public string signalFlag = "";
		public bool signal2Mode = false;

		//public Sprite signalOffSprite;
		//public Sprite signalOnSprite;
		// ？强制使用索引 ..
		public int signalOffSpriteIndex;
		public int signalOnSpriteIndex;

		public bool signalLinkU = false;
		public bool signalLinkL = false;
		public bool signalLinkD = false;
		public bool signalLinkR = false;

		//public Sprite signal2OffSprite;
		//public Sprite signal2OnSprite;
		//public int signal2OffSpriteIndex;
		//public int signal2OnSpriteIndex;
		//public Sprite signalOffOnSprite;
		//public Sprite signalOnOnSprite;
		public int signalOffOnSpriteIndex;
		public int signalOnOnSpriteIndex;

		public bool signal2LinkU = false;
		public bool signal2LinkL = false;
		public bool signal2LinkD = false;
		public bool signal2LinkR = false;

		// ？调用 SignalTile 的方法 改变其图案 ...
		// ？而非 SignalTile 自己实现更新图案的逻辑 ...
		// ？因为 作为 ScriptableObject 不适合调用游戏中的逻辑 ...

		// signal1 : 连接 上方 的线路
		//protected bool m_bSignalOn = false;
		//protected bool m_bSignal2On = false;

		// ？具体某坐标的数据 不能储存在 瓦片的数据 里 ...

		protected SignalState GetTileDataSignalState(Vector2Int location)
		{
			if (GridGameMap.Inst)
				return GridGameMap.Inst.GetSignalState(location);
			else
				return null;
		}

		/*public void SetPattern(bool isOn)
		{
			m_bSignalOn = isOn;
			//RefreshPatternSprite();
		}

		public void SetPattern2(bool isOn)
		{
			m_bSignal2On = isOn;
			//RefreshPatternSprite();
		}

		public void SetPattern2(bool isOn, bool isOn2)
		{
			m_bSignalOn = isOn;
			m_bSignal2On = isOn2;
			//RefreshPatternSprite();
		}

		// ？需要通过 tilemap 进行刷新 同时需要重写 GetTileData ...
		*/


		/*public void RefreshPatternSprite()
		{
			if (spriteSet)
			{
				if (signal2Mode && m_bSignal2On)
				{
					sprite = m_bSignalOn
						? spriteSet.GetSprite(signalOnOnSpriteIndex)
						: spriteSet.GetSprite(signalOffOnSpriteIndex);
				}
				else
				{
					sprite = m_bSignalOn
						? spriteSet.GetSprite(signalOnSpriteIndex)
						: spriteSet.GetSprite(signalOffSpriteIndex);
				}
			}
			else
			{
				sprite = null;
			}
		}* /

		//public Sprite GetPatternSprite()
		public Sprite GetPatternSprite(Vector2Int pos)
		{
			if (spriteSet)
			{
				var state = GetTileDataSignalState(pos);
				bool signalOn = state != null ? state.signalOn : false;
				bool signal2On = state != null ? state.signal2On : false;

				//if (signal2Mode && m_bSignal2On)
				if (signal2Mode && signal2On)
				{
					//return m_bSignalOn
					return signalOn
						? spriteSet.GetSprite(signalOnOnSpriteIndex)
						: spriteSet.GetSprite(signalOffOnSpriteIndex);
				}
				else
				{
					//return m_bSignalOn
					return signalOn
						? spriteSet.GetSprite(signalOnSpriteIndex)
						: spriteSet.GetSprite(signalOffSpriteIndex);
				}
			}
			else
			{
				return null;
			}
		}

		public override void GetTileData(Vector3Int location, ITilemap tileMap, ref TileData tileData)
		{
			base.GetTileData(location, tileMap, ref tileData);
			//tileData.sprite = GetPatternSprite();
			tileData.sprite = GetPatternSprite(new Vector2Int(location.x, location.y));
		}
	}*/
}
