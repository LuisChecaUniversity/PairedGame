using System;
using System.Collections.Generic;

namespace PairedGame
{
	abstract static class AssetManager
	{
		protected static Dictionary<string, object> resourceMap;
		
		public static void AddAsset(string key, object asset)
		{
			if (!IsAssetLoaded(key)) resourceMap.Add(key, asset);
		}
		
		public static void RemoveAsset(string key)
		{
			resourceMap.Remove(key);
		}
		
		public static bool IsAssetLoaded(string key)
		{
			return resourceMap.ContainsKey(key);	
		}
	}
}

