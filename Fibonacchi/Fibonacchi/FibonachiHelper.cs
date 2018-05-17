﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fibonacchi
{
    public class FibonachiHelper
    {
        public static int WriteFibonacciSequenceToFile(string path, int count)
        {

            try
            {
                if (!File.Exists(path))
                {
                    List<int> res = FibonachiHelper.SequenceCalculation(count);
                    if (res != null)
                    {
                        string s = string.Join(" ", res);
                        File.WriteAllText(path, s);

                        return res.Count;
                    }

                }
                return 0;
            }
            catch (UnauthorizedAccessException)
            {
                return -1;
            }
            catch (ArgumentException)
            {
                return -1;
            }
        }

        public static bool CheckFibonachiSequence(string path)
        {
            if (!File.Exists(path))
                return false;
            string text = File.ReadAllText(path);
            string[] newSequnce = text.Split(' ');
            int A = newSequnce.Length;
            List<int> res = SequenceCalculation(A);
            List<int> comp = new List<int>();
            foreach (string str in newSequnce)
            {
                comp.Add(Int32.Parse(str));
            }
            return res.SequenceEqual(comp);
        }

        public static List<int> SequenceCalculation(int length)
        {
            if (length <= 0) return null;
            List<int> result = new List<int>();

            if (length == 1)
                result.Add(0);
            else
            {
                int First = 0;
                int Second = 1;
                int F = 0;
                result.Add(0);
                result.Add(1);
                {
                    for (int i = 3; i <= length; i++)
                    {
                        F = First + Second;
                        result.Add(F);
                        First = Second;
                        Second = F;
                    }
                }
            }
            return result;
        }
    }
}
