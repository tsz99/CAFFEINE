using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Math;

namespace Parsing
{
    public static class CaffProcessor
    {
        public static CaffResult ParseCaff(byte[] caff, string folderName)
        {
            Caff.ParseCAFF(caff, caff.Length, Encoding.ASCII.GetBytes(folderName));

            byte[] returnBuffer;

            returnBuffer = new byte[calculateSizeFromByteSize(8)];
            Caff.GetCreatorLength(returnBuffer);
            var creatorLength = getIntFromBuffer(returnBuffer);

            returnBuffer = new byte[creatorLength];
            Caff.GetCreator(returnBuffer);
            var creator = getStringFromBuffer(returnBuffer);

            returnBuffer = new byte[calculateSizeFromByteSize(2)];
            Caff.GetYear(returnBuffer);
            var year = getIntFromBuffer(returnBuffer);

            returnBuffer = new byte[calculateSizeFromByteSize(1)];
            Caff.GetMonth(returnBuffer);
            var month = getIntFromBuffer(returnBuffer);

            returnBuffer = new byte[calculateSizeFromByteSize(1)];
            Caff.GetDay(returnBuffer);
            var day = getIntFromBuffer(returnBuffer);

            returnBuffer = new byte[calculateSizeFromByteSize(1)];
            Caff.GetHour(returnBuffer);
            var hour = getIntFromBuffer(returnBuffer);

            returnBuffer = new byte[calculateSizeFromByteSize(1)];
            Caff.GetMinute(returnBuffer);
            var minute = getIntFromBuffer(returnBuffer);

            returnBuffer = new byte[calculateSizeFromByteSize(8)];
            Caff.GetNumberOfCiffs(returnBuffer);
            var numberOfCiffs = getIntFromBuffer(returnBuffer);

            var ciffResults = new List<CiffResult>();
            for (int i = 0; i < numberOfCiffs; i++)
            {
                returnBuffer = new byte[calculateSizeFromByteSize(8)];
                Caff.GetDurationOfCiff(returnBuffer, 0);
                var duration = getIntFromBuffer(returnBuffer);

                returnBuffer = new byte[calculateSizeFromByteSize(8)];
                Caff.GetWidthOfCiff(returnBuffer, 0);
                var width = getIntFromBuffer(returnBuffer);

                returnBuffer = new byte[calculateSizeFromByteSize(8)];
                Caff.GetHeightOfCiff(returnBuffer, 0);
                var height = getIntFromBuffer(returnBuffer);

                returnBuffer = new byte[8];
                Caff.GetImageName(returnBuffer, 0);
                var imageName = getStringFromBuffer(returnBuffer).TrimEnd('\0');

                returnBuffer = new byte[calculateSizeFromByteSize(8)];
                Caff.GetCaptionAndTagsLength(returnBuffer, 0);
                var captionAndTagsLength = getIntFromBuffer(returnBuffer);

                returnBuffer = new byte[captionAndTagsLength];
                Caff.GetCaption(returnBuffer, 0);
                var caption = getStringFromBuffer(returnBuffer).TrimEnd('\0');

                returnBuffer = new byte[captionAndTagsLength];
                Caff.GetTags(returnBuffer, 0);
                var tags = getStringFromBuffer(returnBuffer);
                var tagArray = tags.Split('\n');

                var ciffResult = new CiffResult()
                {
                    Duration = duration,
                    Width = width,
                    Height = height,
                    ImageName = imageName,
                    Caption = caption,
                    Tags = tagArray.Select(tag => tag.TrimEnd('\0')).ToList()
                };
                ciffResults.Add(ciffResult);
            }

            var caffResult = new CaffResult()
            {
                Creator = creator,
                Year = year,
                Month = month,
                Day = day,
                Hour = hour,
                Minute = minute,
                Ciffs = ciffResults
            };

            Caff.Dispose();

            return caffResult;
        }

        private static long calculateSizeFromByteSize(int size)
        {
            return ((long)Log10(Pow(255, size))) + 1;
        }

        private static int getIntFromBuffer(byte[] buffer)
        {
            return Int32.Parse(Encoding.ASCII.GetString(buffer));
        }

        private static string getStringFromBuffer(byte[] buffer)
        {
            return Encoding.ASCII.GetString(buffer);
        }
    }
}
