using System;
namespace FabrykaWytworcza
{
	public class EuropeanCarShop : CarShop
	{
		public override Car CreateCar(string item)
		{
			if (item.Equals("audi"))
			{
				return new EuropeanAudi();
			}
			else if (item.Equals("vw"))
			{
				return new EuropeanVw();
			}
			else return null;
		}
	}
}
