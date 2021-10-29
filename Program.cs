using System;
using System.IO;
using System.Threading;
using Ionic.Zip;
using System.Linq;


class Program {

    // CALLUMS HEXADECIMAL PROJECT


    /* 
    The Inital Username Is = "user"
    The Intial Password Is = "pass"
    */


    // globally declare the string path of hexColours.csv
    static string hexPath = "csvFiles/hexColours.csv";
    // globally declare the string path of userDetails.csv
    static string userPath = "csvFiles/userDetails.csv";
    // globally declare the 2d array 'colours'
    static string[,] colours;
    // globally declares the enteredUsername
    static string enteredUsername;
    // globally declares the enteredPassword
    static string enteredPassword;

  public static void Main (string[] args) 
  {
      // runs start
    Start();
  }

  static void Start()
  {
      Check();
      Login();
  }

  static void MainMenu()
  {
      Console.Clear();
      // makes sure the console is white
      Console.ForegroundColor = ConsoleColor.White;
      // takes in what method to do
      Console.WriteLine("Please Choose What Method To Do");
      Thread.Sleep(500);
      Console.ForegroundColor = ConsoleColor.Green;
      Console.WriteLine("Hexadecimal Guesser");
      Thread.Sleep(500);
      Console.WriteLine("Colour Guesser");
      Thread.Sleep(500);
      Console.WriteLine("New Login");
      Thread.Sleep(500);
      // turns console red for option "Quit"
      Console.ForegroundColor = ConsoleColor.Red;
      Console.WriteLine("Logout");
      Thread.Sleep(500);
      Console.WriteLine("Quit");
      Console.ForegroundColor = ConsoleColor.White;
      Console.Write("> ");
      // crates string choice and turns it lower case
      string choice = Console.ReadLine().ToLower();
      // if string has a value then run code
      if(String.IsNullOrWhiteSpace(choice) == false)
      {
          // switch to decide user input
          switch(choice)
          {
              case "hexadecimal guesser":
                ReadInUserString();
                break;
              case "colour guesser":
                ColourGuesser();
                break;
              case "new login":
                NewLogin();
                break;
              case "logout":
                Console.WriteLine("Logging Out...");
                Thread.Sleep(1000);
                //quits program
                Login();
                break;
              case "quit":
                Console.WriteLine("Quitting Program");
                Thread.Sleep(500);
                Environment.Exit(0);
                break;
              default:
                // retrys the mainmenu
                Console.WriteLine("That Was Not A Valid Method");
                Thread.Sleep(500);
                Console.WriteLine("Please Try Again");
                Thread.Sleep(2000);
                MainMenu();
                break;
          }
      }
  }



  static void ColourGuesser()
  {
      // lets player choose easy or hard colour guessing game
      Console.Clear();
      Console.WriteLine("Would You Like To Play Easy Or Hard");
      Thread.Sleep(500);
      Console.ForegroundColor = ConsoleColor.Green;
      Console.WriteLine("Easy");
      Thread.Sleep(500);
      Console.ForegroundColor = ConsoleColor.Red;
      Console.WriteLine("Hard");
      Console.ForegroundColor = ConsoleColor.White;
      Thread.Sleep(500);
      Console.WriteLine("Main Menu");
      Console.Write("> ");
      // takes in user input
      string enteredHardness = Console.ReadLine().ToLower();
      if(enteredHardness == "easy")
      {
          // runs easy
          Console.WriteLine("You Have Selected Easy");
          Thread.Sleep(500);
          EasyGuesser();
      }
      else if(enteredHardness == "hard")
      {
          // warns and then runs hard
          Console.ForegroundColor = ConsoleColor.Red;
          Console.Clear();
          Console.WriteLine("You Have Selected Hard");
          Thread.Sleep(500);
          Console.WriteLine("Warning: Very Hard");
          Thread.Sleep(1000);
          Console.ForegroundColor = ConsoleColor.White;
          HardGuesser();
      }
      else if(enteredHardness == "main menu")
      {
          // returns to main menu
          Console.Clear();
          MainMenu();
      }
      else
      {
          // tries again if string is not a valid value
          Console.WriteLine("You Have Not Entered A Correct Hardness");
          Thread.Sleep(500);
          Console.WriteLine("Please Try Again");
          Thread.Sleep(2000);
          ColourGuesser();
      }
  }

  static void EasyGuesser()
  {

      // gets csv file
      string[] easyColourCsv = File.ReadAllLines("csvFiles/easyColours.csv");
      // splits array by commas
      string[] easyColours = easyColourCsv[0].Split(',');
      // craetes random object
      Random rand = new Random();
      // creates random variabkle
      int random = rand.Next(0,easyColours.Length);
      // sets string as random string from array
      string ComputerColour = easyColours[random];


      Console.Clear();
      Console.WriteLine("Please Enter Your Guessed Colour");
      Console.Write("> ");
      string guessed = Console.ReadLine().ToLower();
      Console.Clear();
      // if the user input is equal to the computers color, declares win
      if(guessed == ComputerColour)
      {
          Console.WriteLine("You Guessed The Right Colour!");
          Thread.Sleep(1500);
          Console.WriteLine("Would You Like To Play Again? (Y or N)");
          Console.Write("> ");
          char retry = Convert.ToChar(Console.ReadLine().ToLower());
          if(retry == 'y')
            EasyGuesser();
          else
          {
              Console.Clear();
              Console.WriteLine("Thanks For Playing!");
              Thread.Sleep(1000);
              MainMenu();
          }
      }
      // else say incorrect and allow user to retry
      else
      {
          Console.WriteLine("You Guessed The Wrong Colour");
          Console.WriteLine("The Colour Was " + ComputerColour);
          Thread.Sleep(1500);
          EasyGuesser();
      }
  }


