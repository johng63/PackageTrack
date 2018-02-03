using System;
using System.Collections.Generic;
using System.Text;

using System.Linq;

using System.Threading.Tasks;
using System.IO;
using Xamarin.Forms;

namespace PackageTrack
{
    class FileEngine : IFileEngine
    {
        IFileEngine fileEngine = DependencyService.Get<IFileEngine>();


        public Task<IEnumerable<string>> GetFilesAsync()
        {
            return fileEngine.GetFilesAsync();
        }

        public Task<string> ReadTextAsync(string storedText)
        {
            return fileEngine.ReadTextAsync(storedText);
        }

        public Task WriteTextAsync(string storedText, string text)
        {
            return fileEngine.WriteTextAsync(storedText, text);
        }
    }
}
