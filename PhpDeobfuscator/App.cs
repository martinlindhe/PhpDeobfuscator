using System;

namespace PhpDeobfuscator
{
	public class App
	{
		public static void Main ()
		{
			string s = "eval(gzinflate(base64_decode('S03OyFdQykjNycmPyVOyBgA=')));";

			var x = PhpDeobfuscator.Decode (s);

			Console.WriteLine (x);
		}

	}
}

