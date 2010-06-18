using System;
using System.Collections.Generic;
using System.Text;
using Jint.Delegates;

namespace Jint.Native
{
    [Serializable]
    public class JsMathConstructor : JsObject
    {
        public IGlobal Global { get; set; }

        public JsMathConstructor(IGlobal global)
        {
            Global = global;

            #region Functions
            this["abs"] = new ClrFunction(new Func<double, JsNumber>((d) => { return Global.NumberClass.New(Math.Abs(d)); }));
            this["acos"] = new ClrFunction(new Func<double, JsNumber>((d) => { return Global.NumberClass.New(Math.Acos(d)); }));
            this["asin"] = new ClrFunction(new Func<double, JsNumber>((d) => { return Global.NumberClass.New(Math.Asin(d)); }));
            this["atan"] = new ClrFunction(new Func<double, JsNumber>((d) => { return Global.NumberClass.New(Math.Atan(d)); }));
            this["atan2"] = new ClrFunction(new Func<double, double, JsNumber>((y, x) => { return Global.NumberClass.New(Math.Atan2(y, x)); }));
            this["ceil"] = new ClrFunction(new Func<double, JsNumber>((d) => { return Global.NumberClass.New(Math.Ceiling(d)); }));
            this["cos"] = new ClrFunction(new Func<double, JsNumber>((d) => { return Global.NumberClass.New(Math.Cos(d)); }));
            this["exp"] = new ClrFunction(new Func<double, JsNumber>((d) => { return Global.NumberClass.New(Math.Exp(d)); }));
            this["floor"] = new ClrFunction(new Func<double, JsNumber>((d) => { return Global.NumberClass.New(Math.Floor(d)); }));
            this["log"] = new ClrFunction(new Func<double, JsNumber>((d) => { return Global.NumberClass.New(Math.Log(d)); }));
            this["max"] = new ClrFunction(new Func<double, double, JsNumber>((a, b) => { return Global.NumberClass.New(Math.Max(a, b)); }));
            this["min"] = new ClrFunction(new Func<double, double, JsNumber>((a, b) => { return Global.NumberClass.New(Math.Min(a, b)); }));
            this["pow"] = new ClrFunction(new Func<double, double, JsNumber>((a, b) => { return Global.NumberClass.New(Math.Pow(a, b)); }));
            this["random"] = global.FunctionClass.New(new Func<double>(() => { return new Random(DateTime.Now.Millisecond).NextDouble(); }));
            this["round"] = new ClrFunction(new Func<double, JsNumber>((d) => { return Global.NumberClass.New(Math.Round(d)); }));
            this["sin"] = new ClrFunction(new Func<double, JsNumber>((d) => { return Global.NumberClass.New(Math.Sin(d)); }));
            this["sqrt"] = new ClrFunction(new Func<double, JsNumber>((d) => { return Global.NumberClass.New(Math.Sqrt(d)); }));
            this["tan"] = new ClrFunction(new Func<double, JsNumber>((d) => { return Global.NumberClass.New(Math.Tan(d)); }));
            #endregion

            this["E"] = global.NumberClass.New(Math.E);
            this["LN2"] = global.NumberClass.New(Math.Log(2));
            this["LN10"] = global.NumberClass.New(Math.Log(10));
            this["LOG2E"] = global.NumberClass.New(Math.Log(Math.E, 2));
            this["PI"] = global.NumberClass.New(Math.PI);
            this["SQRT1_2"] = global.NumberClass.New(Math.Sqrt(0.5));
            this["SQRT2"] = global.NumberClass.New(Math.Sqrt(2));
        }

        public const string MathType = "object";

        public override string Class
        {
            get { return MathType; }
        }
    }
}
