using UnityEngine;
using System;
using System.Collections;

public class Hex : MonoBehaviour
{
	Vector2 cartPoint;
	Vector2 hexPoint;
	
	void Start()
	{
		float s = 1.0f / Mathf.Sin(Mathf.PI / 3.0f);
		
		var mesh = new Mesh();
		{
			mesh.vertices = new [] {
				new Vector3(0, 0, 0),
				new Vector3(s, 0, 0),
				new Vector3(s * 0.5f, 0, -1),
				new Vector3(-s * 0.5f, 0, -1),
				new Vector3(-s, 0, 0),
				new Vector3(-s * 0.5f, 0, 1),
				new Vector3(s * 0.5f, 0, 1),
			};
			
			mesh.triangles = new [] {
				0, 6, 1,
				0, 1, 2,
				0, 2, 3,
				0, 3, 4,
				0, 4, 5,
				0, 5, 6,
			};
		}
		
		GetComponent<MeshFilter>().mesh = mesh;
	}
	
	void Update()
	{
		Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		
		cartPoint.x = worldPoint.x;
		cartPoint.y = worldPoint.z;
		
		CartesianPointToHexPoint(ref cartPoint, out hexPoint);
		
		Vector2 newCartPoint, snappedHexPoint = new Vector2(Mathf.Round(hexPoint.x), Mathf.Round(hexPoint.y));
		HexPointToCartesianPoint(ref snappedHexPoint, out newCartPoint);

		if(Input.GetMouseButton(0))
			transform.position = new Vector3(newCartPoint.x, 0.0f, newCartPoint.y);
	}
	
	void OnGUI()
	{
		GUILayout.Label(String.Format("{0:0.00}, {1:0.00}", cartPoint.x, cartPoint.y));
		GUILayout.Label(String.Format("{0:0.00}, {1:0.00}", hexPoint.x, hexPoint.y));
	}
	
	static void CartesianPointToHexPoint(ref Vector2 pointIn, out Vector2 pointOut)
	{
		// | 0.577350 0.000000 |
		// |-0.288675 0.500000 |
		pointOut = new Vector2(0.577350f * pointIn.x, -0.288675f * pointIn.x + 0.5f * pointIn.y);
	}
	
	static void HexPointToCartesianPoint(ref Vector2 pointIn, out Vector2 pointOut)
	{
		// | 1.732051 0.000000 |
		// | 1.000000 2.000000 |
		pointOut = new Vector2(1.732051f * pointIn.x, pointIn.x + 2.0f * pointIn.y);
	}
}
