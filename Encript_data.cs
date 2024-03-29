﻿using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace WebApi
{
    public class Encript_data
    {
        public static async Task<string> Encript_(string encrypt)
        {
            // Text to encrypt and decrypt.
            //var text = "12345678";
            byte[] decryptedBytes;

            // Use OAEP padding (PKCS#1 v2).
            bool doOaepPadding = true;
            // RSA 512-bit key: Public (Modulus), Private (D) and CRT (P, Q, DP, DQ, InverseQ).
            string xmlParams = "<RSAKeyValue><Modulus>qpckDXTWK8imuKMozgNexHnABZLqZ+iI55uNkZ5y1R5eDceIrOEfWUd5V+KIkq+5QepL9upDdnFp4PWUqj++dVR7DcuFMqFQ9DSERsRUr/VxyZ7pDn0xjAPhAmeoe0ffoVnrJAqbhYE5jccsg5+78vrpGPicYH1E7Y+gxq01PuM=</Modulus><Exponent>AQAB</Exponent><P>2aLcuWDVM++oWb75p9eSO6zqmv6K190rAJ4r1SNpcv4FpajhO6+0H1TSeD0Rx3XkNcmPIEVLTom6jhasmSmFdw==</P><Q>yKlFg8RoxzJ7khGKCj6qcObCYlNxaCjiPF5c3TBn5VXaByElJmPCEiODZgbI8FntQE92mZEiHjp/bjb6Zvyc9Q==</Q><DP>A67K12Q5F2Dl02b06I8wTUw2yBqolNCMSr1idn/b5/M+ezgpX44wmRshWKGH7H0lOHfJsT0a8iBIhOEDWLAoLw==</DP><DQ>JgDJBZehMHjDJnrj5eTQaumJTw32oH99uWk1tT6BrtF/pXIFkyu5ia3oKN6IF90wLcne8F6oU4lIsRsAeZjGMQ==</DQ><InverseQ>nA+wqIY5OPnclY2YqW5K4wTpVjZq4s43eKrCwoSKx03aL/oMxMUxpUkQgB/MhEmD78wvZmPCL6dLU1rMWRsxlw==</InverseQ><D>pQZ3Wwkm0s5V8pHsPHdoKvt4tius1X5PSnbhmfhFMEQjSoM3hb52XCDXkxxTcEvMFKb6e8+eGauXeIc6HQRzUmsSFs/xpbNJ4DYkqFYy0cWxENOFWKCSPh9cER1I3OgeM+su+Qj7LozB5ztKL3PEq5xWyfdU+VGCn7WqmR8KWkk=</D></RSAKeyValue>";
            //StreamReader readerxml = new StreamReader("C:\\temp\\rsakey");
            //var responsexmldata = readerxml.ReadToEnd();
            //string xmlParams = responsexmldata.ToString();
            //readerxml.Dispose();
            // ------------------------------------------------
            // RSA Keys
            // ------------------------------------------------
            var rsa = new RSACryptoServiceProvider();
            // Import parameters from XML string.
            rsa.FromXmlString(xmlParams);
            // Export RSA key to RSAParameters and include:
            //    false - Only public key required for encryption.
            //    true  - Private key required for decryption.
            // Export parameters and include only Public Key (Modulus + Exponent) required for encryption.
            var rsaParamsPublic = rsa.ExportParameters(false);
            // Export Public Key (Modulus + Exponent) and include Private Key (D) required for decryption.
            var rsaParamsPrivate = rsa.ExportParameters(true);
            //rsa.Dispose();
            // ------------------------------------------------
            // Encrypt
            // ------------------------------------------------
            decryptedBytes = Encoding.UTF8.GetBytes(encrypt.ToString());
            // Create a new instance of RSACryptoServiceProvider.
            //rsa = new RSACryptoServiceProvider();
            // Import the RSA Key information.
            rsa.ImportParameters(rsaParamsPublic);
            // Encrypt byte array.

            return await Task.Run(() =>
             {
                 return SomeLongRunningMethodThatReturnsAString(decryptedBytes, doOaepPadding, rsa);
             });



        }

        public static string SomeLongRunningMethodThatReturnsAString(byte[] decryptedBytes, bool doOaepPadding, RSACryptoServiceProvider rsa)
        {
            byte[] encryptedBytes;
            string encryptedString;
            encryptedBytes = rsa.Encrypt(decryptedBytes, doOaepPadding);
            // Convert bytes to base64 string.
            encryptedString = Convert.ToBase64String(encryptedBytes);
            Console.WriteLine(encryptedString);
            rsa.Dispose();
            return encryptedString.ToString();
        }
    }
}
