using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.IO;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void FibonachiSequenceGenerator_GenerateSequenceWith7Elements_0112358()
        {
            List<int> expectedSequence = new List<int> { 0, 1, 1, 2, 3, 5, 8 };
            List<int> actualSequence = Fibonacchi.FibonachiHelper.SequenceCalculation(7);
            for (int i = 0; i < expectedSequence.Count; i++)
            {
                Assert.AreEqual(expectedSequence[i], actualSequence[i]);
            }
        }
        [TestMethod]
        public void WriteSequenceToFile_WriteSequenceWith6Elements_FileIsCreated011235()
        {
            List<int> expectedSequence = new List<int> { 0, 1, 1, 2, 3, 5 };
            string expectedSequenceString = string.Join(" ", expectedSequence);
            if (File.Exists("D:\\fib"))
            {
                File.Delete("D:\\fib");
            }

            Fibonacchi.FibonachiHelper.WriteFibonacciSequenceToFile("D:\\fib", 6);
            Assert.IsTrue(File.Exists("D:\\fib"));
            Assert.AreEqual(expectedSequenceString, File.ReadAllText("D:\\fib"));
        }
        [TestMethod]
        public void ReadFromFile_FileCanBeReaded()
        {
            string sequenceToWrite = "0 1 1 2 3 5 8 13";
            File.WriteAllText("D:\\fbnc", sequenceToWrite);
            Assert.IsTrue(Fibonacchi.FibonachiHelper.CheckFibonachiSequence("D:\\fbnc"));
        }

    }
}
