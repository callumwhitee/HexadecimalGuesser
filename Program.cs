using System;
using System.IO;
using System.Threading;


class Program {

    //static variables
    static string path = "hexColours.csv";

  public static void Main (string[] args) {
    readInHexColours();
  }

  static void readInHexColours()
  {





      string[] hexColours = new string[File.ReadAllLines(path).Length];
      int colums = 2;
      int rows = hexColours.Length;
      string [,] colours = new string[rows, colums];
      
      try
      {
        hexColours = File.ReadAllLines(path);
      }

      catch(Exception e)
      {
        Console.WriteLine("oopsie daisies " + e.Message);
      }

      string[] temp = new string[colums];

      for(int i = 0; i < rows;i++)
      {
        temp = hexColours[i].Split(',');
        for(int j = 0; j< colums;j++)
        {
          colours[i,j] = temp[j];
        }
      }



      


      
  }
  
  
}