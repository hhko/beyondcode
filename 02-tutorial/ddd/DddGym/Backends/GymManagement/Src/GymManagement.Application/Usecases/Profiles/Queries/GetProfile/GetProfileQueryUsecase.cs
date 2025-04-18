namespace GymManagement.Application.Usecases.Profiles.Queries.GetProfile;

//// TODO: LanguageExt
//internal sealed class GetProfileQueryUsecase
//    : IQueryUsecase<GetProfileQuery, GetProfileResponse>
//{
//    private readonly IUsersRepository _usersRepository;

//    public GetProfileQueryUsecase(IUsersRepository usersRepository)
//    {
//        _usersRepository = usersRepository;
//    }

//    public async Task<IErrorOr<GetProfileResponse>> Handle(GetProfileQuery query, CancellationToken cancellationToken)
//    {
//        User? user = await _usersRepository.GetByIdAsync(query.UserId);
//        if (user is null)
//        {
//            return Error
//                .NotFound(description: "User not found")
//                .ToErrorOr<GetProfileResponse>();
//        }

//        return user
//            .ToResponse()
//            .ToErrorOr();
//    }
//}