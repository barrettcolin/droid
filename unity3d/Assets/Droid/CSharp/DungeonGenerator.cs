using UnityEngine;

namespace Droid
{
	public class DungeonGenerator
	{
		public static int[,] Generate(int seed, int rows, int cols)
		{
			Random.seed = seed;
			
			var output = new int[rows, cols];
			for(int r = 0; r < rows; r++)
				for(int c = 0; c < cols; c++)
					output[r, c] = (Random.value > 0.5f) ? 1 : 0;
			
			return FourFive(FourFive(FourFive(output)));
		}
		
		static int[,] FourFive(int[,] input)
		{
			var rows = input.GetLength(0);
			var cols = input.GetLength(1);
			var output = new int[rows, cols];
			for(int r = 0; r < rows; r++)
			{
				for(int c = 0; c < cols; c++)
				{
					int numSolid =
						GetSolid(input, r - 1, c - 1) +
						GetSolid(input, r - 1, c) +
						GetSolid(input, r - 1, c + 1) +
						GetSolid(input, r, c - 1) +
						GetSolid(input, r, c) +
						GetSolid(input, r, c + 1) +
						GetSolid(input, r + 1, c - 1) +
						GetSolid(input, r + 1, c) +
						GetSolid(input, r + 1, c + 1);
					
					if(numSolid >= 5)
						output[r, c] = 1;
				}
			}
			
			return output;
		}
		
		static int GetSolid(int[,] input, int row, int col)
		{
			var rows = input.GetLength(0);
			var cols = input.GetLength(1);
			if(row < 0 || col < 0 || row >= rows || col >= cols)
				return 1;
			
			return input[row, col];
		}
	}
}
