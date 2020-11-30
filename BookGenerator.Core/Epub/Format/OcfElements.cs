﻿using System.Xml.Linq;

namespace BookGenerator.Core.Epub.Format
{
	internal static class OcfElements
	{
		public static readonly XName Container = Constants.OcfNamespace + "container";
		public static readonly XName RootFiles = Constants.OcfNamespace + "rootfiles";
		public static readonly XName RootFile = Constants.OcfNamespace + "rootfile";
	}
}