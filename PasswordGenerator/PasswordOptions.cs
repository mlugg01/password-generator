public class PasswordOptions
{
	public int PasswordLength { get; set; }
	public bool IncludeUpperCaseLetters { get; set; }
	public bool IncludeLowerCaseLetters { get; set; }
	public bool IncludeNumbers { get; set; }
	public bool IncludeSpecialCharacters { get; set; }
	public string ExcludedCharacters { get; set; }
	public bool StartWithLetter { get; set; }
	public bool PreventDuplicateCharacters { get; set; }
	public bool PreventSequentialCharacters { get; set; }
}