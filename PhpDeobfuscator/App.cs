using System;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace PhpDeobfuscator
{
	public class App
	{
		public static void Main ()
		{
			string filename = "../../../sample1.txt";


			var lines = ReadTextFile (filename);

			foreach (var line in lines) {

				Console.WriteLine (PhpDeobfuscator.DecodeLine (line));
			}
		}

		public static List<string> ReadTextFile (string filename)
		{
			FileStream fStream = new FileStream (filename, FileMode.Open, FileAccess.Read);
			StreamReader inFile = new StreamReader (fStream);

			var lines = new List<string> ();

			while (!inFile.EndOfStream) {
				var currLine = inFile.ReadLine ();
				lines.Add (currLine);
			}

			return lines;
		}
	}
}

