using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SoundInput;

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
		startingHeights = myTerrain.terrainData.GetHeights(0,0,myTerrain.terrainData.heightmapWidth,myTerrain.terrainData.heightmapHeight);
	}

	void Update()
	{
		
	}

	private void OnCollisionEnter(Collision collision)
	{
		//print("collided with: " + collision.gameObject.name);
<<<<<<< HEAD
		RaiseTerrain(collision.transform.position, collision.transform.localScale, collision.gameObject.GetComponent<TerraformRing_Script>().pitch);
=======
		RaiseTerrain(collision.transform.position, collision.transform.localScale*5, collision.gameObject.GetComponent<TerraformRing_Script>().pitch);
>>>>>>> Niki

		Destroy(collision.gameObject);
	}

	private void RaiseTerrain(Vector3 point, Vector3 size, float heightIncrease)
	{
		int terX =(int)((point.x / myTerrain.terrainData.size.x) * xResolution);
		int sizeX = (int)((size.x / myTerrain.terrainData.size.x) * xResolution);
        int terZ =(int)((point.z / myTerrain.terrainData.size.z) * zResolution);
		int sizeZ = (int)((size.z / myTerrain.terrainData.size.z) * zResolution);
        float[,] height = myTerrain.terrainData.GetHeights(terX - (sizeX/2),terZ - (sizeZ/2),sizeX,sizeZ);  //new float[1,1];
		//print(size.x + " / " + myTerrain.terrainData.size.x + " * " + xResolution + " = " + sizeX);
		//print("Raising Terrain! " + terX + " " + sizeX + " " + terZ + " " + sizeZ);
        for(int tempY = 0; tempY < sizeZ; tempY++)
            for(int tempX = 0; tempX < sizeX; tempX++)
            {
                float dist_to_target = Mathf.Abs((float)tempY - ((float)sizeZ/2.0f)) + Mathf.Abs ((float)tempX - ((float)sizeX/2.0f));
                float maxDist=sizeX;
				
                float proportion = dist_to_target / maxDist;
				//print(dist_to_target + " -> " + proportion);
 
                height[tempX,tempY] += 0.005f * heightIncrease * (1.0f - proportion) * Random.Range(0.01f, 1.0f);
                //heights[terX - (sizeX/2) + tempX,terZ - (sizeZ/2) + tempY] += 0.005f * (1f - proportion);
            }
 
        myTerrain.terrainData.SetHeights(terX - (sizeX/2), terZ - (sizeX/2), height);
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
                //heights[terX - (sizeX/2) + tempX,terZ - (sizeZ/2) + tempY] -= 0.005f * (1f - proportion);
            }
 
        myTerrain.terrainData.SetHeights(terX - (sizeX/2), terZ - (sizeX/2), height);
	}

<<<<<<< HEAD
	private void OnApplicationQuit()
=======
	private void OnDisable()
>>>>>>> Niki
	{
		myTerrain.terrainData.SetHeights(0,0,startingHeights);
	}

}
