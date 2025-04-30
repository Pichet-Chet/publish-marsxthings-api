using System;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;

namespace MarsXApps.Module.Environment
{
	public class Helper
	{
        public static DateTime GetDateTimeByGMT(int value)
        {
            DateTimeOffset currentTimeUtc = DateTimeOffset.UtcNow;

            int gmtOffset = value;

            TimeSpan offset = TimeSpan.FromHours(gmtOffset);

            DateTimeOffset currentTimeGmt = currentTimeUtc.ToOffset(offset);

            return currentTimeGmt.DateTime;
        }

        public static void TransferData_ClassA_to_ClassB<A, B>(A TempleteA, ref B TempleteB, List<string> lst_NotTransferColumn = null)
        {
            try
            {
                if (lst_NotTransferColumn == null) lst_NotTransferColumn = new List<string>();

                foreach (PropertyInfo item in TempleteB.GetType().GetProperties())
                {
                    //string Value = string.Empty;
                    if (!lst_NotTransferColumn.Contains(item.Name)) //check not transfer data column
                    {
                        PropertyInfo PropA = TempleteA.GetType().GetProperty(item.Name);
                        if (PropA != null)
                        {
                            object tmp = PropA.GetValue(TempleteA, BindingFlags.GetProperty, null, null, null);
                            if (item.CanWrite)
                            {
                                item.SetValue(TempleteB, tmp, null);
                            }
                        }
                    }
                }
            }
            catch
            {
                throw;
            }
        }
    }
}

