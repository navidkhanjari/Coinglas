using System;

namespace Academy.Application.Utils.Generators
{
   public class CodeGenerator
    {
        public static string GenerateUniqueCode()
        {
            return Guid.NewGuid().ToString().Replace("-", "");
        }
    }
}
