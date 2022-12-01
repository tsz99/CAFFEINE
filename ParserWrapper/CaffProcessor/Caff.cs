using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Parsing
{
    public class Caff
    {


        private const string dllPath = "caff.dll";

        [DllImport(dllPath)]
        public static extern void parseCAFF(byte[] bytes, int length, byte[] folderName);

        [DllImport(dllPath)]
        public static extern void getCreatorLength(byte[] returnString);

        [DllImport(dllPath)]
        public static extern void getCreator(byte[] returnString);

        [DllImport(dllPath)]
        public static extern void getYear(byte[] returnString);

        [DllImport(dllPath)]
        public static extern void getMonth(byte[] returnString);

        [DllImport(dllPath)]
        public static extern void getDay(byte[] returnString);

        [DllImport(dllPath)]
        public static extern void getHour(byte[] returnString);

        [DllImport(dllPath)]
        public static extern void getMinute(byte[] returnString);

        [DllImport(dllPath)]
        public static extern void getNumberOfCiffs(byte[] returnString);

        [DllImport(dllPath)]
        public static extern void getDurationOfCiff(byte[] returnString, int index);

        [DllImport(dllPath)]
        public static extern void getWidthOfCiff(byte[] returnString, int index);

        [DllImport(dllPath)]
        public static extern void getHeightOfCiff(byte[] returnString, int index);

        [DllImport(dllPath)]
        public static extern void getImageName(byte[] returnString, int index);

        [DllImport(dllPath)]
        public static extern void getCaptionAndTagsLength(byte[] returnString, int index);

        [DllImport(dllPath)]
        public static extern void getCaption(byte[] returnString, int index);

        [DllImport(dllPath)]
        public static extern void getTags(byte[] returnString, int index);

        [DllImport(dllPath)]
        public static extern void dispose();

    }
}
