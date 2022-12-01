using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyGameplay.Signal
{
	[CreateAssetMenu(fileName = "SignalTile", menuName = "2D/Tiles/Signal Tileset")]
	public class SignalTileset : ScriptableObject
	{
		public List<Sprite> spriteList = new List<Sprite>();

		public Sprite GetSprite(int index)
		{
			return index < spriteList.Count ? spriteList[index] : null;
		}
	}
}
