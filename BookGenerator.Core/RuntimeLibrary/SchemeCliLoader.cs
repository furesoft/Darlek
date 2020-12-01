using Schemy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace BookGenerator.Core.RuntimeLibrary
{
    public static class SchemeCliLoader
    {
        public static void Apply(Assembly ass, Interpreter interpreter)
        {
            foreach (var t in ass.GetTypes())
            {
                var att = t.GetCustomAttribute<RuntimeTypeAttribute>();
                if (att != null)
                {
                    var mctoratt = t.GetCustomAttribute<RuntimeCtorMethodAttribute>();
                    if (mctoratt != null)
                    {
                        var name = "make-" + mctoratt.Name;
                        var predName = mctoratt.Name + "?";

                        interpreter.DefineGlobal(Symbol.FromString(name), new NativeProcedure(_ =>
                        {
                            return Activator.CreateInstance(t, _.ToArray());
                        }, name));

                        interpreter.DefineGlobal(Symbol.FromString(predName), new NativeProcedure(_ =>
                        {
                            return t.IsAssignableFrom(_.FirstOrDefault().GetType());
                        }, name));
                    }

                    foreach (var mi in t.GetMethods())
                    {
                        var matt = mi.GetCustomAttribute<RuntimeMethodAttribute>();
                        if (matt != null)
                        {
                            interpreter.DefineGlobal(Symbol.FromString(matt.Name), new NativeProcedure(_ =>
                            {
                                return CallMethodInfo(_, mi);
                            }, matt.Name));
                        }
                    }
                }
            }
        }

        private static object CallMethodInfo(List<object> args, MethodBase m, object target = null)
        {
            var buffer = new List<object>();

            var pms = m.GetParameters();
            for (int i = 0; i < pms.Length; i++)
            {
                if (i < args.Count)
                {
                    buffer.Add(args[i]);
                }
                else
                {
                    buffer.Add(null);
                }
            }

            return m.Invoke(target, buffer.ToArray());
        }
    }
}