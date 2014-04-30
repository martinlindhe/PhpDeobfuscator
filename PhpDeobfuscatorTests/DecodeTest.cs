using System;
using NUnit.Framework;
using PhpDeobfuscator;

[TestFixture]
public class DecodeTest
{
	[Test]
	public static void Hex1 ()
	{
		var line = "${\"G\\x4c\\x4f\\x42A\\x4c\\x53\"}[\"h\\x66\\x6d\\x6ei\\x62g\\x74\\x69\\x67\"]";

		Assert.AreEqual (
			PhpDeobfuscator.PhpDeobfuscator.Decode (line),
			"${\"GLOBALS\"}[\"hfmnibgtig\"]"
		);
	}

	[Test]
	public static void EvalAndBase64 ()
	{
		var line = "$x=1; eval( base64_decode(\"ZWNobyAiaGVsbG8gd29ybGQiOw==\") ); $y=2;";

		Assert.AreEqual (
			PhpDeobfuscator.PhpDeobfuscator.Decode (line), 
			"$x=1; echo \"hello world\"; $y=2;"
		);

	}
}
