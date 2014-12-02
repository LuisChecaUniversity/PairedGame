using System;

namespace PairedGame
{
	public static class TextureManager: AssetManager
	{
		override public static void RemoveAsset(string key)
		{
			resourceMap[key].Dispose();
			base.RemoveAsset(key);
		}
	}
}

