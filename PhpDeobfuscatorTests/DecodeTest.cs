using System;
using NUnit.Framework;
using PhpDeobfuscator;

[TestFixture]
public class DecodeTest
{
	[Test]
	public static void FromFile ()
	{
		string filename = "../../../samples/rot13.php";

		var decodedLines = PhpDeobfuscator.Deobfuscate.DecodeTextFile (filename);

		//Console.WriteLine (decodedLines);
	}

	[Test]
	public static void Hex1 ()
	{
		var line = "${\"G\\x4c\\x4f\\x42A\\x4c\\x53\"}[\"h\\x66\\x6d\\x6ei\\x62g\\x74\\x69\\x67\"]";

		Assert.AreEqual (
			"${\"GLOBALS\"}[\"hfmnibgtig\"]",
			PhpDeobfuscator.Deobfuscate.Decode (line)
		);
	}

	[Test]
	public static void EvalBase64 ()
	{
		var line = "eval(base64_decode('ZWNobyAiaGVsbG9cbiI7'));";
		Assert.AreEqual (
			"echo \"hello\\n\";",
			PhpDeobfuscator.Deobfuscate.Decode (line)
		);
	}

	[Test]
	public static void EvalBase64InContext ()
	{
		var line = "$x=1; eval( base64_decode(\"ZWNobyAiaGVsbG8gd29ybGQiOw==\") ); $y=2;";

		Assert.AreEqual (
			"$x=1; echo \"hello world\"; $y=2;",
			PhpDeobfuscator.Deobfuscate.Decode (line)
		);
	}

	[Test]
	public static void PrettyPrint ()
	{
		var line = "$x=1;echo $x;$x=2;";
		Assert.AreEqual (
			"",
			PhpDeobfuscator.Deobfuscate.PrettyPrint (line)
		);
	}

}
