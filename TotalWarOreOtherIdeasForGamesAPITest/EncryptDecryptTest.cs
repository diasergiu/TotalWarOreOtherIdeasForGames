using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using TotalWarOreOtherIdeasForGames.Services;

namespace TotalWarOreOtherIdeasForGamesAPITest
{
    [TestClass]
    public class EncryptDecryptTest
    {
        [TestMethod]
        public void TestCorectEncryption()
        {
            string password = "1234";

            byte[] resultEncryption = EncryptDecryptService.EncryptString(password);
            // i dont know how to get the byte code from the first 
            String resultDecryption = EncryptDecryptService.DectyptByte(resultEncryption);
            Assert.AreEqual(resultDecryption, password);
        }
    }
}
