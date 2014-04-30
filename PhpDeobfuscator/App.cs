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

			/*
			var lines = ReadTextFile (filename);

			foreach (var line in lines) {

				Console.WriteLine (DecodeLine (line));
			}

*/

			var line = "${\"G\x4c\x4f\x42A\x4c\x53\"}[\"h\x66\x6d\x6ei\x62g\x74\x69\x67\"]";

			Console.WriteLine (DecodeLine (line));
		}

		public static string DecodeLine (string line)
		{
			// ${"G\x4c\x4f\x42A\x4c\x53"}["h\x66\x6d\x6ei\x62g\x74\x69\x67"]=

			// TODO decode \xNN hex codes
			// TODO2 only decode hex inside "" strings

			//int pos = line.IndexOf (@"\x");
			/*
			foreach (int value in AllIndexesOf(line, @"\x")) {
				Console.Write (value);
				Console.Write (" ");
			}*/

			string findStr = @"\x";

			var x = new StringBuilder ();

			for (int i = 0; i < line.Length; i++) {
				if ((i < line.Length - findStr.Length) && line.Substring (i, findStr.Length) == findStr) {
					//Console.Write (" X ");
					var hex = line.Substring (i + 2, 2);

					int intValue = int.Parse (hex, System.Globalization.NumberStyles.HexNumber);

					//Console.WriteLine (hex);
					x.Append ((char)intValue);
					i += 4;
				} else {
					x.Append (line.Substring (i, 1));
				}
			}


			return x.ToString ();
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

		public static IEnumerable<int> AllIndexesOf (string str, string value)
		{
			// TODO make it a extension method
			if (String.IsNullOrEmpty (value)) {
				throw new ArgumentException ("the string to find may not be empty", "value");
			}

			for (int index = 0;; index += value.Length) {
				index = str.IndexOf (value, index);

				if (index == -1) {
					break;
				}

				yield return index;
			}
		}
	}
}

