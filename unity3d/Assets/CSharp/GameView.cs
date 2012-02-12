using UnityEngine;
using System.Collections;

public class GameView : MonoBehaviour
{
	const int Rows = 5;
	const int Cols = 5;
	
	public Hex tile;
	
	void Awake()
	{
		MakeTile_(2, 0, 0);
		MakeTile_(3, 0, 2);

		MakeTile_(1, 1, 2);
		MakeTile_(2, 1, 2);
		MakeTile_(3, 1, 0);
		MakeTile_(4, 1, 2);

		MakeTile_(0, 2, 2);
		MakeTile_(1, 2, 0);
		MakeTile_(2, 2, 0);
		MakeTile_(3, 2, 2);
		MakeTile_(4, 2, 2);

		MakeTile_(0, 3, 0);
		MakeTile_(1, 3, 0);
		MakeTile_(2, 3, 0);
		MakeTile_(3, 3, 0);

		MakeTile_(1, 4, 2);
		MakeTile_(2, 4, 2);
	}
	
	Hex MakeTile_(int r, int c, int h)
	{
		Vector2 posIn = new Vector2(r, c), posOut;
		Hex.HexPointToCartesianPoint(ref posIn, out posOut);
		
		var tileInst = (Hex)Instantiate(tile, new Vector3(posOut.x, h * 0.375f, posOut.y), Quaternion.identity);
		tileInst.transform.parent = transform;
		
		return tileInst;
	}
}
