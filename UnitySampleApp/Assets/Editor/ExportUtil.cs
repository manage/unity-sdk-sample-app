using UnityEngine;
using System.Collections;
using UnityEditor;

public class ExportUtil  {
	[MenuItem("Assets/Export Package")]
	public static void ExportPlugin(){
		string path = "Assets/Plugins";
		AssetDatabase.ExportPackage (path, "Plugin.unitypackage", ExportPackageOptions.Default);

	}
}
