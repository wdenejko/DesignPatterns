using System;
namespace FabrykaWytworcza
{
	public class AmericanCarShop : CarShop
	{
		public override Car CreateCar(string item)
		{
			if (item.Equals("audi"))
			{
				return new AmericanAudi();
			}
			else if (item.Equals("vw"))
			{
				return new AmericanVw();
			}
			else return null;
		}
	}
}
