using System;
using System.Collections.Generic;

namespace PairedGame
{
	abstract class AssetManager
	{
		protected static Dictionary<string, object> resourceMap;
		
		public static void AddAsset(string key, object asset)
		{
			resourceMap.Add(key, asset);
		}
		
		public static void RemoveAsset(string key)
		{
			resourceMap[key].Dispose();
			resourceMap.Remove(key);
		}
		
		public static bool IsAssetLoaded(string key)
		{
			return resourceMap.ContainsKey(key);	
		}
	}
}

