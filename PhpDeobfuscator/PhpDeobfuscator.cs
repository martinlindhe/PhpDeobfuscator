using System;
using System.Text;
using System.IO;

namespace PhpDeobfuscator
{
	public class PhpDeobfuscator
	{
		public static string Decode (string line)
		{
			// TODO only decode hex inside "" strings

			var unhexed = DecodeHex (line);
			return EvalAndBase64StringDecode (unhexed);
		}


		public static string DecodeTextFile (string filename)
		{
			FileStream stream = new FileStream (filename, FileMode.Open, FileAccess.Read);
			StreamReader reader = new StreamReader (stream);

			var lines = new StringBuilder ();

			while (!reader.EndOfStream) {
				lines.Append (reader.ReadLine ());
				lines.Append ("\n");
			}

			return Decode(lines.ToString ());
		}

		private static string DecodeHex (string line)
		{
			string findStr = @"\x";

			var x = new StringBuilder ();

			for (int i = 0; i < line.Length; i++) {
				if ((i < line.Length - findStr.Length) && line.Substring (i, findStr.Length) == findStr) {

					var hex = line.Substring (i + 2, 2);

					int intValue = int.Parse (hex, System.Globalization.NumberStyles.HexNumber);

					x.Append ((char)intValue);
					i += 3;
				} else {
					x.Append (line.Substring (i, 1));
				}
			}

			return x.ToString ();
		}

		/**
		 * Detects and decodes eval( base64_decode() ) method in s (PHP code)
		 */
		private static string EvalAndBase64StringDecode (string s)
		{
			// find ofs of eval
			int ofsEval = s.IndexOf ("eval");
			if (ofsEval == -1) {
				return s;
			}

			// find offset to base64_decode
			int ofsBase64Deccode = s.IndexOf ("base64_decode");
			if (ofsBase64Deccode == -1) {
				return s;
			}

			// find ( and ) following method name
			int brace1 = s.IndexOf ("(", ofsBase64Deccode);
			int brace2 = s.IndexOf (")", ofsBase64Deccode);
			if (brace1 == -1 || brace2 == -1) {
				return s;
			}

			var base64 = s.Substring (brace1 + 1, brace2 - brace1 - 1).Trim ();

			string decoded = "";

			if (base64.StartsWith ("'") && base64.EndsWith ("'")) {
				decoded = FromBase64 (base64.Substring (1, base64.Length - 2));
			}

			if (base64.StartsWith ("\"") && base64.EndsWith ("\"")) {
				decoded = FromBase64 (base64.Substring (1, base64.Length - 2));
			}

			if (decoded.Length == 0) {
				return s;
			}

			string before = s.Substring (0, ofsEval);

			// remove closing ) and ; from eval statement
			int closingPos = s.IndexOf (")", brace2 + 1);
			closingPos = s.IndexOf (";", closingPos);

			string after = s.Substring (closingPos + 1);

			return before + decoded + after;
		}

		private static string FromBase64 (string input)
		{
			var x = System.Convert.FromBase64String (input);
			return System.Text.ASCIIEncoding.UTF8.GetString (x);
		}
	}
}

