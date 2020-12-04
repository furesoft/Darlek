using BookGenerator.Core.Epub;
using LiteDB;
using Schemy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace BookGenerator.Core.RuntimeLibrary
{
    public static class SchemeCliLoader
    {
        public static Dictionary<Symbol, Schemy.Environment> Modules = new Dictionary<Symbol, Schemy.Environment>();

        public static void Apply(Assembly ass, Interpreter interpreter)
        {
            InitGlobals(interpreter);

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

                        foreach (var prop in t.GetProperties())
                        {
                            interpreter.DefineGlobal(Symbol.FromString("get-" + prop.Name.ToLower()), new NativeProcedure(_ =>
                            {
                                return CallMethodInfo(_, prop.GetGetMethod(), _.FirstOrDefault());
                            }, "get-" + prop.Name.ToLower()));
                            interpreter.DefineGlobal(Symbol.FromString("set-" + prop.Name.ToLower() + "!"), new NativeProcedure(_ =>
                            {
                                return prop.GetSetMethod().Invoke(_.FirstOrDefault(), new object[] { _.Last() });
                            }, "set-" + prop.Name.ToLower() + "!"));
                        }

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
                            var procedure = new NativeProcedure(_ =>
                            {
                                return CallMethodInfo(_, mi);
                            });

                            if (att.Module != null)
                            {
                                if (!Modules.ContainsKey(att.Module))
                                {
                                    var env = new Schemy.Environment(new Dictionary<Symbol, object>(), interpreter.Environment);

                                    Modules.Add(att.Module, env);
                                }

                                Modules[att.Module].Define(Symbol.FromString(matt.Name), procedure);
                            }
                            else
                            {
                                interpreter.DefineGlobal(Symbol.FromString(matt.Name), procedure);
                            }
                        }
                    }
                }
            }
        }

        private static void InitGlobals(Interpreter interpreter)
        {
            //(make-struct 'name (list 'prop1 'prop2))
            interpreter.DefineGlobal(Symbol.FromString("make-struct"), new NativeProcedure(_ =>
            {
                var s = new RuntimeStruct { Typename = (Symbol)_.First() };

                var props = (List<object>)_.Last();

                for (int i = 0; i < props.Count - 1; i++)
                {
                    s.Add(null);
                    var prop = (Symbol)props[i];

                    //define getter
                    interpreter.DefineGlobal(Symbol.FromString("get-" + prop.AsString), new NativeProcedure(_ =>
                    {
                        return ((RuntimeStruct)_.First())[i];
                    }));

                    //define setter
                    interpreter.DefineGlobal(Symbol.FromString("set-" + prop.AsString + "!"), new NativeProcedure(_ =>
                    {
                        //Does not work. Need to rethink
                        ((RuntimeStruct)_.First())[i] = _.Last();

                        return true;
                    }));
                }

                //define predicate
                //define ctor

                interpreter.DefineGlobal(Symbol.FromString("make-" + s.Typename.AsString),
                    new NativeProcedure(_ =>
                    {
                        var tmp = new RuntimeStruct { Typename = s.Typename };

                        for (int i = 0; i < _.Count - 1; i++)
                        {
                            tmp[i] = _[i];
                        }

                        return tmp;
                    }));

                return s;
            }));
            interpreter.DefineGlobal(Symbol.FromString("open"), new NativeProcedure((args) =>
            {
                foreach (var ns in args.Cast<Symbol>())
                {
                    if (Modules.ContainsKey(ns))
                    {
                        OpenModule(ns, interpreter.Environment);
                    }
                    else
                    {
                        throw new KeyNotFoundException($"Module '{ns.AsString}' not found");
                    }
                }

                return None.Instance;
            }));
        }

        private static void OpenModule(Symbol ns, Schemy.Environment env)
        {
            foreach (var obj in Modules[ns].store)
            {
                env.Define(obj.Key, obj.Value);
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