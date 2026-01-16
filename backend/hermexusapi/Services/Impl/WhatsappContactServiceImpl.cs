using hermexusapi.DTO.V1;
using hermexusapi.Hypermedia.Utils;
using hermexusapi.Models;
using hermexusapi.Repositories;
using Mapster;

namespace hermexusapi.Services.Impl
{
    public class WhatsappContactServiceImpl(
        IWhatsappContactRepository repository
            ) : IWhatsappContactService
    {
        private IWhatsappContactRepository _repository = repository;

        public WhatsappContactDTO Create(WhatsappContactDTO whatsapp_contact)
        {
            var entity = whatsapp_contact.Adapt<WhatsappContact>();
            entity = _repository.Create(entity);
            return entity.Adapt<WhatsappContactDTO>();
        }

        public WhatsappContactDTO FindById(long id)
        {
            return _repository.FindById(id).Adapt<WhatsappContactDTO>();
        }

        public WhatsappContactDTO Update(WhatsappContactDTO whatsapp_contact)
        {
            var entity = whatsapp_contact.Adapt<WhatsappContact>();
            entity = _repository.Update(entity);
            return entity.Adapt<WhatsappContactDTO>();
        }

        public bool Delete(long id)
        {
            return _repository.Delete(id);
        }

        public PagedSearchDTO<WhatsappContactDTO> FindWithPagedSearch(
            string name, string sortDirection, int pageSize, int page)
        {
            var result = _repository.FindWithPagedSearch(name, sortDirection, pageSize, page);
            return result.Adapt<PagedSearchDTO<WhatsappContactDTO>>();
        }
    }
}
