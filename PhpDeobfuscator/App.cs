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

			var decodedLines = PhpDeobfuscator.Decode (lines);
			Console.WriteLine (decodedLines);


			string s = "$x=1; eval( base64_decode(\"ZWNobyAiaGVsbG8gd29ybGQiOw==\") ); $y=2;";

			var x = PhpDeobfuscator.Decode (s);

			Console.WriteLine (x);
		}

		public static string ReadTextFile (string filename)
		{
			FileStream stream = new FileStream (filename, FileMode.Open, FileAccess.Read);
			StreamReader reader = new StreamReader (stream);

			var lines = new StringBuilder ();

			while (!reader.EndOfStream) {
				lines.Append (reader.ReadLine ());
				lines.Append ("\n");
			}

			return lines.ToString ();
		}
	}
}

