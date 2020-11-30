﻿using System;
using BookGenerator.Core.Epub.Format;

namespace BookGenerator.Core.Epub.Extensions
{
	internal static class ByteArrayExt
	{
		public static byte[] TrimEncodingPreamble(this byte[] data)
		{
			var preamble = Constants.DefaultEncoding.GetPreamble();
			if (data.Length < preamble.Length)
			{
				return data;
			}

			for (var i = 0; i < preamble.Length; ++i)
			{
				if (data[i] != preamble[i])
				{
					return data;
				}
			}

			var newData = new byte[data.Length - preamble.Length];
			Array.Copy(data, preamble.Length, newData, 0, newData.Length);
			return newData;
		}
	}
}