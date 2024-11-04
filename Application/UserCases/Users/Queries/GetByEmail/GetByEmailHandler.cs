using Application.Dto;
using AutoMapper;
using Domain.Abstractions;
using MediatR;

namespace Application.UserCases.Users.Queries.GetByEmail
{
    public class GetByEmailHandler : IRequestHandler<GetByEmailQuery, GetByEmailResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetByEmailHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<GetByEmailResponse> Handle(GetByEmailQuery request, CancellationToken cancellationToken)
        {
            var user = _unitOfWork.UserRepository.GetByEmailAsync(request.email, cancellationToken);
            if (user is null)
                return new GetByEmailResponse { Sucess = false};

            var userDTO = _mapper.Map<GetUserDto>(user);
            return new GetByEmailResponse { Sucess = true, User = userDTO };

        }
    }
}
