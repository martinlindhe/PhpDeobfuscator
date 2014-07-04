using System;
using NUnit.Framework;
using PhpDeobfuscator;

[TestFixture]
public class DecodeTest
{
	[Test]
	public static void FromFile ()
	{
		string filename = "../../../samples/sample1.txt";

		var decodedLines = PhpDeobfuscator.PhpDeobfuscator.DecodeTextFile (filename);

		Console.WriteLine (decodedLines);

	}

	[Test]
	public static void Hex1 ()
	{
		var line = "${\"G\\x4c\\x4f\\x42A\\x4c\\x53\"}[\"h\\x66\\x6d\\x6ei\\x62g\\x74\\x69\\x67\"]";

		Assert.AreEqual (
			"${\"GLOBALS\"}[\"hfmnibgtig\"]",
			PhpDeobfuscator.PhpDeobfuscator.Decode (line)
		);
	}

	[Test]
	public static void EvalBase64 ()
	{
		var line = "eval(base64_decode('ZWNobyAiaGVsbG9cbiI7'));";
		Console.WriteLine (PhpDeobfuscator.PhpDeobfuscator.Decode (line));
		Assert.AreEqual (
			"echo \"hello\\n\";",
			PhpDeobfuscator.PhpDeobfuscator.Decode (line)
		);
	}

	[Test]
	public static void EvalBase64_2 ()
	{
		var line = "$x=1; eval( base64_decode(\"ZWNobyAiaGVsbG8gd29ybGQiOw==\") ); $y=2;";

		Assert.AreEqual (
			"$x=1; echo \"hello world\"; $y=2;",
			PhpDeobfuscator.PhpDeobfuscator.Decode (line)
		);
	}

}
