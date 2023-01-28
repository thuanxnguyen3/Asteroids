using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using System.Text;

public class ToyotaConnect : MonoBehaviour
{
    
    static void Main()
    {
        using (StreamReader reader = new StreamReader(Console.OpenStandardInput()))
        while (!reader.EndOfStream)
            {
                string line = reader.ReadLine();
                int result = 0;
                int[] num = new int[line.Length]; 
                for(int i = 0; i < num.Length; i++)
                {
                    num[0] = Integer.parseInt(line);
                }
                for(int x = num.Length; x >= 1; x--)
                {
                    int s = num[x] - num[0];
                    int temp = Math.Abs(s);
                    result += temp;
                }
                Console.WriteLine(result);
            }
     }
}
