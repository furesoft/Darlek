﻿namespace BookGenerator.Core.Engine.Core
{
	public interface IIterator
	{
		IIteratorResult next(Arguments arguments = null);

		IIteratorResult @return();

		IIteratorResult @throw(Arguments arguments = null);
	}
}