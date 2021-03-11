using EComerence.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SixLabors.ImageSharp;
using System.IO;

namespace EComerence.Core.Repositories
{
    public interface IFileRepository
    {
        Task<FileStream> GetAsync(string path);
        Task AddAsync(Image image,string path, Guid id);

        Task DeleteAsync(string path);
    }
}
