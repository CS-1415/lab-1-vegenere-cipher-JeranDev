using System.Diagnostics;

// Ask user for message
Console.Clear();
Console.WriteLine("This program encrypts the characters of a message using the Vigenere method.");
Console.WriteLine("Please enter the message: ");
Console.Write("\t");
string message = Console.ReadLine();

// Validate message
if (!IsValidInput(message))
{
    Console.Error.WriteLine("Sorry, we only support lower-case letters.");
    return;
}

// Ask user for key
Console.WriteLine("Please enter an encryption key:");
Console.Write("\t");
string key = Console.ReadLine();

// Validate key
if (!IsValidInput(key))
{
    Console.Error.WriteLine("Sorry, we only support lower-case letters.");
    return;
}

// Run tests
TestIsLowercaseLetter();
TestIsValidInput();
TestShiftLetter();
TestShiftMessage();

// Print message
Console.WriteLine("Here is the encrypted message:");
Console.Write("\t");
Console.WriteLine(ShiftMessage(message, key));

// Returns true if the given character is a lowercase letter and false otherwise
static bool IsLowercaseLetter(char c)
{
    return Char.IsLower(c);
}

static void TestIsLowercaseLetter()
{
    Debug.Assert(IsLowercaseLetter('a'));
    Debug.Assert(IsLowercaseLetter('b'));
    Debug.Assert(IsLowercaseLetter('z'));
    Debug.Assert(!IsLowercaseLetter('A'));
    Debug.Assert(!IsLowercaseLetter('`'));
    Debug.Assert(!IsLowercaseLetter('{'));
}

// Returns true if the given string contains only lowercase letters
static bool IsValidInput(string s)
{
    foreach (char c in s)
    {
        if (!IsLowercaseLetter(c))
        {
            return false;
        }
    }

    return true;
}

static void TestIsValidInput()
{
    Debug.Assert(IsValidInput("test"));
    Debug.Assert(IsValidInput("t"));
    Debug.Assert(!IsValidInput("Test"));
    Debug.Assert(!IsValidInput("'"));
    Debug.Assert(!IsValidInput("{"));
}

// Accept two characters (one from the message and one from the key) and return the appropriate shifted character
static char ShiftLetter(char messageChar, char keyChar)
{
    // Convert messageChar and keyChar into integers
    int m = messageChar - 'a';
    int k = keyChar - 'a';

    // Shift message by key, wrap around with % 25 (the length of the alphabet starting at 0)
    int shifted = (m + k) % 26;

    return (char)(shifted + 'a');
}

static void TestShiftLetter()
{
    Debug.Assert(ShiftLetter('a', 'b') == 'b');
    Debug.Assert(ShiftLetter('a', 'd') == 'd');
    Debug.Assert(ShiftLetter('z', 'b') == 'a');
}

// Accept two strings (the message and the key) and return the encoded message.
static string ShiftMessage(string message, string key)
{
    string result = "";
    for (int i = 0; i < message.Length; i++)
    {
        char messageChar = message[i];
        char keyChar = key[i % key.Length]; // repeat key
        result += ShiftLetter(messageChar, keyChar);
    }
    return result;
}

static void TestShiftMessage()
{
    Debug.Assert(ShiftMessage("abc", "b") == "bcd");
    Debug.Assert(ShiftMessage("xyz", "b") == "yza");
    Debug.Assert(ShiftMessage("endzz", "bc") == "fpeba");
}
