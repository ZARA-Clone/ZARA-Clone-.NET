namespace E_CommerceProject.WebAPI.Helper
{
    public class FileProvider : IFileProvider
    {
        public string validatImage(IFormFile file)
        {
            if (file == null)
            {
                return $"Image is not found";
            }
            if(file.Length > 1000_000)
            {
                return $"Image size is too large";
            }
            var allowedExtensions = new string[] { ".jpg", ".svg", ".png", ".jpeg" };
            var result =  Path.GetExtension(file.FileName).ToLower();
            if(!allowedExtensions.Contains(result) )
            {
                return $"Image extension is not valid";
            }
            return "Ok";
         }
    }
}
