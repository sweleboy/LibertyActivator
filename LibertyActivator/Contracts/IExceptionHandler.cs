using System;
using System.Threading.Tasks;

namespace LibertyActivator.Contracts
{
	public interface IExceptionHandler
	{
		public void Handle(Exception exception);
	}
}
