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
      private static readonly string _directory = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).FullName  + "\\default"+ ".png");


      public async Task AddAsync(Image image, string path)
      {
         await image.SaveAsPngAsync(path);
         await Task.CompletedTask;
      }

      public async Task DeleteAsync(string path)
      {
         File.Delete(path);
         await Task.CompletedTask;
      }

      public async Task<FileStream> GetAsync(string path)
      {
         if (!File.Exists(path))
         {
            path = _directory;
         }
         var @image = await Task.FromResult(File.OpenRead(path));
         return image;
      }

   }
}
