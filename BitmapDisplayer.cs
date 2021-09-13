using System.Drawing;
using System.IO;

namespace RaytracingInOneWeekend {
    class BitmapDisplayer {
        public static void WriteToFile(Bitmap bitmap) {
            bitmap.Save("out.png");
        }
    }
}