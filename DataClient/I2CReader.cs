using DataClient.Interfaces;
using DataClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace DataClient
{
    public class I2CReader: IGridEyeReader
    {

        [DllImport("libc.so.6", EntryPoint = "open")]
        public static extern int Open(string fileName, int mode);

        [DllImport("libc.so.6", EntryPoint = "ioctl", SetLastError = true)]
        private extern static int Ioctl(int fd, int request, int data);

        [DllImport("libc.so.6", EntryPoint = "read", SetLastError = true)]
        internal static extern int Read(int handle, byte[] data, int length);

        [DllImport("libc.so.6", EntryPoint = "pread", SetLastError = true)]
        internal static extern int Read(int handle, byte[] data, int length, int offset);

        private static int OPEN_READ_WRITE = 2;
        private static int I2C_Slave = 0x0703;
        private static int GRID_EYE_ADDRESS = 0x69;
        private int i2cBushandle;

        private byte[] gridEyeData = new byte[256];

        public I2CReader()
        {
            OpenConnection();
        }

        public int OpenConnection()
        {
            i2cBushandle = Open("/dev/i2c-1", OPEN_READ_WRITE);
            return Ioctl(i2cBushandle, I2C_Slave, GRID_EYE_ADDRESS);
        }

        public byte[] ReadAllBytesFromGridEye()
        {
            gridEyeData = new byte[256];
            Read(i2cBushandle, gridEyeData, gridEyeData.Length);
            return gridEyeData;
        }

        /// <summary>
        /// Get temperature by Pixel 
        /// </summary>
        /// <param name="pixelNumber"> 1 - 64 look at documetation https://cdn-learn.adafruit.com/assets/assets/000/043/261/original/Grid-EYE_SPECIFICATIONS%28Reference%29.pdf </param>
        /// <returns></returns>
        public double GetPixelTemperature(int pixelNumber)
        {
            if (pixelNumber >= 1 && pixelNumber <= 64)
            {
                var data = ReadAllBytesFromGridEye();
                return BitConverter.ToInt16(data, 0x80 + (pixelNumber - 1) * 2) * 0.25;
            }
            else
            {
                throw new ArgumentException("pixelNumber is out of range 1-64");
            }
        }

        public double GetThermistorTemperature()
        {
            return BitConverter.ToInt16(gridEyeData, 0x0E) * 0.0625;
        }

        /// <summary>
        /// Get ALL pixels array temperature
        /// </summary>
        /// <returns></returns>
        public double[] GetPixelsTemperature()
        {
            var data = ReadAllBytesFromGridEye();
            var temperature = new double[64];

            for (var i = 0; i < 64; ++i)
            {
                temperature[i] = BitConverter.ToInt16(data, 0x80 + i * 2) * 0.25;
            }
            return temperature;
        }

        public void WriteTemperatureMatrix()
        {
            var matrix = GetPixelsTemperature();
            var builder = new StringBuilder(256);
            for (var i = 0; i < 8; ++i)
            {
                for (var j = 0; j < 8; ++j)
                {
                    builder.Append($" {matrix[8 * i + j].ToString("N0")}");
                }
                builder.AppendLine();
            }

            Console.Write(builder.ToString());
            Console.SetCursorPosition(0, Console.CursorTop - 8);
        }

        public GridEyeDataModel GetCurrentState()
        {
            //change array from 64-1 to 1-64
            return new GridEyeDataModel
            {
                Temperature = GetPixelsTemperature().Reverse().ToArray(),
                ThermistorTemperature = GetThermistorTemperature(),
                TimeStamp = DateTime.Now
            };
        }
    }
}
