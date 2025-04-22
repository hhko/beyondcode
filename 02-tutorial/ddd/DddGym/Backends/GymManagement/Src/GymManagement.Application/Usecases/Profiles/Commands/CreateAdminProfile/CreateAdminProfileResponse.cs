using DddGym.Framework.BaseTypes.Cqrs;
using LanguageExt;

namespace GymManagement.Application.Usecases.Profiles.Commands.CreateAdminProfile;

public sealed record CreateAdminProfileResponse(
    Option<Guid> AdminId = default)
    : IResponse;
//{
//    public static CreateAdminProfileResponse Create(Guid adminId) =>
//        new(adminId);
//}
