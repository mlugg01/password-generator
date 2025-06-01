public class Program
{
	public static void Main(string[] args)
	{
		PasswordOptions options = new()
		{
			PasswordLength = 12,
			IncludeLowerCaseLetters = true,
			IncludeSpecialCharacters = true,
			IncludeNumbers = true,
			IncludeUpperCaseLetters = true,
			// ExcludedCharacters = "'\"",
			StartWithLetter = true,
			PreventDuplicateCharacters = true,
			PreventSequentialCharacters = true
		};
		
		var password = PasswordGenerator.Generate(options);
		if (string.IsNullOrWhiteSpace(password))
		{
			Console.WriteLine("Password generation failed");	
		}
		else
		{
			Console.WriteLine("New PWD: " + password);					
		}

		password = PasswordGenerator.Generate(options);
		if (string.IsNullOrWhiteSpace(password))
		{
			Console.WriteLine("Password generation failed");
		}
		else
		{
			Console.WriteLine("New PWD: " + password);
		}

		password = PasswordGenerator.Generate(options);
		if (string.IsNullOrWhiteSpace(password))
		{
			Console.WriteLine("Password generation failed");
		}
		else
		{
			Console.WriteLine("New PWD: " + password);
		}

		password = PasswordGenerator.Generate(options);
		if (string.IsNullOrWhiteSpace(password))
		{
			Console.WriteLine("Password generation failed");
		}
		else
		{
			Console.WriteLine("New PWD: " + password);
		}

		password = PasswordGenerator.Generate(options);
		if (string.IsNullOrWhiteSpace(password))
		{
			Console.WriteLine("Password generation failed");
		}
		else
		{
			Console.WriteLine("New PWD: " + password);
		}
	}
}

