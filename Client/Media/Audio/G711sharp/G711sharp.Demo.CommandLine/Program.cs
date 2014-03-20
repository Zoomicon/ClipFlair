using System;

namespace G711sharp.Demo.CommandLine
{
	public static class Program
	{
		public static void Main(string[] args)
		{
			SampleTest();
			RangeTest();
		  Console.ReadLine();
		}

		private static void RangeTest()
		{
			//Calculate average error for the whole range
			var µlaw = 0.0d;
			const double den = 100 / (double) (ALawEncoder.Max);
			var alaw = 0.0d;
			for (var i = 1; i <= ALawEncoder.Max; i++)
			{
				alaw += Math.Abs((ALawDecoder.ALawDecode(ALawEncoder.ALawEncode(i)) - i) / (double) i);
				µlaw += Math.Abs((MuLawDecoder.MuLawDecode(MuLawEncoder.MuLawEncode(i)) - i) / (double) i);
			}
			Console.WriteLine("Overall average percent error for each codec:");
			Console.WriteLine("µ-Law: " + (µlaw * den));
			Console.WriteLine("A-Law: " + (alaw * den));
			Console.WriteLine();
		}

		private static void SampleTest()
		{
			var rnd = new Random();
			int s;
			var c = 0;
			var alaw = 0.0d;
			var mulaw = 0.0d;
			Console.WriteLine("Orig\tµEnc\tµDec\tµ%Diff\tAEnc\tADec\tA%Diff");
			for (s = 122; s < MuLawEncoder.Max; s = (int) (s * (1 + rnd.NextDouble() / 11.4)))
			{
				c++; // keep a tally of how many times we've run this, for averaging purposes
				var menc = MuLawEncoder.MuLawEncode(s);
				int mdec = MuLawDecoder.MuLawDecode(menc);
				var mpercentDiff = (100 * (mdec - s)) / (double) s;
				mulaw += Math.Abs(mpercentDiff);
				var aenc = ALawEncoder.ALawEncode(s);
				int adec = ALawDecoder.ALawDecode(aenc);
				var apercentDiff = (100 * (adec - s)) / (double) s;
				alaw += Math.Abs(apercentDiff);
				Console.WriteLine(s + " \t" + menc + " \t" + mdec + " \t" + Math.Round(mpercentDiff, 2) + " \t" + aenc + " \t" + adec + " \t" + Math.Round(apercentDiff, 2));
			}
			Console.WriteLine();
			Console.WriteLine("Average percent error for each codec for this sample:");
			Console.WriteLine("µ-Law: " + (mulaw / c));
			Console.WriteLine("A-Law: " + (alaw / c));
			Console.WriteLine();
		}
	}
}
