using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zakazivanje.Classes
{
    public static class Credentials
    {
        public static string emailPattern = @"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$";
        public static string passwordPattern = @"^(?=.*[A-Z])(?=.*\d)(?=.*[!@#$%^&*()-_+=])[A-Za-z\d!@#$%^&*()-_+=]{8,}$";
        public static string namePattern = @"^[A-Za-z]+(?: [A-Za-z]+)*$";
        public static string addressPattern = @"^[A-Za-z0-9\s.,\-]+$";
        public static string phonePattern = @"^\+381\d{9}$";
        public static string idPattern = @"^\d{13}$";
    }
}
