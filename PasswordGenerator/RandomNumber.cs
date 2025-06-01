using System.Security.Cryptography;

public class RandomNumber
{
	public static int Generate(int minValue, int maxValue)
	{
		if (minValue < 0)
			throw new ArgumentOutOfRangeException("minValue must be non-negative.");

		if (minValue >= maxValue)
			throw new ArgumentOutOfRangeException("minValue must be lower than maxExclusiveValue.");

		// 5 - 3 + 1 gives a range of 3 allowed values.
		// 6 - 0 + 1 gives a range of 7 allowed values.
		var range = maxValue - minValue + 1;
		if (range >= int.MaxValue)
			throw new ArgumentOutOfRangeException("Random number generation range is too large.");

		var randomInt = GetRandomInt();		
		
		// Perform a bit-wise AND between randomInt and int.MaxValue to ensure that the returned value is positive,
		// since the sign bit of int.MaxValue is 0.
		// perform the modulus and add the minValue to ensure that the returned value is within the specified range.
		return ((randomInt & int.MaxValue) % range) + minValue;
	}

	private static int GetRandomInt()
	{
		var randomNumberBytes = GenerateRandomBytes();
		return BitConverter.ToInt32(randomNumberBytes, 0);		
	}

	private static byte[] GenerateRandomBytes()
	{
		var randomNumberBytes = new byte[4];
		using (var rng = RandomNumberGenerator.Create())
		{
			rng.GetBytes(randomNumberBytes);
		}
		return randomNumberBytes;
	}
}