using System.Text;

namespace ASM2_KSTH.Helpers
{
    public class MyUtil
    {
        public static string GenerateRandomKey(int length = 5)
        {
            var parttern = @"qazwsxedcrfvtgbyhnujmiklopQAZWSXEDCRFVTGBYHNUJMIKLOP!";
            var sb = new StringBuilder();
            var rd = new Random();
            for (int i = 0; i < length; i++)
            {
                sb.Append(parttern[rd.Next(0, parttern.Length)]);
            }

            return sb.ToString();
        }
    }
}
