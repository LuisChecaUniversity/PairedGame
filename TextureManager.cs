using System;
using Sce.PlayStation.HighLevel.GameEngine2D.Base;

namespace PairedGame
{
	public class TextureManager: AssetManager<TextureInfo>
	{
		new public static void RemoveAsset(string key)
		{
			if(!IsAssetLoaded(key))
				return;
			resourceMap[key].Dispose();
			resourceMap.Remove(key);
		}
	}
}

