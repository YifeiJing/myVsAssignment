
using System;
using System.IO.Ports;
using System.Windows;

namespace Example1
{
    class SerilPortFunc
    {
        #region Private members

        private SerialPort serialPort = new SerialPort();

        #endregion

        #region Public members

        /// <summary>
        /// The number of the port
        /// </summary>
        public string PortNumber { get; set; } = "COM1";

        /// <summary>
        /// The value of the baud
        /// </summary>
        public int Baud { get; set; } = 9600;

        /// <summary>
        /// The message to get and send
        /// </summary>
        public string Message { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Initialize the serial port with specific port number and baud
        /// </summary>
        public SerilPortFunc()
        {
            serialPort = new SerialPort(PortNumber, Baud);
            serialPort.DataReceived += (s,e) => Message = ReceiveData((s as SerialPort).ReadBufferSize);
        }

        #endregion

        #region Public functions

        /// <summary>
        /// The async function to send the data to the port
        /// </summary>
        /// <param name="m">The message</param>
        public async void SendDataAsyn(string m)
        {
            await Application.Current.Dispatcher.BeginInvoke(new Action (() => serialPort.Write(m)));
        }

        /// <summary>
        /// Simple version to send the data to the port
        /// </summary>
        /// <param name="m"></param>
        public void SendData(string m)
        {
            serialPort.Write(m);
        }

        /// <summary>
        /// Get the data from the port and convert it to string
        /// </summary>
        /// <param name="bufsize"></param>
        /// <returns></returns>
        public string ReceiveData(int bufsize)
        {
            char[] buffer = new char[bufsize];
            serialPort.Read(buffer, 0, bufsize);

            return new string(buffer);
        }
        #endregion
    }
}
