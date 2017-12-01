using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class GameData
{
    public string MapSize;
	public string TurretsNumber;
	public List<TurretLocation> TurretsLocations;
}

[System.Serializable]
public class TurretLocation
{
	public string X;
	public string Y;
}