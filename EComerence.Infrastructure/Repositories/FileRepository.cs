using EComerence.Core.Domain;
using EComerence.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using SixLabors.ImageSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EComerence.Infrastructure.Repositories
{
    public class FileRepository : IFileRepository
    {

        public async Task AddAsync(Image image, string path, Guid id)
        {
            await image.SaveAsPngAsync(Path.Combine(path + "\\uploads\\" + id + ".png"));
            await Task.CompletedTask;
        }

        public async Task DeleteAsync(string path)
        {
            File.Delete(path);
            await Task.CompletedTask;
        }

        public async Task<FileStream> GetAsync(string path)
        {
            var @image = await Task.FromResult(File.OpenRead(path));
            return image;
        }

    }
}
