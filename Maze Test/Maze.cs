using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Maze_Test
{
	public class Maze
	{
		public int height, width, start_y, start_x, end_x, end_y, total_x, total_y;

		public char[][] Maze_Array;

		public char[][] Maze_Masque;

		public List<string> mapChars;

		public override string ToString()
		{
			return $"Heigh: {height}, Width: {width}, Start X: {start_x}, Start y:{start_y}, End X: {end_x}, End Y: {end_y}";
		}

		public void DisplaySimpleMap()
		{
			//do not forget to adjust total by -1, we start at 0;
			StringBuilder sbr = new StringBuilder();
			for(int i=0; i <= total_x - 1; i++)
			{
				for(int j=0; j <= total_y - 1; j++)
				{
					sbr.Append(Maze_Array[i][j].ToString());
				}
				//this is unnessacary but just helps for personal clarity.
				Console.WriteLine(sbr.ToString());
				sbr.Clear();
			}
		}

		public void DisplayAdvnacedMap()
		{
			//do not forget to adjust total by -1, we start at 0;
			StringBuilder sbr = new StringBuilder();
			for (int i = 0; i <= total_x - 1; i++)
			{
				for (int j = 0; j <= total_y - 1; j++)
				{
					sbr.Append(Maze_Masque[i][j].ToString());
				}
				//this is unnessacary but just helps for personal clarity.
				Console.WriteLine(sbr.ToString());
				sbr.Clear();
			}
		}

	}

	public class MazeBuilder
	{
		protected Maze Maze = new Maze();

		public MazeMapBuilder MazeMapInitialiser => new MazeMapBuilder(Maze);

		public static implicit operator Maze(MazeBuilder mazeBuilder)
		{
			return mazeBuilder.Maze;
		}
	}

	public class MazeMapBuilder : MazeBuilder
	{
		public MazeMapBuilder(Maze maze)
		{
			this.Maze = maze;
		}

		public MazeMapBuilder MazeDataInit(FileReader fileReader)
		{
			var data = fileReader.data;
			
			//this should be the first element of the list;
			string[] stringSpliter = data[0].Split(' ');
			Maze.width = Int32.Parse(stringSpliter[0].ToString());
			Maze.height = Int32.Parse(stringSpliter[1].ToString());

			stringSpliter = data[1].Split(' ');
			Maze.start_x = Int32.Parse(stringSpliter[0].ToString());
			Maze.start_y = Int32.Parse(stringSpliter[1].ToString());

			stringSpliter = data[2].Split(' ');
			Maze.end_x = Int32.Parse(stringSpliter[0].ToString());
			Maze.end_y = Int32.Parse(stringSpliter[1].ToString());

			var MapValues = new List<string>();

			//need to remove white spcces from the list

			//this also needs to be done in reverse to make it read in correctly.
			//it was easier to do this here, this would make it so that it would be accurate to the map specs.
			for (int i = data.Count -1 ; i >= 3; i--)
			{
				string d = data[i].ToString();
				d = Regex.Replace(d, @"\s+", "");
				MapValues.Add(d);
			}

			Maze.mapChars = MapValues;

			return this;
		}

		public MazeMapBuilder CreateMazeMap()
		{
			var charList = new List<char[]>();

			foreach(var el in Maze.mapChars)
			{

				char[] chars = el.ToCharArray();
				Maze.total_x = chars.Length;
				charList.Add(chars);
			}

			char[][] result = charList.Select(item => item.ToArray()).ToArray();
			Maze.total_y = result.Length;
			Maze.Maze_Array = result;//yipee I did it.
			return this;                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                              
		}

		public MazeMapBuilder CreateLogicalMap()
		{
			List<char[]> charList = new List<char[]>();

			//do not forget to adjust total by -1, we start at 0;
			StringBuilder sbr = new StringBuilder();

			for (int i = 0; i <= Maze.total_y - 1; i++)
			{
				for (int j = 0; j <= Maze.total_x - 1; j++)
				{
					if(Maze.Maze_Array[i][j].ToString() == "1")
					{
						sbr.Append("#");
					}
					else if(Maze.Maze_Array[i][j].ToString() == "0")
					{
						sbr.Append(" ");
					}
				}
				//this is unnessacary but just helps for personal clarity.
				//convert sbr to string.
				string s = sbr.ToString();
				//then convert string to char array
				char[] c = s.ToCharArray();
				charList.Add(c);
				sbr.Clear();
			}
			Maze.Maze_Masque = charList.Select(item => item.ToArray()).ToArray();

			return this;
		}


		public MazeMapBuilder MazeArrayInit(char[][] mapArray)
		{
			Maze.Maze_Array = mapArray;

			return this;
		}

		public MazeMapBuilder MazeMasqueInit(char[][] masqueArray)
		{
			Maze.Maze_Masque = masqueArray;

			return this;
		}


	}


}
