using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Hex : MonoBehaviour
{
	static Mesh sharedMesh;
	
	Vector2 cartPoint;
	Vector2 hexPoint;
	
	void Start()
	{
		if(sharedMesh == null)
			sharedMesh = MakeSharedMesh_();
			
		GetComponent<MeshFilter>().mesh = sharedMesh;
	}
#if false	
	void Update()
	{
		Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		
		cartPoint.x = worldPoint.x;
		cartPoint.y = worldPoint.z;
		
		CartesianPointToHexPoint(ref cartPoint, out hexPoint);
		
		Vector2 newCartPoint, snappedHexPoint = new Vector2(Mathf.Round(hexPoint.x), Mathf.Round(hexPoint.y));
		HexPointToCartesianPoint(ref snappedHexPoint, out newCartPoint);
	}
	
	void OnGUI()
	{
		GUILayout.Label(String.Format("{0:0.00}, {1:0.00}", cartPoint.x, cartPoint.y));
		GUILayout.Label(String.Format("{0:0.00}, {1:0.00}", hexPoint.x, hexPoint.y));
	}
#endif	
	public static void CartesianPointToHexPoint(ref Vector2 pointIn, out Vector2 pointOut)
	{
		// | 0.577350 0.000000 |
		// | 0.288675 0.500000 |
		pointOut = new Vector2(0.577350f * pointIn.x, 0.288675f * pointIn.x + 0.5f * pointIn.y);
	}
	
	public static void HexPointToCartesianPoint(ref Vector2 pointIn, out Vector2 pointOut)
	{
		// | 1.732051 0.000000 |
		// |-1.000000 2.000000 |
		pointOut = new Vector2(1.732051f * pointIn.x, -pointIn.x + 2.0f * pointIn.y);
	}
	
	static Mesh MakeSharedMesh_()
	{
		float s = 1.0f / Mathf.Sin(Mathf.PI / 3.0f);
		float h = 3.0f;
#if false		
		var mesh = new Mesh();
		{
			mesh.vertices = new [] {
				new Vector3(-s, 0, 0),
				new Vector3(-s * 0.5f, 0, 1),
				new Vector3(-s * 0.5f, 0, -1),
				new Vector3(s * 0.5f, 0, 1),
				new Vector3(s * 0.5f, 0, -1),
				new Vector3(s, 0, 0),
				
				new Vector3(-s * 0.5f, 0, -1),
				new Vector3(s * 0.5f, 0, -1),
				new Vector3(-s * 0.5f, -h, -1),
				new Vector3(s * 0.5f, -h, -1),				

				new Vector3(-s * 0.5f, 0, 1),
				new Vector3(-s, 0, 0),
				new Vector3(-s * 0.5f, -h, 1),
				new Vector3(-s, -h, 0),

				new Vector3(-s, 0, 0),
				new Vector3(-s * 0.5f, 0, -1),
				new Vector3(-s, -h, 0),
				new Vector3(-s * 0.5f, -h, -1),
			};
			
			mesh.normals = new [] {
				new Vector3(0, 1, 0),
				new Vector3(0, 1, 0),
				new Vector3(0, 1, 0),
				new Vector3(0, 1, 0),
				new Vector3(0, 1, 0),
				new Vector3(0, 1, 0),

				new Vector3(0, 0,-1),
				new Vector3(0, 0,-1),
				new Vector3(0, 0,-1),
				new Vector3(0, 0,-1),

				new Vector3(-1, 0, s * 0.5f).normalized,
				new Vector3(-1, 0, s * 0.5f).normalized,
				new Vector3(-1, 0, s * 0.5f).normalized,
				new Vector3(-1, 0, s * 0.5f).normalized,

				new Vector3(-1, 0, -s * 0.5f).normalized,
				new Vector3(-1, 0, -s * 0.5f).normalized,
				new Vector3(-1, 0, -s * 0.5f).normalized,
				new Vector3(-1, 0, -s * 0.5f).normalized,
			};
			
			mesh.uv = new [] {
				new Vector2(0, 0),
				new Vector2(0, 0),
				new Vector2(0, 0),
				new Vector2(0, 0),
				new Vector2(0, 0),
				new Vector2(0, 0),

				new Vector2(0, 0),
				new Vector2(0, 0),
				new Vector2(0, 0),
				new Vector2(0, 0),

				new Vector2(0, 0),
				new Vector2(0, 0),
				new Vector2(0, 0),
				new Vector2(0, 0),

				new Vector2(0, 0),
				new Vector2(0, 0),
				new Vector2(0, 0),
				new Vector2(0, 0),
			};
			
			mesh.triangles = new [] {
				0, 1, 2,
				2, 1, 3,
				2, 3, 4,
				4, 3, 5,
				
				6, 7, 8,
				8, 7, 9,
				
				10, 11, 12,
				12, 11, 13,

				14, 15, 16,
				16, 15, 17,
			};
		}

		return mesh;
#endif
		return CylinderBody_();
	}
	
	static Mesh CylinderBody_()
	{
		var positions = new List<Vector3>
		{
			new Vector3(-1, 0, 0),
			new Vector3(-0.5f, 0, Mathf.Sin(Mathf.PI / 3.0f)),
			new Vector3(-0.5f, 0, -Mathf.Sin(Mathf.PI / 3.0f)),
			new Vector3(0.5f, 0, Mathf.Sin(Mathf.PI / 3.0f)),
			new Vector3(0.5f, 0, -Mathf.Sin(Mathf.PI / 3.0f)),
			new Vector3(1, 0, 0),
		};
		
		var normals = new List<Vector3>
		{
			new Vector3(0, 1, 0),
			new Vector3(0, 1, 0),
			new Vector3(0, 1, 0),
			new Vector3(0, 1, 0),
			new Vector3(0, 1, 0),
			new Vector3(0, 1, 0),
		};
		
		var indices = new List<int>
		{
			0, 1, 2,
			2, 1, 3,
			2, 3, 4,
			4, 3, 5,
		};
		
		var indices2 = new List<int>();
		
		CylinderBodySub_(positions, normals, indices, 6, 4.0f, 1.0f, positions.Count, false);
		
		CylinderBodySub_(positions, normals, indices2, 6, 4.0f, (1.0f / Mathf.Sin(Mathf.PI / 3.0f)), positions.Count, true);
		
		Mesh mesh = new Mesh();
		{
			mesh.vertices = positions.ToArray();
			mesh.normals = normals.ToArray();
			
			mesh.subMeshCount = 2;
			mesh.SetTriangles(indices.ToArray(), 0);
			mesh.SetTriangles(indices2.ToArray(), 1);			
		}
		
		return mesh;
	}
	
	static void CylinderBodySub_(List<Vector3> positions, List<Vector3> normals, List<int> indices, int numSides, float depth, float radius, int firstIndex, bool flip)
	{
		for(int i = 0; i < numSides; i++)
		{
			float angleRadians = Mathf.Deg2Rad * 360.0f * i / numSides;
			var normal = new Vector3(Mathf.Cos(angleRadians), 0.0f, Mathf.Sin(angleRadians)) * (flip ? -1.0f : 1.0f);
			
			positions.Add(normal * radius);
			positions.Add(normal * radius + new Vector3(0.0f,-depth, 0.0f));
			
			normals.Add(normal);
			normals.Add(normal);
			
			int ta = 2 * i + firstIndex;
			int tb = ta + 1;
			bool isLastIteration = (i == numSides - 1);
			int tc = (isLastIteration ? firstIndex : ta + 2);
			int td = (isLastIteration ? firstIndex + 1 : ta + 3);
			if(flip)
			{
				indices.Add(tb); indices.Add(tc); indices.Add(ta);
				indices.Add(td); indices.Add(tc); indices.Add(tb);
			}
			else
			{
				indices.Add(ta); indices.Add(tc); indices.Add(tb);
				indices.Add(tb); indices.Add(tc); indices.Add(td);
			}
		}		
	}
}
