using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using GridMovement;

namespace MyGameplay.Signal
{
	//[CreateAssetMenu(fileName = "SignalTile", menuName = "2D/Tiles/Signal Sub Tile")]
	[CreateAssetMenu(fileName = "SignalSubTile", menuName = "Signal Sub Tile")]
	public class SignalSubTile : Tile
	{
		public SignalTileset spriteSet;

		public int signalOffSpriteIndex;
		public int signalOnSpriteIndex;

		public bool upSignalLink = false;
		//public bool downSignalLink = false;
		public bool leftSignalLink = false;
		public bool downSignalLink = false;
		public bool rightSignalLink = false;


		public bool IsLinkToDir(SignalDir dir)
		{
			if (dir == SignalDir.up)
				return upSignalLink;
			else if (dir == SignalDir.down)
				return downSignalLink;
			else if (dir == SignalDir.left)
				return leftSignalLink;
			else if (dir == SignalDir.right)
				return rightSignalLink;
			else
				return false;
		}
		/*public bool IsLinkToDirRev(SignalDir dir)
		{
			if (dir == SignalDir.up)
				//return upSignalLink;
				return downSignalLink;
			else if (dir == SignalDir.down)
				//return downSignalLink;
				return upSignalLink;
			else if (dir == SignalDir.left)
				//return leftSignalLink;
				return rightSignalLink;
			else if (dir == SignalDir.right)
				//return rightSignalLink;
				return leftSignalLink;
			else
				return false;
		}*/

		public IEnumerable<SignalDir> EachLinkDir()
		{
			if (upSignalLink)
				yield return SignalDir.up;
			if (downSignalLink)
				yield return SignalDir.down;
			if (leftSignalLink)
				yield return SignalDir.left;
			if (rightSignalLink)
				yield return SignalDir.right;
		}


		protected SignalState2 GetTileDataSignalState(Vector2Int location)
		{
			if (GridGameMap.Inst)
				return GridGameMap.Inst.GetSignalState(location);
			else
				return null;
		}

		protected bool HasAnySignal(SignalState2 state)
		{
			if (state != null)
			{
				if (upSignalLink && state.upSignalOn)
					return true;
				else if (downSignalLink && state.downSignalOn)
					return true;
				else if (leftSignalLink && state.leftSignalOn)
					return true;
				else if (rightSignalLink && state.rightSignalOn)
					return true;
				else
					return false;
			}
			else
			{
				return false;
			}
		}

		public Sprite GetPatternSprite(Vector2Int pos)
		{
			if (spriteSet)
			{
				if (HasAnySignal(GetTileDataSignalState(pos)))
					return spriteSet.GetSprite(signalOnSpriteIndex);
				else
					return spriteSet.GetSprite(signalOffSpriteIndex);
			}
			else
			{
				return null;
			}
		}

		public override void GetTileData(Vector3Int location, ITilemap tileMap, ref TileData tileData)
		{
			base.GetTileData(location, tileMap, ref tileData);
			tileData.sprite = GetPatternSprite(new Vector2Int(location.x, location.y));
		}
	}
}
