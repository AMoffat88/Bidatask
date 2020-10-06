using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maze_Test
{
	public class FileReader
	{
		public List<string> data;

		public char[][] CharArray;

		public FileStream FS;
	}

	public class FileReaderBuilder
	{
		protected FileReader FileReader = new FileReader();

		public FileDataReaderBuilder DataReader => new FileDataReaderBuilder(FileReader);

		public FileDataOutPutter DataOutPutter => new FileDataOutPutter(FileReader);

		public static implicit operator FileReader(FileReaderBuilder fileReaderBuilder)
		{
			return fileReaderBuilder.FileReader;
		}
	}


	public class FileDataReaderBuilder : FileReaderBuilder
	{
		public FileDataReaderBuilder(FileReader fileReader)
		{
			this.FileReader = fileReader;
			this.FileReader.data = new List<string>();
		}

		public FileDataReaderBuilder ReadData(string fileName)
		{
			FileStream fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read);
			var dataList = new List<string>();
			var stringBuilder = new StringBuilder();
			using (StreamReader sr = new StreamReader(fileStream, encoding: System.Text.Encoding.UTF8))
			{
				foreach(var element in sr.ReadToEnd())
				{
					if(element.ToString() == "\r")
					{
						dataList.Add(stringBuilder.ToString());
						stringBuilder.Clear();
					}
					else if(element.ToString() == "\n")
					{
						continue; // this just removes the new line, its not pretty but it works;
					}
					else
					{
						stringBuilder.Append(element);
					}

				}
				this.FileReader.data = dataList;
			}
			return this;
		}

	}

	public class FileDataOutPutter : FileReaderBuilder
	{
		public FileDataOutPutter(FileReader fileReader)
		{
			this.FileReader = fileReader;
		}

		public FileDataOutPutter OutPutData()
		{
			foreach(var element in this.FileReader.data)
			{
				Console.WriteLine(element.ToString());
			}
			Console.ReadLine();
			return this;
		}
	}



}
