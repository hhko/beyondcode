using FluentValidation;
using GymDdd.Framework.BaseTypes.Cqrs;
using GymManagement.Domain.AggregateRoots.Users;
using LanguageExt;
using LanguageExt.Common;

namespace GymManagement.Application.Usecases.Profiles.Commands;

public static class CreateAdminProfileCommand
{
    public sealed record Request(
        Guid UserId)
        : ICommand<Response>;

    public sealed record Response(
        Option<Guid> AdminId)
        : IResponse;

    internal sealed class Validator
        : AbstractValidator<Request>
    {
        public Validator()
        {
            RuleFor(_ => _.UserId)
                .NotEmpty();
        }
    }

    internal sealed class Telemetry
    {

    }

    internal sealed class Usecase(
        IUsersRepository usersRepository)
        : ICommandUsecase<Request, Response>
    {
        private readonly IUsersRepository _usersRepository = usersRepository;


        //public async Task<IErrorOr> Handle(CreateAdminProfileCommand command, CancellationToken cancellationToken)
        //{
        //    User? user = await _usersRepository.GetByIdAsync(command.UserId);
        //    if (user is null)
        //    {
        //        return Error
        //            .NotFound(description: "User not found")
        //            .ToErrorOr<Guid>();
        //    }

        //    ErrorOr<Guid> createAdminProfileResult = user.CreateAddminProfile();
        //    if (createAdminProfileResult.IsError)
        //    {
        //        return createAdminProfileResult
        //            .Errors
        //            .ToErrorOr<Guid>();
        //    }

        //    await _usersRepository.UpdateAsync(user);

        //    return createAdminProfileResult;
        //}

        public async Task<Fin<Response>> Handle(Request request, CancellationToken cancellationToken)
        {
            //return (await _usersRepository.GetByIdAsync(request.UserId))
            //    .Bind(user => user.CreateAdminProfile())
            //    .Map(guid => new CreateAdminProfileResponse(guid));

            //return from user in await _usersRepository.GetByIdAsync(request.UserId)
            //       from adminId in user.CreateAdminProfile()
            //       select new CreateAdminProfileResponse(adminId);


            //        from _2 in _1.CreateAdminProfile()
            //        from _3 in _2.
            //liftIO(env => _usersRepository.GetByIdAsync(request.UserId))
            //    .Bind()
            //return await
            //(
            //    from _1 in FinT<Task, User>.LiftIO(env => _usersRepository.GetByIdAsync(request.UserId))
            //    from adminId in _1.Bind(user => user.CreateAdminProfile())
            //    from 
            //)

            Fin<User> userResult = await _usersRepository.GetByIdAsync(request.UserId);
            if (userResult.IsFail)
            {
                return (Error)userResult;
            }
            User user = (User)userResult;

            Fin<Guid> adminResult = user.CreateAdminProfile();
            if (adminResult.IsFail)
            {
                return (Error)adminResult;
            }
            Guid adminId = (Guid)adminResult;

            await _usersRepository.UpdateAsync(user);

            //return new CreateAdminProfileResponse 
            //{ 
            //    //AdminId = adminId 
            //    BoolEnum = MyBoolEnum.True
            //};

            return new Response(user.AdminId);
        }
    }
}
