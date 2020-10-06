using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Maze_Test
{
	interface IMazeSolver
	{
		void MazeLogicSolver(Maze maze);
		void displayMaze(char[][] map, int y, int x);
	}


	public class MazeSolver : IMazeSolver
	{
		void IMazeSolver.MazeLogicSolver(Maze maze)
		{
			char[][] map = maze.Maze_Masque;
			int start_x = maze.start_x;
			int start_y = maze.start_y;
			int current_x = maze.start_x;
			int current_y = maze.start_y;
			int dest_x = maze.end_x;
			int dest_y = maze.end_y;

			map[current_y][current_x] = 's';
			map[dest_y][dest_x] = 'e';

			bool solved = false;
			while(!solved)
			{
				for (int i = start_y; i <= maze.total_y - 1; i++)
				{
					for (int j = start_x; j <= maze.total_x - 1; j++)
					{
						if (map[current_y][current_x] == 'e')
						{
							solved = true;
							Console.WriteLine("-------------------------------");
							displayMaze(map, maze.total_y, maze.total_x);
							Console.WriteLine("------------SOLVED-------------");
							return;
						}
						else
						{
							map[current_y][current_x] = 'x';
						}
						//test cardinal position
						if (map[i][j - 1] != '#')
						{
							current_y = i;
							current_x = j - 1;							
						}
						else if (map[i][j + 1] != '#')
						{
							current_y = i;
							current_x = j;
						}
						else if (map[i - 1][j] != '#')
						{
							current_y = i - 1;
							current_x = j;
						}
						else if (map[i + 1][j] != '#')
						{
							current_y = i + 1;							
							current_x = j;
						}
					}
				}
			}		
		}
		public void displayMaze(char[][] map,int  y, int x)
		{
			//do not forget to adjust total by -1, we start at 0;
			StringBuilder sbr = new StringBuilder();
			for (int i = 0; i <= y - 1; i++)
			{
				for (int j = 0; j <= x - 1; j++)
				{
					sbr.Append(map[i][j].ToString());
				}
				//this is unnessacary but just helps for personal clarity.
				Console.WriteLine(sbr.ToString());
				sbr.Clear();
			}
			
		}

	}
}
