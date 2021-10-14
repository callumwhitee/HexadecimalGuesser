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
      int colums = 2;
      int rows = hexColours.Length;
      string[] hexColours = File.ReadAllLines(path);
      string[colums,hexColours.Length] colours;
      for(int i = 0; i < rows; i++)
      {
          for(int j = 0; j < colums;j++)
          {
              string[] tempArray = hexColours[i];
              colours[i,j] = tempArray[0,1];
              Console.WriteLine(colours[i]);
              Console.WriteLine(colours[j]);
          }
      }
  }
  
}