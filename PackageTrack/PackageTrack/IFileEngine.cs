using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace PackageTrack
{
    public interface IFileEngine
    {
        Task WriteTextAsync(string storedText, string text);
        Task<string> ReadTextAsync(string storedText);
        Task<IEnumerable<string>> GetFilesAsync();
    }
}
