using UnityEngine;
using System.Collections;

public class HexGrid : MonoBehaviour
{
	public Hex hex;
	
	void Start()
	{
		int side = 10;
		for(int r = 0; r < side; r++)
		{
			for(int c = 0; c < side; c++)
			{
				Vector2 hpos = new Vector2(c, r), pos;
				Hex.HexPointToCartesianPoint(ref hpos, out pos);
				
				Instantiate(hex, new Vector3(pos.x, 0, pos.y), Quaternion.identity);
			}
		}	
	}
}
