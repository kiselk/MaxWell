using System;
using System.Collections.Generic;
using System.Text;

namespace MaxWell.Services
{
    public interface IMediaService
    {
        byte[] ResizeImage(byte[] imageData, float width, float height);
        byte[] ResizeImage(string imagePath, float width, float height);
    }
}
