using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class GameData
{
    public int MapSize;
	public int TurretsNumber;
	public List<TurretLocation> TurretsLocations;
}

[System.Serializable]
public class TurretLocation
{
	public int X;
	public int Y;
}