﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Dieser Code wurde von einem Tool generiert.
//     Laufzeitversion:4.0.30319.42000
//
//     Änderungen an dieser Datei können falsches Verhalten verursachen und gehen verloren, wenn
//     der Code erneut generiert wird.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BookGenerator.Properties {
    using System;
    
    
    /// <summary>
    ///   Eine stark typisierte Ressourcenklasse zum Suchen von lokalisierten Zeichenfolgen usw.
    /// </summary>
    // Diese Klasse wurde von der StronglyTypedResourceBuilder automatisch generiert
    // -Klasse über ein Tool wie ResGen oder Visual Studio automatisch generiert.
    // Um einen Member hinzuzufügen oder zu entfernen, bearbeiten Sie die .ResX-Datei und führen dann ResGen
    // mit der /str-Option erneut aus, oder Sie erstellen Ihr VS-Projekt neu.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Gibt die zwischengespeicherte ResourceManager-Instanz zurück, die von dieser Klasse verwendet wird.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("BookGenerator.Properties.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Überschreibt die CurrentUICulture-Eigenschaft des aktuellen Threads für alle
        ///   Ressourcenzuordnungen, die diese stark typisierte Ressourcenklasse verwenden.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Sucht eine lokalisierte Zeichenfolge, die (open &apos;cli-vector)
        ///
        ///(define myCommand (lambda (args)
        ///	(display (get-value args &quot;query&quot;))
        ///
        ///	0
        ///))
        ///
        ///(register-command 
        ///	&quot;my-command&quot;
        ///	&quot;Do Something&quot;
        ///	&quot;my-command --query &quot;
        ///	myCommand
        ///) ähnelt.
        /// </summary>
        internal static string Commands1 {
            get {
                return ResourceManager.GetString("Commands1", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Sucht eine lokalisierte Ressource vom Typ System.Byte[].
        /// </summary>
        internal static byte[] cover {
            get {
                object obj = ResourceManager.GetObject("cover", resourceCulture);
                return ((byte[])(obj));
            }
        }
        
        /// <summary>
        ///   Sucht eine lokalisierte Zeichenfolge, die (open &apos;ObjectModel)
        ///
        ///(define mycrawler (lambda (url) (
        ///	(define result (make-object))
        ///	(set-property result &apos;title &quot;hello scheme&quot;)
        ///
        ///	result
        ///))
        ///
        ///(register-crawler mycrawler) ähnelt.
        /// </summary>
        internal static string SampleCrawler1 {
            get {
                return ResourceManager.GetString("SampleCrawler1", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Sucht eine lokalisierte Zeichenfolge, die &lt;html xmlns=&quot;http://www.w3.org/1999/xhtml&quot;&gt;
        ///&lt;head&gt;
        ///    &lt;meta http-equiv=&quot;Content-Type&quot; content=&quot;text/html; charset=windows-1252&quot;&gt;
        ///    &lt;style type=&quot;text/css&quot;&gt;
        ///        .title {
        ///            text-align: center;
        ///        }
        ///
        ///        .sidebar {
        ///            float: right;
        ///            padding-left: 15px;
        ///        }
        ///
        ///        .content {
        ///            display: block;
        ///            text-align: justify;
        ///            margin-right: 15px;
        ///        }
        ///
        ///        .footer {
        ///            display: block;
        ///            position: absolute;
        ///      [Rest der Zeichenfolge wurde abgeschnitten]&quot;; ähnelt.
        /// </summary>
        internal static string Template {
            get {
                return ResourceManager.GetString("Template", resourceCulture);
            }
        }
    }
}