  static void HardGuesser()
  {
      // uses method to craete 2d array from csv file
      readInHexColours();
      // craetes random object
      Random rand = new Random();
      // creates a random int from 0 to length of first coloum of 2d array
      int random = rand.Next(0,colours.GetLength(0));
      // craetes a string from the 2d array
      string ComputerColour = colours[random,1];


      Console.Clear();
      Console.WriteLine("Please Enter Your Guessed Colour");
      Console.Write("> ");
      string guessed = Console.ReadLine().ToLower();
      Console.Clear();
      // if the user input = the computer colour, declare win
      if(guessed == ComputerColour)
      {
          Console.WriteLine("You Guessed The Right Colour!");
          Thread.Sleep(1500);
          Console.WriteLine("Would You Like To Play Again? (Y or N)");
          Console.Write("> ");
          char retry = Convert.ToChar(Console.ReadLine().ToLower());
          if(retry == 'y')
          // gives user chance to retry
            ColourGuesser();
          else
          {
              // returns to main menu
              Console.Clear();
              Console.WriteLine("Thanks For Playing!");
              Thread.Sleep(1000);
              MainMenu();
          }
      }
      // else alow user to retry or return to main menu
      else
      {
          Console.WriteLine("You Guessed The Wrong Colour");
          Console.WriteLine("The Colour Was " + ComputerColour);
          Thread.Sleep(1500);
          Console.WriteLine("Would You Like To Play Again? (Y or N)");
          Console.Write("> ");
          char retry = Convert.ToChar(Console.ReadLine().ToLower());
          if(retry == 'y')
            HardGuesser();
          else
          {
              Console.Clear();
              Console.WriteLine("Thanks For Playing!");
              Thread.Sleep(1000);
              MainMenu();
          }
      }






  }

  static void NewLogin()
  {
      UnZip();
      Console.Clear();
      Console.WriteLine("Enter New Username");
      Console.Write("> ");
      string newUsername = Console.ReadLine();
      Console.Clear();
      Console.WriteLine("Enter New Password");
      Console.Write("> ");
      string newPassword = Console.ReadLine();
      Console.Clear();
      string NewUserPass = newUsername + newPassword;
      try
      {
          using(StreamWriter file = new StreamWriter(@userPath , true))
          {
              file.Write("," + NewUserPass);
          }
          Console.WriteLine("Successfully Added The New Details");
          Thread.Sleep(1500);
          Zip();
          MainMenu();
      }
      catch(Exception E)
      {
          Zip();
          Console.WriteLine("There Was An Error Adding Your New Details");
          Console.WriteLine(E.Message);
      }
      Zip();
  }

  static void Check()
  {
      if(File.Exists(userPath))
      {
        Zip();
      }
  }

  static void Login()
  {
      UnZip();
      Console.Clear();
      Console.WriteLine("Please Enter Your Username");
      Console.Write("> ");
      enteredUsername = Console.ReadLine();
      Console.Clear();
      Console.WriteLine("Please Enter Password");
      Console.Write("> ");
      enteredPassword = Console.ReadLine();

      //try entering username File

      try
      {
          string userNamesReadIn = File.ReadAllText(userPath);
          string[] details = userNamesReadIn.Split(',');
          string userPassword = enteredUsername + enteredPassword;
          Zip();
          if(details.Contains(userPassword))
          {
              Console.Clear();
              Console.WriteLine("Your Details Are Correct");
              Thread.Sleep(1000);
              MainMenu();
          }
          else
          {
              Console.Clear();
              Console.WriteLine("Your Entered Details Were Not Correct.");
              Console.WriteLine("Please Try Again");
              Thread.Sleep(1000);
              Login();
          }
      }
      catch(Exception e)
      {
          Console.WriteLine(e.Message);
      }



  }



  static void Zip()
  {
    using(ZipFile zip = new ZipFile())
      {
          zip.AddFile(userPath);
          zip.Save("csvFiles/userPathZipped");
          File.Delete(userPath);
      }
  }

  static void UnZip()
  {
      using(ZipFile zip = new ZipFile("csvFiles/userPathZipped"))
      {
          zip.ExtractAll("", Ionic.Zip.ExtractExistingFileAction.OverwriteSilently);
          File.Delete("csvFiles/userPathZipped");
      }
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
        // takes in user input
        // checks the input against
        // the 2d array colours[];
      CheckAgainstColours(EnteredHexadecimal);
    }
    else
    {
        // user presses key to retry program
        Console.Clear();
        Console.WriteLine("That Was Not A Valid Hexadecimal Value");
        Thread.Sleep(750);
        Console.WriteLine("Press Any Key To Retry");
        Console.Write("> ");
        Console.ReadKey();
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
      Console.WriteLine("Press R To Retry.");
      Thread.Sleep(800);
      Console.WriteLine("Press Any Other Key To Go Back To Main Menu");
      Console.Write("> ");
      string input = Console.ReadLine().ToLower();
      char charInput = Convert.ToChar(input);
      if(charInput == 'r')
        ReadInUserString(); 
      else
        MainMenu();
    }
  }
  

  

  

  static void readInHexColours()
  {
      // creates a array of the hexPath value
      string[] hexColours = new string[File.ReadAllLines(hexPath).Length];
      // sets number of coloums of csv to 2
      int colums = 2;
      // sets rows to number of rows
      int rows = hexColours.Length;
      colours = new string[rows, colums];
      //read all lines in path
      try
      {
        hexColours = File.ReadAllLines(hexPath);
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