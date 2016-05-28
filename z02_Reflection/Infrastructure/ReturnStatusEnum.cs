using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace z02_Reflection.Infrastructure
{
    public sealed class ReturnStatusEnum
    {
        private readonly string _code;
        private readonly string _message;

        /// <summary>
        /// 取得狀態代碼。
        /// </summary>
        /// <value>狀態代碼。</value>
        public string Code { get { return this._code; } }

        /// <summary>
        /// 取得狀態訊息。
        /// </summary>
        /// <value>
        /// 狀態訊息。
        /// </value>
        public string Message { get { return this._message; } }

        /// <summary>
        /// 取得成功的回傳狀態。
        /// </summary>
        /// <value>成功的回傳狀態。</value>
        public static ReturnStatusEnum Success { get { return new ReturnStatusEnum("200", "Success"); } }

        /// <summary>
        /// 取得無資料的回傳狀態。
        /// </summary>
        /// <value>無資料的回傳狀態。</value>
        public static ReturnStatusEnum NoData { get { return new ReturnStatusEnum("404", "No Data"); } }
        public static ReturnStatusEnum NoData1 { get { return new ReturnStatusEnum("404", "No Data"); } }
        public static ReturnStatusEnum NoData2 { get { return new ReturnStatusEnum("404", "No Data"); } }
        public static ReturnStatusEnum NoData3 { get { return new ReturnStatusEnum("404", "No Data"); } }
        public static ReturnStatusEnum NoData4 { get { return new ReturnStatusEnum("404", "No Data"); } }
        public static ReturnStatusEnum NoData5 { get { return new ReturnStatusEnum("404", "No Data"); } }
        public static ReturnStatusEnum NoData6 { get { return new ReturnStatusEnum("404", "No Data"); } }
        public static ReturnStatusEnum NoData7 { get { return new ReturnStatusEnum("404", "No Data"); } }
        public static ReturnStatusEnum NoData8 { get { return new ReturnStatusEnum("404", "No Data"); } }
        public static ReturnStatusEnum NoData9 { get { return new ReturnStatusEnum("404", "No Data"); } }
        public static ReturnStatusEnum NoData0 { get { return new ReturnStatusEnum("404", "No Data"); } }
        public static ReturnStatusEnum NoData11 { get { return new ReturnStatusEnum("404", "No Data"); } }
        public static ReturnStatusEnum NoData12 { get { return new ReturnStatusEnum("404", "No Data"); } }
        public static ReturnStatusEnum NoData13 { get { return new ReturnStatusEnum("404", "No Data"); } }
        public static ReturnStatusEnum NoData14 {get { return new ReturnStatusEnum("404", "No Data"); } }
        public static ReturnStatusEnum NoData15 { get { return new ReturnStatusEnum("404", "No Data"); } }
        public static ReturnStatusEnum NoData16 { get { return new ReturnStatusEnum("404", "No Data"); } }
        public static ReturnStatusEnum NoData17{ get { return new ReturnStatusEnum("404", "No Data"); } }
        public static ReturnStatusEnum NoData18{ get { return new ReturnStatusEnum("404", "No Data"); } }
        public static ReturnStatusEnum NoData19 { get { return new ReturnStatusEnum("404", "No Data"); } }
        public static ReturnStatusEnum NoData20 { get { return new ReturnStatusEnum("404", "No Data"); } }
        public static ReturnStatusEnum NoData21 { get { return new ReturnStatusEnum("404", "No Data"); } }
        public static ReturnStatusEnum NoData22 { get { return new ReturnStatusEnum("404", "No Data"); } }
        public static ReturnStatusEnum NoData23 { get { return new ReturnStatusEnum("404", "No Data"); } }
        public static ReturnStatusEnum NoData24 { get { return new ReturnStatusEnum("404", "No Data"); } }
        public static ReturnStatusEnum NoData25 { get { return new ReturnStatusEnum("404", "No Data"); } }
        public static ReturnStatusEnum NoData26 { get { return new ReturnStatusEnum("404", "No Data"); } }
        public static ReturnStatusEnum NoData27 { get { return new ReturnStatusEnum("404", "No Data"); } }
        public static ReturnStatusEnum NoData28 { get { return new ReturnStatusEnum("404", "No Data"); } }
        public static ReturnStatusEnum NoData29 { get { return new ReturnStatusEnum("404", "No Data"); } }
        public static ReturnStatusEnum NoData30 { get { return new ReturnStatusEnum("404", "No Data"); } }

        public static bool ReflectionGet()
        {
            PropertyInfo[] bb = typeof(ReturnStatusEnum)
                                .GetProperties(BindingFlags.Public | BindingFlags.Static);
            return bb.Any(x => ((ReturnStatusEnum)x.GetValue(null, null)).Code == "999");
        }

        public static bool ListGet()
        {
            List<string> l = new List<string>() {
                "200", "404", "404", "404", "404", "404", "404", "404", "404", "404",
                "404", "404", "404", "404", "404", "404", "404", "404", "404", "404",
                "404", "404", "404", "404", "404", "404", "404", "404", "404", "404",
                "404",};

            return l.Any(x => x == "999");
        }


        private ReturnStatusEnum(string code, string message)
        {
            this._code = code;
            this._message = message;
        }
    }
}
