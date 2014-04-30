using System;
using System.Text;

namespace PhpDeobfuscator
{
	public class PhpDeobfuscator
	{
		public static string DecodeLine (string line)
		{
			// TODO only decode hex inside "" strings

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
	}
}

