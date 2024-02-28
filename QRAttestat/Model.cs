using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;
using QRAttestat.Tools;

namespace QRAttestat
{
    public class Model : BaseVM
    {
        private DateTime date;

        public string X {  get; set; }
        public string Y { get; set; }
        public string Z { get; set; }
        public string Number {  get; set; }
        public DateTime Date 
        {
            get => date;
            set
            {
                date = value;
                Signal();
            }
        }
    }
}
