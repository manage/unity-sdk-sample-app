using UnityEngine;
using System.Collections;

public class ShowAdButtonScript : MonoBehaviour {

	public GameObject adUnit;
	
	void OnClick()
	{
		if (adUnit != null) adUnit.SendMessage("PresentAd");
	}
}
