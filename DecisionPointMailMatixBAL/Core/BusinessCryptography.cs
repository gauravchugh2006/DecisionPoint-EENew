

// ******************************************************************************************************************************
//                                                 class:BusinessCryptography.cs
// ******************************************************************************************************************************
//                                     M O D I F I C A T I O N   M I L E S T O N E S
// ------------------------------------------------------------------------------------------------------------------------------
//  Date             |  Created By          | Description 
// --------+-------------------+-------------------------------------------------------------------------------------------------
// Oct. 13, 2014     |Sumit Saurav     | This class holds the interaction logic for BusinessCryptography.cs
// ******************************************************************************************************************************

using System;
namespace DecisionPointMailMatixBAL.Core
{
    public class BusinessCryptography
    {
        /// <summary>
        /// Methods to encrypt string
        /// </summary>
        /// <param name="data">data</param>
        /// <CreatedBy>Sumit Saurav</CreatedBy>
        /// <CreatedDate>Oct 13 2014</CreatedDate>
        /// <returns>string</returns>
        public static string base64Encode(string data)
        {
            try
            {
                byte[] encData_byte = new byte[data.Length];
                encData_byte = System.Text.Encoding.UTF8.GetBytes(data);
                string encodedData = Convert.ToBase64String(encData_byte);
                return encodedData;
            }
            catch (Exception e)
            {
                throw new Exception("Error in base64Encode" + e.Message);
            }
        }

        /// <summary>
        /// method to decrypt encrypted string
        /// </summary>
        /// <param name="sData">sData</param>
        ///  <CreatedBy>Sumit Saurav</CreatedBy>
        /// <CreatedDate>Oct 13 2014</CreatedDate>
        /// <returns>string</returns>
        public static string base64Decode2(string sData)
        {
            System.Text.UTF8Encoding encoder = new System.Text.UTF8Encoding();
            System.Text.Decoder utf8Decode = encoder.GetDecoder();
            byte[] todecode_byte = Convert.FromBase64String(sData);
            int charCount = utf8Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
            char[] decoded_char = new char[charCount];
            utf8Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
            string result = new String(decoded_char);
            return result;
        }
    }
}
