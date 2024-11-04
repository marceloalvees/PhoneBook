using Application.Dto;
using AutoMapper;
using Domain.Abstractions;
using MediatR;

namespace Application.UserCases.Users.Queries.Get
{
    public class GetUserByIdHandler : IRequestHandler<GetUserByIdQuery, GetUserByIdResponse>
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetUserByIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<GetUserByIdResponse> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.UserRepository.GetByIdAsync(request.Id, cancellationToken);
            
            if (user == null)
                return new GetUserByIdResponse {Sucess = false};

            var userDto = _mapper.Map<GetUserDto>(user);
            return new GetUserByIdResponse { Sucess = true, User = userDto};
        }
    }
}
