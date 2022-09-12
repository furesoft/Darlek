using Darlek.Core.Schemy;
using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Darlek.Core.RuntimeLibrary;

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

                    var environment = interpreter.Environment;

                    if (att.Module != null)
                    {
                        if (!Modules.ContainsKey(att.Module))
                        {
                            Modules.Add(att.Module, new Schemy.Environment(new Dictionary<Symbol, object>(), null));
                        }

                        environment = Modules[att.Module];
                    }

                    var make = (Symbol.FromString(name), new NativeProcedure(_ => Activator.CreateInstance(t, _.ToArray())));

                    environment.Define(make.Item1, make.Item2);

                    foreach (var prop in t.GetProperties())
                    {
                        interpreter.DefineGlobal(Symbol.FromString("get-" + prop.Name.ToLower()), new NativeProcedure(_ => CallMethodInfo(_, prop.GetGetMethod(), _.FirstOrDefault()), "get-" + prop.Name.ToLower()));
                        interpreter.DefineGlobal(Symbol.FromString("set-" + prop.Name.ToLower() + "!"), new NativeProcedure(_ => prop.GetSetMethod().Invoke(_.FirstOrDefault(), new object[] { _.Last() }), "set-" + prop.Name.ToLower() + "!"));
                    }

                    environment.Define(Symbol.FromString(predName), new NativeProcedure(_ => t.IsAssignableFrom(_.FirstOrDefault().GetType()), name));
                }

                foreach (var mi in t.GetMethods())
                {
                    var matt = mi.GetCustomAttribute<RuntimeMethodAttribute>();
                    if (matt != null)
                    {
                        var procedure = new NativeProcedure(_ => CallMethodInfo(_, mi));

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
        //(make-struct 'name '('prop1 'prop2))
        interpreter.DefineGlobal(Symbol.FromString("make-struct"), new NativeProcedure(_ => {
            var s = new RuntimeStruct { Typename = (Symbol)_.First() };

            var props = (List<object>)_.Last();

            foreach (List<object> item in props)
            {
                var prop = (Symbol)item[1];
                s.Add(prop, null);

                //define getter
                interpreter.DefineGlobal(Symbol.FromString(s.Typename.AsString + "-get-" + prop.AsString), new NativeProcedure(_ => ((RuntimeStruct)_.First())[prop]));

                //define setter
                interpreter.DefineGlobal(Symbol.FromString(s.Typename.AsString + "-set-" + prop.AsString + "!"), new NativeProcedure(_ => {
                    ((RuntimeStruct)_.First())[prop] = _.Last();

                    return true;
                }));
            }

            //define predicate
            interpreter.DefineGlobal(Symbol.FromString(s.Typename.AsString + "?"),
               new NativeProcedure(_ => _[0] is RuntimeStruct value && value.Typename == s.Typename));

            //define ctor
            interpreter.DefineGlobal(Symbol.FromString("make-" + s.Typename.AsString),
            new NativeProcedure(_ => {
                var tmp = new RuntimeStruct { Typename = s.Typename };

                for (var i = 0; i < props.Count; i++)
                {
                    tmp.Add((Symbol)((List<object>)props[i])[1], _[i]);
                }

                return tmp;
            }));

            return s;
        }));
        interpreter.DefineGlobal(Symbol.FromString("open"), new NativeProcedure((args) => {
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
        interpreter.DefineGlobal(Symbol.FromString("define-module"), new NativeProcedure((args) => {
            var name = (Symbol)args[0];
            var env = new Dictionary<Symbol, object>();

            foreach (var arg in args.OfType<ExportModule>())
            {
                env.Add(arg.Symbol, arg.Value);
            }

            Modules.Add(name, new Schemy.Environment(env, interpreter.Environment));

            return None.Instance;
        }));
        interpreter.DefineGlobal(Symbol.FromString("export"), new NativeProcedure((args) => {
            return new ExportModule((Symbol)args[0], args.Skip(1).First());
        }));

        interpreter.DefineGlobal(Symbol.FromString("display"), new NativeProcedure(_ => {
            Console.WriteLine(_.First().ToString());

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
        for (var i = 0; i < pms.Length; i++)
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