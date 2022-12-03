using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Parsing
{
    public static class Caff
    {

        private const string dllPath = "caff.dll";

        public static void ParseCAFF(byte[] bytes, int length, byte[] folderName)
        {
            parseCAFF(bytes, length, folderName);
        }

        public static void GetCreatorLength(byte[] returnString)
        {
            getCreatorLength(returnString);
        }

        public static void GetCreator(byte[] returnString)
        {
            getCreator(returnString);
        }

        public static void GetYear(byte[] returnString)
        {
            getYear(returnString);
        }

        public static void GetMonth(byte[] returnString)
        {
            getMonth(returnString);
        }

        public static void GetDay(byte[] returnString)
        {
            getDay(returnString);
        }

        public static void GetHour(byte[] returnString)
        {
            getHour(returnString);
        }

        public static void GetMinute(byte[] returnString)
        {
            getMinute(returnString);
        }

        public static void GetNumberOfCiffs(byte[] returnString)
        {
            getNumberOfCiffs(returnString);
        }

        public static void GetDurationOfCiff(byte[] returnString, int index)
        {
            getDurationOfCiff(returnString, index);
        }

        public static void GetWidthOfCiff(byte[] returnString, int index)
        {
            getWidthOfCiff(returnString, index);
        }

        public static void GetHeightOfCiff(byte[] returnString, int index)
        {
            getHeightOfCiff(returnString, index);
        }

        public static void GetImageName(byte[] returnString, int index)
        {
            getImageName(returnString, index);
        }

        public static void GetCaptionAndTagsLength(byte[] returnString, int index)
        {
            getCaptionAndTagsLength(returnString, index);
        }

        public static void GetCaption(byte[] returnString, int index)
        {
            getCaption(returnString, index);
        }

        public static void GetTags(byte[] returnString, int index)
        {
            getTags(returnString, index);
        }

        public static void Dispose()
        {
            dispose();
        }

        [DllImport(dllPath)]
        private static extern void parseCAFF(byte[] bytes, int length, byte[] folderName);

        [DllImport(dllPath)]
        private static extern void getCreatorLength(byte[] returnString);

        [DllImport(dllPath)]
        private static extern void getCreator(byte[] returnString);

        [DllImport(dllPath)]
        private static extern void getYear(byte[] returnString);

        [DllImport(dllPath)]
        private static extern void getMonth(byte[] returnString);

        [DllImport(dllPath)]
        private static extern void getDay(byte[] returnString);

        [DllImport(dllPath)]
        private static extern void getHour(byte[] returnString);

        [DllImport(dllPath)]
        private static extern void getMinute(byte[] returnString);

        [DllImport(dllPath)]
        private static extern void getNumberOfCiffs(byte[] returnString);

        [DllImport(dllPath)]
        private static extern void getDurationOfCiff(byte[] returnString, int index);

        [DllImport(dllPath)]
        private static extern void getWidthOfCiff(byte[] returnString, int index);

        [DllImport(dllPath)]
        private static extern void getHeightOfCiff(byte[] returnString, int index);

        [DllImport(dllPath)]
        private static extern void getImageName(byte[] returnString, int index);

        [DllImport(dllPath)]
        private static extern void getCaptionAndTagsLength(byte[] returnString, int index);

        [DllImport(dllPath)]
        private static extern void getCaption(byte[] returnString, int index);

        [DllImport(dllPath)]
        private static extern void getTags(byte[] returnString, int index);

        [DllImport(dllPath)]
        private static extern void dispose();

    }
}
