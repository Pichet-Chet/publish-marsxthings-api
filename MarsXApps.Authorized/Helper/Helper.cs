using System;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;

namespace MarsXApps.Module.Authorized
{
    public class Helper
    {
        //public static Guid GenerateGuid()
        //{
        //    Guid guid = Guid.NewGuid();

        //    return guid;
        //}

        //public static string GenerateSecretKey()
        //{
        //    Guid guid = Guid.NewGuid();

        //    string shortGuid = guid.ToString().Replace("-", "");

        //    return shortGuid;
        //}

        //public static DateTime DateTimeNow()
        //{
        //    DateTime dateTime = DateTime.Now;

        //    return dateTime;
        //}

        //public static DateTime GetDateTimeByGMT(int value)
        //{
        //    DateTimeOffset currentTimeUtc = DateTimeOffset.UtcNow;

        //    int gmtOffset = value;

        //    TimeSpan offset = TimeSpan.FromHours(gmtOffset);

        //    DateTimeOffset currentTimeGmt = currentTimeUtc.ToOffset(offset);

        //    return currentTimeGmt.DateTime;
        //}

        //public static void TransferData_ClassA_to_ClassB<A, B>(A TempleteA, ref B TempleteB, List<string> lst_NotTransferColumn = null)
        //{
        //    try
        //    {
        //        if (lst_NotTransferColumn == null) lst_NotTransferColumn = new List<string>();
        //        foreach (PropertyInfo item in TempleteB.GetType().GetProperties())
        //        {
        //            //string Value = string.Empty;
        //            if (!lst_NotTransferColumn.Contains(item.Name)) //check not transfer data column
        //            {
        //                PropertyInfo PropA = TempleteA.GetType().GetProperty(item.Name);
        //                if (PropA != null)
        //                {
        //                    object tmp = PropA.GetValue(TempleteA, BindingFlags.GetProperty, null, null, null);
        //                    if (item.CanWrite)
        //                    {
        //                        item.SetValue(TempleteB, tmp, null);
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        //_ClasName.Error(ex.Message);
        //    }
        //}

        //public static string HashPassword(string password, byte[] salt)
        //{
        //    using (var sha256 = new SHA256Managed())
        //    {
        //        byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
        //        byte[] saltedPassword = new byte[passwordBytes.Length + salt.Length];

        //        // Concatenate password and salt
        //        Buffer.BlockCopy(passwordBytes, 0, saltedPassword, 0, passwordBytes.Length);
        //        Buffer.BlockCopy(salt, 0, saltedPassword, passwordBytes.Length, salt.Length);

        //        // Hash the concatenated password and salt
        //        byte[] hashedBytes = sha256.ComputeHash(saltedPassword);

        //        // Concatenate the salt and hashed password for storage
        //        byte[] hashedPasswordWithSalt = new byte[hashedBytes.Length + salt.Length];
        //        Buffer.BlockCopy(salt, 0, hashedPasswordWithSalt, 0, salt.Length);
        //        Buffer.BlockCopy(hashedBytes, 0, hashedPasswordWithSalt, salt.Length, hashedBytes.Length);

        //        return Convert.ToBase64String(hashedPasswordWithSalt);
        //    }
        //}

        //public static byte[] GenerateSalt()
        //{
        //    using (var rng = new RNGCryptoServiceProvider())
        //    {
        //        byte[] salt = new byte[16]; // Adjust the size based on your security requirements
        //        rng.GetBytes(salt);
        //        return salt;
        //    }
        //}
    }
}

