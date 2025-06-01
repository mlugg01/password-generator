Password Generator generates complex passwords to use for various online sites.

The basic functionality works as follows:
1) Password options, such as PasswordLength, IncludeUpperCaseLetters, IncludeLowerCaseLetters, IncludeNumbers, IncludeSpecialCharacters, and others, are populated in an a PasswordOptions object, and are
   passed to the PasswordGenerator.Initialize method.
2) The Initialize method determines which characters are selectable for the password based upon the provided PasswordOptions, and adds them to a list of SelectableCharacter objects.
3) PasswordGenerator.GeneratePassword is then called.  While the password length is less than the desired PasswordOptions.PasswordLength, a random number is selected within the range of indexes of list of SelectableCharacter objects.
4) So long as the selected character meets all restrictions in the PasswordOptions object, it is added to the password.
5) Once the target length is reached, the algorithm terminates, and the final password is returned.
