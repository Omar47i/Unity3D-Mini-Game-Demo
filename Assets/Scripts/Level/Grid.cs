using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour 
{
	[SerializeField]
	GameObject nodePrefab, turretPrefab;        // Map prefabs to be instantiated at the start of the game

	GameData gameData;                          // A class representing the parsed Json file
    float nodeRadius = .5f;
    float nodeDiameter;

	void Awake()
	{
        nodeDiameter = Mathf.RoundToInt(nodeRadius * 2f);

		DataParser.DataParsed.AddListener(OnDataParsed);
	}

	public void OnDataParsed(GameData data)
	{
		gameData = data;

		// Remove the listener once received the message to avoid memory leaks
		DataParser.DataParsed.RemoveListener (OnDataParsed);

		// Create the grid and turrets based on file values.
		CreateGrid();
        CreateTurrets();
    }

	void CreateGrid()
	{
		float mapSize = gameData.MapSize;
		Vector3 worldBottomLeft = transform.position - Vector3.right * mapSize / 2f - Vector3.forward * mapSize / 2f;

        // Create the map as a grid of N size
        for (int x = 0; x < mapSize; x++)
            for (int y = 0; y < mapSize; y++)
            {
                Vector3 worldPoint = worldBottomLeft + Vector3.right * (x * nodeDiameter + nodeRadius) + Vector3.forward * (y * nodeDiameter + nodeRadius);
                Instantiate(nodePrefab, worldPoint, Quaternion.identity, transform);
            }

        // Create a box collider for the grid nodes (one box collider is better than multiple for each node)
        GameObject mapCollider = new GameObject("Map Collider");
        mapCollider.AddComponent<BoxCollider>().size = new Vector3(mapSize, nodeDiameter, mapSize); ;
        mapCollider.transform.SetParent(transform, false);
    }

    void CreateTurrets()
    {
        int turretsSize = gameData.TurretsNumber;
        GameObject turretsParent = new GameObject("Turrets");

        // Create turrets read from file with their position (Position are grid based like a two dimentional array)
        for (int i = 0; i < turretsSize; i++)
        {
            float xWorldPos = gameData.Turrets[i].X * nodeDiameter - (gameData.MapSize / 2f - nodeRadius);
            float zWorldPos = gameData.Turrets[i].Y * nodeDiameter - (gameData.MapSize / 2f - nodeRadius);
            Vector3 worldPoint = new Vector3(xWorldPos, 1.5f, zWorldPos);

            GameObject turret = Instantiate(turretPrefab, worldPoint, Quaternion.identity, turretsParent.transform);

            // Initialize each turret with its projectile speed and coverage area
            turret.GetComponent<Turret>().InitializeTurret(gameData.Turrets[i].ProjectileSpeed, gameData.Turrets[i].CoverageArea);
        }
    }
}
