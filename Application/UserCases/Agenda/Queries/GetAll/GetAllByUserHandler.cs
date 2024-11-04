using Application.Dto;
using AutoMapper;
using Domain.Abstractions;
using MediatR;

namespace Application.UserCases.Agenda.Queries.GetAll
{
    public class GetAllByUserHandler : IRequestHandler<GetAllByUserQueries, GetAllByUserResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetAllByUserHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<GetAllByUserResponse> Handle(GetAllByUserQueries request, CancellationToken cancellationToken)
        {
            try
            {
                if(request.Equals(null))
                    throw new ArgumentNullException(nameof(request));

                var contact = await _unitOfWork.ContactRepository.GetByUserIdAsync(request.userId, cancellationToken);
                var contactdto = _mapper.Map<List<GetContactDto>>(contact);
                return new GetAllByUserResponse { Sucess = true, Contacts = contactdto };
            }
            catch
            {
                return new GetAllByUserResponse { Sucess = false };
            }
        }
    }
}
