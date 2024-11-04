using Application.Dto;
using AutoMapper;
using Domain.Abstractions;
using MediatR;

namespace Application.UserCases.Users.Queries.GetAll
{
    public class GetUsersHandler : IRequestHandler<GetUsersQuery, GetUsersResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public GetUsersHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<GetUsersResponse> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {

            try
            {
                var users = await _userRepository.GetAllAsync(cancellationToken);

                var usersDto = _mapper.Map<List<GetUserDto>>(users);

                return new GetUsersResponse { Sucess = true, Users = usersDto };
            }
            catch
            {
                return new GetUsersResponse { Sucess = false};
            }
        }
    }
}
