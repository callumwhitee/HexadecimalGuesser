using System;
using System.IO;
using System.Threading;


class Program {

    // globally declare the string path 
    static string path = "hexColours.csv";

    // globally declare the 2d array 'colours'
    static string[,] colours;

  public static void Main (string[] args) {
    // reads in user input
    ReadInUserString();
  }



  static void ReadInUserString()
  {
    Console.Clear();
    // reads in the user input of a potential hexadeciaml value
    Console.WriteLine("Please Enter A Hexadecimal String Of 6 Characters");
    Thread.Sleep(750);
    Console.Write("> ");
    // takes in user input
    string EnteredHexadecimal = Console.ReadLine();
    // validates the hexadeciamal input
    bool isValid = isValidHex(EnteredHexadecimal);
    if(isValid)
    {
      CheckAgainstColours(EnteredHexadecimal);
    }
    else
    {
      // retry method
      ReadInUserString();
    }
  }




  static void CheckAgainstColours(string input)
  {
    // poplates 2d array with hexColours.csv
    readInHexColours();
    int[,] compareResults;
    // initiales the 2d array
    compareResults = new int[colours.GetLength(0), 2];
    
    // this for loop compares the user input
    // with the hexadecimal values
    for(int i = 0; i < colours.GetLength(0); i++)
    {
      compareResults[i,0] = Compare(input, colours[i,0]);
      compareResults[i,1] = i;
    }
    

    // initialse the levinshein value
    int LevensheinValue;
    // for loop to cycle thru the levenshein results
    for(int j = 0; j < compareResults.GetLength(0); j++)
    {
      for(int i = 0; i < compareResults.GetLength(0); i++)
      {
        // sets levensheinValue to the iteration of loop
        LevensheinValue = compareResults[i,0];
        // checks if method CheckLevenshein returns true
        if(CheckLevenshein(LevensheinValue, j)) 
        {
          // sets finalColour to the iteration of the array
          string finalColour = colours[i,1];
          // runs WriteFinalColour
          // passes finalColour
          // passes if colour is exact value
          if(j == 0)
          {
            WriteFinalColour(finalColour, false);
          }
          else
          {
            WriteFinalColour(finalColour, true);
          }
          // stops loop 
          break;
        }
      }
    }
  }

  static bool CheckLevenshein(int LevensheinValue, int value)
  {
    if(LevensheinValue == value)
    {
      return true;
    }
    return false;
  }


  static void WriteFinalColour(string colour, bool close)
  {
    if(close == false)
    {
      Console.Clear();
      Console.WriteLine("Your Colour Was " + colour);
      Thread.Sleep(2000);
      Console.WriteLine("Press Any Key To Exit.");
      Console.ReadKey();
      Console.Clear();
      Environment.Exit(0);
    }
    else
    {
      Console.Clear();
      Console.WriteLine("Your Value Was Closest To " + colour);
      Thread.Sleep(2000);
      Console.WriteLine("Press Any Key To Exit.");
      Console.ReadKey();
      Console.Clear();
      Environment.Exit(0);
    }
  }
  

  

  

  static void readInHexColours()
  {
      string[] hexColours = new string[File.ReadAllLines(path).Length];
      int colums = 2;
      int rows = hexColours.Length;
      colours = new string[rows, colums];
      //read all lines in path
      try
      {
        hexColours = File.ReadAllLines(path);
      }
      // catches exceptions
      catch(Exception e)
      {
        Console.WriteLine("oopsie daisies " + e.Message);
      }
      // craetes a temparoy array for the nested for loop
      string[] temp = new string[colums];

      for(int i = 0; i < rows;i++)
      {
        // splits array every ','
        temp = hexColours[i].Split(',');
        for(int j = 0; j< colums;j++)
        {
          //populates the 2d array
          colours[i,j] = temp[j];
        }
      }
  }

  static int Compare(string s, string t)
  {
    // checks that both strings actually have values
    if(string.IsNullOrEmpty(s))
    {
      if(string.IsNullOrEmpty(t))
      {
        return 0;
      }
      return t.Length;
    }
    if(string.IsNullOrEmpty(t))
      return s.Length;
    int n = s.Length;
    int m = t.Length;
    // creates a 2d array 
    int[,] d = new int[n + 1, m + 1];
    // initiales the size of the array
    for(int i = 0; i <= n; d[i,0] = i++);
    for(int j = 1; j <= m; d[0,j] = j++);
    // uses the 'Damereau Levenshein' algorithm to check difference 
    // between the two strings
    for(int i = 1; i <= n; i++)
    {
      for(int j = 1; j <= m; j++)
      {
        int cost = (t[j - 1] == s[i - 1]) ? 0 : 1;
        int min1 = d[i - 1, j] + 1;
        int min2 = d[i, j - 1] + 1;
        int min3 = d[i - 1, j - 1] + cost;
        d[i, j] = Math.Min(Math.Min(min1, min2), min3);
      }
    }
    // returns an int value based on difference
    return d[n, m];


  }


  static bool isValidHex(string string1)
  {
    bool hexLength = false;
    // checks the values of the string are correct based on hexadecimal values
    bool validHex = System.Text.RegularExpressions.Regex.IsMatch(string1, @"\A\b[0-9a-fA-F]+\b\Z");
    int lengthOfHex = string1.Length;
    // checks the correct length of the string
    if(lengthOfHex <= 6)
    {
      hexLength = true;
    }
    // if both are true then return true
    if(validHex && hexLength)
      return true;
    else 
      return false;
      
  }







  
  
}