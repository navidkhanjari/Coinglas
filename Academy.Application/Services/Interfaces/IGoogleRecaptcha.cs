using System.Threading.Tasks;

namespace Academy.Application.Services.Interfaces
{
   public interface IGoogleRecaptcha
    {
        Task<bool> IsConfirmed();
    }
}
