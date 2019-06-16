using System;
using System.Collections.Generic;
using System.Text;

namespace Application.HelperClasses
{
    public class ImageUpload
    {
        public static IEnumerable<string> AllowedExtensions => new List<string> { ".jpeg", ".jpg", ".png" };
    }
}
