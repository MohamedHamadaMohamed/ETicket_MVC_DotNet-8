namespace ETicket.Utility.Utilities
{
    public static class Utility
    {
        public static string UploadFile(IFormFile file, string FolderName)
        {
            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "./wwwroot\\Files\\Images", FolderName, fileName);
            using (var stream = System.IO.File.Create(filePath))
            {
                file.CopyTo(stream);
            }
            return fileName;
        }
        public static void DeleteFile(string fileName, string FolderName)
        {
            var oldPath = Path.Combine(Directory.GetCurrentDirectory(), "./wwwroot\\Files\\Images", FolderName);
            if (System.IO.File.Exists(oldPath))
            {
                System.IO.File.Delete(oldPath);
            }
        }


    }
}
