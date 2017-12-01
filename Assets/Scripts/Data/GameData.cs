using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class GameData
{
    public int MapSize;
	public int TurretsNumber;
	public List<TurretsData> Turrets;
}

[System.Serializable]
public class TurretsData
{
	public int X;
	public int Y;
    public int ProjectileSpeed;
    public int CoverageArea;
}