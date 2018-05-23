using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainDeformer : MonoBehaviour {

	public Terrain myTerrain;
	int xResolution;
	int zResolution;
	float[,] heights;
	float[,] startingHeights;

	void Start()
	{
		xResolution = myTerrain.terrainData.heightmapWidth;
		zResolution = myTerrain.terrainData.heightmapHeight;
		//print(xResolution + "--" + zResolution);
		heights = myTerrain.terrainData.GetHeights(0,0,xResolution,zResolution);
		startingHeights = heights;
	}

	void Update()
	{
		/*if(Input.GetMouseButton(0))
		{
			RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			if(Physics.Raycast(ray, out hit))
			{
				RaiseTerrain(hit.point);
			}
		}
		if(Input.GetMouseButton(1))
		{
			RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			if(Physics.Raycast(ray, out hit))
			{
				LowerTerrain(hit.point);
			}
		}*/
	}

	private void RaiseTerrain(Vector3 point)
	{
		int terX =(int)((point.x / myTerrain.terrainData.size.x) * xResolution);
		//print(point.x + " -> " + terX);
        int terZ =(int)((point.z / myTerrain.terrainData.size.z) * zResolution);
        float[,] height = myTerrain.terrainData.GetHeights(terX - 4,terZ - 4,9,9);  //new float[1,1];
 
        for(int tempY = 0; tempY < 9; tempY++)
            for(int tempX = 0; tempX < 9; tempX++)
            {
                float dist_to_target = Mathf.Abs((float)tempY - 4f) + Mathf.Abs ((float)tempX - 4f);
                float maxDist = 8f;
                float proportion = dist_to_target / maxDist;
 
                height[tempX,tempY] += 0.01f * (1f - proportion);
                heights[terX - 4 + tempX,terZ - 4 + tempY] += 0.01f * (1f - proportion);
            }
 
        myTerrain.terrainData.SetHeights(terX - 4, terZ - 4, height);
	}

	public void LowerTerrain(Vector3 point, Vector3 size)
	{
		int terX =(int)((point.x / myTerrain.terrainData.size.x) * xResolution);
		int sizeX = (int)((size.x / myTerrain.terrainData.size.x) * xResolution);
        int terZ =(int)((point.z / myTerrain.terrainData.size.z) * zResolution);
		int sizeZ = (int)((size.z / myTerrain.terrainData.size.z) * zResolution);
        float[,] height = myTerrain.terrainData.GetHeights(terX - (sizeX/2),terZ - (sizeZ/2),sizeX,sizeZ);  //new float[1,1];
 
        for(int tempY = 0; tempY < sizeZ; tempY++)
            for(int tempX = 0; tempX < sizeX; tempX++)
            {
                float dist_to_target = Mathf.Abs((float)tempY - (sizeZ/2)) + Mathf.Abs ((float)tempX - (sizeX/2));
                float maxDist = sizeX;
                float proportion = dist_to_target / maxDist;
 
                height[tempX,tempY] -= 0.005f * (1f - proportion);
                heights[terX - (sizeX/2) + tempX,terZ - (sizeZ/2) + tempY] -= 0.005f * (1f - proportion);
            }
 
        myTerrain.terrainData.SetHeights(terX - (sizeX/2), terZ - (sizeX/2), height);
	}

	private void OnApplicationQuit()
	{
		myTerrain.terrainData.SetHeights(0,0,startingHeights);
	}

}
