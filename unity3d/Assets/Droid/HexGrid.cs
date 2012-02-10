using UnityEngine;
using System.Collections;

public class HexGrid : MonoBehaviour
{
	public Hex hex;
	
	void Start()
	{
		int rows = 32;
		int cols = 64;
		var solid = Droid.DungeonGenerator.Generate((int)System.DateTime.Now.Ticks, rows, cols);
		
		for(int r = 0; r < rows; r++)
		{
			for(int c = 0; c < cols; c++)
			{
				Vector2 hpos = new Vector2(c, r), pos;
				Hex.HexPointToCartesianPoint(ref hpos, out pos);
				
				if(solid[r, c] == 0)
					Instantiate(hex, new Vector3(pos.x, 0, pos.y), Quaternion.identity);
			}
		}	
	}
}
