using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UntxPrinter.Untx.LogUtil
{
    class P
    {

        public static bool isdebug=false;


        public static void t(string info){

            if (isdebug) {


                Console.WriteLine(">:" + info);
                try
                {
                    new Log().Write(info, MsgType.Information);
                }
                catch { }

            }

            
                

        }
    }
}
