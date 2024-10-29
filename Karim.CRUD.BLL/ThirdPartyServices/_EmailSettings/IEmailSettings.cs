using Karim.CRUD.BLL.ModelDtos.EmailDtos;

namespace Karim.CRUD.BLL.ThirdPartyServices.EmailSettings
{
	public interface IEmailSettings
	{
		void SendEmail(Email email);
	}
}
