using System;
using NUnit.Framework;
using PhpDeobfuscator;

[TestFixture]
public class DecodeTest
{
	[Test]
	public static void Decode1 ()
	{
		var line = "${\"G\\x4c\\x4f\\x42A\\x4c\\x53\"}[\"h\\x66\\x6d\\x6ei\\x62g\\x74\\x69\\x67\"]";

		Assert.AreEqual (PhpDeobfuscator.PhpDeobfuscator.DecodeLine (line), "${\"GLOBALS\"}[\"hfmnibgtig\"]");
	}
}
