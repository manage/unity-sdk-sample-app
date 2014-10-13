using UnityEngine;
using System.Collections;

public class LoadAdButtonScript : MonoBehaviour {

	public GameObject adUnit;
	
	void OnClick()
	{
		if (adUnit != null) adUnit.SendMessage("Load");
	}
}
