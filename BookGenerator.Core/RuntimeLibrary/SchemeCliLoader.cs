using Schemy;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace BookGenerator.Core.RuntimeLibrary
{
    public static class SchemeCliLoader
    {
        private static Dictionary<string, MethodInfo> _methods = new Dictionary<string, MethodInfo>();
        private static Dictionary<string, (object, MethodInfo)> _predicates = new Dictionary<string, (object, MethodInfo)>();

        public static void Load(Assembly ass)
        {
            foreach (var t in ass.GetTypes())
            {
                var att = t.GetCustomAttribute<RuntimeTypeAttribute>();
                if (att != null)
                {
                    foreach (var mi in t.GetMethods())
                    {
                        var matt = mi.GetCustomAttribute<RuntimeMethodAttribute>();
                        if (matt != null)
                        {
                            _methods.Add(matt.Name, mi);
                        }

                        var mctoratt = mi.GetCustomAttribute<RuntimeCtorMethodAttribute>();
                        if (mctoratt != null)
                        {
                            _methods.Add("make-" + mctoratt.Name, mi);

                            var predicate = new Func<object, bool>(_ =>
                            {
                                return t.IsAssignableFrom(_.GetType());
                            });

                            var target = predicate.Target;

                            _predicates.Add(mctoratt.Name + "?", (target, predicate.Method));
                        }
                    }
                }
            }
        }

        public static void Apply(Interpreter interpreter)
        {
            foreach (var m in _methods)
            {
                interpreter.DefineGlobal(Symbol.FromString(m.Key), new NativeProcedure(_ =>
                {
                    return CallMethodInfo(_, m.Value);
                }, m.Value.Name));
            }

            foreach (var m in _predicates)
            {
                interpreter.DefineGlobal(Symbol.FromString(m.Key), new NativeProcedure(_ =>
                {
                    return CallMethodInfo(_, m.Value.Item2, m.Value.Item1);
                }, m.Value.Item2.Name));
            }
        }

        private static object CallMethodInfo(List<object> args, MethodInfo m, object target = null)
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