using System.Text;

public class PasswordGenerator
{	
	private static List<SelectableCharacter> _availableCharacters;
	private static List<SelectableCharacter> _selectedCharacters;

	private static List<CharacterTypes> _sequentialCharacterTypes = new List<CharacterTypes>
	{
		CharacterTypes.UpperCase,
		CharacterTypes.LowerCase,
		CharacterTypes.Number
	};

	public static string Generate(PasswordOptions passwordOptions)
	{
		try
		{
			Initialize(passwordOptions);
			
			return GeneratePassword(passwordOptions);
		}
		catch (Exception ex)
		{
			Console.WriteLine($"{nameof(Generate)} exception: {ex.Message}");
		}
		return null;
	}

	private static void Initialize(PasswordOptions passwordOptions)
	{
		_availableCharacters = new();
		_selectedCharacters = new();
		
		char[] excludedCharacters;
		if (string.IsNullOrWhiteSpace(passwordOptions.ExcludedCharacters))
		{
			excludedCharacters = Array.Empty<char>();
		}
		else
		{
			excludedCharacters = passwordOptions.ExcludedCharacters.ToCharArray();
		}  

		if (passwordOptions.IncludeUpperCaseLetters)
		{
			LoadSelectableCharacters(CharacterTypes.UpperCase, "ABCDEFGHIJKLMNOPQRSTUVWXYZ", excludedCharacters);
		}

		if (passwordOptions.IncludeLowerCaseLetters)
		{
			LoadSelectableCharacters(CharacterTypes.LowerCase, "abcdefghijklmnopqrstuvwxyz", excludedCharacters);
		}

		if (passwordOptions.IncludeNumbers)
		{
			LoadSelectableCharacters(CharacterTypes.Number, "0123456789", excludedCharacters);
		}

		if (passwordOptions.IncludeSpecialCharacters)
		{
			LoadSelectableCharacters(CharacterTypes.SpecialCharacter, "~`!@#$%^&*()-_=+[]{}|\\;:'\",.<>?/", excludedCharacters);
		}
	}

	private static void LoadSelectableCharacters(CharacterTypes characterType, string selectableCharacters, char[] excludedCharacters)
	{
		int index;
		if (_availableCharacters.Count == 0)
		{
			index = 0;
		}
		else 
		{
			index = _availableCharacters.Max(c => c.Index) + 1;
		}
		
		foreach (var c in selectableCharacters.ToCharArray())
		{
			if (excludedCharacters.Contains(c))
				continue;
						
			_availableCharacters.Add(new SelectableCharacter(index, characterType, c));
			
			index++;
		}
	}
	
	private static string GeneratePassword(PasswordOptions passwordOptions)
	{
		try
		{
			StringBuilder password = new();
			
			SelectableCharacter selectedCharacter;
			if (passwordOptions.StartWithLetter)
			{
				selectedCharacter = SelectLetter();
				if (selectedCharacter is null)
					return null;
					
			 	AddCharacterToPassword(password, selectedCharacter);
			}
			
			int passwordIndex = password.Length;
			int targetPasswordLength = passwordOptions.PasswordLength;
			
			while (password.Length < targetPasswordLength)
			{				
				selectedCharacter = SelectCharacter();
				if (selectedCharacter is null)
					return null;

				if (DuplicateCharacterSelectedWhenNotAllowed(passwordOptions, selectedCharacter))
					continue;

				if (SequentialCharacterSelectedWhenNotAllowed(passwordOptions, selectedCharacter))
					continue;

				AddCharacterToPassword(password, selectedCharacter);

				passwordIndex++;
			}

			return password.ToString();
		}
		catch (Exception ex)
		{
			Console.WriteLine(ex.Message, nameof(GeneratePassword));
		}
		return null;
	}
	
	private static SelectableCharacter SelectLetter()
	{
		try
		{
			var sortedSelectableCharacters = _availableCharacters.OrderBy(c => c.Index);
			
			
			var minIndex = sortedSelectableCharacters.FirstOrDefault(c => c.CharacterType == CharacterTypes.UpperCase || c.CharacterType == CharacterTypes.LowerCase)?.Index;
			if (minIndex is null)
				throw new Exception("There are no letters available to select.");

			var maxIndex = sortedSelectableCharacters.Last(c => c.CharacterType == CharacterTypes.UpperCase || c.CharacterType == CharacterTypes.LowerCase).Index;
			if (minIndex >= maxIndex)
				throw new Exception("Not enough characters available to select.");

			int selectableCharacterIndex = RandomNumber.Generate(minIndex.Value, maxIndex);
			return _availableCharacters[selectableCharacterIndex];
		}
		catch (Exception ex)
		{
			Console.WriteLine(ex.Message, nameof(SelectCharacter));
		}
		return null;
	}
	
	private static SelectableCharacter SelectCharacter()
	{
		try
		{
			var minValue = 0;
			var maxValue = _availableCharacters.Count - 1;
			int selectableCharacterIndex = RandomNumber.Generate(minValue, maxValue);
			return _availableCharacters[selectableCharacterIndex];
		}
		catch (Exception ex)
		{
			Console.WriteLine(ex.Message, nameof(SelectCharacter));
		}
		return null;
	}
	
	private static void AddCharacterToPassword(StringBuilder password, SelectableCharacter character)
	{
		password.Append(character.Character);
		_selectedCharacters.Add(character);
	}
	
	private static bool DuplicateCharacterSelectedWhenNotAllowed(PasswordOptions passwordOptions, SelectableCharacter character)
	{
		return passwordOptions.PreventDuplicateCharacters && _selectedCharacters.Any(c => c.Index == character.Index);
	}
	
	private static bool SequentialCharacterSelectedWhenNotAllowed(PasswordOptions passwordOptions, SelectableCharacter character)
	{
		if (!passwordOptions.PreventSequentialCharacters)
			return false;
		
		if (_selectedCharacters.Count == 0)
			return false;		
		
		if (!_sequentialCharacterTypes.Contains(character.CharacterType))
			return false;		
		
		var lastSelectedCharacter = _selectedCharacters.Last();
		if (lastSelectedCharacter.CharacterType != character.CharacterType)
			return false;

		// If we made it here, check if the characters are sequential.
		// Take the absolute value of the difference to prevent the selection of
		// ascending (ABC, 123, hij, etc) or descending (CBA, 321, jih, etc) sequential characters.
		var indexDiff = Math.Abs(lastSelectedCharacter.Index - character.Index);
		return indexDiff == 1;			
	}

	#region  Helper Classes/Enums	

	class SelectableCharacter
	{
		public int Index { get; set; }
		public CharacterTypes CharacterType { get; set; }
		public char Character { get; set; }
		
		public SelectableCharacter(int index, CharacterTypes characterType, char character)
		{
			this.Index = index;
			this.CharacterType = characterType;
			this.Character = character;
		}
	}

	enum CharacterTypes
	{
		UpperCase = 1,
		LowerCase = 2,
		Number = 3,
		SpecialCharacter = 4
	}

	#endregion
}