using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour 
{
	[SerializeField]
	GameObject nodePrefab;

	void Awake()
	{
		DataParser.DataParsed.AddListener(OnDataParsed);
	}

	public void OnDataParsed(GameData data)
	{
		
	}
}
